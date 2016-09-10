



CREATE     PROCEDURE dbo.A_SP_PRODUCT_CREATE_PRICELIST 
@myRoot varchar(50),
@strNTLogin varchar(50)
AS
if not exists(SELECT * FROM A_V_PRODUCTS_APPROVED_DATA WHERE ID = @myRoot)
	begin
	print 'This is not a product ID'
	goto fin
	end
print 'In A_SP_PRODUCT_CREATE_PRICELIST'
declare @procID as varchar(50),
	@histRef as varchar(50),
	@creator as varchar(50),
	@objID as varchar(50)
SELECT @histRef = HISTORY_REF_ID,@procID = PROCEDURE_ID FROM A_V_PRODUCTS_APPROVED_DATA WHERE ID = @myRoot
SELECT @objID = OBJECT_ID FROM A_PRODUCTS_HISTORY WHERE ID = @histRef
SELECT @creator = CREATED_BY FROM A_OBJECTS WHERE ID = @objID

declare @newID as varchar(50),
	@msg as nvarchar (2000),
	@creatorDept as varchar(50)
select @creatorDept = COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @creator

if not exists(SELECT * FROM A_V_PROD_PRICE_LIST_APPROVED_DATA WHERE PRODUCT = @myRoot)
	begin
	print 'I did not find a price list for this product so I am going to make one'
	exec A_SP_PROD_PRICE_LIST_UPDATE 
		@newID OUTPUT,
		@msg OUTPUT,
		null, --objID
		@myRoot, --Product
		@creatorDept, --Customers
		null, --Unit
		null,--Min Qty
		'0', --Unit Price
		null, --est Unit Price
		null, --est Labor
		null, --est Parts PRov and Stay
		null, --Est Parts PRov Take back
		null, --Est Parts Consumed
		'ACTUAL', --Invoice From
		NULL, --Production Time
		NULL, --Production Time Unit
		NULL, --Capacity
		NULL, --Capacity Unit
		null, --Special Customers
		null, --Cust Exceptions
		'1', --For indi sale
		@creator --strNTLogin
	end
else print 'There is already a product price list for this product, so I am not gon gto make this person amake another one.'



fin:




