







CREATE     procedure A_SP_TT_VERBS_COPY_ONE
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
INSERT INTO A_TT_VERBS_HISTORY (
			ID,
			NAME ,
			VERB_TYPE ,
			DRCM, 
			MODBY)
SELECT @newID as ID,
		@copyPrefix + NAME ,
		VERB_TYPE ,
		getDate(), 
		@strNTLogin FROM A_TT_VERBS_HISTORY WHERE ID = @strID


--find out what object id the new one got
SELECT @newObjID = OBJECT_ID FROM A_TT_VERBS_HISTORY WHERE ID = @newID
print 'The new object ID is ' + @newObjID







