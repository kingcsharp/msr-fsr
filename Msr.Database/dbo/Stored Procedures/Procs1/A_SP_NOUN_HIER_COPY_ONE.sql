


CREATE  procedure A_SP_NOUN_HIER_COPY_ONE
	@newObjID nvarchar(50) OUTPUT,
	@strID nvarchar(50),
	@copyPrefix nvarchar(50),
	@strNTLogin nvarchar(50)
as
--Make a new ID for the copy
declare @newID as nvarchar(50)
exec sp_getUniqueID3 @newID OUTPUT
--Insert the new one
INSERT INTO A_NOUN_HIERARCHIES_HISTORY (ID,ROOT_CHILD,NAME,DRCM,MODBY)
SELECT @newID as ID,ROOT_CHILD,NAME,getDate(),@strNTLogin FROM A_NOUN_HIERARCHIES_HISTORY WHERE ID = @strID
--if it is a copy then change the name
if len(@copyPrefix) > 0
	begin
	UPDATE A_NOUN_HIERARCHIES_HISTORY SET
	NAME = @copyPrefix + NAME
	WHERE
	ID = @newID
	end
--find out what object id the new one got
print 'Copying the hierarchy'
SELECT @newObjID = OBJECT_ID FROM A_NOUN_HIERARCHIES_HISTORY WHERE ID = @newID
print 'The new object ID is ' + @newObjID
--now we need to copy all the children from the other one to this one
print 'Copying all the children'
INSERT INTO A_NOUN_HIERARCHY_CHILDREN_EDITING (ID,ROOT_ID,APPROVED_OBJ_ID,PARENT_ID,HIERARCHY_ID,DRCM,MODBY,BEEN_APPROVED,HIDDEN)
SELECT ID,ROOT_ID,APPROVED_OBJ_ID,PARENT_ID,@newID as HIERARCHY_ID,DRCM,MODBY,BEEN_APPROVED,HIDDEN FROM A_NOUN_HIERARCHY_CHILDREN_EDITING WHERE
HIERARCHY_ID = @strID
--if this was a copy we need to set all the branches to not have been approved
if len(@copyPrefix) > 0
	begin
	print 'Updating been approved to null'
	UPDATE A_NOUN_HIERARCHY_CHILDREN_EDITING SET BEEN_APPROVED = NULL WHERE HIERARCHY_ID = @newID
	end



