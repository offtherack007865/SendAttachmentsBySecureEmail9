-- SQL Server Instance:  smg-sql01
IF (@@SERVERNAME <> 'smg-sql01')
BEGIN
PRINT 'Invalid SQL Server Connection'
RETURN
END

USE [Utilities];
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('opsemail.qy_GetSendAttachmentsBySecureEmailConfig'))
   DROP PROC [opsemail].[qy_GetSendAttachmentsBySecureEmailConfig]
GO
CREATE PROCEDURE [opsemail].[qy_GetSendAttachmentsBySecureEmailConfig]
/* -----------------------------------------------------------------------------------------------------------
   Procedure Name  :  qy_GetSendAttachmentsBySecureEmailConfig
   Business Analyis:
   Project/Process :   
   Description     :  Get configuration settings for the 
                      'Send Attachments By Secure Email' application.
	  
   Author          :  Philip Morrison 
   Create Date     :  9/11/2025

   ***********************************************************************************************************
   **         Change History                                                                                **
   ***********************************************************************************************************

   Date       Version    Author             Description
   --------   --------   -----------        ------------
   9/11/2025   1.01.001   Philip Morrison    Created

*/ -----------------------------------------------------------------------------------------------------------                                   

AS
BEGIN

  -- KeyValueTable
  DECLARE @KeyValueTable Table
  (
    [Name] [nvarchar] (1000)
	,[Value] [nvarchar] (1000)
  );
  
  DECLARE @FromEmailAddress [nvarchar] (1000) = ''
  
-- Template Declarations
DECLARE @Application            varchar(128) = 'OpsEmail'
DECLARE @Version                varchar(25)  = '1.00.001'

DECLARE @ProcessID              int          = 0
DECLARE @Process                varchar(128) = 'SendAttachmentsBySecureEmail'

DECLARE @BatchOutID             int
DECLARE @BatchDescription       varchar(1000) = @@ServerName + '  - ' + @Version
DECLARE @BatchDetailDescription varchar(1000)
DECLARE @BatchMessage           varchar(MAX)
DECLARE @User                   varchar(128) = SUSER_NAME()

DECLARE @AnticipatedRecordCount int 
DECLARE @ActualRecordCount      int

SET NOCOUNT ON

BEGIN TRY

--  Initialize Batch
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  NULL, 'BatchStart', @BatchDescription, @ProcessID, @Process
----------------------------------------------------------------------------------------------------------------------------------------------------

    SET @BatchDetailDescription = '010/030:  Populate KeyValueTable with call to [administration].[qy_GetApplicationSettings]'
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailStart', @BatchDetailDescription
	
	  SELECT @AnticipatedRecordCount = COUNT(*)
	                                   FROM @KeyValueTable;
	  
      -- Populate KeyValueTable with call to [administration].[qy_GetApplicationSettings]
      INSERT INTO @KeyValueTable
      (
        [Name]
	    ,[Value]
      )
      EXEC [Admin].[Utilities].[administration].[qy_GetApplicationSettings] 'OpsEmail', 'Default', 'SendAttachmentsBySecureEmail', NULL, 'AppUser';
	
    SET @ActualRecordCount = @@ROWCOUNT
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailEnd', NULL, NULL, NULL, @AnticipatedRecordCount, @ActualRecordCount

----------------------------------------------------------------------------------------------------------------------------------------------------
/*	
    [FromEmailAddress]
*/
    SET @BatchDetailDescription = '020/030:  Populate @FromEmailAddress'
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailStart', @BatchDetailDescription
	
	  SELECT @AnticipatedRecordCount = COUNT(*)
      FROM @KeyValueTable
      WHERE [Name] = 'FromEmailAddress';	
	  
      -- Populate @FromEmailAddress
      SELECT @FromEmailAddress = [Value]
      FROM @KeyValueTable
      WHERE [Name] = 'FromEmailAddress';	
	
    SET @ActualRecordCount = @@ROWCOUNT
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailEnd', NULL, NULL, NULL, @AnticipatedRecordCount, @ActualRecordCount

----------------------------------------------------------------------------------------------------------------------------------------------------
/*	
  DECLARE @FromEmailAddress
*/
    SET @BatchDetailDescription = '030/030:  Output Settings'
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailStart', @BatchDetailDescription
	
	  SET @AnticipatedRecordCount = 1;
	  
      -- Output 
      SELECT 
        @FromEmailAddress AS [FromEmailAddress];
	
    SET @ActualRecordCount = @@ROWCOUNT
    EXEC Admin.Utilities.logs.di_Batch @BatchOutID OUTPUT,  @BatchOutID, 'DetailEnd', NULL, NULL, NULL, @AnticipatedRecordCount, @ActualRecordCount
----------------------------------------------------------------------------------------------------------------------------------------------------
--  Close batch
    EXEC Admin.Utilities.logs.di_batch @BatchOutID OUTPUT, @BatchOutID, 'BatchEnd'

END TRY


BEGIN CATCH
DECLARE @Err              int
     ,  @ErrorMessage     varchar(Max)
     ,  @ErrorLine        varchar(128)
     ,  @Workstation      varchar(128) = @Application
     ,  @Procedure        VARCHAR(500)

    IF ERROR_NUMBER() IS NULL 
      SET @Err =0;
    ELSE
      SET @Err = ERROR_NUMBER();

    SET @ErrorMessage = ERROR_MESSAGE()
    SET @ErrorLine    = 'SP Line Number: ' + CAST(ERROR_LINE() as varchar(10)) 
    
	SET @Workstation  = HOST_NAME()
	
    SET @Procedure    = @@SERVERNAME + '.' + DB_NAME() + '.' + OBJECT_SCHEMA_NAME(@@ProcID) + '.' + OBJECT_NAME(@@ProcID) + ' - ' + @ErrorLine + ' - ' + LEFT(@BatchDetailDescription, 7)
    EXEC Admin.Utilities.administration.di_ErrorLog  @Application ,@Process, @Version ,0, @ErrorMessage, @Procedure,  @User , @Workstation

    SET @BatchMessage = 'Process Failed:  ' +  @ErrorMessage
    EXEC Admin.Utilities.logs.di_batch @BatchOutID OUTPUT, @BatchOutID, 'BatchEnd', @BatchMessage
	
    RAISERROR(@ErrorMessage, 16,1)

END CATCH


END