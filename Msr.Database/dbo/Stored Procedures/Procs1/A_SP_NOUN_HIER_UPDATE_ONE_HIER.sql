








CREATE       PROCEDURE A_SP_NOUN_HIER_UPDATE_ONE_HIER
@newObjID nvarchar(50) OUTPUT,
@messages nvarchar(500) OUTPUT,
@objID nvarchar(50),
@name nvarchar(50),
@rootChild nvarchar(50),
@strNTLogin nvarchar(50)
AS

if not(@objID is null)
begin
	print 'The objId is not null it = ' + @objID
	declare @tester as nvarchar(50)
	SELECT @tester = ID FROM A_O_NOUN_HIER_HISTORY WHERE OBJECT_ID = @objID 
	if @tester is null
		print 'Error cannot find the Prepop ' + @objID + ' to update IT '
	else
		begin
			print 'Updating the Hier whose objID = ' + @objID
			UPDATE A_NOUN_HIERARCHIES_HISTORY SET
			DRCM = getDate(),
			MODBY = @strNTLogin,
			NAME = @name
			WHERE OBJECT_ID = @objID
		end
	set @newObjID = @objID
end
else
	begin
		set @messages = @messages + 'The object ID is null'
		declare @newID as nvarchar(50)
		exec sp_getUniqueID3 @newID OUTPUT
		set @messages = @messages + 'The new ID is ' + isnull(@newID,'NULL??')
		INSERT INTO A_NOUN_HIERARCHIES_HISTORY (
			ID,
			NAME ,
			DRCM ,
			MODBY) VALUES(
			@newID ,
			@name,
			getDate(),
			@strNTLogin) 		declare @myObjID as nvarchar(50)
		SELECT @myObjId = OBJECT_ID FROM A_NOUN_HIERARCHIES_HISTORY WHERE ID = @newID
		set @messages = @messages + 'Making the objectID creating ' + @myObjID
		exec A_SP_OBJECT_MAKE_CREATING @myObjID,@strNTLogin
		set @messages = @messages + 'Done making it Creating ' + @strNTLogin
		select @newID as ID,@myObjID as OBJECT_ID
		set @newObjId = @myObjId
	end
declare @curHier as nvarchar(50)
select @curHier = ID FROM A_NOUN_HIERARCHIES_HISTORY WHERE OBJECT_ID = @newObjID

declare @curRootChild as nvarchar(50)
select @curRootChild = ROOT_CHILD FROM A_NOUN_HIERARCHIES_HISTORY WHERE ID = @curHier
--If this Hierarchy had no root child and we have a root Child coming in then we need to set it up
if ((@curRootChild is NULL) and not(@rootChild is null))
	begin
	print 'Adding a new root child since the old one is null and this one adds one.'
	exec sp_getUniqueID3 @curRootChild OUTPUT
	print 'Adding now'
	INSERT INTO A_NOUN_HIERARCHY_CHILDREN_EDITING 
		(ID,ROOT_ID,APPROVED_OBJ_ID,HIERARCHY_ID,DRCM,MODBY)
		VALUES (@curRootChild,@curRootChild,@rootChild,@curHier,getDate(),@strNTLogin)
	UPDATE A_NOUN_HIERARCHIES_HISTORY SET ROOT_CHILD = @curRootChild WHERE ID = @curHier
	end








