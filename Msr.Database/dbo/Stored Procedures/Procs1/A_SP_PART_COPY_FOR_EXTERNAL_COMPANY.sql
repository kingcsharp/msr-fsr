

CREATE   procedure dbo.A_SP_PART_COPY_FOR_EXTERNAL_COMPANY
@newPartID varchar(50) OUTPUT,
@partID varchar(50),
@destCo varchar(50),
@strNTLogin varchar(50)
AS
declare @partHistID varchar(50),@curCo varchar(50),@isNew tinyint
set @isNew = 1
SELECT @partHistID = PARTS_HISTORY_ID FROM A_PARTS WHERE ID = @partID
SELECT @curCo = CREATING_CO FROM A_OBJECTS WHERE OBJ_ID = @partHistID
declare @partDesc nvarchar(2000),@objID varchar(50),@creatorCo varchar(50),
	@custAdmin varchar(50),	@newID varchar(50),@curCustName varchar(50)


SELECT @newPartID = INT_PART_ID 
	FROM A_V_EXTERNAL_PARTS_WITH_COMPANY 
	WHERE EXT_PART_ID = @partID AND EXT_CO = @curCo

if @newPartID is not null
	begin
		print 'This one already exists for this company, so we dont need to create it'
		set @isNew = 0
	 	goto createChildParts

	end

print 'We are going to create this part like we are an admin of the customer'
SELECT @custAdmin = p.ID
	FROM A_V_ROLES_APPROVED_WITH_PEOPLE_IDS ra INNER JOIN
         A_V_PEOPLE_APPROVED_DATA p ON ra.PERSON = p.ID
	WHERE (ra.IS_ADMIN = 1) AND p.ROOT_COMPANY = @destCo

print 'The customer Admin is ' + isNULL(@custAdmin,'NULL')
if @custAdmin is null 
	begin
	print 'There is no Admin at this customer so I can not make the part'
	goto PROBLEM
	end

print 'The part Hist ID is ' + @partHistID
SELECT @partDesc = NAME FROM A_PARTS_HISTORY WHERE ID = @partHistID
print 'The part is ' + @partDesc
print 'The Company ID is ' + @curCo
SElECT @curCustName = NAME FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @curCo
print 'The Company is ' + @curCustName

SELECT @partDesc = '(' + @curCustName + ') ' + @partDesc

print 'The new Part name will be ' + isNull(@partDesc,'NULL')

exec sp_GetUniqueID3 @newID OUTPUT
INSERT INTO A_PARTS_HISTORY
([ID], [UNIT], [NAME],SPARE,CONSUMABLE,  [COMPANY], [UNIT_SHIPPING_WEIGHT], [COMPANY_PART_NUMBER], 
[SUPPLIER_SEE_INSTALL_BASE], [SUPPLIER_SEE_AVAILABILITY], [CUSTOMER_SEE_AVAILABILITY],[DRCM], [MODBY],WEIGHT_TYPE
)
SELECT 
@newID, [UNIT], @partDesc,SPARE,CONSUMABLE, @destCo, [UNIT_SHIPPING_WEIGHT], [COMPANY_PART_NUMBER], 
1, 1, 0, getDate(), @custAdmin, WEIGHT_TYPE
	FROM A_PARTS_HISTORY WHERE ID = @partHistID

SELECT @objID = OBJECT_ID FROM A_PARTS_HISTORY WHERE ID = @newID
set @newPartID = @objID
print 'We inserted it and we have an object ID of ' + @objID

print 'Now we need to add the external reference to this part'
INSERT INTO 
	A_PARTS_EXTERNAL_EQUALS 
		(ID,PART_ID,EQUAL_PART_ID,DRCM,MODBY)
	VALUES 
		(newID(),@newID,@partID,getDate(),@custAdmin)

createChildParts:
SELECT @newID = obj_ID FROM A_OBJECTS WHERE ID = @newPartID
print 'Now we need to make the children parts'
print 'first delete the existing children'
DELETE FROM A_PARTS_SUB_PARTS WHERE PARENT = @newID
Declare @it nvarchar(50), @curs Cursor,@newSubPartID varchar(50),@qty float,@nn varchar(50)
set @curs = Cursor For SELECT PART_ID,QTY,NICK_NAME 
 	FROM A_PARTS_SUB_PARTS WHERE PARENT = @partHistID
open @curs
Fetch Next from @curs Into @it,@qty,@nn
while (@@fetch_status = 0)
Begin
	print 'Creating a new subPart for this part = ' + @it
	exec A_SP_PART_COPY_FOR_EXTERNAL_COMPANY @newSubPartID OUTPUT,@it,@destCo,@strNTLogin
	INSERT INTO A_PARTS_SUB_PARTS (ID,PARENT,PART_ID,DRCM,MODBY,QTY,NICK_NAME)
 		VALUES (newID(),@newID,@newSubPartID,getDate(),@custAdmin,@qty,@nn)
	set @newSubPartID = null
 	Fetch Next from @curs Into @it,@qty,@nn
End
close @curs
Deallocate @curs

if @isNew = 1
	begin
	print 'Now we need to go ahead and finish the approval since we just made this part'
	UPDATE A_OBJECTS SET STATUS = 'APPROVED', APPROVAL_DATE = getDate(),LOCKED_BY = NULL WHERE ID = @objID
	exec A_SP_PARTS_FINISH_WF @newID,@objID,@custAdmin
	end

fin:
return 0
PROBLEM:
print 'There was an error'
return 1


