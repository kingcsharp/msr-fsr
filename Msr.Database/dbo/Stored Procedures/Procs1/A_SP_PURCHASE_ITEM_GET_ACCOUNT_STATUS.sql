CREATE PROCEDURE dbo.A_SP_PURCHASE_ITEM_GET_ACCOUNT_STATUS
@ret varchar(50) OUTPUT,
@ordID varchar(50)
AS
declare @supID varchar(50),@custID varchar(50),@acctID varchar(50)
SELECT @supID = SUPPLIER_ID,@custID = CUST_ID,@acctID = ACCOUNT_ID 
FROM A_V_PURCHASE_ITEM_WITH_SUP_AND_CUST WHERE
ID = @ordID
declare @rootCo as varchar(50)
SELECT @rootCo = ROOT_CO FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @supID
print @rootCo
declare @rootCo2 as varchar(50)
SELECT @rootCo2 = ROOT_CO FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @custID
print @rootCo2
if @rootCo = @rootCo2
	begin
	set @ret = 'INT_ACCT_OPTIONAL'
	goto fin
	end
if @acctID is null
	begin
	set @ret = 'ITEM_NEEDS_ACCOUNT'
	goto fin
	end
set @ret = 'ALL_ITEMS_HAVE_ACCOUNTS'


fin:
