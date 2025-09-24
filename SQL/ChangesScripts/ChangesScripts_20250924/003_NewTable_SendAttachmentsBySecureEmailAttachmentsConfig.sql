-- SQL Server Instance: smg-sql01
IF (@@SERVERNAME <> 'smg-sql01')
BEGIN
PRINT 'Invalid SQL Server Connection'
RETURN
END

USE [EmailWebApi7];

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SendAttachmentsBySecureEmailAttachmentsConfig' AND TABLE_SCHEMA = 'dbo')
   DROP TABLE [dbo].[SendAttachmentsBySecureEmailAttachmentsConfig];
GO
/* -----------------------------------------------------------------------------------------------------------
   Table Name  :  SendAttachmentsBySecureEmailAttachmentsConfig
   Business Analyis:
   Project/Process :   
   Description     :  The Config for the handling of attachments for different systems.
                        
   Author          :   Philip Morrison
   Create Date     :   9/17/2025 

   ***********************************************************************************************************
   **         Change History                                                                                **
   ***********************************************************************************************************

   Date       Version    Author             Description
   --------   --------   -----------        ------------
   9/17/2025   1.01.001   Philip Morrison    Created
*/ -----------------------------------------------------------------------------------------------------------                                   


CREATE TABLE [dbo].[SendAttachmentsBySecureEmailAttachmentsConfig](
	[SendAttachmentsBySecureEmailAttachmentsConfigID] [int] IDENTITY(1,1) NOT NULL
    ,[Enabled] [bit] NOT NULL
    ,[SystemName] [nvarchar] (255) NOT NULL
	,[AttachmentReadFolder] [nvarchar] (500) NOT NULL  
    ,[AttachmentInputArchiveFolder] [nvarchar] (500) NOT NULL  
	,[EmailToAddresses] [nvarchar] (500) NOT NULL  
    ,[EmailSubject] [nvarchar] (255) NOT NULL
    ,[EmailFormatPlainTextOrHtml] [nvarchar] (30) NOT NULL
    ,[EmailBodyStart] [nvarchar] (255) NOT NULL 
    ,[EmailBodyEnd] [nvarchar] (255) NOT NULL 
CONSTRAINT [pk_dboSendAttachmentsBySecureEmailAttachmentsConfig] PRIMARY KEY CLUSTERED 
(
	[SendAttachmentsBySecureEmailAttachmentsConfigID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

-- Insert Entry for Talkiatry
SELECT 1;
-- 1 record 

-- BEGIN TRAN
INSERT INTO [dbo].[SendAttachmentsBySecureEmailAttachmentsConfig]
           ([Enabled]
           ,[SystemName]
           ,[AttachmentReadFolder]
           ,[AttachmentInputArchiveFolder]
           ,[EmailToAddresses]
           ,[EmailSubject]
           ,[EmailFormatPlainTextOrHtml]
           ,[EmailBodyStart]
           ,[EmailBodyEnd])
     VALUES
           (1
           ,'Talkiatry'
           ,'\\ps-nas\backuptodisk\development\SendAttachmentsBySecureEmail_Attachments\Talkiatry'
           ,'\\ps-nas\backuptodisk\development\SendAttachmentsBySecureEmail_Attachments\Talkiatry\InputArchive'
           ,'pwmorrison@summithealthcare.com'
           ,'Summit Healthcare Talkiatry document(s)'
           ,'Html'
           ,'Please review the following Summit Healthcare Talkiatry document(s) attached:'
           ,'Thanks.<br>Philip Morrison<br>Summit Healthcare<br>IT BI Development');

-- COMMIT TRAN
-- ROLLBACK TRAN



