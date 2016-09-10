
CREATE                    procedure A_SP_OBJECT_ADD
	@OBJ_TABLE as nvarchar(50),
	@OBJ_ID as nvarchar(50),
	@OBJ_DESC as nvarchar(2000),
	@strNTLogin as nvarchar(50),
	@COMPANY_PART_NUMBER as nvarchar(50),
	@PT_NAME as nvarchar(50),
	@CO_NAME as nvarchar(50)
as
print 'Adding a new object'
declare @tester as nvarchar(50)
declare @sql as nvarchar(800)
declare @myCOName as nvarchar(200)
SELECT @tester = ID FROM A_OBJECTS WHERE OBJ_ID = @OBJ_ID AND OBJ_TABLE = @OBJ_TABLE
print 'Checking to see if this item is already in the object table '
print 'Tester = ' + isnull(@tester,'NULL')
if @tester is null
	begin
		print 'tester was null so we need to make this one.'
		declare @newID as nvarchar(50)
		exec sp_GetUniqueID3 @newID OUTPUT
		if @OBJ_ID is null
			begin 
			print 'The object is null table = '
			print @OBJ_TABLE
			print 'ERROR so we are not going to insert anything now'
			end
		else
			begin
				declare @myCo as nvarchar(50)
				SELECT @myCO = CO,@myCoName = COMPANY_NAME FROM A_V_PEOPLE_WITH_COMPANIES WHERE PERSON = @strNTLogin
				if @myCO is null
					begin 
					print 'The Company is null -- ID  = '
					print @strNTLogin
					end		
				declare @ApprovalAct as nvarchar(50)
				exec A_SP_GET_APPROVAL_ACTIVITY_BY_TABLE_AND_ID @ApprovalAct OUTPUT,@OBJ_TABLE,@OBJ_ID

				print 'Inserting the object ' + @newID
				INSERT INTO A_OBJECTS (ID,ROOT,REV,CREATING_CO,OBJ_TABLE,
								OBJ_ID,OBJ_DESC,CO_PART_NUM,PART_TYPE,PART_CO,DRCM,
								MODBY,CREATE_DATE,CREATED_BY,APPROVAL_ACTIVITY,CREATING_CO_NAME) 
				VALUES (@newID,@newID,1,@myCo,@OBJ_TABLE,@OBJ_ID,@OBJ_DESC,
								@COMPANY_PART_NUMBER,@PT_NAME,@CO_NAME,getDate(),@strNTLogin,
								getDate(),@strNTLogin,@ApprovalAct,@myCoName)
		end
	end
else
	begin
		print 'This object already exists so no need to make it'
	end








