

/*
STORED PROCEDURE CALLED IN A_SP_PROD_PRICE_LIST_UPDATE
*/
CREATE   PROCEDURE dbo.A_SP_PROD_PRICE_SET_REAL_CUSTOMERS
@ppID nvarchar(50),
@strNTLogin nvarchar(50)
AS
DELETE FROM A_PROD_PRICE_LIST_REAL_CUSTOMERS WHERE PP_LIST_ID = @ppID
Declare @curs Cursor,
		@it varchar(50), 
		@test varchar(50),
		@supDept varchar(50),
		@supRootCo varchar(50),
		@prod varchar(50)

SELECT @prod = PRODUCT FROM A_PROD_PRICE_LIST_HISTORY WHERE ID = @ppID
SELECT @supDept = SUPPLIER_ID FROM A_V_PRODUCTS_APPROVED_DATA WHERE ID = @prod
declare @tRoot as varchar(50)
set @tRoot = @supDept
while @tRoot is not null
	begin
	set @supRootCo = @tRoot
	SELECT @tRoot = PARENT FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @supRootCo
	end
print 'The Supplying Dept = ' + @supDept
print 'The Supplying Root Co = ' + @supRootCo

print 'inside A_SP_PROD_PRICE_SET_REAL_CUSTOMERS '

print 'Going through the included customer list now'
set @curs = Cursor For 
SELECT c.CUST_ID 
FROM A_PROD_PRICE_CUSTOMERS c
WHERE PP_LIST_ID = @ppID
AND NOT EXISTS 
(SELECT ID FROM A_PROD_PRICE_LIST_EXCEPTIONS WHERE PP_LIST_ID = @ppID AND CUST_ID = c.ID) 
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
	Begin
	print 'I am adding ' + isnull(@it,'NULL') + ' to the PPID = ' + isNull(@ppID,'NULL')
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
	Fetch Next from @curs Into @it
	End
close @curs
Deallocate @curs
print 'Done with the included customer list'
print 'Now looking at the special customer items'
print 'Check if this is for all external customers'
declare @tester as varchar(50)
SELECT @tester = ID FROM A_PROD_PRICE_LIST_SPECIAL_DISTRIBUTIONS WHERE PP_LIST_ID = @ppID AND SPEC_ID = 'ALL_EXTERNAL'
if @tester is not null
	begin
	print 'This one is for all external customers'
	set @curs = Cursor For 
	SELECT c.ID 
	FROM A_V_COMPANIES_APPROVED_DATA c
	WHERE c.ID <> @supRootCo AND c.PARENT is NULL
	AND NOT EXISTS 
	(SELECT ID FROM A_PROD_PRICE_LIST_EXCEPTIONS WHERE PP_LIST_ID = @ppID AND CUST_ID = c.ID) 
	open @curs
	Fetch Next from @curs Into @it
	while (@@fetch_status = 0)
		Begin
		print 'I am adding ' + isnull(@it,'NULL') + ' to the PPID = ' + isNull(@ppID,'NULL')
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
		Fetch Next from @curs Into @it
		End
	close @curs
	Deallocate @curs
	end

print 'Done with the external Customers'
print 'Check if this is for all internal customers'
set @tester = NULL
SELECT @tester = ID FROM A_PROD_PRICE_LIST_SPECIAL_DISTRIBUTIONS WHERE PP_LIST_ID = @ppID AND SPEC_ID = 'ALL_INTERNAL_BUT_ME'
if @tester is not null
	begin
	print 'This one is for all internal customers but me'
	set @curs = Cursor For 
	SELECT c.ID 
	FROM A_V_COMPANIES_APPROVED_DATA c
	WHERE c.ID <> @supDept AND ((c.PARENT = @supRootCo) or (c.ID = @supRootCo))
	AND NOT EXISTS 
	(SELECT ID FROM A_PROD_PRICE_LIST_EXCEPTIONS WHERE PP_LIST_ID = @ppID AND CUST_ID = c.ID) 
	open @curs
	Fetch Next from @curs Into @it
	while (@@fetch_status = 0)
		Begin
		print 'I am adding ' + isnull(@it,'NULL') + ' to the PPID = ' + isNull(@ppID,'NULL')
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
		Fetch Next from @curs Into @it
		End
	close @curs
	Deallocate @curs
	end








