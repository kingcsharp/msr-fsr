







CREATE      procedure A_SP_OBJECT_CHECK_FOR_VALIDITY
	@returnVal nvarchar(1000) OUTPUT,
	@messages nvarchar(1000) OUTPUT,
	@objID nvarchar(50),
	@strNTLogin nvarchar(50)
as
print 'Entering A_SP_OBJECT_CHECK_FOR_VALIDITY'
declare @myTable as nvarchar(50)
declare @myID as nvarchar(50)
declare @retVal as nvarchar(2000)

SELECT @myTable = OBJ_TABLE,@myID = OBJ_ID FROM A_OBJECTS WHERE ID = @objID
print 'MyTable = ' + @myTable
--if @myTable = 'A_ROLES_HISTORY'
--	exec A_SP_ROLES_FINISH_APPROVAL_WF @myID,@strNTLogin
if @myTable = 'A_PEOPLE_HISTORY'
	exec A_SP_PEOPLE_CHECK_VALIDITY @retVal OUTPUT,@myID,@strNTLogin
--if @myTable = 'A_LOCATIONS_HISTORY'
--	exec A_SP_LOCATIONS_FINISH_WF @myID,@objID,@strNTLogin
--if @myTable = 'A_REGIONS_HISTORY'
--	exec A_SP_REGIONS_FINISH_WF @myID,@objID,@strNTLogin
if @myTable = 'A_COMPANIES_HISTORY'
	exec A_SP_COMPANY_CHECK_VALIDITY @retVal OUTPUT,@myID,@strNTLogin
--if @myTable = 'A_PARTS_HISTORY'
--	exec A_SP_PARTS_FINISH_WF @myID,@objID,@strNTLogin
--if @myTable = 'A_PART_TYPES_HISTORY'
--		exec A_SP_PART_TYPES_FINISH_WF @myID,@objID,@strNTLogin

if @myTable = 'A_PRODUCTS_HISTORY'
	exec A_SP_PRODUCTS_CHECK_VALIDITY @retVal OUTPUT,@myID,@strNTLogin


set @returnVal = @retVal
print 'Return Val = ' + isNull(@returnVal,'NULL')






