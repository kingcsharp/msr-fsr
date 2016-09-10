





CREATE     PROCEDURE A_SP_OBJECT_MAKE_CREATING
@newObjID nvarchar(50),
@strNTLogin nvarchar(50)
as
print 'Making object ' + @newObjID + ' Creating '
--Now update the new one to have the same root and highest revision
UPDATE A_OBJECTS SET STATUS = 'CREATING', LOCKED_BY = @strNTLogin, 
LOCKED_BY_NAME = (SELECT NAME FROM A_APPROVED_PEOPLE WHERE ID = @strNTLogin) 
WHERE ID = @newObjID

UPDATE A_OBJECTS SET LOCKED_BY = @strNTLogin, 
LOCKED_BY_NAME = (SELECT NAME FROM A_APPROVED_PEOPLE WHERE ID = @strNTLogin) 
WHERE ROOT = (SELECT ROOT FROM A_OBJECTS WHERE ID = @newObjID)









