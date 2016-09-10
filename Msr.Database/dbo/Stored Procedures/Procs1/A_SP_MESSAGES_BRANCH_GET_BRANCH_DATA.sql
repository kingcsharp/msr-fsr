CREATE                   PROCEDURE A_SP_MESSAGES_BRANCH_GET_BRANCH_DATA
@messageID varchar(50),
@strNTLogin nvarchar(50)
AS
print 'processing id of ' +isNull(@messageID, 'Null')
declare @hasRefFile as varchar(50)
declare @hasProcedure as varchar(50)
declare @hasBusinessPurpose as varchar(50)
declare @hasObject as varchar(50)
declare @hasProject as varchar(50)
declare @parentID varchar(50)



CREATE TABLE #tempToRootList(
							ID varchar(50),
							HAS_REF_FILE varchar(50),
							HAS_PROCEDURE varchar(50),
							HAS_BUSINESS_PURPOSE varchar(50),
							HAS_OBJECT varchar(50),
							HAS_PROJECT varchar(50)
							)

WHILE @messageID is not null 
	begin	
	set @hasRefFile = null
	set @hasProcedure = null
	set @hasBusinessPurpose = null
	set @hasObject = null
	set @hasProject = null

	SELECT @hasRefFile = ID from A_MESSAGES_ATTACHMENTS WHERE MESSAGE_ID = @messageID
	print 'Checking to see it my first one has a procedure for MESSAGE_ID of ' + isNull(@messageID,'NULL')
	SELECT @hasProcedure = ID from A_MESSAGES_PROCEDURE_LINK WHERE MESSAGE_ID = @messageID
	SELECT @hasBusinessPurpose = ID from A_BUSINESS_PURPOSES_ITEM_LINK WHERE ITEM_ID = @messageID AND ITEM_TYPE = 'A_MESSAGES'
	SELECT @hasObject = ID from A_OBJECT_ITEM_LINK WHERE ITEM_ID = @messageID AND ITEM_TYPE = 'A_MESSAGES'
	
	if @hasRefFile is not null
		begin
		print 'I have a reference file so setting @hasRefFile to 1 for MESSAGE ID OF ' + isNull(@messageID,'NULL') 
		set @hasRefFile = '1'
		end 
	else
		begin
		print 'I do not have a reference file so setting @hasRefFile to 0 for MESSAGE ID of'  + isNull(@messageID,'NULL')
		set @hasRefFile = '0'
		end 
	if @hasProcedure is not null
		begin
		print 'I have a reference file so setting @hasProcedure to 1 for MESSAGE ID of' + isNull(@messageID,'NULL')
		set @hasProcedure = '1'
		end 
	else
		begin
		print 'I do not have a procedure so setting @hasProcedure to 0 for MESSAGE ID of' + isNull(@messageID,'NULL')
		set @hasProcedure = '0'
		end 
	if @hasBusinessPurpose is not null
		begin
		print 'I have a business purpose so setting @hasBusinessPurpose to 1 for MESSAGE ID of' + isNull(@messageID,'NULL')
		set @hasBusinessPurpose = '1'
		end 
	else
		begin
		print 'I do not have a business purpose so setting @hasBusinessPurpose to 0 for MESSAGE ID of' + isNull(@messageID,'NULL')
		set @hasBusinessPurpose = '0'
		end 
	if @hasObject is not null
		begin
		print 'I have a object so setting @hasObject to 1 for MESSAGE ID of' + isNull(@messageID,'NULL')
		set @hasObject = '1'
		end 
	else
		begin
		print 'I do not have a object so setting @hasObject to 0 for MESSAGE ID of' + isNull(@messageID,'NULL')
		set @hasObject = '0'
		end 
	if @hasProject is not null
		begin
		print 'Need to add @hasProject code. '
		print 'I have a project FOR MY FIRST ID so setting @hasProject to 1 for MESSAGE ID of' + isNull(@messageID,'NULL')
		set @hasProject = '1'
		end 
	else
		begin
		print 'Need to add @hasProject code. '
		print 'I do not have a proejct FOR MY FIRST ID so setting @hasProject to 0 for MESSAGE ID of' + isNull(@messageID,'NULL')
		set @hasProject = '0'
		end 
	set @parentID = null
 	SELECT @parentID = PARENT_ID
		FROM A_MESSAGES 
	WHERE ID = @messageID
	INSERT INTO #tempToRootList (ID,HAS_REF_FILE,HAS_PROCEDURE,HAS_BUSINESS_PURPOSE,HAS_OBJECT,HAS_PROJECT) VALUES (@messageID,@hasRefFile,@hasProcedure,@hasBusinessPurpose,@hasObject,@hasProject)
	set @messageID = @parentID
	end


SELECT DISTINCT R.ID ,
				R.HAS_REF_FILE AS HAS_REF_FILE,
				R.HAS_PROCEDURE AS HAS_PROCEDURE,
				R.HAS_BUSINESS_PURPOSE AS HAS_BUSINESS_PURPOSE,
				R.HAS_OBJECT AS HAS_OBJECT,
				R.HAS_PROJECT AS HAS_PROJECT,
				V.DATE_SENT AS DATE_SENT,
				V.SENDER_NAME AS SENDER_NAME,
				V.MESSAGE AS MESSAGE,	
				V.IMPORTANCE AS IMPORTANCE,
				V.HIDE_MESSAGE AS HIDE_MESSAGE
FROM #tempToRootList R, A_V_MESSAGES_SEARCH_DATA V
WHERE R.ID = V.ID











