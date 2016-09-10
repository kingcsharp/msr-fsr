













CREATE                PROCEDURE A_SP_THEORY_CREATE_E_ACCESS_PRODUCT
	@myID varchar(50),
	@strNTLogin varchar(50)
as
begin Transaction


print 'creating the E-Access Product for ' + @myID
declare @creatorID as varchar(50) --Get the ID of the person that created this theory
declare @theoryName as varchar(50) 
declare @myDept as varchar(50) --Get the department of the person that created this theory
SELECT @creatorID = CREATED_BY,@theoryName = NAME FROM A_O_THEORY WHERE ROOT = @myID
exec A_SP_PEOPLE_GET_DEPARTMENT_BY_ID @myDept OUTPUT, @creatorID
print 'My department = ' + @myDept

declare @tester as varchar(50) --Test to see if there already is an E-Access product for the creating department 
SELECT @tester = ID FROM A_APPROVED_PRODUCTS 
WHERE SUPPLIER_ID = @myDept AND SYSTEM_ID = 'SYS_E_ACCESS' 
AND STATUS = 'APPROVED' AND APP_OBJECT = @myID
if @tester is Null --it is not in there so add it
	begin
		declare @newID as varchar(50)
		declare @messages as varchar(500)
		declare @prodName as nvarchar(200),
				@procedureID as varchar(50)
		SELECT @procedureID = ID FROM A_V_PROCEDURES_APPROVED_DATA WHERE SYSTEM_ID = 'SYS_E_ACCESS'
		set @prodName = 'E-Access ' + isNull(@theoryName,'')
		print 'tester was null so i need to go ahead and make a Product'
		exec A_SP_PRODUCT_UPDATE_ONE_PRODUCT @newID OUTPUT,	@messages OUTPUT,null,null,@myDept,
		@prodName,null,@procedureID,@myID,null,null,null,null,null,null,NULL,NULL,NULL,NULL,NULL,@creatorID
		print 'Created the Product with obj ID = ' + isNull(@newID,'No Freakin ID')
		declare @prodObjID as varchar(50)
		SELECT @prodObjID = @newID
		UPDATE A_OBJECTS SET STATUS = 'APPROVED', LOCKED_BY = NULL WHERE ID = @prodObjID
		print 'Finishing the product workflow'
		exec A_SP_PRODUCTS_FINISH_WF @newID,@newID,@creatorID
		print 'Approved the crazy thing now making a price for the bastard'
		declare @priceObj as varchar(50)
		exec A_SP_PROD_PRICE_LIST_UPDATE
			@priceObj OUTPUT,@messages OUTPUT,null, --objID
			@newID,@myDept,null,null,'0', --unit PRice
			null,null,null,null,null,null,null,null, --prod time unit
			null,null,null,null,@creatorID
		UPDATE A_OBJECTS SET STATUS = 'APPROVED', LOCKED_BY = NULL WHERE ID = @priceObj
		print 'Finishing the prod price workflow'
		exec A_SP_PROD_PRICE_LIST_FINISH_APPROVAL_WF @priceObj,@priceObj,@creatorID
		print 'Approved the price'
	end

COMMIT TRANSACTION
return 0
fin:
COMMIT TRANSACTION
print 'This is a system procedure so I will not make the Product cause that is really not necessary'
return 0
Problem:
print 'Error in A_SP_THEORY_CREATE_E_ACCESS_PRODUCT'
if @@trancount > 0 	ROLLBACK TRANSACTION
return 1










