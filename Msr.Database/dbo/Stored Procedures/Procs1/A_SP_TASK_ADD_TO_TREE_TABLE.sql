






CREATE      PROCEDURE DBO.A_SP_TASK_ADD_TO_TREE_TABLE
@ID varchar(50),
@LEV integer,
@exAll smallint
AS
print 'First Checking to see if we are in the expand All List'
If @ID in (SELECT ID FROM #tempExpandAllList)
	begin
	print 'ID = ' + @ID + 'Is in the expand all list'
	set @exAll = 1
	end
print 'Insert My ID and Level into the tree table'
declare @tester as varchar(50)
SELECT TOP 1  @tester = ID FROM A_TASKS WHERE PARENT_ID = @ID
IF @tester is not null
	set @tester = '1'
INSERT INTO #tempCoTree(ID,LEV,HAS_CHILD,EXPANDED) VALUES(@ID,@LEV,CONVERT(smallint,@tester),0)
IF ((@ID in (SELECT ID FROM #tempExpandList)) or (@exAll = 1))
	begin
	UPDATE #tempCoTree SET EXPANDED = 1 WHERE ID = @ID
	print 'This one needs to be expanded so make a cursor for children and do it'
	Declare @it nvarchar(50)
	Declare @curs Cursor
	set @curs = Cursor For SELECT ID
		--STEP_ID 
		FROM A_V_TASKS_WITH_PROCEDURE_STEP_ORDER_II
		--A_V_TASKS_WITH_PROCEDURE_STEP_ORDER 
		WHERE PARENT_ID = @ID 
		ORDER BY STEP_ORDER,ACTUAL_STOP_DATE,ACTUAL_START_DATE,CUR_PLANNED_START_DATE,ORIG_PLANNED_START_DATE
	open @curs
	Fetch Next from @curs Into @it
	set @LEV = @LEV + 1
	while (@@fetch_status = 0)
		Begin
		print 'Adding  a child ' + @it
		exec A_SP_TASK_ADD_TO_TREE_TABLE @it,@LEV,@exAll
		Fetch Next from @curs Into @it
		End
	close @curs
	Deallocate @curs
	end









