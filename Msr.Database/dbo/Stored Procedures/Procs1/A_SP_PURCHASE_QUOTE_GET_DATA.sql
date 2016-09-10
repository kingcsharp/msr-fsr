







CREATE   PROCEDURE dbo.A_SP_PURCHASE_QUOTE_GET_DATA
@strPurchaseHistID varchar(50),
@strQuoteHistID varchar(50),
@strNTLogin nvarchar(50)
AS
declare @strQuoteID varchar(50)
SELECT @strQuoteID = ID FROM A_QUOTES WHERE HISTORY_REF_ID = @strQuoteHistID
print 'Getting the info about purchase = ' + @strPurchaseHistID + ' quote = ' + @strQuoteID
SELECT * FROM A_V_PURCHASES_WITH_SUPPLIER_QUOTES WHERE PURCH_HIST_ID = @strPurchaseHistID AND QUOTE_ID = @strQuoteID




fin:
return 0 

problem:
print 'Error purchase ID = ' + isnull(@strPurchaseHistID,'NULL')
return 1 









