




CREATE   PROCEDURE A_SP_LOCATIONS_GET_FULL_ADDRESS
	@Info nvarchar(1000) OUTPUT,
	@ID nvarchar(100)
as
print 'Getting the full address'
declare @currentParent as nvarchar(50)
declare @nextParent as nvarchar(50)
declare @pID as nvarchar(50)
declare @pADD_1 as nvarchar(100)
declare @pADD_2 as nvarchar(100)
declare @pINTERNAL as nvarchar(100)
declare @pObj as nvarchar(50)


SELECT @pID = ID,@pADD_1 = ADDRESS_1,@pADD_2 = ADDRESS_2,
		@pINTERNAL = INTERNAL_ADDRESS,@currentParent = PARENT_LOCATION,
		@pObj = OBJECT_ID FROM A_LOCATIONS_HISTORY WHERE ID = @ID
		set @Info = isNull(@pADD_1 + char(13),'') + isNull(@pADD_2 + char(13),'') + isNull(@pINTERNAL + char(13),'') + isNull(@Info,'')
		print 'Info = ' + @Info
print 'The Current PArent is ' + @currentParent

while not (@currentParent is NULL)
	begin
		
		SELECT @pID = ID,@pADD_1 = ADDRESS_1,@pADD_2 = ADDRESS_2,@pINTERNAL = INTERNAL_ADDRESS,@nextParent = PARENT_LOCATION,@pObj = OBJECT_ID FROM
			A_APPROVED_LOCATIONS WHERE ID = @currentParent
		set @Info = isNull(@pADD_1 + char(13),'') + isNull(@pADD_2 + char(13),'') + isNull(@pINTERNAL + char(13),'') + isNull(@Info,'')
		print 'Info = ' + @Info
		SELECT @currentParent = @nextParent
	end
set @Info = rtrim(@Info)
print 'Done getting the address which is ' + @Info






