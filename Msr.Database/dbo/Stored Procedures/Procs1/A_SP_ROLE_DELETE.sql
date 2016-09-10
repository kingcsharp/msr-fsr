






CREATE     PROCEDURE A_SP_ROLE_DELETE
@id nvarchar(50),
@strNTLogin nvarchar(50)
AS
print 'In Role Delete'
print 'Deleting Assignees'
DELETE FROM A_ROLE_ASSIGNEE WHERE ROLE = @ID
print 'Deleting me'
DELETE FROM A_ROLES_HISTORY WHERE ID = @id








