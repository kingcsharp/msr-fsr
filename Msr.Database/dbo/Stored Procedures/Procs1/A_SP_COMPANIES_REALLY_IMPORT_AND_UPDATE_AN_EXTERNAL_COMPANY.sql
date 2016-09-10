
--if the @retMsg is null then there must have been an error.  There shold always be a return value

CREATE     procedure dbo.A_SP_COMPANIES_REALLY_IMPORT_AND_UPDATE_AN_EXTERNAL_COMPANY
@retMsg varchar(2000) OUTPUT,
@externalCoID varchar(100),
@coName nvarchar(2000),
@internalParentID varchar(50),
@strNTLogin varchar(50)
AS
declare @internalCoID varchar(50),@newID nvarchar(50),@messages nvarchar(500)
declare @histID varchar(50),@objID varchar(50),@internalCoRootID varchar(50)
declare @rootCo varchar(50)
SELECT @rootCo = ROOT_COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @strNTLogin

--first check to see if it is already in the database
SELECT @internalCoRootID = ROOT_OBJ_ID 
	FROM A_OBJECT_EXTERNAL_REF 
	WHERE EXTERNAL_REF_ID = @rootCo + '___' + @externalCoID
--if it is then we just need to update it and return a message that we updated it
if @internalCoRootID is not null
	begin
	SELECT @histID = HISTORY_REF_ID FROM A_COMPANIES WHERE ID = @internalCoRootID
	print 'int root id = ' + @internalCoRootID
	print 'histid = ' + @histID
	SELECT @objID = OBJECT_ID FROM A_COMPANIES_HISTORY WHERE ID = @histID
	UPDATE A_COMPANIES_HISTORY SET 
		PARENT = @internalParentID,
		NAME = @coName
	WHERE ID = @histID
	set @retMsg = @coName + ' Updated. '
	end
else
	begin
--since it does not exist we will have to create it
	exec A_SP_COMPANIES_UPDATE_ONE_COMPANY
	@newID OUTPUT,
	@messages OUTPUT,
	@objID,
	@coName,
	'COMPANY', --@TYPE
	@internalParentID,
	null, --@PHONE nvarchar(50),
	null, --@HEAD_PEOPLE varchar(8000),
	null, --@LOCATION_ID varchar(50),
	null, --@CO_SUP_PRODS varchar(8000),
	null, --@PIC_FILES varchar(8000),
	null, --@LOGO_FILES varchar(8000),
	null, --@REFERENCE_FILES varchar(8000),
	null, --@NEW_PERSON_LOGIN nvarchar(200),
	null, --@NEW_PERSON_PASSWORD nvarchar(50),
	null, --@NEW_PERSON_EMAIL nvarchar(200),
	null, --@NEW_PERSON_FIRST_NAME nvarchar(200),
	null, --@NEW_PERSON_LAST_NAME nvarchar(200),
	@strNTLogin
	UPDATE A_OBJECTS SET STATUS = 'APPROVED',UNLOCKED_BY = @strNTLogin,LOCKED_BY = NULL,LOCKED_BY_NAME = NULL
	WHERE ID = @newID
	SELECT @histID = ID FROM A_COMPANIES_HISTORY WHERE OBJECT_ID = @newID
	exec A_SP_COMPANIES_FINISH_WF @histID,@newID,@strNTLogin
	INSERT INTO A_OBJECT_EXTERNAL_REF 
	(ID,HISTORY_ID,ROOT_OBJ_ID,ITEM_TABLE,EXTERNAL_REF_ID,MODBY,DRCM)
	VALUES
	(newID(),@histID,@newID,'A_COMPANIES_HISTORY',@rootCo + '___' + @externalCoID,@strNTLogin,getDate())
--return that we created it
	set @retMsg = @coName + ' Created. '
	end




