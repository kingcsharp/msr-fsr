




CREATE   PROCEDURE DBO.A_SP_PART_HIERARCHY_ADD_ITEM_TO_TREE_TABLE
@ID varchar(50),
@LEV integer,
@exAll smallint
AS
declare @childID varchar(50)
print 'First Checking to see if we are in the expand All List'
If @ID in (SELECT ID FROM #tempExpandAllList)
	begin
	print 'ID = ' + @ID + 'Is in the expand all list'
	set @exAll = 1
	end
print 'Insert My ID and Level into the tree table'
declare @tester as varchar(50)
SELECT TOP 1 @tester = ID FROM A_V_PARTS_GET_SUB_PART_DATA WHERE ROOT = (@ID)
print 'Checking for Child using ID = ' + @ID
if @tester is not null
	begin
	set @tester = '1'
	end
else
	set @tester = '0'
print 'Inserting my ID into temp table ' + @ID
INSERT INTO #tempAPTree(ID,LEV,HAS_CHILD,EXPANDED) VALUES(@ID,@LEV,convert(smallInt,@tester),0)
IF ((@ID in (SELECT ID FROM #tempExpandList)) or (@exAll = 1))
	begin
	UPDATE #tempAPTree SET EXPANDED = 1 WHERE ID = @ID
	print 'This one needs to be expanded so make a cursor for children and do it'
	Declare @it nvarchar(50)
	Declare @curs Cursor
	set @curs = Cursor For SELECT SUB_PART_ID FROM A_V_PARTS_GET_SUB_PART_DATA 
	WHERE ROOT = @ID ORDER BY SUB_PART_NAME
	open @curs
	Fetch Next from @curs Into @it
	set @LEV = @LEV + 1
	while (@@fetch_status = 0)
		Begin
		print 'Adding  a child ' + @it
		print 'Since this is the approved ID we need to change to the history ref id'
		SELECT @childID = PARTS_HISTORY_ID FROM A_PARTS WHERE ID = @it
		exec A_SP_PART_HIERARCHY_ADD_ITEM_TO_TREE_TABLE @childID,@LEV,@exAll
		Fetch Next from @curs Into @it
		End
	close @curs
	Deallocate @curs
	end







