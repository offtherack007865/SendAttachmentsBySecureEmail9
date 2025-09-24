-- SQL Server Instance:  smg-sql01
IF (@@SERVERNAME <> 'smg-sql01')
BEGIN
PRINT 'Invalid SQL Server Connection'
RETURN
END

USE [SimpleFileMover8];

-- Step 001 of 005
-- Insert Config Entry for Talkiatry
SELECT 1;
-- 1 record 

-- BEGIN TRAN

INSERT INTO [dbo].[SimpleFileMover8_Config]
           ([Enabled]
           ,[SystemName]
           ,[SourceDirectory]
           ,[SearchRootSourceDirectory]
           ,[SearchSubdirectoriesOfRootSourceDirectory]
           ,[RequiredFilePrefix]
           ,[DeleteSourceFile]
           ,[ConfigWebApiUrl]
           ,[CreatedBy]
           ,[CreatedTimestamp]
           ,[UpdatedBy]
           ,[UpdatedTimestamp])
     VALUES
           (1
           ,'Talkiatry'
           ,'\\smg-ftp\Athena\Talkiatry'
           ,1
           ,0
           ,'Talkiatry'
           ,1
           ,'http://webservices:8085/api/Ops/GetMySimpleFileMover8Config'
           ,'pwmorrison'
           ,GETDATE()
           ,'pwmorrison'
           ,GETDATE());

-- COMMIT TRAN
-- ROLLBACK TRAN

DECLARE @SimpleFileMover8_ConfigID [bigint] = 0;
SELECT @SimpleFileMover8_ConfigID = [Pk]
FROM [dbo].[SimpleFileMover8_Config]
WHERE [SystemName] = 'Talkiatry';

IF @SimpleFileMover8_ConfigID <= 0 BEGIN
  RETURN;
END


-- Step 002 of 005
-- Insert DestDirs 1 Entry for Talkiatry - send it to the read directory for the process
-- which sends the Talkiatry document to Talkiatry via secure email.
SELECT 1;
-- 1 record 

-- BEGIN TRAN

INSERT INTO [dbo].[SimpleFileMover8_Config_DestDirs]
           ([SimpleFileMover8_ConfigPk]
           ,[DestDir]
           ,[CreatedBy]
           ,[CreatedTimestamp]
           ,[UpdatedBy]
           ,[UpdatedTimestamp])
     VALUES
           (@SimpleFileMover8_ConfigID
           ,'\\ps-nas\backuptodisk\development\SendAttachmentsBySecureEmail_Attachments\Talkiatry'
           ,'pwmorrison'
           ,GETDATE()
           ,'pwmorrison'
           ,GETDATE());
           
-- COMMIT TRAN
-- ROLLBACK TRAN

-- Step 003 of 005
-- Insert DestDirs 2 Entry for Talkiatry - send it to the smg-ftp Talkiatry archive.
SELECT 1;
-- 1 record 

-- BEGIN TRAN

INSERT INTO [dbo].[SimpleFileMover8_Config_DestDirs]
           ([SimpleFileMover8_ConfigPk]
           ,[DestDir]
           ,[CreatedBy]
           ,[CreatedTimestamp]
           ,[UpdatedBy]
           ,[UpdatedTimestamp])
     VALUES
           (@SimpleFileMover8_ConfigID
           ,'\\smg-ftp\Athena\Talkiatry\Transferred to vendor'
           ,'pwmorrison'
           ,GETDATE()
           ,'pwmorrison'
           ,GETDATE());
           
-- COMMIT TRAN
-- ROLLBACK TRAN

-- Step 004 of 005
-- Insert Emailees 1 Entry for Talkiatry - pwmorrison@summithealthcare.com
SELECT 1;
-- 1 record 

-- BEGIN TRAN

INSERT INTO [dbo].[SimpleFileMover8_Config_Emailees]
           ([SimpleFileMover8_ConfigPk]
           ,[EmailAddress]
           ,[CreatedBy]
           ,[CreatedTimestamp]
           ,[UpdatedBy]
           ,[UpdatedTimestamp])
     VALUES
           (@SimpleFileMover8_ConfigID
           ,'pwmorrison@summithealthcare.com'
           ,'pwmorrison'
           ,GETDATE()
           ,'pwmorrison'
           ,GETDATE());           
-- COMMIT TRAN
-- ROLLBACK TRAN

-- Step 005 of 005
-- Insert Emailees 2 Entry for Talkiatry - hgossett@summithealthcare.com
SELECT 1;
-- 1 record 

-- BEGIN TRAN

INSERT INTO [dbo].[SimpleFileMover8_Config_Emailees]
           ([SimpleFileMover8_ConfigPk]
           ,[EmailAddress]
           ,[CreatedBy]
           ,[CreatedTimestamp]
           ,[UpdatedBy]
           ,[UpdatedTimestamp])
     VALUES
           (@SimpleFileMover8_ConfigID
           ,'hgossett@summithealthcare.com'
           ,'pwmorrison'
           ,GETDATE()
           ,'pwmorrison'
           ,GETDATE());           
-- COMMIT TRAN
-- ROLLBACK TRAN
