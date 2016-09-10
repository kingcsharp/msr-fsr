






CREATE  PROCEDURE dbo.A_SP_PURCHASE_SHOW_QUOTES
@strPurchaseID varchar(50),
@strNTLogin nvarchar(50)
AS

SELECT * FROM A_V_PURCHASES_WITH_SUPPLIER_QUOTES WHERE PURCH_HIST_ID = @strPurchaseID


fin:
return 0 

problem:
print 'Error order ID = ' + isnull(@strPurchaseID,'NULL')
return 1 








