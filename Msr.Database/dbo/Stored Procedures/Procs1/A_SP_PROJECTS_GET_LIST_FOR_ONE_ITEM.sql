




CREATE PROCEDURE A_SP_PROJECTS_GET_LIST_FOR_ONE_ITEM
@ID varchar(50),
@strType varchar(50),
@strNTLogin varchar(50)
AS
print 'Getting Projects for item ' + @ID + ' with Type = ' + @strType
SELECT p.ID AS [ID],p.NAME AS NAME FROM A_PROJECTS p,A_PROJECT_ITEM_LINK l WHERE
p.ID = l.PROJECT_ID AND l.ITEM_ID = @ID AND l.ITEM_TYPE = @strType





