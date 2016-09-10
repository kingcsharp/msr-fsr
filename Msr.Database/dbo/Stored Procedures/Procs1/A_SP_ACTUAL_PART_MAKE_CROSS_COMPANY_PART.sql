CREATE PROCEDURE dbo.A_SP_ACTUAL_PART_MAKE_CROSS_COMPANY_PART
@newPartID varchar(50) OUTPUT,
@actPartID varchar(50),
@fillID varchar(50),
@destCo varchar(50),
@strNTLogin varchar(50)
AS
declare @curCo varchar(50),@partID varchar(50)
declare @destCoName varchar(50)
declare @purchItemID varchar(50),@purchaser varchar(50),@phID varchar(50),@partHistID varchar(50)
declare @partIDObjectID varchar(50)

print 'Checking to make sure a part is crossable betwix two companies.'
print 'Get the part ID'
SELECT @partID = PART_ID FROM A_V_ACTUAL_PARTS_APPROVED_DATA WHERE ID = @actPartID
SELECT @partHistID = HISTORY_REF_ID,@partIDObjectID = OBJECT_ID FROM A_V_PARTS_APPROVED_DATA WHERE ID = @partID
print 'get the company that this actual part part ID is registered too'
SELECT @curCo = CREATING_CO FROM A_OBJECTS WHERE ID = @partIDObjectID
if @destCo is null
	begin
	if @fillID is null goto PROBLEM
	SELECT @purchItemID = PURCH_ITEM_ID FROM A_FILLS WHERE ID = @fillID
	SELECT @phID = PURCHASE_HIST_ID FROM A_ORDER_ITEMS WHERE ID = @purchItemID
	SELECT @purchaser = PURCHASER FROM A_PURCHASES_HISTORY WHERE ID = @phID
	SELECT @destCo = ROOT_COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @purchaser
	end

SELECT @destCoName = NAME FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @destCO
print 'The destination company = ' + @destCo + ' ' + @destCoName
print 'The part hist id = ' + @partHistID + ' '
print 'now that I have the destination company I can cross ref this part'

print 'First check to see if they already have a part related to this part'
SELECT @newPartID = LOCAL_PART_ID FROM A_V_PARTS_WITH_RELATED_EXTERNAL_CO_INFO 
	WHERE EXTERNAL_PART_ID = @partID AND LOCAL_CO = @destCo

if @newPartID is not null goto fin

print 'There is not a part at the dest co that corresponds to this part so we need to make it and all its sub parts'
exec A_SP_COPY_PART_FOR_EXTERNAL_COMPANY
@newPartID OUTPUT,
@partID,
@destCo,
@strNTLogin



fin:

return 0
PROBLEM:
print 'There was a problem'
return 1
