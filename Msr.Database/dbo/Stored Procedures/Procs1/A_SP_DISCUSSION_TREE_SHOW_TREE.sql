



CREATE                         PROCEDURE A_SP_DISCUSSION_TREE_SHOW_TREE
@strRootID varchar(50),
@strListToexpand varchar(8000),
@strExpandAllList varchar(8000),
@strNTLogin nvarchar(50)
AS

CREATE TABLE #tempExpandList(ID varchar(50))
INSERT INTO #tempExpandList Exec A_SP_Z_SPLIT @strListToexpand,','
CREATE TABLE #tempExpandAllList(ID varchar(50))
INSERT INTO #tempExpandAllList Exec A_SP_Z_SPLIT @strExpandAllList,','
CREATE TABLE #tempDiscussionTree(IDENT int IDENTITY,ID varchar(50),LEV int,HAS_CHILD smallInt,HAS_FILE smallInt, EXPANDED smallInt)

exec A_SP_DISCUSSION_TREE_ADD_TO_TREE_TABLE @strRootID,0,0,@strNTLogin

-- select * from #tempDiscussionTree


SELECT DISTINCT t.IDENT AS IDENT,
				t.ID as ID,
				t.LEV as TREE_LEVEL,
				t.HAS_CHILD as TREE_HAS_CHILD,
				t.HAS_FILE as HAS_FILE,
				t.EXPANDED as EXPANDED,
				c.RESPONSE AS RESPONSE,	
				c.DRCM AS DRCM,
				c.WRITER_NAME AS WRITER_NAME,
				c.STATUS AS STATUS,
				c.COLOR AS COLOR
FROM #tempDiscussionTree t, A_V_DISCUSSION_GET_RESPONSE_DATA_BY_ID c
WHERE c.ID = t.ID 














