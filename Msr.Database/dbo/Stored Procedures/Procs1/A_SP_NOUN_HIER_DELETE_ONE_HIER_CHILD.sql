

CREATE  PROCEDURE A_SP_NOUN_HIER_DELETE_ONE_HIER_CHILD
	@retVal nvarchar(50) OUTPUT,
	@strID nvarchar(50),
	@strNTLogin nvarchar(50)
AS
declare @apStat as nvarchar(50)
declare @editor as nvarchar(50)
declare @hierID as nvarchar(50)
--Check to see if this item has already been approved
SELECT @apStat = BEEN_APPROVED, @hierID = HIERARCHY_ID FROM A_NOUN_HIERARCHY_CHILDREN_EDITING WHERE ID = @strID
if not(@apStat is null)
	begin
		print 'This has been approved so you can not delete it'
		set @retVal = 'FALSE -- It has been approved so can not delete'
		goto failure
	end
--Check to see if I am the one who has this thing checked out
SELECT @editor = LOCKED_BY FROM A_O_NOUN_HIERARCHIES WHERE  ID = @hierID
if (@editor != @strNTLogin)
	begin
		print 'This person is not the editor so we can not delete it'
		set @retVal = 'FALSE -- You are nto the editor'
		goto failure
	end

--Make sure it has no children
declare @oneChild as nvarchar(50)
SELECT @oneChild = ID FROM A_NOUN_HIERARCHY_CHILDREN_EDITING WHERE PARENT_ID = @strID
if not(@oneChild is null)
	begin
		print 'This one has Children so you can not delete it'
		set @retVal = 'FALSE -- Had Children Still'
		goto failure
	end


--Everything checks out to delete it so go ahead and delete it
DELETE FROM A_NOUN_HIERARCHY_CHILDREN_EDITING WHERE ID = @strID
set @retVal = 'TRUE'

goto success

failure:

success:






