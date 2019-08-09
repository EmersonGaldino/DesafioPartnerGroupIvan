/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/


/*
--------------------------------------------------------------------------------------
 Administrator
--------------------------------------------------------------------------------------
*/
IF  EXISTS (SELECT * FROM sys.server_principals WHERE name = N'Administrator')
set noexec on

USE [master]
GO
CREATE LOGIN [Administrator] WITH PASSWORD=N'abc123', DEFAULT_DATABASE=[$(DatabaseName)], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
EXEC master..sp_addsrvrolemember @loginame = N'Administrator', @rolename = N'sysadmin'
GO

set noexec off

USE [master]
GO
ALTER LOGIN [Administrator] WITH DEFAULT_DATABASE=[$(DatabaseName)]
GO


/*
--------------------------------------------------------------------------------------
 Operator
--------------------------------------------------------------------------------------
*/
IF  EXISTS (SELECT * FROM sys.server_principals WHERE name = N'Operator')
set noexec on

USE [master]
GO
CREATE LOGIN [Operator] WITH PASSWORD=N'Operator', DEFAULT_DATABASE=[$(DatabaseName)], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
EXEC master..sp_addsrvrolemember @loginame = N'Operator', @rolename = N'sysadmin'
GO

set noexec off

USE [master]
GO
ALTER LOGIN [Operator] WITH DEFAULT_DATABASE=[$(DatabaseName)]
GO


/*
--------------------------------------------------------------------------------------
 SimpleUser
--------------------------------------------------------------------------------------
*/
IF  EXISTS (SELECT * FROM sys.server_principals WHERE name = N'SimpleUser')
set noexec on

USE [master]
GO
CREATE LOGIN [MillUser] WITH PASSWORD=N'SimpleUser', DEFAULT_DATABASE=[$(DatabaseName)], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

set noexec off

USE [master]
GO
ALTER LOGIN [MillUser] WITH DEFAULT_DATABASE=[$(DatabaseName)]
GO

USE [$(DatabaseName)]
GO

IF EXISTS(SELECT * FROM sys.database_principals WHERE name = 'SimpleUser')
DROP USER [SimpleUser]
GO

/** Object:  User [SimpleUser] */
/*
CREATE USER [SimpleUser] FOR LOGIN [SimpleUser] WITH DEFAULT_SCHEMA=[dbo]
GO

Grant select on [func_XYZ1] to [SimpleUser] 
Go
Grant select on [func_XYZ2] to [SimpleUser] 
Go
*/
