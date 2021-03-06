IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Documents]') AND type in (N'U'))
ALTER TABLE [dbo].[Documents] DROP CONSTRAINT IF EXISTS [DF_Documents_isUploadedS3_123]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Documents]') AND type in (N'U'))
ALTER TABLE [dbo].[Documents] DROP CONSTRAINT IF EXISTS [DF_Documents_IsSecuredDocument]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Documents]') AND type in (N'U'))
ALTER TABLE [dbo].[Documents] DROP CONSTRAINT IF EXISTS [DF_Documents_CreatedDate]
GO
DROP TABLE IF EXISTS [dbo].[DocumentType]
GO
DROP TABLE IF EXISTS [dbo].[DocumentStatus]
GO
DROP TABLE IF EXISTS [dbo].[Documents]
GO
DROP TABLE IF EXISTS [dbo].[DocumentAccess]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentAccess](
	[DocumentID] [bigint] NOT NULL,
	[EntityTypeID] [int] NOT NULL,
	[EntityID] [bigint] NOT NULL,
 CONSTRAINT [PK_DocumentAccess_DocumentID] PRIMARY KEY CLUSTERED 
(
	[DocumentID] ASC,
	[EntityTypeID] ASC,
	[EntityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Documents](
	[DocumentID] [bigint] IDENTITY(1,1) NOT NULL,
	[DocumentTypeID] [int] NOT NULL,
	[FileType] [nvarchar](10) NOT NULL,
	[FileName] [nvarchar](300) NOT NULL,
	[Description] [nvarchar](300) NOT NULL,
	[Notes] [nvarchar](3000) NOT NULL,
	[DocumentStatusID] [int] NOT NULL,
	[OwnerTypeID] [int] NOT NULL,
	[OwnerID] [bigint] NOT NULL,
	[CreatedBy] [bigint] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[IsSecuredDocument] [bit] NOT NULL,
	[DocumentCreatedDate] [datetime] NULL,
	[DocumentID_123] [nvarchar](10) NOT NULL,
	[URL_123] [varchar](200) NOT NULL,
	[isUploadedS3_123] [bit] NOT NULL,
 CONSTRAINT [PK_Documents_DocumentID] PRIMARY KEY CLUSTERED 
(
	[DocumentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentStatus](
	[DocumentStatusID] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_DocumentStatus_DocumentStatusID] PRIMARY KEY CLUSTERED 
(
	[DocumentStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentType](
	[DocumentTypeID] [int] NOT NULL,
	[Name] [varchar](100) NOT NULL,
 CONSTRAINT [PK_DocumentType_DocumentTypeID] PRIMARY KEY CLUSTERED 
(
	[DocumentTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Documents] ADD  CONSTRAINT [DF_Documents_CreatedDate]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Documents] ADD  CONSTRAINT [DF_Documents_IsSecuredDocument]  DEFAULT ((0)) FOR [IsSecuredDocument]
GO
ALTER TABLE [dbo].[Documents] ADD  CONSTRAINT [DF_Documents_isUploadedS3_123]  DEFAULT ((0)) FOR [isUploadedS3_123]
GO
