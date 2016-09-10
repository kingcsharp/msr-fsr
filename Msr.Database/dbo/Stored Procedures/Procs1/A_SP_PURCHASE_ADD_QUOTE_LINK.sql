





CREATE       PROCEDURE DBO.A_SP_PURCHASE_ADD_QUOTE_LINK
@ID varchar(50),
@strNTLogin varchar(50)
AS

print 'Adding the Quote Links for ' + @ID
Declare @qID nvarchar(50)
Declare @curs Cursor,@it varchar(50)
set @curs = Cursor For 
	SELECT     qh.ID AS QUOTE_ID
	FROM         dbo.A_PURCHASES_HISTORY ph INNER JOIN
                      dbo.A_ORDERS o ON ph.ORDER_ID = o.ID INNER JOIN
                      dbo.A_QUOTES_HISTORY qh ON o.ID = qh.ORDER_ID INNER JOIN
                      dbo.A_QUOTES q ON qh.ID = q.HISTORY_REF_ID
	WHERE ph.ID = @ID
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'Adding Quote = ' + @it
	INSERT INTO A_PURCHASE_QUOTE_STATUS (ID,PURCHASE_ID,QUOTE_ID,STATUS,DRCM,MODBY)
	VALUES (newID(),@ID,@IT,'ITEM_NEEDS_ACCOUNT',getDate(),@strNTLogin)
	exec A_SP_PURCHASE_QUOTE_UPDATE_STATUS @ID,@IT,@strNTLogin
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs






