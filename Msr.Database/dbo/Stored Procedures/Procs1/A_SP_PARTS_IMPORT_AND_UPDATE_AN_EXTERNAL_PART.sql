




CREATE      procedure dbo.A_SP_PARTS_IMPORT_AND_UPDATE_AN_EXTERNAL_PART
@newID varchar(2000) OUTPUT,
@retPartID varchar(2000)OUTPUT,
@externalPartID varchar(100),
@partName varchar(2000),
@strNTLogin varchar(50)
AS
declare @internalPartID varchar(50)
declare @histID varchar(50),@objID varchar(50),@internalPartRootID varchar(50)
declare @rootCo varchar(50)
SELECT @rootCo = ROOT_COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @strNTLogin
declare @newExtID varchar(100)
set @newExtID = @rootCo + '___' + @externalPartID

SELECT @internalPartRootID = ROOT_OBJ_ID 
	FROM A_OBJECT_EXTERNAL_REF 
	WHERE EXTERNAL_REF_ID = @newExtID
	and ITEM_TABLE = 'A_PARTS_HISTORY'

if @internalPartRootID is not null
	begin
	SELECT @histID = PARTS_HISTORY_ID FROM A_PARTS WHERE ID = @internalPartRootID
	print 'int root id = ' + @internalPartRootID
	print 'histid = ' + @histID
	SELECT @objID = OBJECT_ID FROM A_PARTS_HISTORY WHERE ID = @histID
	UPDATE A_PARTS_HISTORY SET 
		NAME = @partName
	WHERE ID = @histID
	set @newID = 'Updated the part ' + @partName
	end
else
	begin
	declare @msgs varchar(5000)
	exec A_SP_PARTS_UPDATE_ONE_PART
	@objID OUTPUT,
	@msgs OUTPUT, --@messages nvarchar(500) OUTPUT,
	null, --@objID nvarchar(50),
	@rootCo, --@COMPANY  nvarchar(50),
	@externalPartID, --@COMPANY_PART_NUMBER  nvarchar(50),
	@partName, --@NAME nvarchar(100),
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
	@strNTLogin
	print '#######################The new OBJECT_ID is ' + isnull(@objID,'NULL')
	UPDATE A_OBJECTS SET STATUS = 'APPROVED',UNLOCKED_BY = @strNTLogin,LOCKED_BY = NULL,LOCKED_BY_NAME = NULL
	WHERE ID = @objID
	SELECT @histID = ID FROM A_PARTS_HISTORY WHERE OBJECT_ID = @objID
	declare @sql varchar(2000)
	exec A_SP_PARTS_FINISH_WF @histID,@objID,@strNTLogin
	INSERT INTO A_OBJECT_EXTERNAL_REF 
	(ID,HISTORY_ID,ROOT_OBJ_ID,ITEM_TABLE,EXTERNAL_REF_ID,MODBY,DRCM)
	VALUES
	(newID(),@histID,@objID,'A_PARTS_HISTORY',@newExtID,@strNTLogin,getDate())
	set @newID = 'Created the part ' + @partName
	set @retPartID = @objID
	end






