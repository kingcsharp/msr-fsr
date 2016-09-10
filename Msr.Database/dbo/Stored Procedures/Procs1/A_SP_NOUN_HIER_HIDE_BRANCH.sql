



CREATE    PROCEDURE A_SP_NOUN_HIER_HIDE_BRANCH
	@retVal nvarchar(500) OUTPUT,
	@ID nvarchar(50),
	@HIER_ID nvarchar(50),
	@strNTLogin nvarchar(50)
AS
declare @editor as nvarchar(50)
--Check to see if I am the one who has this thing checked out
SELECT @editor = LOCKED_BY FROM A_O_NOUN_HIERARCHIES WHERE  ID = @HIER_ID
if (@editor != @strNTLogin)
	begin
		print 'This person is not the editor so we can not delete it'
		set @retVal = 'FALSE -- You are nto the editor'
		goto failure
	end

--Everything checks out to hide it so go ahead and hide it
UPDATE A_NOUN_HIERARCHY_CHILDREN_EDITING SET HIDDEN = 'TRUE' WHERE ID = @ID AND HIERARCHY_ID = @HIER_ID
set @retVal = 'TRUE'

goto success

failure:

success:








