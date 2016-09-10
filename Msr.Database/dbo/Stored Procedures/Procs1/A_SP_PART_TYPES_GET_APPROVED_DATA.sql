








CREATe       procedure DBO.A_SP_PART_TYPES_GET_APPROVED_DATA
	@strID nvarchar(50),
	@strNTLogin nvarchar(50)
as
SELECT * FROM A_V_PART_TYPES_APPROVED_DATA WHERE ID = @strID



