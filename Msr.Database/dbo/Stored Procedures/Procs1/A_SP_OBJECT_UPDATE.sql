




CREATE      procedure A_SP_OBJECT_UPDATE
	@OBJ_TABLE as nvarchar(50),
	@OBJ_ID as nvarchar(50),
	@OBJ_DESC as nvarchar(2000),
	@strNTLogin as nvarchar(50),
	@COMPANY_PART_NUMBER as nvarchar(50),
	@PT_NAME as nvarchar(50),
	@CO_NAME as nvarchar(50)
as
declare @tester as nvarchar(50)
declare @sql as nvarchar(800)
SELECT @tester = ID FROM A_OBJECTS WHERE OBJ_ID = @OBJ_ID AND OBJ_TABLE = @OBJ_TABLE
if not(@tester is null)
	begin
		UPDATE A_OBJECTS SET
			OBJ_DESC = @OBJ_DESC,
			CO_PART_NUM = @COMPANY_PART_NUMBER,
			PART_TYPE = @PT_NAME,
			PART_CO = @CO_NAME,
			DRCM = getDate(),
			MODBY = @strNTLogin
		WHERE
			OBJ_TABLE = @OBJ_TABLE AND OBJ_ID = @OBJ_ID
	end





