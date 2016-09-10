
CREATE    procedure dbo.A_SP_QUOTE_ORDER_LINK_CREATE_LINK_FOR_ORDER
@oID nvarchar(50),
@strNTLogin nvarchar(50)
as
print 'We need to add a record to the quote link for all the different suppliers of this Order'
print 'We have the approved Order ID which is: ' + @oID
print 'We need to get the orders history ID'
declare @ohID as varchar(50)
SELECT @ohID = HISTORY_REF_ID FROM A_ORDERS WHERE ID = @oID
print 'The order History ID = ' + @ohID
print 'Get the customer ID'
declare @custID as varchar(50)
SELECT @custID = CUSTOMER_CO FROM A_ORDERS_HISTORY WHERE ID = @ohID
print 'The Customer is = ' + @custID
print 'Now We need to find out all the different suppliers for this order and add them to the link table'
Declare @it nvarchar(50)
Declare @curs Cursor
print 'SELECT DISTINCT SUPPLIER_ID FROM A_V_ORDER_ITEMS_DATA_WITH_SHIPPING WHERE ORDER_ID = ''' + @ohID + '''
AND SUPPLIER_ID NOT IN 
		(SELECT SUPPLIER 
			FROM A_QUOTE_ORDER_LINK 
			WHERE ORDER_ID = ''' + @oID + ''' AND
			CUSTOMER = ''' + @custID + ''')

'
set @curs = Cursor For 
	SELECT DISTINCT SUPPLIER_ID 
	FROM A_V_ORDER_ITEMS_DATA_WITH_SHIPPING 
	WHERE ORDER_ID = @ohID
	AND SUPPLIER_ID NOT IN 
		(SELECT SUPPLIER 
			FROM A_QUOTE_ORDER_LINK 
			WHERE ORDER_ID = @oID AND
			CUSTOMER = @custID)

open @curs
declare @newID as varchar(50)
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	exec sp_GetUniqueID3 @newID OUTPUT
	print 'Adding a quote order link to this order for the supplier ' + @it
	INSERT INTO A_QUOTE_ORDER_LINK (ID,QUOTE_ID,ORDER_ID,STATUS,CUSTOMER,SUPPLIER,DRCM,MODBY)
	VALUES (@newID,NULL,@oID,'NEW',@custID,@it,getDate(),@strNTLogin)
	print 'now we need to add a task for the person who is going to make the quote'
	exec A_SP_QUOTE_CREATE_TASK_TO_MAKE_QUOTE @newID,@strNTLogin
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs



