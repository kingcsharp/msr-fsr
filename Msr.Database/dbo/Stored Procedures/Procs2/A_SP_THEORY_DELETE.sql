








CREATE        PROCEDURE A_SP_THEORY_DELETE
@strID nvarchar(50),
@strNTLogin nvarchar(50)
AS
print 'Deleting a theory'

begin transaction
print 'delete the theory allowed roles'
DELETE FROM A_THEORY_ROLES_ALLOWED WHERE THEORY_ID = @strID
if @@error <> 0 goto Problem
print 'delete the paragraphs'
DELETE FROM A_THEORY_PARAGRAPHS WHERE THEORY_ID = @strID
if @@error <> 0 goto Problem
print 'Delete any place we are referred to as copied'
UPDATE A_THEORY_PARAGRAPHS SET OLD_THEORY_ID = NULL WHERE OLD_THEORY_ID = @strID
if @@error <> 0 goto Problem
print 'Delete The theory Now'
DELETE FROM A_THEORY_HISTORY WHERE ID = @strID
if @@error <> 0 goto Problem
print 'Finished Deleting the theory stuff'

fin:
commit transaction
return 0

Problem:
rollback transaction
return 1










