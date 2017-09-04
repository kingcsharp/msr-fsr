CREATE PROCEDURE [dbo].[Portal_SpFilesShowForObject]
@objID nvarchar(50),
@type char(20),
@strNTLogin nvarchar(50)
as
if @type is null
	begin
		SELECT NAME AS SHOW,LINKED_DOC_ID AS VALUE,SERVER_PATH as ServerPath FROM A_V_DOCUMENTS_WITH_LINKED_ITEM WHERE OBJECT_ID = @objID and TYPE is NULL
	end
else
	begin
		SELECT NAME AS SHOW,LINKED_DOC_ID AS VALUE,SERVER_PATH as ServerPath FROM A_V_DOCUMENTS_WITH_LINKED_ITEM WHERE OBJECT_ID = @objID and TYPE = @type
	end