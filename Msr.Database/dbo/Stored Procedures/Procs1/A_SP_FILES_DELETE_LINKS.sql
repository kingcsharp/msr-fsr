






CREATE    PROCEDURE A_SP_FILES_DELETE_LINKS 
@objID nvarchar(50),
@type char(20),
@strNTLogin nvarchar(50)
as
if @type is null
	DELETE FROM A_DOCUMENT_LINK WHERE OBJECT_ID = @objID AND TYPE is NULL
else
	DELETE FROM A_DOCUMENT_LINK WHERE OBJECT_ID = @objID AND TYPE = @type







