/* UDFs and Stored Procedures */
IF OBJECT_ID (N'[dbo].[sp_get_enrollments_cancellations]') IS NOT NULL  
    DROP PROCEDURE [dbo].[sp_get_enrollments_cancellations];  
GO
IF OBJECT_ID (N'[dbo].[sp_getMemberCountByStatus]') IS NOT NULL  
    DROP PROCEDURE [dbo].[sp_getMemberCountByStatus];  
GO 
IF OBJECT_ID (N'[dbo].[sp_getBrokerHierarchyCount]') IS NOT NULL  
    DROP PROCEDURE [dbo].[sp_getBrokerHierarchyCount];  
GO
IF OBJECT_ID (N'[dbo].[sp_getMemberCountByProductStatus]') IS NOT NULL  
    DROP PROCEDURE [dbo].[sp_getMemberCountByProductStatus];  
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Krithika Vijayan
-- Create date: 04 September
-- Description:	Udf to get tree for loggedIn BrokerId

-- Select * from [dbo].[udf_getBrokerIds] (12956,0, 1)
-- =============================================
IF OBJECT_ID (N'[dbo].[udf_getBrokerIds]') IS NOT NULL  
    DROP FUNCTION [dbo].[udf_getBrokerIds];  
GO  
CREATE FUNCTION [dbo].[udf_getBrokerIds](@loggedInBrokerId bigint, @viewBy int, @brokerId bigint NULL)
RETURNS @brokerIds TABLE 
(
	BrokerId BIGINT
)
AS
BEGIN	

	DECLARE @Ids TABLE 
	(
		BrokerId BIGINT
	)

    ;WITH brkHierarchy AS
    (
        SELECT BrokerId FROM BrokerHierarchy
        WHERE BrokerId = @loggedInBrokerId
        UNION ALL
        SELECT bh.brokerid FROM BrokerHierarchy bh
        INNER JOIN brkHierarchy bhCTE ON bh.ParentBrokerId = bhCTE.BrokerId
    )	 
	INSERT @Ids SELECT BrokerId FROM brkHierarchy
	
	IF @viewBy = 0 -- All
	BEGIN
		INSERT @brokerIds 
		SELECT * FROM @Ids
	END
	ELSE IF  @viewBy = 1 -- Only me
	BEGIN
		INSERT @brokerIds 
		SELECT @loggedInBrokerId		
	END
	ELSE IF  @viewBy = 2 -- Tree only
	BEGIN
		INSERT @brokerIds 
		SELECT * FROM @Ids WHERE BrokerId <> @loggedInBrokerId		
	END
	ELSE IF  @viewBy = 3 --Broker
	BEGIN
		INSERT @brokerIds 
		SELECT @brokerId
	END

	RETURN

END;
GO

--==========================================================================

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================================================================
-- Author:		Krithika Vijayan
-- Create date: 13 September
-- Description:	udf to get BrokerIds for Broker hierarchy graph by view by filter

-- Select * from [dbo].[udf_getBrokerIdsForBrokerHierarchyGraph] (12956,1, 1)
-- =============================================================================================
IF OBJECT_ID (N'[dbo].[udf_getBrokerIdsForBrokerHierarchyGraph]') IS NOT NULL  
    DROP FUNCTION [dbo].[udf_getBrokerIdsForBrokerHierarchyGraph];  
GO  
CREATE FUNCTION [dbo].[udf_getBrokerIdsForBrokerHierarchyGraph](@loggedInBrokerId bigint, @viewBy int, @brokerId bigint NULL)
RETURNS @brokerIds TABLE 
(
	BrokerId BIGINT
)
AS
BEGIN	

	DECLARE @Ids TABLE 
	(
		BrokerId BIGINT
	)

	;WITH brkHierarchy AS
	(
		SELECT BrokerId FROM BrokerHierarchy
		WHERE BrokerId = 
			CASE
				WHEN @viewBy != 3 THEN @loggedInBrokerId
				ELSE @brokerId
			END
		UNION ALL
		SELECT bh.brokerid FROM BrokerHierarchy bh
		INNER JOIN brkHierarchy bhCTE ON bh.ParentBrokerId = bhCTE.BrokerId
	)	 
	INSERT @brokerIds SELECT BrokerId FROM brkHierarchy
	
	IF @viewBy = 1 -- Only me
	BEGIN
		DELETE FROM @brokerIds WHERE BrokerId = @loggedInBrokerId
	END
	ELSE IF  @viewBy = 3
	BEGIN
		DELETE FROM @brokerIds WHERE BrokerId = @brokerId
	END

	RETURN

END;
GO
--==================================================================================

IF OBJECT_ID (N'[dbo].[GetBrokerTree]') IS NOT NULL 
    DROP PROCEDURE [dbo].[GetBrokerTree]; 
GO

CREATE PROC [dbo].[GetBrokerTree]
@BrokerId BIGINT 
WITH RECOMPILE
AS 
BEGIN 

set nocount on

	DECLARE @abc TABLE
	(CumulativeChildren INT ,
    BrokerId BIGINT INDEX IX2 NONCLUSTERED (ParentID,CumulativeChildren asc),
	ParentID BIGINT );

	WITH BrokerHierarchyCTE AS ( 
		SELECT BrokerId, BrokerId as RootId, ParentBrokerId as ParentId, 0 as ischild FROM BrokerHierarchy
		WHERE  BrokerId <> ParentBrokerId
		UNION ALL
		SELECT bh.brokerid, bhCTE.RootId, bh.ParentBrokerId as ParentId, 1 as ichild FROM BrokerHierarchy bh
		INNER JOIN BrokerHierarchyCTE bhCTE ON bh.ParentBrokerId = bhCTE.BrokerId and bh.BrokerId <> bhCTE.BrokerId
	)

	insert @abc
	select S.CumulativeChildren,bb.BrokerId,bb.ParentId from BrokerHierarchyCTE bb
	LEFT OUTER JOIN (SELECT RootId, isnull(SUM(IsChild),0) AS CumulativeChildren FROM BrokerHierarchyCTE GROUP BY RootID) AS S 
	ON S.RootId = bb.BrokerId
	where bb.RootId=@BrokerId
	OPTION (MAXRECURSION 0);

	select  TermDate,ParentId,IsActive,IsWebsiteActive, ExternalId, a.CumulativeChildren, bt.BrokerTypeName,  LastName, MiddleName,   FirstName, b.BrokerId,
	(select count(1) from MemberSubscription ms join [member] m on m.MemberId=ms.MemberId and [Status] <> 0 and m.ExternalId is not null  where ms.BrokerId=b.BrokerId) as MemberCount,
	[Status]
	from [broker] b join @abc a on b.brokerid=a.BrokerId
	left join BrokerType bt on b.BrokerId=bt.BrokerTypeId order by b.BrokerId

END

--===================================================================================
--Procedure Commissions--
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID (N'[dbo].[BrokerIdTree]') IS NOT NULL  
    DROP PROCEDURE [dbo].[BrokerIdTree];  
GO

CREATE PROC [dbo].[BrokerIdTree] 
@BrokerId BIGINT  
WITH RECOMPILE 
AS 
BEGIN
	SET NOCOUNT ON;
	WITH brkHierarchy AS
	(
		SELECT BrokerId FROM BrokerHierarchy
		WHERE BrokerId = @BrokerId
		UNION ALL
		SELECT bh.brokerid FROM BrokerHierarchy bh
		INNER JOIN brkHierarchy bhCTE ON bh.ParentBrokerId = bhCTE.BrokerId
	)
	SELECT BrokerId FROM brkHierarchy
	SET NOCOUNT OFF;
END
GO


--============================================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ====================================================================================================
-- Author:		Krithika Vijayan
-- Create date: 04 September 2019
-- Description:	Stored procedure gets list of memberIds enrolled and cancelled with in a duration

-- EXEC [dbo].[GetEnrollmentsCancellations] 1, 12956, '0', 1
-- =====================================================================================================
IF OBJECT_ID (N'[dbo].[GetEnrollmentsCancellations]') IS NOT NULL  
    DROP PROCEDURE [dbo].[GetEnrollmentsCancellations];  
GO  
CREATE PROCEDURE [dbo].[GetEnrollmentsCancellations]	
	@period INT,
	@loggedInBrokerId BIGINT,
	@brokerExternalId NVARCHAR(100),
	@viewBy INT
WITH RECOMPILE
AS
BEGIN
	
	/*		
		Last3Months = 1,
        Today = 2,
        Yesterday = 3,
        Last7Days = 4,
        LastWeek = 5,
        LastMonth = 6,
		CurrentMonth = 6,
        CurrentMonthToDate = 7       
	*/

	DECLARE @brokerId BIGINT;
	SELECT @brokerId = BrokerId FROM [Broker] WHERE ExternalId = @brokerExternalId
	
	DECLARE @startDate DATETIME;
	DECLARE @endDate DATETIME;
	DECLARE @firstDayOfQ1 DATETIME = DATEADD(yy, DATEDIFF(yy, 0, GETDATE()), 0)

	DECLARE @timePeriod TABLE
	(
		Id INT,
		ExternalId VARCHAR(100),
		[Period] VARCHAR(10),
		OrderBy INT,
		IsEnrollment BIT
	)

	--Last3Months
	IF @period = 1 
	BEGIN
		--Last3Months
		SELECT @startDate = DATEADD(MONTH, -2,DATEADD(mm, DATEDIFF(mm, 0, GETDATE()), 0))
		, @endDate = EOMONTH(GETDATE())
	END
	--Today
	ELSE IF @period = 2 
	BEGIN
		SELECT @startDate = CAST(GETDATE() AS DATE), @endDate = CAST(GETDATE() AS DATE)
	END
	--Yesterday
	ELSE IF @period = 3 
	BEGIN
		SELECT @startDate = DATEADD(DAY, -1, CAST(GETDATE() AS  DATE)), @endDate = DATEADD(DAY, -1, CAST(GETDATE() AS DATE))
	END
	--Last7Days
	ELSE IF @period = 4 
	BEGIN
		SELECT @startDate = CAST(DATEADD(DAY,-7, GETDATE()) AS DATE), @endDate = CAST(GETDATE() - 1 AS DATE)
	END
	--LastWeek
	ELSE IF @period = 5 
	BEGIN
		SELECT @startDate = dateadd(wk, datediff(wk, 0, getdate()), 0) - 7, @endDate = dateadd(wk, datediff(wk, 0, getdate()), 0) - 1
	END
	--LastMonth
	ELSE IF @period = 6
	BEGIN
		SELECT @startDate = DATEADD(mm, DATEDIFF(mm, 0, GETDATE()) - 1, 0), @endDate = DATEADD(DAY, -(DAY(GETDATE())), GETDATE())		
	END
	--LastMonth
	ELSE IF @period = 7 
	BEGIN		
		SELECT @startDate = DATEADD(mm, DATEDIFF(mm, 0, GETDATE()), 0), @endDate = GETDATE()
	END
	
	 DECLARE @brokerIds TABLE(BrokerId BIGINT);
        DECLARE @Ids TABLE(BrokerId BIGINT);
        INSERT INTO @Ids
        EXEC [BrokerIdTree] 
             @BrokerId = @brokerId;
        IF @viewBy = 0 -- All
            BEGIN
                INSERT INTO @brokerIds
                       SELECT BrokerId
                       FROM @Ids;
        END;
            ELSE
            IF @viewBy = 1 -- Only me
                BEGIN
                    INSERT INTO @brokerIds
                           SELECT @loggedInBrokerId;
            END;
                ELSE
                IF @viewBy = 2 -- Tree only
                    BEGIN
                        INSERT INTO @brokerIds
                               SELECT BrokerId
                               FROM @Ids
                               WHERE BrokerId <> @loggedInBrokerId;
                END;
                    ELSE
                    IF @viewBy = 3 --Broker
                        BEGIN
                            INSERT INTO @brokerIds
                                   SELECT @brokerId;
                    END;
	--Get Enrollments and Cancellations

	IF @period = 1 --OR @period = 8 OR @period = 9 OR @period = 10 OR @period = 11
	BEGIN

		DECLARE @months TABLE
		(	
			ExternalId VARCHAR(100),
			[Period] VARCHAR(100),
			OrderBy INT,
			IsEnrollment BIT
		)

		INSERT INTO @months
		SELECT 
			CAST(MONTH(DATEADD(MONTH, x.number, @startDate)) AS VARCHAR(2)) AS ExternalId			
			,LEFT(DATENAME(MONTH, DATEADD(MONTH, x.number, @startDate)), 3) + ' ' + RIGHT(YEAR(GETDATE()), 2) AS [Period]
			,MONTH(DATEADD(MONTH, x.number, @startDate)) AS OrderBy
			,NULL AS IsEnrollment
		FROM master.dbo.spt_values x
		WHERE x.type = 'P'        
		AND x.number <= DATEDIFF(MONTH, @startDate, @endDate);
		
		INSERT INTO @timePeriod
		SELECT
		ROW_NUMBER() OVER (ORDER BY table1.OrderBy) AS Id, ExternalId,[Period],OrderBy,IsEnrollment
		FROM
		(
			SELECT ExternalId,[Period],OrderBy,IsEnrollment FROM @months
			UNION
			SELECT 
				m.ExternalId
				,CASE
					WHEN msh.InActiveDate IS NULL THEN LEFT(DATENAME(MONTH, msh.ActiveDate), 3) + ' ' + RIGHT(YEAR(GETDATE()), 2) 
					ELSE LEFT(DATENAME(MONTH, msh.InActiveDate), 3) + ' ' + RIGHT(YEAR(GETDATE()), 2) 
				END AS [Period]
				,CASE
					WHEN msh.InActiveDate IS NULL THEN MONTH(msh.ActiveDate) 
					ELSE MONTH(msh.InActiveDate) 
				END AS OrderBy
				,CASE
						WHEN msh.InActiveDate IS NULL THEN CAST(1 AS BIT) 
						ELSE CAST(0 AS BIT) 
				END AS IsEnrollment
			FROM
				MemberStatusHistory msh		
				JOIN Member m ON m.MemberId = msh.MemberId
				JOIN MemberSubscription ms ON ms.MemberId = msh.MemberId		
			WHERE
					m.[Status] != 0
					AND ms.BrokerId IN (SELECT BrokerId FROM @brokerIds)
					AND 
					((msh.InActiveDate IS NULL AND msh.ActiveDate >= @startDate AND msh.ActiveDate <= @endDate)
					OR (msh.InActiveDate IS NOT NULL AND msh.InActiveDate >= @startDate AND msh.InActiveDate <= @endDate))
			GROUP BY
				DATENAME(MONTH, msh.ActiveDate),
				m.ExternalId,
				MONTH(msh.ActiveDate),
				MONTH(msh.InActiveDate),
				msh.InActiveDate
		) AS table1
		ORDER BY Id
	END
	
	IF @period = 2 OR @period = 3
	BEGIN
		
		DECLARE @today TABLE
		(	
			ExternalId VARCHAR(100),
			[Period] VARCHAR(100),
			OrderBy INT,
			IsEnrollment BIT
		)

		INSERT INTO @today
		SELECT 
			'0' AS ExternalId			
			,CASE 
				WHEN @period = 2 THEN 'Today'
				ELSE 'Yesterday'
			END AS [Period]
			,1 AS OrderBy
			,NULL AS IsEnrollment 

		INSERT INTO @timePeriod
		SELECT
		ROW_NUMBER() OVER (ORDER BY table1.OrderBy) AS Id, ExternalId,[Period],OrderBy,IsEnrollment
		FROM
		(
			SELECT ExternalId,[Period],OrderBy,IsEnrollment FROM @today
			UNION
			SELECT 			
				m.ExternalId,
				CASE 
					WHEN @period = 2 THEN 'Today'
					ELSE 'Yesterday'
				END AS [Period],			
				CASE
					WHEN msh.InActiveDate IS NULL THEN MONTH(msh.ActiveDate)
					ELSE MONTH(msh.InActiveDate)
				END AS OrderBy,			
				CASE
					WHEN msh.InActiveDate IS NULL THEN CAST(1 AS BIT) 
					ELSE CAST(0 AS BIT) 
				END AS IsEnrollment
			FROM
				MemberStatusHistory msh		
				JOIN Member m ON m.MemberId = msh.MemberId
				JOIN MemberSubscription ms ON ms.MemberId = msh.MemberId		
			WHERE
				m.[Status] != 0
				AND ms.BrokerId IN (SELECT BrokerId FROM @brokerIds)
				AND 
				((msh.InActiveDate IS NULL AND msh.ActiveDate >= @startDate AND msh.ActiveDate <= @endDate)
				OR (msh.InActiveDate IS NOT NULL AND msh.InActiveDate >= @startDate AND msh.InActiveDate <= @endDate))
			GROUP BY
				CASE
					WHEN msh.InActiveDate IS NULL THEN MONTH(msh.ActiveDate)
					ELSE MONTH(msh.InActiveDate)
				END, 
				CASE
					WHEN msh.InActiveDate IS NULL THEN DATENAME(MONTH, msh.ActiveDate)
					ELSE DATENAME(MONTH, msh.InActiveDate)
				END,
				m.ExternalId,
				msh.InActiveDate
		) AS table1
		ORDER BY Id	
	END

	IF @period = 4 -- last 7 days, display period date wise
	BEGIN

		DECLARE @last7Days TABLE
		(	
			ExternalId VARCHAR(100),
			[Period] VARCHAR(100),
			OrderBy INT,
			IsEnrollment BIT
		)
		
		INSERT INTO @last7Days
		SELECT 
			CAST(x.number AS VARCHAR(2)) AS ExternalId			
			,CAST(DATEADD(DAY, x.number, @startDate) AS VARCHAR(7)) AS [Period]
			,x.number AS OrderBy
			,NULL AS IsEnrollment
		FROM master.dbo.spt_values x
		WHERE x.type = 'P'        
		AND x.number <= DATEDIFF(DAY, @startDate, @endDate);		
		
		INSERT INTO @timePeriod
		SELECT
		ROW_NUMBER() OVER (ORDER BY table1.ExternalId) AS Id, ExternalId,[Period],OrderBy,IsEnrollment
		FROM
		(
			SELECT ExternalId,[Period],OrderBy,IsEnrollment FROM @last7Days
			UNION
			SELECT 
				m.ExternalId
				,CASE
					WHEN msh.InActiveDate IS NULL THEN CAST(DATEADD(DAY, 0, msh.ActiveDate) AS VARCHAR(7)) 
					ELSE CAST(DATEADD(DAY, 0, msh.InActiveDate) AS VARCHAR(7)) 			
				END AS [Period]
				,CASE
					WHEN msh.InActiveDate IS NULL THEN DAY(msh.ActiveDate) 
					ELSE DAY(msh.InActiveDate) 
				END AS OrderBy
				,CASE
					WHEN msh.InActiveDate IS NULL THEN CAST(1 AS BIT) 
					ELSE CAST(0 AS BIT) 
				END AS IsEnrollment
			FROM
				MemberStatusHistory msh		
				JOIN Member m ON m.MemberId = msh.MemberId
				JOIN MemberSubscription ms ON ms.MemberId = msh.MemberId		
			WHERE
				m.[Status] != 0
				AND ms.BrokerId IN (SELECT BrokerId FROM @brokerIds)
				AND 
				((msh.InActiveDate IS NULL AND msh.ActiveDate >= @startDate AND msh.ActiveDate <= @endDate)
				OR (msh.InActiveDate IS NOT NULL AND msh.InActiveDate >= @startDate AND msh.InActiveDate <= @endDate))
			GROUP BY
				CASE
					WHEN msh.InActiveDate IS NULL THEN DAY(msh.ActiveDate)
					ELSE DAY(msh.InActiveDate)
				END, 
				CASE
					WHEN msh.InActiveDate IS NULL THEN DATEADD(DAY, 0, msh.ActiveDate)
					ELSE DATEADD(DAY, 0, msh.InActiveDate)
				END,
				m.ExternalId,
				msh.InActiveDate,
				msh.ActiveDate
		) AS table1
		ORDER BY Id
	END

	IF @period = 5 -- last week, display period by week days
	BEGIN

		DECLARE @lastWeek TABLE
		(	
			ExternalId VARCHAR(100),
			[Period] VARCHAR(100),
			OrderBy INT,
			IsEnrollment BIT
		)
		
		INSERT INTO @lastWeek
		SELECT 
			CAST(x.number AS VARCHAR(2)) AS ExternalId			
			,LEFT(DATENAME(WEEKDAY, DATEADD(WEEKDAY, x.number, @startDate)), 3) AS [Period]
			,x.number AS OrderBy
			,NULL AS IsEnrollment
		FROM master.dbo.spt_values x
		WHERE x.type = 'P'        
		AND x.number <= DATEDIFF(DAY, @startDate, @endDate);		
		
		INSERT INTO @timePeriod
		SELECT
		ROW_NUMBER() OVER (ORDER BY table1.ExternalId) AS Id, ExternalId,[Period],OrderBy,IsEnrollment
		FROM
		(
			SELECT ExternalId,[Period],OrderBy,IsEnrollment FROM @lastWeek
			UNION
			SELECT 
				m.ExternalId
				,CASE
					WHEN msh.InActiveDate IS NULL THEN LEFT(DATENAME(WEEKDAY, msh.ActiveDate), 3)
					ELSE LEFT(DATENAME(WEEKDAY, msh.InActiveDate), 3)			
				END AS [Period]
				,CASE
					WHEN msh.InActiveDate IS NULL THEN DAY(msh.ActiveDate)
					ELSE DAY(msh.InActiveDate)
				END AS OrderBy
				,CASE
					WHEN msh.InActiveDate IS NULL THEN CAST(1 AS BIT) 
					ELSE CAST(0 AS BIT) 
				END AS IsEnrollment
			FROM
				MemberStatusHistory msh		
				JOIN Member m ON m.MemberId = msh.MemberId
				JOIN MemberSubscription ms ON ms.MemberId = msh.MemberId		
			WHERE
				m.[Status] != 0
				AND ms.BrokerId IN (SELECT BrokerId FROM @brokerIds)
				AND 
				((msh.InActiveDate IS NULL AND msh.ActiveDate >= @startDate AND msh.ActiveDate <= @endDate)
				OR (msh.InActiveDate IS NOT NULL AND msh.InActiveDate >= @startDate AND msh.InActiveDate <= @endDate))
			GROUP BY
				CASE
					WHEN msh.InActiveDate IS NULL THEN DAY(msh.ActiveDate)
					ELSE DAY(msh.InActiveDate)
				END, 
				CASE
					WHEN msh.InActiveDate IS NULL THEN DATEADD(DAY, 0, msh.ActiveDate)
					ELSE DATEADD(DAY, 0, msh.InActiveDate)
				END,
				m.ExternalId,
				msh.InActiveDate,
				msh.ActiveDate
		) AS table1
		ORDER BY Id
	END

	IF @period = 6 or @period = 7 --last month, display period date wise (01-05, 06-11, etc.)
	BEGIN
		
		DECLARE @lastMonth TABLE
		(	
			[Period] VARCHAR(100),
			StartDate DATETIME,
			EndDate DATETIME,
			IsEnrollment BIT,
			ExternalId VARCHAR(100)
		)

		INSERT INTO @lastMonth
		values
		('01-05', @startDate, @startDate + 4, 0, '0'),
		('06-10', @startDate + 5, @startDate + 9, 0, '0'),
		('11-15', @startDate + 10, @startDate + 14, 0, '0'),
		('16-20', @startDate + 15, @startDate + 19, 0, '0'),
		('21-25', @startDate + 20, @startDate +24, 0, '0')

		IF @period = 6 
		BEGIN
			INSERT INTO @lastMonth values
			('26-' + CAST(DAY(DATEADD(DAY, -(DAY(GETDATE())), GETDATE())) AS VARCHAR(2)), @startDate + 25, @endDate, 0, '0')
		END
		ELSE
		BEGIN
			INSERT INTO @lastMonth values
			('26-' + CAST(DAY(EOMONTH(GETDATE())) AS VARCHAR(2)), @startDate + 25, @endDate, 0, '0')
		END

		INSERT INTO @timePeriod
		SELECT
		ROW_NUMBER() OVER (ORDER BY table1.ExternalId) AS Id, ExternalId,[Period],OrderBy,IsEnrollment
		FROM
		(
			SELECT 
				ExternalId,
				[Period],
				DAY(StartDate) AS OrderBy,
				IsEnrollment
			FROM 
				@lastMonth
			UNION
			SELECT 
				 m.ExternalId
				,lm.[Period] AS [Period]
				,CASE
					WHEN msh.InActiveDate IS NULL THEN DAY(msh.ActiveDate)
					ELSE DAY(msh.InActiveDate)
				END AS OrderBy
				,CASE
					WHEN msh.InActiveDate IS NULL THEN CAST(1 AS BIT) 
					ELSE CAST(0 AS BIT) 
				END AS IsEnrollment
			FROM
				MemberStatusHistory msh		
				JOIN Member m ON m.MemberId = msh.MemberId
				JOIN MemberSubscription ms ON ms.MemberId = msh.MemberId
				,@lastMonth lm	
			WHERE
				m.[Status] != 0
				AND ms.BrokerId IN (SELECT BrokerId FROM @brokerIds)
				AND 
				((msh.InActiveDate IS NULL AND msh.ActiveDate >= lm.StartDate AND msh.ActiveDate <= lm.EndDate)
				OR (msh.InActiveDate IS NOT NULL AND msh.InActiveDate >= lm.StartDate AND msh.InActiveDate <= lm.EndDate))
			GROUP BY
				lm.[Period], 
				m.ExternalId,
				msh.ActiveDate,
				msh.InActiveDate
		) AS table1
		ORDER BY Id
	END

	SELECT * FROM @timePeriod

END
GO

--==============================================================================================================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ====================================================================================================
-- Author:		Krithika Vijayan
-- Create date: 09 September 2019
-- Description:	Stored procedure gets member count by status

-- EXEC [dbo].[GetMemberCountByStatus]  12956, 1, NULL
-- =====================================================================================================
IF OBJECT_ID (N'[dbo].[GetMemberCountByStatus]') IS NOT NULL  
    DROP PROCEDURE [dbo].[GetMemberCountByStatus];  
GO  
CREATE PROCEDURE [dbo].[GetMemberCountByStatus]	
	@loggedInBrokerId BIGINT,
	@viewBy INT,
	@brokerExternalId NVARCHAR(100) = NULL
WITH RECOMPILE
AS
BEGIN
	
	/*
		Member Status

		Active = 1
		OnHold = 2
		Inactive = 4
		Pending = 6
	*/
	SET NOCOUNT ON;
	DECLARE @brokerId BIGINT;
	SELECT @brokerId = BrokerId FROM [Broker] WHERE ExternalId = @brokerExternalId

	DECLARE @brokerIds TABLE(BrokerId BIGINT);
        DECLARE @Ids TABLE(BrokerId BIGINT);
        INSERT INTO @Ids
        EXEC [BrokerIdTree] 
             @BrokerId = @brokerId;
        IF @viewBy = 0 -- All
            BEGIN
                INSERT INTO @brokerIds
                       SELECT BrokerId
                       FROM @Ids;
        END;
            ELSE
            IF @viewBy = 1 -- Only me
                BEGIN
                    INSERT INTO @brokerIds
                           SELECT @loggedInBrokerId;
            END;
                ELSE
                IF @viewBy = 2 -- Tree only
                    BEGIN
                        INSERT INTO @brokerIds
                               SELECT BrokerId
                               FROM @Ids
                               WHERE BrokerId <> @loggedInBrokerId;
                END;
                    ELSE
                    IF @viewBy = 3 --Broker
                        BEGIN
                            INSERT INTO @brokerIds
                                   SELECT @brokerId;
                    END;

	select [Count],[Status], 
	cast(grp.[Count] * 100.0/sum([Count]) over() as decimal(10,2)) as [Percentage],
	ROW_NUMBER() OVER (ORDER BY grp.[Status]) AS Id
	from
	(
		select 
			count(1) as [Count],
			m.[Status]
		from
			MemberSubscription ms
			join Member m on m.MemberId = ms.MemberId
		where
			m.ExternalId IS NOT NULL
			AND m.[Status] != 0
			AND ms.BrokerId IN (SELECT BrokerId FROM @brokerIds)
		group by
			m.[Status]
	) as grp

END
GO

--==============================================================================================================================================

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ====================================================================================================
-- Author:		Krithika Vijayan
-- Create date: 09 September 2019
-- Description:	Stored procedure gets member count by status

-- EXEC [dbo].[GetBrokerHierarchyCount]  12956, 1, NULL
-- =====================================================================================================
IF OBJECT_ID (N'[dbo].[GetBrokerHierarchyCount]') IS NOT NULL  
    DROP PROCEDURE [dbo].[GetBrokerHierarchyCount];  
GO  
CREATE PROCEDURE [dbo].[GetBrokerHierarchyCount] @loggedInBrokerId BIGINT, 
                                                @viewBy           INT, 
                                                @brokerExternalId NVARCHAR(100) = NULL
WITH RECOMPILE
AS
    BEGIN

/*
		Broker Status
		Active = 1
		Pending = 2
		Terminated = 3
	*/
        SET NOCOUNT ON;
        DECLARE @brokerId BIGINT;
        SELECT @brokerId = BrokerId
        FROM [Broker]
        WHERE ExternalId = @brokerExternalId;
        SET @brokerId =
        (
            SELECT CASE
                       WHEN @viewBy != 3
                       THEN @loggedInBrokerId
                       ELSE @brokerId
                   END
        );
        DECLARE @Ids TABLE(BrokerId BIGINT);
        INSERT INTO @Ids
        EXEC [BrokerIdTree] 
             @BrokerId = @brokerId;

        SELECT COUNT(BrokerId) AS [Count], 
               [Status]
        FROM [Broker]
        WHERE ExternalId IS NOT NULL
              AND BrokerId IN
        (
            SELECT BrokerId
            FROM @Ids WHERE BrokerId <> @brokerId
        )
        GROUP BY [Status];
    END;
GO

--==============================================================================================================================================

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ====================================================================================================
-- Author:		Krithika Vijayan
-- Create date: 14 September 2019
-- Description:	Stored procedure gets member count by product status

-- EXEC [dbo].[GetMemberCountByProductStatus]  12956, 1, '0'
-- =====================================================================================================
IF OBJECT_ID (N'[dbo].[GetMemberCountByProductStatus]') IS NOT NULL  
    DROP PROCEDURE [dbo].[GetMemberCountByProductStatus];  
GO  
CREATE PROCEDURE [dbo].[GetMemberCountByProductStatus] @loggedInBrokerId BIGINT, 
                                                       @viewBy           INT, 
                                                       @brokerExternalId NVARCHAR(100)
WITH RECOMPILE
AS
    BEGIN

/*
		Product Status
		---------------
		Active = 1
		OnHold = 2
		UnderReview = 3
		Inactive = 4
		Pending = 6
	*/

        SET NOCOUNT ON;
        DECLARE @brokerId BIGINT;
        SELECT @brokerId = BrokerId
        FROM [Broker]
        WHERE ExternalId = @brokerExternalId;
        DECLARE @brokerIds TABLE(BrokerId BIGINT);
        DECLARE @Ids TABLE(BrokerId BIGINT);
        INSERT INTO @Ids
        EXEC [BrokerIdTree] 
             @BrokerId = @brokerId;
        IF @viewBy = 0 -- All
            BEGIN
                INSERT INTO @brokerIds
                       SELECT BrokerId
                       FROM @Ids;
        END;
            ELSE
            IF @viewBy = 1 -- Only me
                BEGIN
                    INSERT INTO @brokerIds
                           SELECT @loggedInBrokerId;
            END;
                ELSE
                IF @viewBy = 2 -- Tree only
                    BEGIN
                        INSERT INTO @brokerIds
                               SELECT BrokerId
                               FROM @Ids
                               WHERE BrokerId <> @loggedInBrokerId;
                END;
                    ELSE
                    IF @viewBy = 3 --Broker
                        BEGIN
                            INSERT INTO @brokerIds
                                   SELECT @brokerId;
                    END;
        SELECT ProductName, 
               COUNT(ExternalId) AS Total, 
               COUNT(CASE
                         WHEN [Status] = 1
                         THEN ExternalId
                     END) AS Active, 
               COUNT(CASE
                         WHEN [Status] = 2
                         THEN ExternalId
                     END) AS OnHold, 
               COUNT(CASE
                         WHEN [Status] = 3
                         THEN ExternalId
                     END) AS UnderReview, 
               COUNT(CASE
                         WHEN [Status] = 4
                         THEN ExternalId
                     END) AS Inactive, 
               COUNT(CASE
                         WHEN [Status] = 6
                         THEN ExternalId
                     END) AS Pending
        FROM
        (
            SELECT pr.ProductLabel AS ProductName, 
                   m.ExternalId, 
                   msp.[Status]
            FROM MemberSubscribedPlan msp
                 JOIN MemberSubscription ms ON ms.MemberSubscriptionId = msp.MemberSubscriptionId
                 JOIN Member m ON m.MemberId = ms.MemberId
                 JOIN [Plan] p ON p.PlanId = msp.PlanId
                 JOIN Product pr ON pr.ProductId = p.ProductId
            WHERE m.ExternalId IS NOT NULL
                  AND msp.[Status] != 0
                  AND ms.BrokerId IN
            (
                SELECT BrokerId
                FROM @brokerIds
            )
                 AND PR.ProductLabel IS NOT NULL
            GROUP BY pr.ProductLabel, 
                     m.ExternalId, 
                     msp.[Status]
        ) AS grp
        GROUP BY ProductName;
    END;
GO


--==============================================================================================================================================
IF OBJECT_ID (N'[dbo].[CalculateFromDate]') IS NOT NULL  
    DROP FUNCTION [dbo].[CalculateFromDate];  
GO 
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[CalculateFromDate] 
(	
	-- Add the parameters for the function here
	@CurrentDate DateTime, 
	@StartPeriod varchar(25),
	@Day1 int,
	@CurrentDay int
)
RETURNS DateTime 
AS
BEGIN
DECLARE @formDate datetime
DECLARE @startCount int = 0;
SELECT 
@startCount = CASE LOWER(@StartPeriod)
WHEN 'sunday' THEN 1
WHEN 'monday' THEN 2
WHEN 'tuesday' THEN 3
WHEN 'wednesday' THEN 4
WHEN 'thursday' THEN 5
WHEN 'friday' THEN 6
WHEN 'saturday' THEN 7
ELSE 0
END
IF @startCount = @Day1  
BEGIN
   set @formDate = @CurrentDate
END
ELSE   
BEGIN
   set @formDate = DATEADD(day,(@startCount - @CurrentDay), @CurrentDate)
END
return @formDate 
END
GO

IF OBJECT_ID (N'[dbo].[PopulateBrokerAllPayPeriodCommissions]') IS NOT NULL  
    DROP PROCEDURE [dbo].[PopulateBrokerAllPayPeriodCommissions];  
GO 
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PopulateBrokerAllPayPeriodCommissions] @ExternalId VARCHAR(10)
WITH RECOMPILE
AS
    BEGIN
   		DECLARE @tblCommissionReport TABLE
        ([Posted Date]           DATETIME, 
         [Created Date]          DATETIME, 
         [Type]                  VARCHAR(7), 
         [Subtype]               VARCHAR(50), 
         [Debit]                 VARCHAR(9), 
         [Credit]                VARCHAR(7), 
         [Total]                 VARCHAR(10), 
         [Member Agent ID]       BIGINT, 
         [Member Company]        VARCHAR(100), 
         [Payee Agent ID]        BIGINT, 
         [Commissionable Amount] VARCHAR(10), 
         [Member ID]             BIGINT, 
         [GroupId]               BIGINT, 
         [Id]                    BIGINT
        );
         DECLARE @tblFilterCommissionReport TABLE
        ([Type]                  VARCHAR(7), 
         [Subtype]               VARCHAR(50), 
         [Debit]                 VARCHAR(9), 
         [Credit]                VARCHAR(7), 
         [Total]                 VARCHAR(10), 
         [Member Agent ID]       BIGINT, 
         [Member Company]        VARCHAR(100), 
         [Payee Agent ID]        BIGINT, 
         [Commissionable Amount] VARCHAR(10), 
         [Member ID]             BIGINT, 
         [GroupId]               BIGINT, 
         [Id]                    BIGINT
        );
         DECLARE @tblCommission TABLE
        ([Type]                  VARCHAR(7), 
         [Debit]                 VARCHAR(9), 
         [Total]                 VARCHAR(10), 
         [Commissionable Amount] VARCHAR(10), 
         [Id]                    BIGINT
        );
         DECLARE @tblCommissionReportBO TABLE
        (Id                  BIGINT, 
         MemberId            BIGINT, 
         [Subtype]           VARCHAR(50), 
         [Debit]             VARCHAR(9), 
         [Credit]            VARCHAR(7), 
         [StartDate]         VARCHAR(25), 
         [EndDate]           VARCHAR(25), 
         MemberBrokerId      BIGINT, 
         MemberExternalId    VARCHAR(10), 
         [GroupId]           BIGINT, 
         GroupExternalId     VARCHAR(25), 
         MemberBrokerCompany VARCHAR(100), 
         PayeeBrokerCompany  VARCHAR(100), 
         Refunds             VARCHAR(10), 
         Premium             VARCHAR(10), 
         [Status]            VARCHAR(10), 
         Commission          VARCHAR(10), 
         PayeeBrokerId       BIGINT, 
         PayeeExternalId     VARCHAR(25)
        );
        DECLARE @BrokerId BIGINT, @CurrentPayPeriod VARCHAR(50), @LastPayPeriod VARCHAR(50), @PayeeBrokerCompany VARCHAR(100);
        SELECT @BrokerId = BrokerId, 
               @PayeeBrokerCompany = Company
        FROM Broker
        WHERE ExternalId = @ExternalId;
        DECLARE @PayPeriod INT, @StartPeriod VARCHAR(25), @EndPeriod VARCHAR(25), @CurrentDate DATETIME, @FromDate DATETIME, @EndDate DATETIME, @LastFromDate DATETIME, @LastEndDate DATETIME;
        SET @CurrentDate = GETUTCDATE();
        SELECT @PayPeriod = PayPeriod, 
               @StartPeriod = StartPeriod, 
               @EndPeriod = EndPeriod
        FROM BrokerPayPeriod
        WHERE BrokerId = @BrokerId;
        SET @FromDate = @CurrentDate;
        SET @EndDate = DATEADD(day, 7, @CurrentDate);
        SET @LastFromDate = @CurrentDate;
        SET @LastEndDate = DATEADD(day, 7, @CurrentDate);
        IF @PayPeriod = 1  -- Pay Period Weekly 
            BEGIN
                DECLARE @day1 INT;
                SET @day1 = DATEPART(dw, @CurrentDate);
                SELECT @FromDate = CASE @day1
                                       WHEN 0
                                       THEN [dbo].[CalculateFromDate](@CurrentDate, 'sunday', @day1, 1)
                                       WHEN 1
                                       THEN [dbo].[CalculateFromDate](@CurrentDate, 'monday', @day1, 2)
                                       WHEN 2
                                       THEN [dbo].[CalculateFromDate](@CurrentDate, 'tuesday', @day1, 3)
                                       WHEN 3
                                       THEN [dbo].[CalculateFromDate](@CurrentDate, 'wednesday', @day1, 4)
                                       WHEN 4
                                       THEN [dbo].[CalculateFromDate](@CurrentDate, 'thursday', @day1, 5)
                                       WHEN 5
                                       THEN [dbo].[CalculateFromDate](@CurrentDate, 'friday', @day1, 6)
                                       WHEN 6
                                       THEN [dbo].[CalculateFromDate](@CurrentDate, 'saturday', @day1, 7)
                                   END;
                SET @EndDate = DATEADD(day, 6, @FromDate);
        END;
            ELSE
            IF @PayPeriod = 2  -- Pay Period BiMonthly 
                BEGIN
                    DECLARE @StartDay INT, @EndDay INT, @DaysInMonth INT, @CurrentDay INT;
                    SET @StartDay = @StartPeriod;
                    SET @EndDay = @EndPeriod;
                    SET @FromDate = DATEADD(day, (@StartDay - DATEPART(dd, @FromDate)), @FromDate);
                    SET @EndDate = DATEADD(day, (@EndDay - DATEPART(dd, @EndDate)), @EndDate);
                    SET @DaysInMonth = DAY(EOMONTH(@FromDate));
                    SET @CurrentDay = DATEPART(d, @CurrentDate);
                    IF @CurrentDay > 15
                        BEGIN
                            SET @LastFromDate = @FromDate;
                            SET @LastEndDate = @EndDate;
                            SET @FromDate = DATEADD(day, 1, @LastEndDate);
                            SET @EndDate = DATEADD(day, (@DaysInMonth - DATEPART(dd, @LastFromDate)), @LastFromDate);
                    END;
            END;
        SET @CurrentPayPeriod = CONVERT(VARCHAR, @FromDate, 1) + '-' + CONVERT(VARCHAR, @EndDate, 1);
        SET @LastPayPeriod = CONVERT(VARCHAR, @LastFromDate, 1) + '-' + CONVERT(VARCHAR, @LastEndDate, 1);
        DECLARE @PayPeriodDay INT;
        IF @PayPeriod = 1
            SET @PayPeriodDay = 7;
            ELSE
            SET @PayPeriodDay = 15;
        INSERT INTO @tblCommissionReport
               SELECT [Posted Date], 
                      [Created Date], 
                      [Type], 
                      [Subtype], 
                      [Debit], 
                      [Credit], 
                      [Total], 
                      [Member Agent ID], 
                      [Member Company], 
                      [Payee Agent ID], 
                      [Commissionable Amount], 
                      [Member ID], 
                      [GroupId], 
                      [Id]
               FROM CommissionReport
               WHERE [Payee Agent ID] = @BrokerId
               ORDER BY [Posted Date] DESC;
        DECLARE @MinDate DATETIME;
        IF
        (
            SELECT COUNT(1)
            FROM @tblCommissionReport
        ) > 0
            SET @MinDate = CONVERT(DATE,
            (
                SELECT MIN([Posted Date])
                FROM @tblCommissionReport
            ));
            ELSE
            SET @MinDate = CONVERT(DATE, @FromDate);
        DECLARE @PayPeriodComission DECIMAL;  --sum of credit and debit=> total column 
        DECLARE @PayPeriodPremiun DECIMAL; --sum of commisionable amount
        DECLARE @PayPeriodRefunds DECIMAL; --sum of debits
        SET @PayPeriodComission = 0;
        SET @PayPeriodPremiun = 0;
        SET @PayPeriodRefunds = 0;
        DECLARE @TempStartDate DATETIME;
        DECLARE @TempEndDate DATETIME;
        SET @TempStartDate = CONVERT(DATE, @FromDate);
        SET @TempEndDate = CONVERT(DATE, @EndDate);
        WHILE(@TempEndDate >= @MinDate)
            BEGIN
                DECLARE @lastMonth DATETIME, @lastMonthDay INT;
                INSERT INTO @tblFilterCommissionReport
                       SELECT [Type], 
                              [Subtype], 
                              [Debit], 
                              [Credit], 
                              [Total], 
                              [Member Agent ID], 
                              [Member Company], 
                              [Payee Agent ID], 
                              [Commissionable Amount], 
                              [Member ID], 
                              [GroupId], 
                              [Id]
                       FROM @tblCommissionReport
                       WHERE [Posted Date] >= @TempStartDate
                             AND [Posted Date] < DATEADD(day, 1, @TempEndDate);
                IF
                (
                    SELECT COUNT(1)
                    FROM @tblFilterCommissionReport
                ) = 0
                    BEGIN
                        IF @PayPeriodDay = 15
                            BEGIN
                                SET @CurrentDay = DATEPART(d, @TempStartDate);
                                IF @CurrentDay <= 15
                                    BEGIN
                                        SET @lastMonth = DATEADD(MONTH, -1, @TempStartDate);
                                        SET @lastMonthDay = DAY(EOMONTH(@lastMonth));
                                        SET @TempStartDate = DATEADD(day, (16 - DATEPART(dd, @lastMonth)), @lastMonth);
                                        SET @TempEndDate = DATEADD(day, (@lastMonthDay - DATEPART(dd, @lastMonth)), @lastMonth);
                                END;
                                    ELSE
                                    BEGIN
                                        SET @TempStartDate = DATEADD(day, (1 - DATEPART(dd, @TempStartDate)), @TempStartDate);
                                        SET @TempEndDate = DATEADD(day, (@PayPeriodDay - 1), @TempStartDate);
                                END;
                        END;
                            ELSE
                            IF @PayPeriodDay = 7
                                BEGIN
                                    SET @TempEndDate = DATEADD(day, -1, @TempStartDate);
                                    SET @TempStartDate = DATEADD(day, -@PayPeriodDay, @TempStartDate);
                            END;
                        CONTINUE;
                END;
                INSERT INTO @tblCommission
                       SELECT [Type], 
                              [Debit], 
                              [Total], 
                              [Commissionable Amount], 
                              [Id]
                       FROM @tblFilterCommissionReport
                       WHERE [Type] <> 'PAYMENT';
                SET @PayPeriodComission =
                (
                    SELECT CAST(ROUND(SUM(CAST(Total AS DECIMAL(18, 2))), 2) AS DECIMAL(18, 2))
                    FROM @tblCommission
                    WHERE [Type] <> 'PAYMENT'
                );
                SET @PayPeriodPremiun =
                (
                    SELECT CAST(ROUND(SUM(CAST(CASE WHEN ISNUMERIC([Commissionable Amount]) = 1
							 THEN [Commissionable Amount] ELSE NULL END AS MONEY)),2) AS DECIMAL(18, 2))
                    FROM @tblCommission
                    WHERE [Type] <> 'PAYMENT'
                );
                SET @PayPeriodRefunds =
                (
                    SELECT CAST(ROUND(SUM(CAST(Debit AS DECIMAL(18, 2))), 2) AS DECIMAL(18, 2))
                    FROM @tblCommission
                    WHERE [Type] <> 'PAYMENT'
                );
                DECLARE @Status VARCHAR(10);
                IF
                (
                    SELECT COUNT(1)
                    FROM @tblFilterCommissionReport
                    WHERE [Type] = 'PAYMENT'
                ) > 0
                    SET @Status = 'Paid';
                    ELSE
                    SET @Status = 'Earned';
                INSERT INTO @tblCommissionReportBO
                       SELECT TOP 1 Id, 
                                    MEM.ExternalId AS MemberId, 
                                    TFCR.Subtype AS SubType, 
                                    TFCR.Debit AS Debit, 
                                    TFCR.Credit AS Credit, 
                                    CONVERT(VARCHAR, @TempStartDate, 1) AS StartDate, 
                                    CONVERT(VARCHAR, @TempEndDate, 1) AS EndDate, 
                                    ISNULL(TFCR.[Member Agent ID], ISNULL(GRP.GroupId, 0)) AS MemberBrokerId, 
                                    ISNULL(MemberAgent.ExternalId, GRP.Admin123Id) AS MemberExternalId, 
                                    GRP.GroupId AS GroupId, 
                                    GRP.Admin123Id AS GroupExternalId, 
                                    TFCR.[Member Company] AS MemberBrokerCompany, 
                                    @PayeeBrokerCompany AS PayeeBrokerCompany, 
                                    CONVERT(VARCHAR, @PayPeriodRefunds) AS Refunds, 
                                    CONVERT(VARCHAR, @PayPeriodPremiun) AS Premium, 
                                    @Status AS [Status], 
                                    CONVERT(VARCHAR, @PayPeriodComission) AS Commission, 
                                    ISNULL(TFCR.[Payee Agent ID], 0) AS PayeeBrokerId, 
                                    @ExternalId AS PayeeExternalId
                       FROM @tblFilterCommissionReport TFCR
                            LEFT OUTER JOIN Member MEM ON TFCR.[Member ID] = MEM.MemberId
                            LEFT OUTER JOIN [Group] GRP ON GRP.GroupId = TFCR.GroupId
                            LEFT OUTER JOIN [Broker] MemberAgent ON MemberAgent.BrokerId = TFCR.[Member Agent ID];
                DELETE FROM @tblFilterCommissionReport;
                DELETE FROM @tblCommission;
                SET @PayPeriodComission = 0;
                SET @PayPeriodPremiun = 0;
                SET @PayPeriodRefunds = 0;
                IF @PayPeriodDay = 15
                    BEGIN
                        SET @CurrentDay = DATEPART(d, @TempStartDate);
                        IF @CurrentDay <= 15
                            BEGIN
                                SET @lastMonth = DATEADD(MONTH, -1, @TempStartDate);
                                SET @lastMonthDay = DAY(EOMONTH(@lastMonth));
                                SET @TempStartDate = DATEADD(day, (16 - DATEPART(dd, @lastMonth)), @lastMonth);
                                SET @TempEndDate = DATEADD(day, (@lastMonthDay - DATEPART(dd, @lastMonth)), @lastMonth);
                        END;
                            ELSE
                            BEGIN
                                SET @TempStartDate = DATEADD(day, (1 - DATEPART(dd, @TempStartDate)), @TempStartDate);
                                SET @TempEndDate = DATEADD(day, (@PayPeriodDay - 1), @TempStartDate);
                        END;
                END;
                    ELSE
                    IF @PayPeriodDay = 7
                        BEGIN
                            SET @TempEndDate = DATEADD(day, -1, @TempStartDate);
                            SET @TempStartDate = DATEADD(day, -@PayPeriodDay, @TempStartDate);
                    END;
            END;
			INSERT INTO [dbo].[CommissionAllPayPeriodData]
			   SELECT 
			   Id, 
               MemberId, 
               SubType, 
               Debit, 
               Credit, 
               StartDate, 
               EndDate, 
               MemberBrokerId, 
               MemberExternalId, 
               GroupId, 
               GroupExternalId, 
               MemberBrokerCompany, 
               PayeeBrokerCompany, 
               Refunds, 
               Premium, 
               [Status], 
               Commission, 
               PayeeBrokerId, 
               PayeeExternalId
        FROM @tblCommissionReportBO;
    END;
GO

IF OBJECT_ID (N'[dbo].[PopulateBrokerPayPeriodCommissions]') IS NOT NULL  
    DROP PROCEDURE [dbo].[PopulateBrokerPayPeriodCommissions];  
GO 
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PopulateBrokerPayPeriodCommissions] 
AS
BEGIN
     SET NOCOUNT ON;
	 DECLARE @StartDate DATETIME,  @EndDate DATETIME

	 SET @EndDate = CONVERT(date, GETUTCDATE());
	 SET @StartDate = CONVERT(date, DATEADD(MM, -3, @EndDate));

	 TRUNCATE TABLE [dbo].[CommissionPayPeriodData];

	 INSERT INTO [dbo].[CommissionPayPeriodData]
	 SELECT [Id],
       [MemberId],
       [Subtype],
       [Debit],
       [Credit],
       [StartDate],
       [EndDate],
       [MemberBrokerId],
       [MemberExternalId],
       [GroupId],
       [GroupExternalId],
       [MemberBrokerCompany],
       [PayeeBrokerCompany],
       [Refunds],
       [Premium],
       [Status],
       [Commission],
       [PayeeBrokerId],
       [PayeeExternalId]
	   FROM [dbo].[CommissionAllPayPeriodData]
       WHERE StartDate >= CONVERT(date, @StartDate) AND EndDate <= CONVERT(date, @EndDate)
END
GO


IF OBJECT_ID (N'[dbo].[BrokerCommissions]') IS NOT NULL  
    DROP PROCEDURE [dbo].[BrokerCommissions];  
GO 
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[BrokerCommissions] 
@BrokerId BIGINT ,
@FromDate DATETIME,  @EndDate DATETIME,
@LastFromDate DATETIME,  @LastEndDate DATETIME,
@PayPeriod INT,
@CurrentPeriod NVARCHAR(50),
@LastPeriod NVARCHAR(50)

WITH RECOMPILE   
AS 
BEGIN
	SET NOCOUNT ON;
     SELECT ROW_NUMBER() OVER (ORDER BY  [Posted Date]) AS Id, @BrokerId AS BrokerId, [Posted Date] AS PostedDate, [Type], Total, CAST('true' AS bit) AS IsCurrentCommission, 
	 @PayPeriod AS PayPeriod,
	 @CurrentPeriod AS CurrentPeriod, @LastPeriod AS LastPeriod 
	 FROM CommissionReport
	 WHERE [Payee Agent ID] = @BrokerId 
	 AND [Posted Date] >= CONVERT(date,@FromDate) 
	 AND [Posted Date] <= CONVERT(date, @EndDate)
	 UNION
	 SELECT ROW_NUMBER() OVER (ORDER BY  [Posted Date]) AS Id, @BrokerId AS BrokerId, [Posted Date] AS PostedDate, [Type], Total, CAST('false' AS bit) AS IsCurrentCommission,
	 @PayPeriod AS PayPeriod,
	 @CurrentPeriod AS CurrentPeriod, @LastPeriod AS LastPeriod 
	 FROM CommissionReport
	 WHERE [Payee Agent ID] = @BrokerId 
	 AND [Posted Date] >= CONVERT(date, @LastFromDate) 
	 AND [Posted Date] <= CONVERT(date, @LastEndDate)
	SET NOCOUNT OFF;
END
GO

IF OBJECT_ID (N'[dbo].[GetBrokerCommissions]') IS NOT NULL  
    DROP PROCEDURE [dbo].[GetBrokerCommissions];  
GO 
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
CREATE PROC [dbo].[GetBrokerCommissions] @BrokerId  BIGINT, 
                                @ExternalId NVARCHAR(100)
WITH RECOMPILE
AS
    BEGIN
		BEGIN TRY
		SET NOCOUNT ON;
 
        DECLARE @ChildBrokerId BIGINT;
		if @ExternalId IS NOT NULL OR @ExternalId != ''
		BEGIN
			WITH brkHierarchy
				 AS (SELECT BrokerId
					 FROM BrokerHierarchy
					 WHERE BrokerId = @BrokerId
					 UNION ALL
					 SELECT bh.brokerid
					 FROM BrokerHierarchy bh
					 INNER JOIN brkHierarchy bhCTE ON bh.ParentBrokerId = bhCTE.BrokerId)

					 SELECT @ChildBrokerId = bh.BrokerId
					 FROM brkHierarchy bh
					 INNER JOIN Broker b ON bh.BrokerId = b.BrokerId
					 AND b.ExternalId = @ExternalId;
			IF @ChildBrokerId != 0
			BEGIN
					SET @BrokerId = @ChildBrokerId
			END;
			ELSE
			BEGIN
					RAISERROR ('Broker you are looking is not in your tree, so you will not be able to see his Commission details', 1, 1); 
			END;
		END;
        DECLARE @StartPeriod NVARCHAR(50) = '', @EndPeriod NVARCHAR(50) = '';
        DECLARE @CurrentDate DATETIME = GETUTCDATE();
        DECLARE @CurrentPeriod NVARCHAR(50) = '';
        DECLARE @LastPeriod NVARCHAR(50) = '';
        DECLARE @FromDate DATETIME = @CurrentDate;
        DECLARE @EndDate DATETIME = DATEADD(dd, 7, @CurrentDate);
        DECLARE @LastFromDate DATETIME = @CurrentDate;
        DECLARE @LastEndDate DATETIME = DATEADD(dd, 7, @CurrentDate);
        DECLARE @PayPeriod INT;
        SELECT @StartPeriod = StartPeriod, 
               @EndPeriod = EndPeriod, 
               @PayPeriod = PayPeriod
        FROM BrokerPayPeriod
        WHERE BrokerId = @BrokerId 
        DECLARE @Day1 INT;
        IF @PayPeriod = 1
        BEGIN
			SET @Day1 =
			(
				SELECT DATEPART(dw, @CurrentDate)
			);
			IF @Day1 = 1
				BEGIN
					SET @FromDate =
					(
						SELECT dbo.CalculateFromDate(@CurrentDate, 'sunday', @Day1, 1)
					);
				END;
				ELSE
				IF @Day1 = 2
				BEGIN
					SET @FromDate =
					(
						SELECT dbo.CalculateFromDate(@CurrentDate, 'monday', @Day1, 2)
					);
				END;
				ELSE IF @Day1 = 3
				BEGIN
					SET @FromDate =
					(
						SELECT dbo.CalculateFromDate(@CurrentDate, 'tuesday', @Day1, 3)
					);
				END;
				ELSE IF @Day1 = 4
				BEGIN
					SET @FromDate =
					(
						SELECT dbo.CalculateFromDate(@CurrentDate, 'wednesday', @Day1, 4)
					);
				END;
				ELSE IF @Day1 = 5
				BEGIN
					SET @FromDate =
					(
						SELECT dbo.CalculateFromDate(@CurrentDate, 'thursday', @Day1, 5)
					);
				END;
				ELSE IF @Day1 = 6
				BEGIN
					SET @FromDate =
					(
						SELECT dbo.CalculateFromDate(@CurrentDate, 'friday', @Day1, 6)
					);
				END;
				ELSE IF @Day1 = 7
				BEGIN
					SET @FromDate =
					(
						SELECT dbo.CalculateFromDate(@CurrentDate, 'saturday', @Day1, 7)
					);
				END;
			SET @EndDate = DATEADD(dd, 6, @FromDate);
			SET @LastEndDate = DATEADD(dd, -1, @FromDate);
			SET @LastFromDate = DATEADD(dd, -6, @LastEndDate);
			PRINT @EndDate + @LastEndDate +  @LastFromDate
        END --IF @PayPeriod = 1;
        ELSE IF @PayPeriod = 2
        BEGIN
            DECLARE @StartDay INT = CAST(@StartPeriod AS INT);
            DECLARE @EndDay INT = CAST(@EndPeriod AS INT);
            SET @FromDate = CONVERT(DATETIME,(CONVERT(NVARCHAR(4), DATEPART(yy, @FromDate))) + '/' +
            (CONVERT(NVARCHAR(2), DATEPART(mm, @FromDate))) + '/' + CONVERT(NVARCHAR(2),@StartDay)); 
            SET @EndDate = CONVERT(DATETIME,(CONVERT(NVARCHAR(4), DATEPART(yy, @FromDate))) + '/' +
            (CONVERT(NVARCHAR(2), DATEPART(mm, @FromDate))) + '/' + CONVERT(NVARCHAR(2), @EndDay)); 
            DECLARE @DaysInMonth INT = (SELECT DAY(EOMONTH(@FromDate)) AS NoOfDays); 
            DECLARE @CurrentDay INT = (SELECT DAY(GETUTCDATE()));
            IF @CurrentDay <= 15
            BEGIN
                    SET @LastFromDate = DATEADD(MM, -1, @FromDate);
                    SET @DaysInMonth =(SELECT DAY(EOMONTH(@LastFromDate)) AS NoOfDays); 
                    SET @LastEndDate = CONVERT(DATETIME,(CONVERT(NVARCHAR(4), DATEPART(yy, @LastFromDate))) + '/' +
                    (CONVERT(NVARCHAR(2), DATEPART(mm, @LastFromDate))) + '/' + CONVERT(NVARCHAR(2),@DaysInMonth)); 
                    IF((@DaysInMonth % 2 = 0) OR ((SELECT DAY(@LastFromDate)) < @DaysInMonth / 2))
                    BEGIN
                        SET @LastFromDate = DATEADD(dd, -(@DaysInMonth / 2), @LastEndDate);
                    END;
            END;
            ELSE
            BEGIN
                SET @LastFromDate = @FromDate;
                SET @LastEndDate = @EndDate;
                SET @FromDate = DATEADD(dd, 1, @LastEndDate);
                SET @EndDate = CONVERT(DATETIME,(CONVERT(NVARCHAR(4), DATEPART(yy, @LastFromDate))) + '/' +
                (CONVERT(NVARCHAR(2), DATEPART(mm, @LastFromDate))) + '/' + CONVERT(NVARCHAR(2), @DaysInMonth));
            END;
        END; --ELSE IF @PayPeriod = 2

        SET @CurrentPeriod = CONVERT(NVARCHAR(10), @FromDate, 101) + ' - ' + CONVERT(NVARCHAR(10), @EndDate, 101);
        SET @LastPeriod = CONVERT(NVARCHAR(10), @LastFromDate, 101) + ' - ' + CONVERT(NVARCHAR(10), @LastEndDate, 101);
 
		EXEC BrokerCommissions 
        @BrokerId = @BrokerId, 
        @FromDate = @FromDate, 
        @EndDate = @EndDate, 
        @LastFromDate = @LastFromDate, 
        @LastEndDate = @LastEndDate,
		@CurrentPeriod = @CurrentPeriod,
		@LastPeriod = @LastPeriod,
		@PayPeriod = @PayPeriod;
        SET NOCOUNT OFF;
		END TRY  
		BEGIN CATCH 
		END CATCH
    END
GO
