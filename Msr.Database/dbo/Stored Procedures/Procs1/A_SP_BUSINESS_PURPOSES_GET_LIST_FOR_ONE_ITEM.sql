





CREATE  PROCEDURE A_SP_BUSINESS_PURPOSES_GET_LIST_FOR_ONE_ITEM
@ID varchar(50),
@strType varchar(50),
@strNTLogin varchar(50)
AS
print 'Getting Purposes for item ' + @ID + ' with Type = ' + @strType
SELECT bp.ID AS [ID],bp.NAME AS NAME FROM A_BUSINESS_PURPOSES bp,A_BUSINESS_PURPOSES_ITEM_LINK l WHERE
bp.ID = l.BUSINESS_PURPOSE_ID AND l.ITEM_ID = @ID AND l.ITEM_TYPE = @strType






