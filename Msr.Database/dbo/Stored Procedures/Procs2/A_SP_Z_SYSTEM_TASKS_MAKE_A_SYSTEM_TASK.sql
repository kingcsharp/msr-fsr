





CREATE      PROCEDURE A_SP_Z_SYSTEM_TASKS_MAKE_A_SYSTEM_TASK
@id varchar(50),
@name nvarchar(50),
@strNTLogin varchar(50)
as
declare @myCo varchar(50)
SELECT @myCO = ROOT_COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @strNTLogin
if EXISTS(SELECT * FROM A_O_PROCEDURES WHERE CREATING_CO = @myCo AND SYSTEM_ID = @id) goto fin
print 'Inserting into the proc hist table'
declare @newID as varchar(50)
exec sp_GetUniqueID3 @newID OUTPUT
INSERT INTO A_PROCEDURES_HISTORY (ID,SYSTEM_ID,NAME,SECURITY_LEVEL,DRCM,MODBY,IS_SYSTEM)
VALUES (@newID,@id,@name,'5',getDate(),@strNTLogin,1)
declare @eacObj as varchar(50)
SELECT @eacObj = OBJECT_ID FROM A_PROCEDURES_HISTORY WHERE ID = @newID
print 'Object ID  = ' + isNull(@eacObj,'NULL')
print 'Setting status to approved and locked by to null'
UPDATE A_OBJECTS SET STATUS = 'APPROVED', LOCKED_BY = NULL WHERE ID = @eacObj
print 'Finishing the wf now'
exec A_SP_PROCEDURES_FINISH_WF @newid,@eacObj,@strNTLogin

fin:


