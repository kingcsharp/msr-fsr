CREATE PROCEDURE [dbo].[Portal_Save_FileUpload]
	-- Add the parameters for the stored procedure here
	@newID varchar(50) OUTPUT,
	@msg nvarchar(1000) OUTPUT,
	@docID varchar(50),
	@oldDocID varchar(50),
	@NAME nvarchar(4000),
	@Desc nvarchar(4000),
	@path varchar(4000),	
	@contentType nvarchar(200),
	@srcID nvarchar(50),
	@srcNAME nvarchar(4000),
	@srcDesc nvarchar(4000),
	@srcPath nvarchar(4000),	
	@srcContentType nvarchar(200),
	@srcChanged varchar(50),
	@docChanged varchar(50),
	@dropSRC varchar(50),
	@strNTLogin varchar(50),
	@taskId varchar(50)

AS
BEGIN
	
	SET NOCOUNT ON;
    
	EXEC [dbo].[A_SP_FILE_SAVE_UPLOAD_FILE] @newID output,@msg output,@docID,@oldDocID,@NAME,@Desc,@path,@contentType,@srcID,@srcNAME,@srcDesc,@srcPath,@srcContentType,@srcChanged,@docChanged,@dropSRC,@strNTLogin
		
	INSERT INTO A_TASK_REF_FILES (ID,TASK_ID,FILE_ID,STATUS,DRCM,MODBY) 
	VALUES (newID(),@taskId, @newID,'ACTIVE',getDate(), @strNTLogin)
	
	select @newID
END