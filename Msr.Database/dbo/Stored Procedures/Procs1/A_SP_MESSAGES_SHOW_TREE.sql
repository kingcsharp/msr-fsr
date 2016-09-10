

CREATE                       PROCEDURE A_SP_MESSAGES_SHOW_TREE
@strRootID varchar(50),
@firstTime varchar(50),
@strListToexpand varchar(8000),
@strExpandAllList varchar(8000),
@strNTLogin nvarchar(50)
AS
declare @parentID varchar(50)
declare @myRootID varchar(50)

set @myRootID = @strRootID 
print 'Getting my frist parent ID to go up the chain with root id of ' + isNull (@myRootID,'null')
SELECT @parentID = PARENT_ID FROM A_MESSAGES WHERE ID = @myRootID
print 'My first parent id is' + isNull (@parentID,'null')

print 'Checking the each one with a parent id'
WHILE @parentID is NOT NULL
begin	
	--set @parentID = null
 	SELECT @parentID = PARENT_ID,
			@myRootID = ID
	FROM A_MESSAGES 
	WHERE ID = @parentID
	print 'the root id is now' + isNull (@myRootID,'null')
end




if @firstTime = 'true' set @strExpandAllList = @myRootID

print 'my root for ' + isNull(@strRootID, 'null') + ' is ' + isNull(@myRootID, 'null') 
CREATE TABLE #tempExpandList(ID varchar(50))
INSERT INTO #tempExpandList Exec A_SP_Z_SPLIT @strListToexpand,','
CREATE TABLE #tempExpandAllList(ID varchar(50))
INSERT INTO #tempExpandAllList Exec A_SP_Z_SPLIT @strExpandAllList,','
CREATE TABLE #tempMessagesTree(IDENT int IDENTITY,ID varchar(50),LEV int,HAS_CHILD smallInt,EXPANDED smallInt)
CREATE TABLE #tempCanSeeList(ID varchar(50))

exec A_SP_MESSAGES_ADD_TO_MESSAGES_CAN_SEE_TABLE @myRootID, 0, @strNTlogin

exec A_SP_MESSAGES_ADD_TO_TREE_TABLE @myRootID,0,0,@strNTLogin

declare @isSender varchar(50)

--select * from #tempCanSeeList

SELECT DISTINCT t.IDENT AS IDENT,
				t.ID as ID,
				t.LEV as TREE_LEVEL,
				t.HAS_CHILD as TREE_HAS_CHILD,
				t.EXPANDED as EXPANDED,
				c.MESSAGE AS MESSAGE,	
				c.DATE_SENT AS DATE_SENT,
				c.HIDE_MESSAGE AS HIDE_MESSAGE,
				c.STATUS AS STATUS,
				c.SENDER_NAME AS SENDER_NAME,
				dbo.A_FN_MESSAGES_IS_SENDER (t.ID,@strNTLogin) AS IS_SENDER,
				dbo.A_FN_MESSAGES_IS_RECIPIENT (t.ID,@strNTLogin) AS IS_RECIPIENT,
				dbo.A_FN_MESSAGES_IS_READ (t.ID,@strNTLogin) AS IS_READ,
				c.TOTAL_READ_COUNT,
				c.TOTAL_COUNT,
				CASE WHEN (c.TOTAL_COUNT = 0) THEN  '0%'
				ELSE convert(varchar(50),ROUND(c.TOTAL_READ_COUNT/c.TOTAL_COUNT * 100,0)) + '%' 
				end TOTAL_READ_RATIO			
FROM #tempMessagesTree t, A_V_MESSAGES_SEARCH_DATA c
WHERE c.ID = t.ID AND DATE_SENT is not null
--ORDER BY DATE_SENT
--SELECT * FROM #tempCoTree










