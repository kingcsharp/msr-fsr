










CREATE                  PROCEDURE [dbo].[A_SP_PRODUCTS_FINISH_WF]
	@myID nvarchar(50),
	@objID nvarchar(50),
	@strNTLogin nvarchar(50)
as
print 'Finishing the wf for a objID = ' + isNull(@objID,'IS NULL')
declare @myRoot as nvarchar(50) --get the root which is the ID of A_PREPOP
declare @ID as nvarchar(50) --get my ID  in the A_PREPOP_HISTORY table
SELECT @myRoot = ROOT,@ID = OBJ_ID FROM A_OBJECTS WHERE ID = @objID
declare @tester as nvarchar(50) --Test to see if it is in the table
SELECT @tester = ID FROM A_PRODUCTS WHERE ID = @myRoot 
if @tester is Null --it is not so add it
	begin
		print 'tester was null so i need to go ahead and make a record'
		INSERT INTO A_PRODUCTS(ID,HISTORY_REF_ID,DRCM,MODBY) VALUES (@myRoot,@ID,getDate(),@strNTLogin)
	end
--so now make sure that the value in A_PREPOP is Approved
declare @curID as nvarchar(50)
SELECT @curID = OBJ_ID FROM A_OBJECTS WHERE ROOT = @myROOT AND STATUS IN
('APPROVED','APPROVED_BUT_REVISING','APPROVED_BUT_DELETING')
if @curID is null UPDATE A_PRODUCTS SET STATUS = 'DELETED' WHERE ID = @myRoot
else UPDATE A_PRODUCTS SET STATUS = 'APPROVED' WHERE ID = @myRoot
exec A_SP_UPDATE_TABLE_FIELD @curID,@myRoot,'A_PRODUCTS','HISTORY_REF_ID'
declare @sql varchar(4000)
exec A_SP_PRODUCT_SET_UP_AUTO_MAIN_ACCOUNT @myRoot,@strNTLogin

--exec A_SP_PRODUCT_CREATE_PRICELIST @myRoot,@strNTLogin
--Update all the product price lists where this product is named

declare @prodName nvarchar(50)
SELECT @prodName = NAME FROM A_PRODUCTS_HISTORY WHERE ID = @curID
UPDATE A_PROD_PRICE_LIST_HISTORY SET PRODUCT_NAME = @prodName WHERE PRODUCT = @myRoot


exec A_SP_PRODUCTS_AUTO_CREATE_PRICE_LIST_AND_PURCHASE_AGREEMENT @objID,@strNTLogin

set @sql = 'exec A_SP_PRODUCTS_AUTO_CREATE_PRICE_LIST_AND_PURCHASE_AGREEMENT ' + 
'''' + @objID + ''',' +
'''' + @strNTLogin  + ''''
print @sql
--exec A_SP_ADMIN_SQL_TO_RUN_QUE_UP @sql,@strNTLogin










