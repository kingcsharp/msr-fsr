










CREATE            procedure dbo.A_SP_ACTUAL_PARTS_IMPORT_AND_UPDATE_AN_EXTERNAL_ACTUAL_PART
@newID varchar(1000) output,
@msgs varchar(2000) output,
@externalActualPartUniqueID varchar(50),
@externalPartID varchar(50),
@externalOwnerID varchar(50),
@externalPartSN varchar(100),
@externalPartNickName nvarchar(1000),
@qty float,
@externalLocID varchar(50),
@parentID varchar(50),
@strNTLogin varchar(50)
AS
declare @internalActualPartRootID varchar(50),@internalPartID varchar(50),@internalCoID varchar(50)
declare @histID varchar(50),@objID varchar(50),@internalLocID varchar(50)
declare @rootCo varchar(50)
SELECT @rootCo = ROOT_COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @strNTLogin
declare @newExtID varchar(100)
set @newExtID = @rootCo + '___' + @externalActualPartUniqueID
if @externalLocID is not null
begin
SELECT @internalLocID = ROOT_OBJ_ID 
	FROM A_OBJECT_EXTERNAL_REF 
	WHERE EXTERNAL_REF_ID = @rootCo + '___' + @externalLocID
	and ITEM_TABLE = 'A_LOCATIONS_HISTORY'
if @internalLocID is null
	begin
		print 'Loc is null'
		SELECT @internalLocID = ID FROM A_LOCATIONS WHERE ID = @externalLocID
		if @internalLocID is null
			begin
			print 'Loc is still null'
			set @newID = 'This Location id = ' + @externalLocID + ' does not exist.  All the companies must be imported prior to importing the Actual Parts.'
			goto problem
			end
	end
end
set @newID = @newID + ' There is no location '



SELECT @internalCoID = ROOT_OBJ_ID 
	FROM A_OBJECT_EXTERNAL_REF 
	WHERE EXTERNAL_REF_ID = @rootCo + '___' + @externalOwnerID
	and ITEM_TABLE = 'A_COMPANIES_HISTORY'
if @internalCoID is null
	begin
		SELECT @internalCoID = ID FROM A_COMPANIES WHERE ID = @externalOwnerID
		if @internalCoID is null
			begin
			set @newID = 'ERROR--This Company id = ' + @externalOwnerID + ' does not exist.  All the companies must be imported prior to importing the Actual Parts.'
			goto problem
			end
	end

SELECT @internalPartID = ROOT_OBJ_ID 
	FROM A_OBJECT_EXTERNAL_REF 
	WHERE EXTERNAL_REF_ID = @rootCo + '___' + @externalPartID
	and ITEM_TABLE = 'A_PARTS_HISTORY'
if @internalPartID is null
	begin
		SELECT @internalPartID = ID FROM A_PARTS WHERE ID = @externalPartID
		if @internalPartID is null
			begin
			set @newID = 'ERROR--This Part id ' + @externalPartID + ' does not exist.  All the parts must be imported prior to imoprting the Actual Parts.'
			goto problem
			end
	end

SELECT @internalActualPartRootID = ROOT_OBJ_ID 
	FROM A_OBJECT_EXTERNAL_REF 
	WHERE EXTERNAL_REF_ID = @newExtID
	and ITEM_TABLE = 'A_ACTUAL_PARTS_HISTORY'

print 'Rollin'

declare @intParentRootID varchar(50),@intParentHistID varchar(50),@intParentObjID  varchar(50)
if @parentID is not null
	begin
	if isnull(@parentID,'') <> @externalActualPartUniqueID
		begin
		exec A_SP_Z_EXT_OBJ_GET_INTERNAL_DATA
			@intParentRootID OUTPUT,@intParentHistID OUTPUT,@intParentObjID OUTPUT,
			@parentID,'A_ACTUAL_PARTS_HISTORY',@strNTLogin
		if @intParentRootID is null
			begin
				set @newID = 'ERROR--This Parent id ' + @intParentRootID + ' does not exist.  All the parts must be imported prior to imoprting the Actual Parts.'
				goto problem
			end
		end
	end




if @internalActualPartRootID is not null
	begin
	SELECT @histID = HISTORY_REF_ID FROM A_ACTUAL_PARTS WHERE ID = @internalActualPartRootID
	print 'int root id = ' + @internalActualPartRootID
	print 'histid = ' + @histID
	SELECT @objID = OBJECT_ID FROM A_ACTUAL_PARTS_HISTORY WHERE ID = @histID
	UPDATE A_ACTUAL_PARTS_HISTORY SET 
	NICK_NAME = @externalPartNickName,
	SERIAL = @externalPartSN,
	PART_ID = @internalPartID,
	CUR_OWNER = @internalCoID,
	LOCATION = @internalLocID,
	PARENT_ID = @intParentRootID
	WHERE ID = @histID
	set @newID = 'Updated this part.'
	end
else
	begin
	print 'Creating the part ' + @externalPartSN

	exec A_SP_ACTUAL_PARTS_UPDATE_PART
	@objID OUTPUT,
	@msgs OUTPUT,
	null,
	@internalPartID,
	@qty,
	@externalPartSN,
	@externalPartNickName,
	@internalLocID,
	@internalCoID,
	null,
	null,
	@intParentRootID,
	null,
	null,
	@strNTLogin


	print '#######################The new OBJECT_ID is ' + isnull(@objID,'NULL')
	UPDATE A_OBJECTS SET STATUS = 'APPROVED',UNLOCKED_BY = @strNTLogin,LOCKED_BY = NULL,LOCKED_BY_NAME = NULL
	WHERE ID = @objID
	SELECT @histID = ID FROM A_ACTUAL_PARTS_HISTORY WHERE OBJECT_ID = @objID
	declare @sql varchar(3000),@myDate varchar(50)
	set @myDate = convert(varchar(50),dateAdd(n,10,getDate()))
	set @sql = 'exec A_SP_ACTUAL_PARTS_FINISH_WF ''' + @histID + ''',''' + 
			@objID + ''',''' + @strNTLogin + ''''
	exec A_SP_ADMIN_SQL_TO_RUN_QUE_UP_WITH_DATE @sql,@myDate,@strNTLogin

	INSERT INTO A_OBJECT_EXTERNAL_REF 
	(ID,HISTORY_ID,ROOT_OBJ_ID,ITEM_TABLE,EXTERNAL_REF_ID,MODBY,DRCM)
	VALUES
	(newID(),@histID,@objID,'A_ACTUAL_PARTS_HISTORY',@newExtID,@strNTLogin,getDate())
	set @newID = 'Created this part '
	end

set @newID = 'Success... ' + @newID
goto fin


problem:
print @newID
set @newID = 'ERROR ' + @newID
fin:
print @newID


SELECT @newID AS RESULT





