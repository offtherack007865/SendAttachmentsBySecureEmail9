-- SQL Server Instance:  smg-sql01
IF (@@SERVERNAME <> 'smg-sql01')
BEGIN
PRINT 'Invalid SQL Server Connection'
RETURN
END

USE [EmailWebApi7];

-- Step 001 of 001
-- Insert Entry for Talkiatry
SELECT COUNT(*)
FROM [dbo].[SendAttachmentsBySecureEmailAttachmentsConfig]
WHERE [SendAttachmentsBySecureEmailAttachmentsConfigID] = 1;
-- 1 record 

-- BEGIN TRAN
UPDATE [dbo].[SendAttachmentsBySecureEmailAttachmentsConfig]
SET [EmailToAddresses] = 'Rachel.Florian@talkiatry.com;rhett.vahos@talkiatry.com;referralops@talkiatry.com;maggie.schell@talkiatry.com;jadunn@summithealthcare.com;pwmorrison@summithealthcare.com;hgossett@summithealthcare'
WHERE [SendAttachmentsBySecureEmailAttachmentsConfigID] = 1;

-- COMMIT TRAN
-- ROLLBACK TRAN


