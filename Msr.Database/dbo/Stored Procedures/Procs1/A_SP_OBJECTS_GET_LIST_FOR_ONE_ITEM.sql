






CREATE   PROCEDURE A_SP_OBJECTS_GET_LIST_FOR_ONE_ITEM
@ID varchar(50),
@strType varchar(50),
@strNTLogin varchar(50)
AS
print 'Getting Objects for item ' + @ID + ' with Type = ' + @strType
SELECT o.ID AS [ID],o.OBJ_DESC AS NAME FROM A_V_APPROVED_OBJECTS o,A_OBJECT_ITEM_LINK l WHERE
o.ID = l.OBJECT_ID AND l.ITEM_ID = @ID AND l.ITEM_TYPE = @strType







