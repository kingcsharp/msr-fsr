

--Used in file asp\nounHierarchy\editHierChild.asp
CREATE  PROCEDURE A_SP_NOUN_HIER_UPDATE_ONE_HIER_CHILD
@newID nvarchar(50) OUTPUT,
@messages nvarchar(500) OUTPUT,
@ID nvarchar(50),
@PARENT_ID nvarchar(50),
@APPROVED_OBJECT_ID nvarchar(50),
@HIER_ID nvarchar(50),
@strNTLogin nvarchar(50)
AS

if not(@ID is null)
begin
	print 'The objId is not null it = ' + @ID
	declare @tester as nvarchar(50)
	SELECT @tester = ID FROM A_NOUN_HIERARCHY_CHILDREN_EDITING WHERE ID = @ID 
	if @tester is null
		print 'Error cannot find the Noun Hier Child ' + @ID + ' to update IT '
	else
		begin
			print 'Updating the Hier Child whose ID = ' + @ID
			UPDATE A_NOUN_HIERARCHY_CHILDREN_EDITING SET
			DRCM = getDate(),
			MODBY = @strNTLogin,
			APPROVED_OBJ_ID = @APPROVED_OBJECT_ID,
			PARENT_ID = @PARENT_ID,
			HIERARCHY_ID = @HIER_ID
			WHERE ID = @ID
		end
	set @newID = @ID
end
else
	begin
		print 'The ID is null'
		exec sp_getUniqueID3 @newID OUTPUT
		set @messages = @messages + 'The new ID is ' + isnull(@newID,'NULL??')
		INSERT INTO A_NOUN_HIERARCHY_CHILDREN_EDITING (
			ID,
			APPROVED_OBJ_ID ,
			PARENT_ID ,
			HIERARCHY_ID ,
			DRCM ,
			MODBY) VALUES(
			@newID ,
			@APPROVED_OBJECT_ID,
			@PARENT_ID,
			@HIER_ID,
			getDate(),
			@strNTLogin)
	end



