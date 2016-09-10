


CREATE     PROCEDURE DBO.A_SP_ACTUAL_PARTS_SHOW_HIERARCHY
@firstTime varchar(50),
@strAPart varchar(50),
@strListToexpand varchar(8000),
@strExpandAllList varchar(8000),
@strNTLogin nvarchar(50)
AS
declare @parentID varchar(50),@runner varchar(50),@cnt smallint
SET @parentID = @strAPart
SET @runner = @parentID
set @cnt = 0
WHILE @runner is not null AND @cnt < 100
	begin
	set @parentID = @runner
	SELECT @runner = PARENT_ID FROM A_V_ACTUAL_PARTS_APPROVED_DATA WHERE ID = @parentID
	set @cnt = @cnt + 1
	end
declare @strRootPart varchar(50)
set  @strRootPart = @parentID
if @firstTime = 'true' set @strExpandAllList = @strRootPart
CREATE TABLE #tempExpandList(ID varchar(50))
INSERT INTO #tempExpandList Exec A_SP_Z_SPLIT @strListToexpand,','
CREATE TABLE #tempExpandAllList(ID varchar(50))
INSERT INTO #tempExpandAllList Exec A_SP_Z_SPLIT @strExpandAllList,','
CREATE TABLE #tempAPTree(ID varchar(50),LEV int,HAS_CHILD smallInt,EXPANDED smallInt,idt smallint identity)
exec A_SP_ACTUAL_PART_ADD_TO_TREE_TABLE @strRootPart,0,0

SELECT ap.ID AS ROOT,t.idt as myOrder,t.LEV as TREE_LEVEL,t.HAS_CHILD as TREE_HAS_CHILD,t.EXPANDED as EXPANDED,ap.* 
FROM #tempAPTree t, A_ACTUAL_PARTS_APPROVED_WITH_OBJECT_DATA ap WHERE
ap.ID = t.ID
ORDER by t.idt



