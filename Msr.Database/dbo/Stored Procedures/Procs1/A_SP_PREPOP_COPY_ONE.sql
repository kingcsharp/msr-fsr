








CREATE      procedure A_SP_PREPOP_COPY_ONE
	@newObjID nvarchar(50) OUTPUT,
	@strID nvarchar(50),
	@strNTLogin nvarchar(50),
	@copyPrefix nvarchar(50)
as
--Make a new ID for the copy
declare @newID as nvarchar(50)
exec sp_getUniqueID3 @newID OUTPUT
--Insert the new one
--Insert the new one
INSERT INTO A_PREPOP_HISTORY (
			ID,
			PROC_STEP_ID ,
			DRCM, 
			MODBY)
SELECT @newID as ID,
		PROC_STEP_ID ,
		getDate(), 
		@strNTLogin FROM A_PREPOP_HISTORY WHERE ID = @strID
exec A_SP_PREPOP_STEP_COPY @strID,@newID,@strNTLogin
--find out what object id the new one got
SELECT @newObjID = OBJECT_ID FROM A_PREPOP_HISTORY WHERE ID = @newID
print 'The new object ID is ' + @newObjID









