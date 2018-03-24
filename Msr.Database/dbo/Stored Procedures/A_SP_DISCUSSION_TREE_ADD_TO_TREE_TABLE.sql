




CREATE          PROCEDURE A_SP_DISCUSSION_TREE_ADD_TO_TREE_TABLE
@ID varchar(50),
@LEV integer,
@exAll smallint,
@strNTLogin varchar(50)
AS

print 'First Checking to see if we are in the expand All List'
If @ID in (SELECT ID FROM #tempExpandAllList)
	begin
	print 'ID = ' + @ID + 'Is in the expand all list'
	set @exAll = 1
	end
print 'Insert My ID and Level into the tree table'
declare @tester as varchar(50)
SELECT TOP 1 @tester = ID FROM A_DISCUSSION_RESPONSE WHERE PARENT_ID = (@ID)
print 'Checking for Child using ID = ' + @ID
if @tester is not null
	begin
	set @tester = '1'
	end
else
	set @tester = '0'

print 'checking to see if my reponse has a document'
declare @hasDocumentTest varchar(50)
declare @hasDocument varchar(50)
SELECT @hasDocumentTest = RESPONSE_ID 
FROM A_DISCUSSION_ATTACHMENTS 
WHERE RESPONSE_ID = @ID

print 'mydocument is ' + isNull(@hasDocumentTest,'NULL')
if @hasDocumentTest is not null
	begin
	print 'This response has files so setting @hasDocument to 1 '
	set @hasDocument =1
	end 
else 
	begin
	print 'This response does not have a file so setting @hasDocument to 0 '
	set @hasDocument = 0
	end 




INSERT INTO #tempDiscussionTree(ID,LEV,HAS_CHILD,HAS_FILE,EXPANDED) VALUES(@ID,@LEV,convert(smallInt,@tester),@hasDocument,0)
	IF ((@ID in (SELECT ID FROM #tempExpandList)) or (@exAll = 1))
		begin
		UPDATE #tempDiscussionTree SET EXPANDED = 1 WHERE ID = @ID
		print 'This one needs to be expanded so make a cursor for children and do it'
		Declare @it nvarchar(50)
		Declare @curs Cursor
		set @curs = Cursor For SELECT ID FROM A_DISCUSSION_RESPONSE WHERE PARENT_ID = @ID ORDER BY DRCM
		open @curs
		Fetch Next from @curs Into @it
		set @LEV = @LEV + 1
		while (@@fetch_status = 0)
			Begin
			print 'Adding  a child ' + @it
			exec A_SP_DISCUSSION_TREE_ADD_TO_TREE_TABLE @it,@LEV,@exAll,@strNTLogin
			Fetch Next from @curs Into @it
			End
		close @curs
		Deallocate @curs
		end
	
print 'done'