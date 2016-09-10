







CREATE      PROCEDURE [dbo].[A_SP_PART_CREATE_SERVICE_PRODUCT]
@objID as varchar(50),
@strNTLogin as varchar(50)
AS
BEGIN TRANSACTION
DECLARE @wfsID varchar(50)
SELECT @wfsID = WFS_ID FROM A_OBJECTS WHERE ID = @objID
declare @creatorID varchar(50)
SELECT @creatorID = STARTED_BY FROM A_WORKFLOWS_STARTED WHERE ID = @wfsID

print 'Creating a service product for obj = ' + @objID
declare @ID varchar(50),@PART_NAME nvarchar(200),@procName nvarchar(500),@supplierCo varchar(50)
SELECT @ID = ID,@PART_NAME = NAME,@procName = isNull(PROC_VERB + ' ','Service to ') + isNull(NAME,' Unnamed Part'),
	@supplierCo = SUPPLIER_CO
	FROM A_PARTS_HISTORY WHERE OBJECT_ID = @objID

declare @creatingCo varchar(50)
SELECT @creatingCo = ROOT_COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @creatorID

declare @procObjID varchar(50),@procMessages nvarchar(500)
exec A_SP_PROCEDURES_UPDATE_ONE_PROCEDURE
	@procObjID OUTPUT,
	@procMessages OUTPUT,
	null,
	@creatingCo,
	null,
	@procName,
	null,
	0,
	0,
	4,
	null,
	0,
	'TIME_SYS_MINUTES',
-- Removed by Greg 02-13-2014
--	NULL,NULL,NULL,NULL,NULL,
	@creatorID

UPDATE A_OBJECTS 
	SET STATUS = 'APPROVED',LOCKED_BY=NULL,LOCKED_BY_NAME=NULL 
	WHERE ID = @procObjID
exec A_SP_PROCEDURES_FINISH_WF null,@procObjId,@creatorID

declare @creatingDept as varchar(50),
	@creatingPerson as varchar(50),
	@root as varchar(50),
	@tester as varchar(50),
	@pName as varchar(500),
	@psID as varchar(50)
print 'objID = ' + @objID
SELECT @pName = @procName,@ROOT = ROOT, @creatingPerson = CREATED_BY FROM A_OBJECTS WHERE ID = @objID
SELECT @creatingDept = COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @creatingPerson
print 'Root ' + @ROOT
print 'Creating Dept = ' + @creatingDept
print 'creator = ' + @creatingPerson
set @psID = @procObjID


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
@supplierCo,
@pName,
'',
@psID,
@ROOT,
'0',
NULL,
NULL,
'1',
NULL,
NULL,
-- Edit by Greg 2/13/2014
NULL,
NULL,
NULL,
NULL,
NULL,
-- end Edit
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








