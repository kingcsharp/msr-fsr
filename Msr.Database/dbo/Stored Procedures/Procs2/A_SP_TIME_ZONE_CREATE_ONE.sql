CREATE PROCEDURE DBO.A_SP_TIME_ZONE_CREATE_ONE
-- 'Darwin','+9.5',@cnt
@name varchar(50),
@gdiff real,
@num int,
@ds smallInt
AS
print 'First we need to check to make sure this time zone is not already here'
declare @myID as varchar(50)
SELECT @myID = ID FROM A_TIME_ZONES WHERE DESCRIPTION = @name
if @myID is null
	begin
	print 'This time zone does not exist so insert it now'
	exec sp_GetUniqueID3 @myID OUTPUT
	INSERT INTO A_TIME_ZONES (ID,DESCRIPTION,G_DIFF,DRCM,MODBY,NUM,DS)
		VALUES (@myID,@name,@gDiff,getDate(),'System',@num,@ds)
	end
else
	begin
	print 'This zone already exists so I need to update the G_DIFF and NUM'
	UPDATE A_TIME_ZONES SET G_DIFF = @gdiff, num = @num, DS = @ds WHERE ID = @myID
	end
print 'Finished updating the Time zone'

