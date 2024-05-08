USE [master]
GO

CREATE LOGIN [docker] WITH PASSWORD=N'docker1234', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

ALTER LOGIN [docker] DISABLE
GO

ALTER SERVER ROLE [sysadmin] ADD MEMBER [docker]
GO

USE [BookDb]
GO

/****** Object:  User [docker]    Script Date: 16-11-2023 09:24:16 ******/
CREATE USER [docker] FOR LOGIN [docker] WITH DEFAULT_SCHEMA=[dbo]
GO

USE [BookDbUsers]
GO

/****** Object:  User [docker]    Script Date: 16-11-2023 09:24:48 ******/
CREATE USER [docker] FOR LOGIN [docker] WITH DEFAULT_SCHEMA=[dbo]
GO



