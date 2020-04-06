--rename databases on RDS SQL Server

EXEC rdsadmin.dbo.rds_modify_db_name N'DMS', N'AlieraDocuments'

EXEC rdsadmin.dbo.rds_modify_db_name N'AuditLog', N'AlieraAudit'


/* 
--commented code to be deployed on SQL Server

USE [master]
GO
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'DMS')
BEGIN
	ALTER DATABASE DMS MODIFY NAME = AlieraDocuments ;
END
GO
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'Audit')
BEGIN
	ALTER DATABASE [Audit] MODIFY NAME = AlieraAudit ;
END
GO
*/
