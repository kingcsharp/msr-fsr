


/*
STORED PROCEDURE CALLED IN A_SP_PROD_PRICE_SET_REAL_CUSTOMERS
*/
CREATE   PROCEDURE dbo.A_SP_PROD_PRICE_SET_REAL_CUSTOMERS_SET_CHILDREN
@ppID nvarchar(50),
@companyID nvarchar(2000),
@strNTLogin nvarchar(50)
AS
declare @myDeptChild varchar(50)
Declare @curs Cursor
Declare @it varchar(50), @test varchar(50)
Declare @childName varchar(50) 
--cursor call
print 'inside A_SP_PROD_PRICE_SET_REAL_CUSTOMERS_SET_CHILDREN '
set @curs = Cursor For 
SELECT c.ID, c.NAME 
FROM A_APPROVED_COMPANIES c
WHERE c.PARENT=@companyID
AND NOT EXISTS 
(SELECT ID FROM A_PROD_PRICE_LIST_EXCEPTIONS WHERE PP_LIST_ID = @ppID AND CUST_ID = c.ID) 
 
open @curs
Fetch Next from @curs Into @it, @childName
while (@@fetch_status = 0)
	Begin
	SELECT @test = ID  
    	FROM A_PROD_PRICE_LIST_REAL_CUSTOMERS
    	WHERE CUST_ID = @it AND PP_LIST_ID = @ppID 
    if @test is null
	    begin  
	 	INSERT INTO A_PROD_PRICE_LIST_REAL_CUSTOMERS
    	    ([ID], [PP_LIST_ID],[CUST_ID], [DRCM],[MODBY])
	 		VALUES(newID(),@ppID,@it, getDate() ,@strNTLogin)
		end 
	exec A_SP_PROD_PRICE_SET_REAL_CUSTOMERS_SET_CHILDREN  @ppID, @it , @strNTLogin 
	Fetch Next from @curs Into @it, @childName
	End
close @curs
Deallocate @curs
--end cursor call



