








CREATE          PROCEDURE DBO.A_SP_Z_DROP_DOWN_POPULATOR_ADD_ITEM
@val varchar(50),
@strType varchar(50),
@key varchar(50),
@strNTLogin varchar(50)
AS

declare @tester varchar(50),@show nvarchar(200)

DELETE FROM A_Z_DROP_DOWN_POPULATOR WHERE 
POP_UP_KEY = @key AND
VAL =  @val AND
USER_ID = @strNTLogin

if @strType in('PEOPLE','A_PEOPLE')
	begin
	print 'We have a person so get their name'
	SELECT @SHOW = FULL_NAME FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @val
	INSERT INTO A_Z_DROP_DOWN_POPULATOR (ID,USER_ID,POP_UP_KEY,SHOW,VAL,DRCM)
	VALUES (newID(),@strNTLogin,@key,@show,@val,getDate())
	end

if @strType IN ('ROLE','A_ROLES')
	begin
	print 'We have a role so get its name'
	SELECT @SHOW = NAME FROM A_V_ROLES_APPROVED_DATA WHERE ID = @val
	INSERT INTO A_Z_DROP_DOWN_POPULATOR (ID,USER_ID,POP_UP_KEY,SHOW,VAL,DRCM)
	VALUES (newID(),@strNTLogin,@key,@show,@val,getDate())
	end
if @strType IN ('COMPANY','A_COMPANIES')
	begin
	print 'We have a company so get its name'
	SELECT @SHOW = NAME FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @val
	INSERT INTO A_Z_DROP_DOWN_POPULATOR (ID,USER_ID,POP_UP_KEY,SHOW,VAL,DRCM)
	VALUES (newID(),@strNTLogin,@key,@show,@val,getDate())
	end

if @strType = 'ACCOUNT'
	begin
	print 'We have a account so get its name'
	SELECT @SHOW = NAME FROM A_V_ACCOUNTS_APPROVED_DATA WHERE ID = @val
	INSERT INTO A_Z_DROP_DOWN_POPULATOR (ID,USER_ID,POP_UP_KEY,SHOW,VAL,DRCM)
	VALUES (newID(),@strNTLogin,@key,@show,@val,getDate())
	end

if @strType = 'BUSINESS_PURPOSE'
	begin
	print 'We have a business purpose so get its name'
	SELECT @SHOW = NAME FROM A_BUSINESS_PURPOSES WHERE ID = @val
	INSERT INTO A_Z_DROP_DOWN_POPULATOR (ID,USER_ID,POP_UP_KEY,SHOW,VAL,DRCM)
	VALUES (newID(),@strNTLogin,@key,@show,@val,getDate())
	end

if @strType = 'A_PART'
	begin
	print 'We have a Part so get its name'
	SELECT @SHOW = NAME FROM A_V_PART_DATA_BY_APPROVED_DATA WHERE ID = @val
	INSERT INTO A_Z_DROP_DOWN_POPULATOR (ID,USER_ID,POP_UP_KEY,SHOW,VAL,DRCM)
	VALUES (newID(),@strNTLogin,@key,@show,@val,getDate())
	end
if @strType = 'A_PART_TYPES'
	begin
	print 'We have a Part type so get its name'
	SELECT @SHOW = NAME FROM A_V_PART_TYPES_APPROVED_DATA WHERE ID = @val
	INSERT INTO A_Z_DROP_DOWN_POPULATOR (ID,USER_ID,POP_UP_KEY,SHOW,VAL,DRCM)
	VALUES (newID(),@strNTLogin,@key,@show,@val,getDate())
	end



if @strType = 'A_LOCATION'
	begin
	print 'We have a Location so get its name'
	SELECT @SHOW = NAME FROM A_V_LOCATIONS_BY_APPROVED_ID WHERE ID = @val
	INSERT INTO A_Z_DROP_DOWN_POPULATOR (ID,USER_ID,POP_UP_KEY,SHOW,VAL,DRCM)
	VALUES (newID(),@strNTLogin,@key,@show,@val,getDate())
	end

if @strType = 'A_ACTUAL_PART'
	begin
	print 'We have an Actual Part so get its name'
	SELECT @SHOW = NAME FROM A_V_ACTUAL_PARTS_APPROVED_DATA WHERE ID = @val
	INSERT INTO A_Z_DROP_DOWN_POPULATOR (ID,USER_ID,POP_UP_KEY,SHOW,VAL,DRCM)
	VALUES (newID(),@strNTLogin,@key,@show,@val,getDate())
	end


if @strType = 'A_OBJECT'
	begin
	print 'We have an object so get its description'
	SELECT @SHOW = OBJ_DESC FROM A_V_APPROVED_OBJECTS WHERE ID = @val
	INSERT INTO A_Z_DROP_DOWN_POPULATOR (ID,USER_ID,POP_UP_KEY,SHOW,VAL,DRCM)
	VALUES (newID(),@strNTLogin,@key,@show,@val,getDate())
	end
if @strType = 'A_PROCEDURES'
	begin
	print 'We have a procedure so get its name'
	SELECT @SHOW = NAME FROM A_V_PROCEDURES_APPROVED_DATA WHERE ID = @val
	INSERT INTO A_Z_DROP_DOWN_POPULATOR (ID,USER_ID,POP_UP_KEY,SHOW,VAL,DRCM)
	VALUES (newID(),@strNTLogin,@key,@show,@val,getDate())
	end
if @strType = 'A_PROCEDURE_TYPES'
	begin
	print 'We have a procedure type so get its name'
	SELECT @SHOW = NAME FROM A_TT_VERBS p,A_TT_VERBS_HISTORY h
		WHERE p.ID = @val AND p.HISTORY_REF_ID = h.ID

	INSERT INTO A_Z_DROP_DOWN_POPULATOR (ID,USER_ID,POP_UP_KEY,SHOW,VAL,DRCM)
	VALUES (newID(),@strNTLogin,@key,@show,@val,getDate())
	end







