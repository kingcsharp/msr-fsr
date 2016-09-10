








CREATE         PROCEDURE dbo.A_SP_ORDER_SHOW_ITEM_TREE_ADD_ONE_TO_TREE
@ID varchar(50),
@LEV integer,
@exAll smallint,
@showAddCost tinyInt
AS
print 'First Checking to see if we are in the expand All List'
If @ID in (SELECT ID FROM #tempExpandAllList)
	begin
	print 'ID = ' + @ID + 'Is in the expand all list'
	set @exAll = 1
	end
print 'Insert My ID and Level into the tree table'
declare @tester as varchar(50)
SELECT TOP 1 @tester = ID FROM A_ORDER_ITEMS WHERE PARENT = (@ID)
print 'Checking for Child using ID = ' + @ID
if @tester is not null
	begin
	set @tester = '1'
	end
else
	set @tester = '0'

INSERT INTO #tempItemTree(ID,LEV,HAS_CHILD,EXPANDED) VALUES(@ID,@LEV,convert(smallInt,@tester),0)
IF ((@ID in (SELECT ID FROM #tempExpandList)) or (@exAll = 1))
	begin
	UPDATE #tempItemTree SET EXPANDED = 1 WHERE ID = @ID
	print 'This one needs to be expanded so make a cursor for children and do it'
	Declare @it nvarchar(50)
	Declare @curs Cursor
	if @showAddCost = 0
		set @curs = Cursor For SELECT a.ID FROM A_ORDER_ITEMS a  
		WHERE a.PARENT = @ID
	else
		set @curs = Cursor For SELECT a.ID FROM A_ORDER_ITEMS a  
		WHERE a.PARENT = @ID

	open @curs
	Fetch Next from @curs Into @it
	set @LEV = @LEV + 1
	while (@@fetch_status = 0)
		Begin
		print 'Adding  a child ' + @it
		exec A_SP_ORDER_SHOW_ITEM_TREE_ADD_ONE_TO_TREE @it,@LEV,@exAll,@showAddCost
		Fetch Next from @curs Into @it
		End
	close @curs
	Deallocate @curs
	end

print 'Finished adding this one and its children'









