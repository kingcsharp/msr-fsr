


CREATE        PROCEDURE A_SP_MESSAGES_BRANCH_ADD_TO_BRANCH_TABLE
@ID varchar(50)
AS
declare @tester as varchar(50)
declare @hasRefFile as varchar(50)
declare @hasProcedure as varchar(50)
declare @hasBusinessPurpose as varchar(50)
declare @hasObject as varchar(50)
declare @hasProject as varchar(50)
set @tester = null
set @hasRefFile = null
set @hasProcedure = null

SELECT TOP 1 @tester = ID FROM A_MESSAGES WHERE PARENT_ID = (@ID)
print 'Checking for Child using ID = ' + @ID
if @tester is not null
	begin
	print 'Checking to see it my first one has a reference file for MESSAGE_ID of' + isNull(@ID,'NULL')
		set @tester = '1'
	end
else
	begin
	print 'setting @tester, @hasRefFile, @hasProcedure to 0'
	set @tester = '0'
	end

SELECT @hasRefFile = ID from A_MESSAGES_ATTACHMENTS WHERE MESSAGE_ID = @ID
print 'Checking to see it my first one has a procedure for MESSAGE_ID of ' + isNull(@ID,'NULL')
SELECT @hasProcedure = ID from A_MESSAGES_PROCEDURE_LINK WHERE MESSAGE_ID = @ID
SELECT @hasBusinessPurpose = ID from A_BUSINESS_PURPOSES_ITEM_LINK WHERE ITEM_ID = @ID AND ITEM_TYPE = 'A_MESSAGES'
SELECT @hasObject = ID from A_OBJECT_ITEM_LINK WHERE ITEM_ID = @ID AND ITEM_TYPE = 'A_MESSAGES'


if @hasRefFile is not null
	begin
	print 'I have a reference file so setting @hasRefFile to 1 for MESSAGE ID OF ' + isNull(@ID,'NULL') 
	set @hasRefFile = '1'
	end 
else
	begin
	print 'I do not have a reference file so setting @hasRefFile to 0 for MESSAGE ID of'  + isNull(@ID,'NULL')
	set @hasRefFile = '0'
	end 
if @hasProcedure is not null
	begin
	print 'I have a reference file so setting @hasProcedure to 1 for MESSAGE ID of' + isNull(@ID,'NULL')
	set @hasProcedure = '1'
	end 
else
	begin
	print 'I do not have a procedure so setting @hasProcedure to 0 for MESSAGE ID of' + isNull(@ID,'NULL')
	set @hasProcedure = '0'
	end 
if @hasBusinessPurpose is not null
	begin
	print 'I have a business purpose so setting @hasBusinessPurpose to 1 for MESSAGE ID of' + isNull(@ID,'NULL')
	set @hasBusinessPurpose = '1'
	end 
else
	begin
	print 'I do not have a business purpose so setting @hasBusinessPurpose to 0 for MESSAGE ID of' + isNull(@ID,'NULL')
	set @hasBusinessPurpose = '0'
	end 
if @hasObject is not null
	begin
	print 'I have a object so setting @hasObject to 1 for MESSAGE ID of' + isNull(@ID,'NULL')
	set @hasObject = '1'
	end 
else
	begin
	print 'I do not have a object so setting @hasObject to 0 for MESSAGE ID of' + isNull(@ID,'NULL')
	set @hasObject = '0'
	end 

print 'Inserting into #tempMessagesTree'
if @ID IN (SELECT ID FROM #tempCanSeeList)
begin
	INSERT INTO #tempMessagesTree(ID,HAS_REF_FILE,HAS_PROCEDURE,HAS_BUSINESS_PURPOSE,HAS_OBJECT,HAS_PROJECT) VALUES(@ID,@hasRefFile,@hasProcedure,@hasBusinessPurpose,@hasObject,@hasProject)
		Declare @it nvarchar(50)
		Declare @curs Cursor
		set @curs = Cursor For SELECT ID FROM A_MESSAGES WHERE PARENT_ID = @ID 
		open @curs
		Fetch Next from @curs Into @it
		while (@@fetch_status = 0)
			Begin
			print 'Adding  a child ' + @it
			exec A_SP_MESSAGES_BRANCH_ADD_TO_BRANCH_TABLE @it
			Fetch Next from @curs Into @it
			End
		close @curs
		Deallocate @curs
end
print 'done'


