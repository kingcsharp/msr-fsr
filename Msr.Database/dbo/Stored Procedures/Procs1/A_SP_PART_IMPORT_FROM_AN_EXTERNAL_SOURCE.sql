
CREATE  procedure dbo.A_SP_PART_IMPORT_FROM_AN_EXTERNAL_SOURCE
@myID varchar(50),
@myMsgs nvarchar(2000),
@externalPartID varchar(100),
@partName varchar(50),
@strNTLogin varchar(50)
AS
 declare @co varchar(50)
 SELECT @co = ROOT_COMPANY FROM A_PEOPLE_APPROVED_DATA WHERE ID = @strNTLogin
-- 
-- declare @internalPartID varchar(50),@messages nvarchar(500)
-- declare @histID varchar(50),@objID varchar(50),@internalPartRootID varchar(50)
-- SELECT @internalPartRootID = ROOT_OBJ_ID 
-- 	FROM A_OBJECT_EXTERNAL_REF 
-- 	WHERE EXTERNAL_REF_ID = @externalCoID
-- 	AND ITEM_TABLE = 'A_PARTS_HISTORY'
-- if @internalPartRootID is not null
-- 	begin
-- 	SELECT @histID = HISTORY_REF_ID FROM A_PARTS WHERE ID = @internalPartRootID
-- 	print 'int root id = ' + @internalPartRootID
-- 	print 'histid = ' + @histID
-- 	SELECT @objID = OBJECT_ID FROM A_PARTS_HISTORY WHERE ID = @histID
-- 	UPDATE A_PARTS_HISTORY SET 
-- 		NAME = @partName
-- 	WHERE ID = @histID
-- 	end
-- else
-- 	begin
-- 	exec A_SP_PARTS_UPDATE_ONE_PART
-- 	@newObjID OUTPUT,
-- 	@messages OUTPUT,
-- 	null,
-- 	@COMPANY  nvarchar(50),
-- 	@COMPANY_PART_NUMBER  nvarchar(50),
-- 	@NAME nvarchar(100),
-- 	@PART_TYPE  nvarchar(50),
-- 	@SPARE  nvarchar(50),
-- 	@CONSUMABLE  nvarchar(50),
-- 	@UNIT  nvarchar(50),
-- 	@UNIT_SHIPPING_WEIGHT  nvarchar(50),
-- 	@subParts varchar(8000),
-- 	@CUSTOMER_SEE_AVAILABILITY varchar(50),
-- 	@SUPPLIER_SEE_AVAILABILITY varchar(50),
-- 	@SUPPLIER_SEE_INSTALL_BASE varchar(50),
-- 	@INTERNAL_EQUAL_PARTS varchar(8000),
-- 	@WEIGHT_TYPE varchar(50),
-- 	@CREATE_PROD tinyInt,
-- 	@SUPPLIER_CO varchar(50),
-- 	@PRODUCT_TYPE varchar(50),
-- 	@PROC_VERB nvarchar(100),
-- 	@SPECIAL_CUSTOMER varchar(8000),
-- 	@CUSTOMER_EXCEPTIONS varchar(8000),
-- 	@CUSTOMERS varchar(8000),
-- 	@PRICE varchar(50),
-- 	@strNTLogin nvarchar(50)
-- 
-- 
-- 	UPDATE A_OBJECTS SET STATUS = 'APPROVED',UNLOCKED_BY = @strNTLogin,LOCKED_BY = NULL,LOCKED_BY_NAME = NULL
-- 	WHERE ID = @newID
-- 	SELECT @histID = ID FROM A_COMPANIES_HISTORY WHERE OBJECT_ID = @newID
-- 	exec A_SP_COMPANIES_FINISH_WF @histID,@newID,@strNTLogin
-- 	INSERT INTO A_OBJECT_EXTERNAL_REF 
-- 	(ID,HISTORY_ID,ROOT_OBJ_ID,ITEM_TABLE,EXTERNAL_REF_ID,MODBY,DRCM)
-- 	VALUES
-- 	(newID(),@histID,@newID,'A_COMPANIES_HISTORY',@externalCoID,@strNTLogin,getDate())
-- 	end
-- 

