CREATE PROCEDURE dbo.A_SP_FILES_ADD_PDF_FOR_TASK_FILES 
@strID varchar(200)
AS
declare @NAME varchar(500),@newID varchar(50),@msg varchar(2000),@PATH varchar(500)
set @NAME = 'PDF of files for task ' + @strID
set @PATH = 'PDFS/' + @strID + '.pdf'
DELETE FROM A_DOCUMENTS WHERE SERVER_PATH = 'PDFS/' + @strID + '.pdf'
exec A_SP_FILE_SAVE_UPLOAD_FILE
	@newID OUTPUT,
	@msg OUTPUT,
	null, --docID
	null, --@oldDocID varchar(50),
	@NAME, --@NAME nvarchar(4000),
	null, --@Desc nvarchar(4000),
	@PATH, --@path varchar(4000),	
	'application/pdf', --@contentType nvarchar(200),
	null, --@srcID nvarchar(50),
	null, --@srcNAME nvarchar(4000),
	null, --@srcDesc nvarchar(4000),
	null, --@srcPath nvarchar(4000),	
	null, --@srcContentType nvarchar(200),
	null, --@srcChanged varchar(50),
	null, --@docChanged varchar(50),
	null, --@dropSRC varchar(50),
	'7' --@strNTLogin varchar(50)
print 'got the file with id = ' + @newID
INSERT INTO A_TASK_REF_FILES (ID,FILE_ID,TASK_ID,DRCM,MODBY)
VALUES (newID(),@newID,@strID,getDate(),'7')
print 'Attached it'
