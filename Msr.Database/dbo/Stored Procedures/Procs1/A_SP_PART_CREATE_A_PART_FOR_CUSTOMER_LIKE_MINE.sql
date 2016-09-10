



CREATE     PROCEDURE DBO.A_SP_PART_CREATE_A_PART_FOR_CUSTOMER_LIKE_MINE
@partID varchar(50),
@extParentID varchar(50),
@qty float,
@custID varchar(50),
@strNTLogin varchar(50)
AS

declare @rootCustID varchar(50)
SELECT @rootCustID = ROOT_CO FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @custID
print 'First check to see if they already have a part related to this part'
declare @partDesc nvarchar(200),
	@ph varchar(50),
	@objID varchar(50),
	@creatorCo varchar(50),
	@custAdmin varchar(50),
	@newID varchar(50)

SELECT @ph = PARTS_HISTORY_ID FROM A_PARTS WHERE ID = @partID

SELECT @objID = LOCAL_PART_ID, @newID = LOCAL_PH_ID
	FROM A_V_PARTS_WITH_RELATED_EXTERNAL_CO_INFO 
	WHERE EXTERNAL_PART_ID = @partID AND LOCAL_CO = @rootCustID
if @objID is not null
	begin
	print 'This part is already referenced by this customer as ' + @objID
	goto createChildren
	end

print ''
print 'In A_SP_PART_CREATE_A_PART_FOR_CUSTOMER_LIKE_MINE'
print 'I am going to set up a part for customer = ' + isNULL(@rootCustID,'NULL')
print 'The part is going to be just like my part ' + isNull(@partID,'NULL')

if 	@creatorCo  = @rootCustID
begin
	print 'This is the same company that made the part.'
	goto fin
end

print 'We are going to creater this part like we are an admin of the customer'
SELECT @custAdmin = p.ID
	FROM A_V_ROLES_APPROVED_WITH_PEOPLE_IDS ra INNER JOIN
         A_V_PEOPLE_APPROVED_DATA p ON ra.PERSON = p.ID
	WHERE (ra.IS_ADMIN = 1) AND p.ROOT_COMPANY = @rootCustID
print 'The customer Admin is ' + isNULL(@custAdmin,'NULL')
if @custAdmin is null 
	begin
	print 'There is no Admin at this customer so I can not make the part'
	goto fin
	end

SELECT @ph = PARTS_HISTORY_ID FROM A_PARTS WHERE ID = @partID
SELECT @partDesc = '(' + c.name + ') ' + p.NAME 
	FROM A_PARTS_HISTORY p,A_V_COMPANIES_APPROVED_DATA c 
	WHERE  p.COMPANY = c.ID AND p.ID = @ph
print 'The new Part name will be ' + isNull(@partDesc,'NULL')


exec sp_GetUniqueID3 @newID OUTPUT
INSERT INTO A_PARTS_HISTORY
([ID], [UNIT], [NAME], [DRCM], [MODBY], [COMPANY], [UNIT_SHIPPING_WEIGHT], [COMPANY_PART_NUMBER], 
[SUPPLIER_SEE_INSTALL_BASE], [SUPPLIER_SEE_AVAILABILITY], [CUSTOMER_SEE_AVAILABILITY])
SELECT 
@newID, [UNIT], @partDesc, getDate(), @custAdmin, @rootCustID, [UNIT_SHIPPING_WEIGHT], [COMPANY_PART_NUMBER], 
[SUPPLIER_SEE_INSTALL_BASE], [SUPPLIER_SEE_AVAILABILITY], [CUSTOMER_SEE_AVAILABILITY] 
	FROM A_PARTS_HISTORY WHERE ID = @ph

SELECT @objID = OBJECT_ID FROM A_PARTS_HISTORY WHERE ID = @newID
print 'We inserted it and we have an object ID of ' + @objID

print 'Now we need to add the external reference to this part'
INSERT INTO 
	A_PARTS_EXTERNAL_EQUALS 
		(ID,PART_ID,EQUAL_PART_ID,DRCM,MODBY)
	VALUES 
		(newID(),@newID,@partID,getDate(),@custAdmin)

print 'Now we need to go ahead and finish the approval'
UPDATE A_OBJECTS SET LOCKED_BY=NULL,STATUS = 'APPROVED', APPROVAL_DATE = getDate() WHERE ID = @objID
exec A_SP_PARTS_FINISH_WF @newID,@objID,@custAdmin

createChildren:
declare @curs as cursor,@it varchar(50),@q float
set @curs = CURSOR FOR SELECT PART_ID,QTY FROM A_PARTS_SUB_PARTS WHERE PARENT = @PH
open @curs
fetch next from @curs into @it,@q
while @@fetch_status =0
	begin
	exec A_SP_PART_CREATE_A_PART_FOR_CUSTOMER_LIKE_MINE
		@it ,
		@objID ,
		@q,
		@custID,
		@strNTLogin



	fetch next from @curs into @it,@q
	end

if @extParentID is not null
	begin
	declare @extParentHistID varchar(50)
	SELECT @extParentHistID = PARTS_HISTORY_ID FROM A_PARTS WHERE ID = @extParentID
	IF NOT EXISTS (SELECT ID FROM A_PARTS_SUB_PARTS WHERE PARENT = @extParentHistID AND PART_ID = @objID)
		INSERT INTO A_PARTS_SUB_PARTS (ID,PARENT,PART_ID,QTY,DRCM,MODBY)
			VALUES (newID(),@extParentHistID,@objID,@qty,getDate(),@strNTLogin)
	end

fin:








