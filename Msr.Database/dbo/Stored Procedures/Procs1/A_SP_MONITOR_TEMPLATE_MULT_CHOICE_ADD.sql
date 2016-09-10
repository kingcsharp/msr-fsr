

CREATE  procedure A_SP_MONITOR_TEMPLATE_MULT_CHOICE_ADD
@MON_ID nvarchar(50),
@strNTLogin nvarchar(50)
AS
declare @newID as nvarchar(50)
exec sp_getUniqueID3 @newID OUTPUT
INSERT INTO A_MONITOR_TEMPLATES_MULT_CHOICE (ID,MONITOR_ID,DRCM,MODBY,ROOT_ID)
VALUES (@newID,@MON_ID,getDate(),@strNTLogin,@newID)


