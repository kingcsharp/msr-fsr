


CREATE  PROCEDURE A_SP_PROJECT_GET_ITEMS_FOR_ONE_PROJECT

@ID varchar(50),
@strType varchar(50),
@strNTLogin varchar(50)
AS

print 'Getting items with Type = ' + @strType + ' for project ' + @ID

SELECT * FROM A_V_PROJECT_ITEMS_AND_NAMES WHERE PROJECT_ID = @ID AND ITEM_TYPE = @strType
ORDER BY ITEM_NAME