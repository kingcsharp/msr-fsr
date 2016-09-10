






CREATE       FUNCTION dbo.A_FN_ACTUAL_PART_GET_NAME_FROM_HIST_ID_OR_ID (@ID varchar(50),@histID varchar(50))
RETURNS nvarchar(3000)
as
BEGIN
if @histID is null SELECT @histID = HISTORY_REF_ID FROM A_ACTUAL_PARTS WHERE ID = @ID
declare @so nvarchar(3000),@pID varchar(50)

if exists(SELECT * FROM A_OBJECTS WHERE OBJ_ID = @histID)
	begin
	SELECT @so = ISNULL('Actual Part # ' + ROOT + ', ', '') FROM A_OBJECTS WHERE OBJ_ID = @histID
	end
else
	begin
	SELECT @so = ISNULL('Actual Part # ' + ID + ', ', '') FROM A_ACTUAL_PARTS_HISTORY WHERE ID = @histID
	end

SELECT @so = @so +  ISNULL('Nick: ' + NICK_NAME + ', ', '') + ISNULL('s/n:' + SERIAL + ', ', ''),@pID = PART_ID
	FROM A_ACTUAL_PARTS_HISTORY WHERE ID = @histID


SELECT @so = @so +  ISNULL(NAME + ' ', '') + ISNULL('(p/n:' + ID + ')', '')
	FROM A_V_PARTS_APPROVED_DATA WHERE ID = @pID

	

return(@so) 
END












