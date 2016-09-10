
CREATE  PROCEDURE dbo.A_SP_PARTS_CREATE_BATCH_PART
@batchPartID varchar(50) OUTPUT,
@retVal integer OUTPUT,
@strNTLogin varchar(50)
AS
begin transaction
declare @newPartObjID varchar(50),@myCo varchar(50)
SELECT @myCO = ROOT_COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @strNTLogin
if @@ERROR <> 0 goto problem
exec A_SP_PARTS_UPDATE_ONE_PART 
@newPartObjID OUTPUT,
null,
null, --@objID nvarchar(50),
@myCo,
'BATCH',
'BATCH',
null, --@PART_TYPE  nvarchar(50),
null, --@SPARE  nvarchar(50),
null, --@CONSUMABLE  nvarchar(50),
null, --@UNIT  nvarchar(50),
null, --@UNIT_SHIPPING_WEIGHT  nvarchar(50),
null, --@subParts varchar(8000),
null, --@CUSTOMER_SEE_AVAILABILITY varchar(50),
null, --@SUPPLIER_SEE_AVAILABILITY varchar(50),
null, --@SUPPLIER_SEE_INSTALL_BASE varchar(50),
null, --@INTERNAL_EQUAL_PARTS varchar(8000),
null, --@WEIGHT_TYPE varchar(50),
null, --@CREATE_PROD tinyInt,
null, --@SUPPLIER_CO varchar(50),
null, --@PRODUCT_TYPE varchar(50),
null, --@PROC_VERB nvarchar(100),
null, --@SPECIAL_CUSTOMER varchar(8000),
null, --@CUSTOMER_EXCEPTIONS varchar(8000),
null, --@CUSTOMERS varchar(8000),
null, --@PRICE varchar(50),
@strNTLogin --nvarchar(50)
if @@ERROR <> 0 goto problem
exec A_SP_OBJECTS_QUICK_APPROVE @newPartObjID,@strNTLogin
if @@ERROR <> 0 goto problem
exec A_SP_PARTS_FINISH_WF null,@newPartObjID,@strNTLogin
if @@ERROR <> 0 goto problem
UPDATE A_PARTS SET IS_BATCH = 1 WHERE ID = @newPartObjID
if @@ERROR <> 0 goto problem
set @batchPartID = @newPartObjID
fin:
if @@trancount > 0 COMMIT TRANSACTION
print 'Finished creating a batch with no errors'
set @retval = 0

PROBLEM:
if @@trancount > 0 	ROLLBACK TRANSACTION
print 'There was a problem vreating a batch so we rolled everything back '
set @retval = 1




