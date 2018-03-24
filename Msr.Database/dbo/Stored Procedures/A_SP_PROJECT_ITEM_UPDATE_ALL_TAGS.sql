

CREATE   PROCEDURE DBO.A_SP_PROJECT_ITEM_UPDATE_ALL_TAGS
@newID varchar(50) OUTPUT, 
@msgs varchar(2000) OUTPUT, 
@itemID varchar(50), 
@itemType varchar(50), 
@purposes varchar(50), 
@appObjects varchar(50), 
@projects varchar(50),
@strNTLogin varchar(50)
AS
if @itemID is not null 
exec A_SP_PROJECT_GENERAL_UPDATE_PURPOSES_AND_OBJECTS
	@itemID,@itemType,@purposes,@appObjects,@projects,@strNTLogin
else
	set @newID = 'ERROR No Item ID'