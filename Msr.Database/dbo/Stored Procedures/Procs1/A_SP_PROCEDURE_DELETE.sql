






CREATE       PROCEDURE A_SP_PROCEDURE_DELETE
@strID nvarchar(50),
@strNTLogin nvarchar(50)
AS
print 'Deleting a procedure'
begin transaction
print 'Delete all the preceding step data'
DELETE FROM A_PROCEDURE_STEP_PRECEDING_STEPS WHERE PROCEDURE_ID = @strID
if @@error <> 0 goto Problem
print 'Delete all the preceding step data using in'
DELETE FROM A_PROCEDURE_STEP_PRECEDING_STEPS 
	WHERE 
		MY_STEP in (SELECT ID FROM A_PROCEDURE_STEPS WHERE PROCEDURE_ID = @strID) OR
		PREV_STEP in (SELECT ID FROM A_PROCEDURE_STEPS WHERE PROCEDURE_ID = @strID)

if @@error <> 0 goto Problem
print 'Delete all the preceding step data'
DELETE FROM A_PROCEDURE_STEP_PRECEDING_STEPS WHERE PROCEDURE_ID = @strID
if @@error <> 0 goto Problem

print 'delete the procedure object links'
DELETE FROM A_PROCEDURE_OBJECT_LINK WHERE PROCEDURE_ID = @strID
if @@error <> 0 goto Problem
print 'delete the steps'
DELETE FROM A_PROCEDURE_STEPS WHERE PROCEDURE_ID = @strID
if @@error <> 0 goto Problem
print 'make any copies of this one not followed'
UPDATE A_PROCEDURE_STEP_PRECEDING_STEPS SET OLD_PROCEDURE_ID = NULL 
WHERE OLD_PROCEDURE_ID = @strID
if @@error <> 0 goto Problem
print 'Delete any place we are referred to as copied'
UPDATE A_PROCEDURE_STEPS SET OLD_PROCEDURE_ID = NULL WHERE OLD_PROCEDURE_ID = @strID
if @@error <> 0 goto Problem
print 'DElete anything in monitors that might relate to this'
DELETE FROM A_MONITOR_TEMPLATES WHERE PROCEDURE_ID = @strID
if @@error <> 0 goto Problem
print 'Delete The Procedure Now'
DELETE FROM A_PROCEDURES_HISTORY WHERE ID = @strID
if @@error <> 0 goto Problem
print 'Finished Deleting the procedure stuff'

fin:
commit transaction
return 0

Problem:
rollback transaction
return 1








