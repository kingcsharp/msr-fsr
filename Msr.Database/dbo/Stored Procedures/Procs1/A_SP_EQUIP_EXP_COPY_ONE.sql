





CREATE       procedure A_SP_EQUIP_EXP_COPY_ONE
	@newObjID nvarchar(50) OUTPUT,
	@strID nvarchar(50),
	@strNTLogin nvarchar(50),
	@copyPrefix nvarchar(50)
as
--Make a new ID for the copy
declare @newID as nvarchar(50)
exec sp_getUniqueID3 @newID OUTPUT
--Insert the new one

INSERT INTO A_EQUIP_EXP_HISTORY 
([ID], [PERSON_ID], [PART_ID], [FIRST_EXPOSURE_DATE], [LAST_EXPOSURE_DATE], 
[HW_INSTALL_EXP_LEVEL], [PROCESS_SETUP_EXP_LEVEL], [OPERATION_EXP_LEVEL], [SM_EXP_LEVEL], 
[UM_EXP_LEVEL], [FORMALLY_TRAINED], [CERTIFIED], [COMMENTS], [DRCM], [MODBY])
SELECT 
@newID as ID, [PERSON_ID], [PART_ID], [FIRST_EXPOSURE_DATE], [LAST_EXPOSURE_DATE], 
[HW_INSTALL_EXP_LEVEL], [PROCESS_SETUP_EXP_LEVEL], [OPERATION_EXP_LEVEL], [SM_EXP_LEVEL], 
[UM_EXP_LEVEL], [FORMALLY_TRAINED], [CERTIFIED], [COMMENTS], getDate(), @strNTLogin
FROM A_EQUIP_EXP_HISTORY WHERE ID = @strID

--find out what object id the new one got
SELECT @newObjID = OBJECT_ID FROM A_EQUIP_EXP_HISTORY WHERE ID = @newID
print 'The new object ID is ' + @newObjID





