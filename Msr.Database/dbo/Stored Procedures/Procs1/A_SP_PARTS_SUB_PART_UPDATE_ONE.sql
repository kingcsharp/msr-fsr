
CREATE  PROCEDURE DBO.A_SP_PARTS_SUB_PART_UPDATE_ONE
@newID varchar(50) OUTPUT,
@msgs varchar(2000) OUTPUT,
@parentObjID varchar(50),
@ID varchar(50),
@PART_ID varchar(50),
@QTY float,
@NICK_NAME nvarchar(200),
@strNTLogin varchar(50)
AS
declare @pid varchar(50)
if @ID is null
	begin
	print 'This is a new one so add it'
	SELECT @pid = ID FROM A_PARTS_HISTORY WHERE OBJECT_ID = @parentObjID 
	exec sp_GetUniqueID3 @newID OUTPUT
	INSERT INTO A_PARTS_SUB_PARTS (ID,PARENT,PART_ID,DRCM,MODBY,QTY,NICK_NAME)
		VALUES (@newID,@pid,@PART_ID,getDate(),@strNTLogin,@QTY,@NICK_NAME)
	end
else
	begin
	UPDATE A_PARTS_SUB_PARTS SET 
		PART_ID = @PART_ID,
		NICK_NAME = @NICK_NAME,
		DRCM = getDate(),
		MODBY = @strNTLogin,
		QTY = @QTY
		WHERE ID = @ID
	end






