







CREATE          PROCEDURE A_SP_THEORY_UPDATE_ONE_THEORY
@newObjID varchar(50) OUTPUT,
@messages varchar(500) OUTPUT,
@objID as varchar(50),
@ID as varchar(50),
@COMPANY varchar(50),
@NAME nvarchar(4000),
@COMMENTS nvarchar(4000),
@SECURITY_LEVEL varchar(50),
@ROLES_TO_VIEW varchar(8000),
@REFERENCE_OBJECTS varchar(8000),
@REFERENCE_THEORY varchar(8000),
@strNTLogin varchar(50)


AS

if (@objID is null)
	begin
		print 'The object ID is null'
		exec sp_getUniqueID3 @ID OUTPUT
		INSERT INTO A_THEORY_HISTORY (ID,NAME,DRCM,MODBY,SECURITY_LEVEL)
		VALUES(@ID,@NAME,getDate(),@strNTLogin,@SECURITY_LEVEL)
		SELECT @newObjID = OBJECT_ID FROM A_THEORY_HISTORY WHERE ID = @ID
	end
else
	begin
		set @newObjID = @objID
	end

print 'Updating the theory whose objID = ' + @objID
UPDATE A_THEORY_HISTORY SET
	NAME = @NAME,
	COMMENTS = @COMMENTS,
	SECURITY_LEVEL = @SECURITY_LEVEL,
	DRCM = getDate(), MODBY = @strNTLogin
	WHERE OBJECT_ID = @newObjID

print 'Delete all roles allowed to view this theory'
DELETE FROM A_THEORY_ROLES_ALLOWED WHERE THEORY_ID = @ID
print 'Now add the new roles allowed to view'
CREATE TABLE #TempItems (IT varchar(50))
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @ROLES_TO_VIEW,','
INSERT INTO A_THEORY_ROLES_ALLOWED (ID,THEORY_ID,ROLE_ID,DRCM,MODBY)
		SELECT newID(),@ID,ltrim(IT),getDate(),@strNTLogin FROM #TempItems

print 'Delete this theorys reference objects'
DELETE FROM A_THEORY_REFERENCE_OBJECTS WHERE THEORY_ID = @ID
DELETE FROM #TempItems
print 'Now add the new objects'
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @REFERENCE_OBJECTS,','
INSERT INTO A_THEORY_REFERENCE_OBJECTS (ID,THEORY_ID,OBJ_ID,DRCM,MODBY)
		SELECT newID(),@ID,ltrim(IT),getDate(),@strNTLogin FROM #TempItems

print 'Delete this theorys reference theorys'
DELETE FROM A_THEORY_REFERENCE_THEORY WHERE THEORY_ID = @ID
DELETE FROM #TempItems
print 'Now add the new theorys'
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @REFERENCE_THEORY,','
INSERT INTO A_THEORY_REFERENCE_THEORY (ID,THEORY_ID,THEORY_LINK,DRCM,MODBY)
		SELECT newID(),@ID,ltrim(IT),getDate(),@strNTLogin FROM #TempItems
DELETE FROM #TempItems



