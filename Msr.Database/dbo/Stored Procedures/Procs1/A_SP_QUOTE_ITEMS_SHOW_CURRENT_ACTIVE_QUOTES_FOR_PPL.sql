
CREATE  PROCEDURE DBO.A_SP_QUOTE_ITEMS_SHOW_CURRENT_ACTIVE_QUOTES_FOR_PPL
@ppl varchar(50),
@strNTLogin varchar(50)
AS
print 'looking for the currently active quotes for the price list ' + @ppl
declare @myCo varchar(50),@rootCo varchar(50)
SELECT @myCo = COMPANY, @rootCo = ROOT_COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @strNTLogin
SELECT * FROM A_V_QUOTE_ITEMS_WITH_QUOTE_DATA WHERE PROD_PRICE_LIST = @ppl
--AND (CUSTOMER_CO = @myCo or CUSTOMER_CO = @rootCo)
AND (EXPIRATION_DATE is null or EXPIRATION_DATE >= getDate())
AND STATUS LIKE 'APPROVED%'


