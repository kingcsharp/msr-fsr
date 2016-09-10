




CREATE   PROCEDURE A_SP_LOCATIONS_GET_PARENT_PATH
	@pInfo nvarchar(1000) OUTPUT,
	@ID nvarchar(100)
as
declare @currentParent as nvarchar(50)
declare @nextParent as nvarchar(50)
declare @pID as nvarchar(50)
declare @pName as nvarchar(100)
declare @pObj as nvarchar(50)

SELECT @pID = ID,@pName = NAME,@nextParent = PARENT_LOCATION,@pObj = OBJECT_ID FROM
A_LOCATIONS_HISTORY WHERE ID = @currentParent
set @pInfo = isNull(@pInfo + '/','') + isNull(@pName + ' ','') + '-*-'
+ isNull('' + @pObj + '','') + '-*-' + isNull('' + @pID + '','')
set @currentParent = @nextParent

while not (@currentParent is NULL)
	begin
		SELECT @pID = ID,@pName = NAME,@nextParent = PARENT_LOCATION,@pObj = OBJECT_ID FROM
			A_APPROVED_LOCATIONS WHERE ID = @currentParent
		set @pInfo = isNull(@pInfo + '/','') + isNull(@pName + ' ','') + '-*-' +isNull('' + @pObj + '','') + '-*-' + isNull('' + @pID + '','')
		SELECT @currentParent = @nextParent
	end



