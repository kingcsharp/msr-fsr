




CREATE   PROCEDURE A_SP_OBJECT_FINISH_CHECKOUT
@oldObjID nvarchar(50),
@newObjID nvarchar(50),
@strNTLogin nvarchar(50)
as
--get the root
declare @root as nvarchar(50)
select @root = root from A_OBJECTS WHERE ID = @oldObjID
print 'the root is ' + @root
--get the highest revision for this rooot
declare @hiRev as numeric
select @hiRev = max(REV)+1 FROM A_OBJECTS WHERE ROOT = @root
--Now update the new one to have the same root and highest revision
UPDATE A_OBJECTS SET ROOT = @root, REV = @hiRev WHERE ID = @newObjID
--Now make the new one creating
EXEC A_SP_OBJECT_MAKE_CREATING @newObjID,@strNTLogin
--Now make the old one being revised
print 'Making the old one revising' + @oldObjID
UPDATE A_OBJECTS SET 
STATUS = 'APPROVED_BUT_REVISING',
MODBY = @strNTLogin, 
DRCM = getDate() 
WHERE ID = @oldObjID









