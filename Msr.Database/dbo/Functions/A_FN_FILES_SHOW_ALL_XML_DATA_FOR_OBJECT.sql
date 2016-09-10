
CREATE FUNCTION dbo.A_FN_FILES_SHOW_ALL_XML_DATA_FOR_OBJECT (@objID varchar(50),@type as varchar(50))
RETURNS nvarchar(4000)
as
BEGIN
	declare @so as nvarchar(4000)
	Declare @ID as varchar(50),@NAME nvarchar(50)
	Declare @cur Cursor
	if @type is null
		begin
		set @cur = Cursor For SELECT LINKED_DOC_ID,SERVER_PATH FROM A_V_DOCUMENTS_WITH_LINKED_ITEM WHERE OBJECT_ID = @objID and TYPE is null
		end
	else
		begin
		set @cur = Cursor For SELECT LINKED_DOC_ID,SERVER_PATH FROM A_V_DOCUMENTS_WITH_LINKED_ITEM WHERE OBJECT_ID = @objID and TYPE = @type
		end
	open @cur
	Fetch Next from @cur Into @ID,@NAME
	while (@@fetch_status = 0)
		Begin
		set @so = isNull(@so,'') + '<r><i>' + @ID + '</i><n>' + @NAME + '</n></r>'
		Fetch Next from @cur Into @ID,@NAME
		End
	close @cur
	Deallocate @cur
	return(@so)
END
