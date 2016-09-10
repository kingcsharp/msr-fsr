



CREATE    PROCEDURE A_SP_OBJECT_RELEASE_OLD_REVS
	@strID nvarchar(50),
	@strNTLogin nvarchar(50)
as
print 'Releasing old Revs which are approved but revising my ID = ' + @strID
declare @myRoot as nvarchar(50)
declare @myID varchar(50)
SELECT  @myRoot = ROOT FROM A_OBJECTS WHERE ID = @strID
SELECT @myID = ID FROM A_OBJECTS WHERE ID = @strID
print 'My ID = ' + isNULL(@myID,'NULL')
print 'My root = ' + isNull(@myRoot,'NULL')

UPDATE A_OBJECTS SET 
STATUS = 'APPROVED',
MODBY = @strNTLogin,
DRCM = getDate()
WHERE ROOT = @myRoot and STATUS = 'APPROVED_BUT_REVISING'

UPDATE A_OBJECTS SET LOCKED_BY = NULL,LOCKED_BY_NAME = NULL WHERE 
ROOT = @myRoot







