







CREATE      PROCEDURE A_SP_FILES_CREATE_LINK 
@objID nvarchar(50),
@docID nvarchar(50),
@type char(20),
@strNTLogin nvarchar(50)
as
print 'Creating a File Link between Object = ' + isNull(@objID,'NULL')
print 'The Document ID is ' + isNull(@docID,'NULL')
print 'The @type is ' + isNull(@type,'NULL')
print 'The @strNTLogin is ' + isNull(@strNTLogin,'NULL')

if @docID is not null
	INSERT INTO A_DOCUMENT_LINK(ID,LINKED_DOC_ID, OBJECT_ID,MODBY, DRCM, TYPE)
	VALUES (newID(),@docID,@objID, @strNTLogin, getDate(),@type)




