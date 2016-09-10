CREATE PROCEDURE dbo.A_SP_ACTUAL_PART_GET_ROOT_PARENT_LOCATION 
@parentLocation varchar(50) OUTPUT,
@ID varchar(50)
AS
print 'Finding the root parent of actual part with object ID = ' + @ID
declare @parentPart varchar(50),@pRunner varchar(50)
set @pRunner = @ID
SELECT @parentPart = PARENT_ID FROM A_V_ACTUAL_PARTS_APPROVED_DATA WHERE ID = @ID
while @parentPart is not null
	begin
	set @pRunner = @parentPart
	set @parentPart = null
	SELECT @parentPart = PARENT_ID FROM A_V_ACTUAL_PARTS_APPROVED_DATA WHERE ID = @pRunner
	end
SELECT @parentLocation = LOCATION FROM A_V_ACTUAL_PARTS_APPROVED_DATA WHERE ID = @pRunner
print 'The root parent = ' + @pRunner + ' The parent Location = ' + @parentLocation




