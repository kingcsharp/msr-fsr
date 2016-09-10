





CREATE      PROCEDURE dbo.A_SP_PARTS_CREATE_AUTO_PRODUCT
@objID as varchar(50),
@strNTLogin as varchar(50)
AS
BEGIN TRANSACTION
declare @creatingDept as varchar(50),
	@creatingPerson as varchar(50),
	@root as varchar(50),
	@tester as varchar(50),
	@pName as varchar(500),
	@psID as varchar(50)
print 'objID = ' + @objID
SELECT @pName = OBJ_DESC,@ROOT = ROOT, @creatingPerson = CREATED_BY FROM A_OBJECTS WHERE ID = @objID
SELECT @creatingDept = COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @creatingPerson
print 'Root ' + @ROOT
print 'Creating Dept = ' + @creatingDept
print 'creator = ' + @creatingPerson
set @psID = dbo.A_FN_PROCEDURE_GET_PROVIDE_AND_STAY_ID(@creatingPerson)


SELECT @tester = ID FROM A_PRODUCTS_HISTORY 
WHERE APP_OBJECT = @ROOT AND 
PROCEDURE_ID = @psID

if @tester is not null
	begin
	print 'This Product is already made'
	goto fin
	end

declare @newID as varchar(50)
declare @msg as nvarchar(4000)
exec A_SP_PRODUCT_UPDATE_ONE_PRODUCT
@newID OUTPUT,
@msg OUTPUT,
null,
null,
@creatingDept,
@pName,
null,
@psID,
@ROOT,
'0',
NULL,
NULL,
'1',
NULL,
NULL,
NULL,NULL,NULL,NULL,NULL,
@creatingPerson

UPDATE A_OBJECTS 
	SET STATUS = 'APPROVED',LOCKED_BY=NULL,LOCKED_BY_NAME=NULL 
	WHERE ID = @newID

exec A_SP_PRODUCTS_FINISH_WF null,@newID,@creatingPerson
exec A_SP_PART_PRODUCT_MAKE_PRICE_LIST @objID,@newID,@creatingPerson


if @@ERROR <> 0 goto problem



fin:
if @@trancount > 0 COMMIT TRANSACTION
print 'Finished A_SP_PARTS_CREATE_AUTO_PRODUCT with no errors'
return 0

PROBLEM:
if @@trancount > 0 	ROLLBACK TRANSACTION
print ' There was a problem in A_SP_PARTS_CREATE_AUTO_PRODUCT and we will terminate and not finish anything '
return 1






