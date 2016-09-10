
CREATE procedure dbo.A_SP_PRODUCTS_CHECK_VALIDITY
	@returnVal nvarchar(2000) OUTPUT,
	@ID nvarchar(50),
	@strNTLogin nvarchar(50)
as
print 'Entering A_SP_PRODUCTS_CHECK_VALIDITY'
declare @cust as tinyInt,@roleResponsible varchar(50)
SELECT @cust = CUSTOMIZABLE, 
@roleResponsible = CUST_MGR_ROLE
FROM A_PRODUCTS_HISTORY WHERE ID = @ID
if @cust = 1
	if @roleResponsible is null 
		set @returnVal = 'CUSTOMIZABLE_PRODUCT_WITHOUT_MANAGER_ROLE'
	



fin:


