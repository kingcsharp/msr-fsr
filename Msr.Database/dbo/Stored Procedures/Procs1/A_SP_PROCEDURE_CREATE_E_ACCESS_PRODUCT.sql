












CREATE               PROCEDURE A_SP_PROCEDURE_CREATE_E_ACCESS_PRODUCT
	@myID varchar(50),
	@strNTLogin varchar(50)
as
begin Transaction

print 'First check that this is not just a system procedure'
declare @t1 as varchar(50)
SELECT @t1 = p.ID FROM A_PROCEDURES p,A_PROCEDURES_HISTORY h where
p.ID = @myID AND
p.HISTORY_REF_ID = h.ID AND IS_SYSTEM = 1
if @t1 is not null goto fin
print 'creating the E-Acces sProduct for ' + @myID
declare @creatorID as varchar(50) --Get the ID of the person that created this procedure
declare @procName as varchar(50) --Get the ID of the System procedure for E-Access
declare @myDept as varchar(50) --Get the department of the person that created this procedure
SELECT @creatorID = CREATED_BY,@procName = NAME FROM A_O_PROCEDURES WHERE ROOT = @myID
if @@ERROR <> 0 goto problem
print 'CreatorID = ' + isNull(@creatorID,'NULL')
exec A_SP_PEOPLE_GET_DEPARTMENT_BY_ID @myDept OUTPUT, @creatorID
if @@ERROR <> 0 goto problem
print 'My department = ' + @myDept

declare @newID as varchar(50)
declare @messages as varchar(500)
declare @prodName as nvarchar(200),@procedureID as varchar(50)
declare @tester as varchar(50) --Test to see if there already is an E-Access product for the creating department 
SELECT @tester = ID FROM A_APPROVED_PRODUCTS 
WHERE SYSTEM_ID = 'SYS_E_ACCESS' 
AND STATUS LIKE 'APPROVED%' AND APP_OBJECT = @myID
if @@ERROR <> 0 goto problem
if @tester is Null --it is not in there so add it
	begin
		SELECT @procedureID = ID FROM A_V_PROCEDURES_APPROVED_DATA WHERE SYSTEM_ID = 'SYS_E_ACCESS'
		if @@ERROR <> 0 goto problem
		set @prodName = 'E-Access ' + isNull(@procName,'')
		print 'tester was null so i need to go ahead and make a Product'
		exec A_SP_PRODUCT_UPDATE_ONE_PRODUCT @newID OUTPUT,	@messages OUTPUT,null,null,@myDept,
		@prodName,null,@procedureID,@myID,null,null,null,null,null,null,NULL,NULL,NULL,NULL,NULL,@creatorID
		if @@ERROR <> 0 goto problem
		print 'Created the Product with obj ID = ' + isNull(@newID,'No Freakin ID')
--		declare @prodObjID as varchar(50)
--		SELECT @prodObjID = @newID
--		UPDATE A_OBJECTS SET STATUS = 'APPROVED', LOCKED_BY = NULL WHERE ID = @prodObjID
--		print 'Finishing the product workflow'
--		exec A_SP_PRODUCTS_FINISH_WF @newID,@newID,@creatorID
--		print 'Approved the crazy thing now making a price for the bastard'
--		declare @priceObj as varchar(50)
--		exec A_SP_PROD_PRICE_LIST_UPDATE
--			@priceObj OUTPUT,@messages OUTPUT,null, --objID
--			@newID,@myDept,null,null,'0', --unit PRice
--			null,null,null,null,null,null,null,null, --prod time unit
--			null,null,null,null,@creatorID
--		UPDATE A_OBJECTS SET STATUS = 'APPROVED', LOCKED_BY = NULL WHERE ID = @priceObj
--		print 'Finishing the prod price workflow'
--		exec A_SP_PROD_PRICE_LIST_FINISH_APPROVAL_WF @priceObj,@priceObj,@creatorID
--		print 'Approved the price'
	end
set @tester = null
SELECT @tester = ID FROM A_APPROVED_PRODUCTS 
WHERE STATUS LIKE 'APPROVED%' AND PROCEDURE_ID = @myID
if @@ERROR <> 0 goto problem
if @tester is Null --it is not in there so add it
	begin
	print 'Creating the service Product for this procedure'
	declare @servProdID as varchar(50)
	exec A_SP_PRODUCT_UPDATE_ONE_PRODUCT @servProdID OUTPUT,@messages OUTPUT,NULL,NULL,@myDept,@procName,NULL,@myID,NULL,
	NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,@creatorID
	if @@ERROR <> 0 goto problem
	print 'Finishing the workflow for the service Product'
--		UPDATE A_OBJECTS SET STATUS = 'APPROVED', LOCKED_BY = NULL WHERE ID = @servProdID
--		exec A_SP_PRODUCTS_FINISH_WF @servProdID,@servProdID,@creatorID
--		print 'Making a price for the service Product'
--		exec A_SP_PROD_PRICE_LIST_UPDATE
--		@priceObj OUTPUT,@messages OUTPUT,null,@servProdID,@myDept,null,null,'0',null,null,null,null,null,null,null,null,null,null,null,null,@creatorID
	end

COMMIT TRANSACTION
return 0
fin:
COMMIT TRANSACTION
print 'This is a system procedure so I will not make the Product cause that is really not necessary'
return 0
Problem:
print 'Error in A_SP_PROCEDURE_CREATE_E_ACCESS_PRODUCT'
if @@trancount > 0 	ROLLBACK TRANSACTION
return 1









