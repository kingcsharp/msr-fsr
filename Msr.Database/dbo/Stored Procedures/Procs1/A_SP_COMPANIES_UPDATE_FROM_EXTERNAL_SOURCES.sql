





CREATE      PROCEDURE dbo.A_SP_COMPANIES_UPDATE_FROM_EXTERNAL_SOURCES 
@newID varchar(2000) OUTPUT,
@messages varchar(2000) OUTPUT,
@externalCoID varchar(100),
@externalParentID varchar(100),
@coName nvarchar(2000),
@parentName nvarchar(2000),
@internalParent varchar(50),
@strNTLogin varchar(50)
AS
declare @rootCo varchar(50)
SELECT @rootCo = ROOT_COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @strNTLogin
if @externalCoID is null or @externalCoID = ''
	begin
	set @newID = 'Can not import a company with no ID' 
	goto problem
	end
if @coName is null or @coName = ''
	begin
	set @newID = 'Can not import a company with no Name' 
	goto problem
	end


declare @intID varchar(50),@intObjID varchar(50),@intRootObjID varchar(50),
@retMsg varchar(2000)
set @newID = 'External CO = ' + isNull(@externalPArentID,'NULL')
if @externalParentID is not null and @externalPArentID <> '' and @externalParentID <> @externalCoID and @externalParentID <> 'OTHER'
	begin
	set @newID = @newID + ' This one has an External Parent Reference. '
	exec A_SP_COMPANIES_REALLY_IMPORT_AND_UPDATE_AN_EXTERNAL_COMPANY
			@retMsg output,@externalParentID,@parentName,@internalParent,@strNTLogin
	if @retMsg is null
		begin
		set @newID = @newID + 'Error Creating or updating the external parent ' + @externalParentID
		goto problem
		end
	select @internalParent = ROOT_OBJ_ID FROM A_OBJECT_EXTERNAL_REF WHERE EXTERNAL_REF_ID = @rootCo + '___' + @externalParentID
	set @newID = @newID + @retMsg + ' the external parent = ' + @externalParentID + ' without error.'
	end --updating the parent

print 'now make the child co'
set @retMsg = null
exec A_SP_COMPANIES_REALLY_IMPORT_AND_UPDATE_AN_EXTERNAL_COMPANY
	@retMsg output,@externalCoID,@coName,@internalParent,@strNTLogin
if @retMsg is null
	begin
	set @newID = @newID + 'Error Creating or updating the external company ' + @externalCoID
	goto problem
	end
set @newID = @newID + @retMsg + ' external company ' + @externalCoID

goto fin

problem:
set @newID = 'Error ' + @newID
fin:






