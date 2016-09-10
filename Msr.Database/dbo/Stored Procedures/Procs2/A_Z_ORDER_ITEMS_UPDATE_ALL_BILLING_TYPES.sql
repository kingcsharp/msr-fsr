CREATE PROCEDURE dbo.A_Z_ORDER_ITEMS_UPDATE_ALL_BILLING_TYPES
AS
Declare @it nvarchar(50),@curs Cursor
declare @myProcID varchar(50),@mySysID varchar(50),@myProdID varchar(50)

set @curs = Cursor For SELECT ID FROM A_ORDER_ITEMS
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	SELECT @myProdID = PRODUCT_ID FROM A_ORDER_ITEMS WHERE ID = @it
	SELECT @myProcID = PROCEDURE_ID  FROM A_V_PRODUCTS_APPROVED_DATA WHERE ID = @myProdID
	SELECT @mySysID = SYSTEM_ID FROM A_V_PROCEDURES_APPROVED_DATA WHERE ID = @myProcID
	UPDATE A_ORDER_ITEMS SET BILL_TYPE = 
		(select case
			when @mySysID in ('SYS_PROVIDE_AND_STAY','SYS_PROVIDE_AND_CONSUMED') then 'ACT_EST'
			else 'ESTIMATE'
		end)
	WHERE ID = @it
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs


