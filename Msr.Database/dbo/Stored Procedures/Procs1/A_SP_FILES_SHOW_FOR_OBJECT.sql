






CREATE    PROCEDURE A_SP_FILES_SHOW_FOR_OBJECT 
@objID nvarchar(50),
@type char(20),
@strNTLogin nvarchar(50)
as
if @type is null
	begin
		SELECT NAME AS SHOW,LINKED_DOC_ID AS VALUE FROM A_V_DOCUMENTS_WITH_LINKED_ITEM WHERE OBJECT_ID = @objID and TYPE is null
	end
else
	begin
		SELECT NAME AS SHOW,LINKED_DOC_ID AS VALUE FROM A_V_DOCUMENTS_WITH_LINKED_ITEM WHERE OBJECT_ID = @objID and TYPE = @type
	end













