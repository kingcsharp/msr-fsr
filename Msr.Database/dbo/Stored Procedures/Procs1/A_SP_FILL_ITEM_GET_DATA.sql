



CREATE  PROCEDURE A_SP_FILL_ITEM_GET_DATA
	@strID nvarchar(50),
	@strNTLogin nvarchar(50)
as
SELECT * FROM A_V_FILLS_SEARCH WHERE ID = @strID




