






CREATE     PROCEDURE A_SP_FILES_SHOW_ALL_DATA_FOR_OBJECT 
@objID nvarchar(50),
@type char(20),
@strNTLogin nvarchar(50)
as
if @type is null
	begin
		SELECT * FROM A_V_DOCUMENTS_WITH_LINKED_ITEM WHERE OBJECT_ID = @objID and TYPE is null
	end
else
	begin
		SELECT * FROM A_V_DOCUMENTS_WITH_LINKED_ITEM WHERE OBJECT_ID = @objID and TYPE = @type
	end













