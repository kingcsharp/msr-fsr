
CREATE  PROCEDURE dbo.A_SP_ORDER_SHOW_ALL_QUOTES_FOR_ONE_ORDER
	@ID nvarchar(50),
	@strNTLogin nvarchar(50)
AS
--find out my Company
declare @myCO as nvarchar(50)
exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCO OUTPUT

SELECT *
FROM A_V_QUOTE_HISTORY_SEARCH WHERE ORDER_ID = @ID

