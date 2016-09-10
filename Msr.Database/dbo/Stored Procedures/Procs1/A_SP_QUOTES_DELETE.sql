







CREATE   PROCEDURE DBO.A_SP_QUOTES_DELETE
@id nvarchar(50),
@strNTLogin nvarchar(50)
as
begin transaction
print 'Setting item parents to null'
UPDATE A_QUOTE_ITEMS SET PARENT = NULL WHERE QUOTE_ID = @ID
if @@ERROR <> 0 goto problem
print 'Deleting Quote Items'
DELETE FROM A_QUOTE_ITEMS WHERE QUOTE_ID = @ID
if @@ERROR <> 0 goto problem
DELETE FROM A_QUOTES_HISTORY WHERE ID = @ID
if @@ERROR <> 0 goto problem



fin:
if @@trancount > 0 COMMIT TRANSACTION
print 'Finished A_SP_QUOTES_DELETE with no errors'
return 0

PROBLEM:
if @@trancount > 0 	ROLLBACK TRANSACTION
print ' There was a problem in A_SP_QUOTES_DELETE and we will terminate and not finish anything '
return 1

