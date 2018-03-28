CREATE              PROCEDURE A_SP_FILE_SAVE_UPLOAD_FILE
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
	@FileUrl nvarchar(4000) = null,
	@FileKey nvarchar(4000) = null
as
if @oldDocID is null
	begin
	if @srcPath is not NULL
		begin
		exec sp_GetUniqueID3 @srcID OUTPUT
		INSERT INTO A_DOCUMENTS
		(ID, DOC_ID, NAME,DRCM,MODBY,SERVER_PATH,DESCRIPTION,CONTENTTYPE)
		values
		(@srcID,@srcID,@srcNAME,getDate(),@strNTLogin,
		@srcPath,@srcDesc,@srcContentType)
		end
	exec sp_GetUniqueID3 @docID OUTPUT
	INSERT INTO A_DOCUMENTS (ID, DOC_ID, NAME,SOURCE_ID,DRCM,MODBY,SERVER_PATH,DESCRIPTION,CONTENTTYPE,FileUrl,FileKey)
	values(@docID,@docID,@NAME,@srcID,getDate(),@strNTLogin,@path,@Desc,@contentType,@FileUrl,@FileKey)
	set @newID = @docID
	end
else
	begin
	print 'We Are Editing a file'
	print 'First we will check if the source should be dropped and drop it if it should be'
	if isNull(@dropSrc,'') = '1'
		begin
		UPDATE A_DOCUMENTS SET SOURCE_ID = NULL WHERE ID = @oldDocID
		end
	print 'Next if we are not dropping the source and we changed the source we will add the source and then link the old document to it'
	if isNull(@dropSrc,'') != '1' and isNull(@srcChanged,'') = 'YES'
		begin
		print 'We are uploading a new src'
		exec sp_GetUniqueID3 @srcID OUTPUT
		INSERT INTO A_DOCUMENTS
		(ID, DOC_ID, NAME,DRCM,MODBY,SERVER_PATH,DESCRIPTION,CONTENTTYPE)
		values
		(@srcID,@srcID,@srcNAME,getDate(),@strNTLogin,
		@srcPath,@srcDesc,@srcContentType)
		print 'We inserted a new src = ' + isNull(@srcID,'NULL')
		UPDATE A_DOCUMENTS SET SOURCE_ID = @srcID WHERE ID = @oldDocID
		end
	print 'Next we will check to see if we changed the original document and if we did we will make this document point at it'
	if isNull(@docChanged,'') = 'YES'
		begin
		UPDATE A_DOCUMENTS SET
		SERVER_PATH = @path,
		DESCRIPTION = @Desc,
		CONTENTTYPE = @contentType
		WHERE ID = @oldDocID
		end
	print 'Finally we will update all the keywords for the main doc'
	UPDATE A_DOCUMENTS SET DESCRIPTION = @Desc WHERE ID = @oldDocID
	SELECT @srcID = SOURCE_ID FROM A_DOCUMENTS WHERE ID = @oldDocID
	UPDATE A_DOCUMENTS SET DESCRIPTION = @srcDesc WHERE ID = @srcID
	set @newID = @oldDocID
	end




