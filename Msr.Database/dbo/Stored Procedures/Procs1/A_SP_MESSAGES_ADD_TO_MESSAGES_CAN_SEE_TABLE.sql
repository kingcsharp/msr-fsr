
/*
STORED PROCEDURE CALLED IN A_SP_MESSAGES_ADD_TO_TREE_TABLE

*/

CREATE    PROCEDURE A_SP_MESSAGES_ADD_TO_MESSAGES_CAN_SEE_TABLE
@ID varchar(50),
@boolAdded int OUTPUT,
@strNTLogin varchar(50)
AS

print 'Adding to Messages can see table for this ID' + isNull(@ID,'NULL') 
declare @senderTester varchar(50)
declare @recipientTester varchar(50)

set @boolAdded = 0

set @senderTester = null
set @recipientTester = null

SELECT @senderTester = ID FROM A_MESSAGES WHERE ID = @ID AND SENDER = @strNTLogin
SELECT @recipientTester = ID FROM A_MESSAGES_PEOPLE_LINK WHERE MESSAGE_ID = @ID AND PERSON_ID = @strNTLogin

if @senderTester is not null or @recipientTester is not null
	begin
	if @senderTester is not null
		print 'I am the sender'	
	if @recipientTester is not null
		print 'I am the recipient'	
	set @boolAdded = 1
	end

	print 'setting up my curosr for the children'
	Declare @it nvarchar(50)
	Declare @curs Cursor
	Declare @thisOneAdded int
	set @thisOneAdded = 0
	set @curs = Cursor For SELECT ID FROM A_MESSAGES WHERE PARENT_ID = @ID 
	open @curs
	Fetch Next from @curs Into @it
	while (@@fetch_status = 0)
		Begin
		print 'Adding  a child ' + @it
		exec  A_SP_MESSAGES_ADD_TO_MESSAGES_CAN_SEE_TABLE @it,@thisOneAdded OUTPUT,@strNTLogin
		if @thisOneAdded <> 0 
			begin
				set @boolAdded = 1
			end
		Fetch Next from @curs Into @it
		End
	close @curs
	Deallocate @curs

if @boolAdded = 1 
	begin
	print 'inserting ID ' + isNull(@ID,'NULL') + 'into #tempCanSeeList'
	INSERT INTO #tempCanSeeList (ID) VALUES(@ID)
	end
