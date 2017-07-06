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
	@fillId varchar(50)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	EXEC [dbo].[A_SP_FILE_SAVE_UPLOAD_FILE] @newID output,@msg output,@docID,@oldDocID,@NAME,@Desc,@path,@contentType,@srcID,@srcNAME,@srcDesc,@srcPath,@srcContentType,@srcChanged,@docChanged,@dropSRC,@strNTLogin

	declare @ActualPartId varchar(50)

	select @ActualPartId= ActualPartId from [Portal_WorkOrders] where fillid = @fillId

	INSERT INTO A_ACTUAL_PARTS_RELATED_FILES (ID,ACTUAL_PART_ID,FILE_ID,STATUS,DRCM,MODBY) VALUES (newID(),@ActualPartId,@newID,'ACTIVE',getDate(),@strNTLogin)

	select @newID
END