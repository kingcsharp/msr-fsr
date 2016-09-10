



CREATE  PROCEDURE A_SP_APPROVED_OBJECT_GET_ALL_DATA
	@strID nvarchar(50),
	@strNTLogin nvarchar(50)
as
declare @tbl varchar(50),@objID varchar(50)
SELECT @tbl = OBJ_TABLE,
	@objID = OBJ_ID 
	FROM A_V_APPROVED_OBJECTS 
	WHERE ID = @strID
if @tbl = 'A_ACTUAL_PARTS_HISTORY'
	begin
	print '
	SELECT ''' + @strID + ''' AS AP_OBJ_ID,
	''' + @tbl + ''' AS OBJ_TABLE,
	ap.*
	FROM A_O_ACTUAL_PARTS_HISTORY ap WHERE ID =''' +  @objID + ''''

	SELECT @strID AS AP_OBJ_ID,
	@tbl AS OBJ_TABLE,
	ap.*
	FROM A_O_ACTUAL_PARTS_HISTORY ap WHERE ID = @objID
	end



