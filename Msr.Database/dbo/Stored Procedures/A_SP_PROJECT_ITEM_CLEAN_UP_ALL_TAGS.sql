
CREATE  PROCEDURE DBO.A_SP_PROJECT_ITEM_CLEAN_UP_ALL_TAGS
@strID varchar(50),
@strType varchar(50),
@strNTLogin varchar(50)
AS
create TABLE #tempObjects (ID varchar(50))
create TABLE #tempPurposes (ID varchar(50))
create TABLE #tempProjects (ID varchar(50))
INSERT INTO #tempProjects 
	SELECT PROJECT_ID FROM A_PROJECT_ITEM_LINK 
	WHERE ITEM_ID = @strID and ITEM_TYPE = @strType
--SELECT '--StartProj',* FROM #tempProjects
INSERT INTO #tempObjects
	SELECT OBJECT_ID FROM A_OBJECT_ITEM_LINK 
	WHERE ITEM_ID = @strID and ITEM_TYPE = @strType
--SELECT '--StartObj',* FROM #tempObjects
INSERT INTO #tempPurposes 
	SELECT BUSINESS_PURPOSE_ID FROM A_BUSINESS_PURPOSES_ITEM_LINK 
	WHERE ITEM_ID = @strID and ITEM_TYPE = @strType
--SELECT '--StartPurp',* FROM #tempPurposes

Declare @it nvarchar(50)
Declare @curs Cursor
set @curs = Cursor For SELECT * FROM #TempProjects
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'Adding stuff From the project = ' + @it
	INSERT INTO #tempObjects SELECT OBJECT_ID FROM A_OBJECT_ITEM_LINK
		WHERE ITEM_ID = @it and ITEM_TYPE = 'A_PROJECTS'
	INSERT INTO #tempPurposes SELECT BUSINESS_PURPOSE_ID FROM A_BUSINESS_PURPOSES_ITEM_LINK
		WHERE ITEM_ID = @it and ITEM_TYPE = 'A_PROJECTS'
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs
--SELECT '--aftProjProj',* FROM #tempProjects
--SELECT '--aftProjObj',* FROM #tempObjects
--SELECT '--aftProjPurp',* FROM #tempPurposes

declare @OBJECT_TYPE varchar(50),@cnt int,@prev varchar(50),@parent varchar(50)
set @curs = Cursor For SELECT * FROM #TempObjects
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'examining the object =  ' + @it
	SELECT @OBJECT_TYPE = OBJ_TABLE FROM A_OBJECTS WHERE ID = @it
	print 'Object Type = ' + @OBJECT_TYPE
	if @OBJECT_TYPE = 'A_PEOPLE_HISTORY'
		begin
		set @cnt = 1
		SELECT @parent = BOSS FROM A_V_PEOPLE_APPROVED_DATA p WHERE ID = @it
		while @parent is not null and @cnt < 50
			begin
			set @cnt = @cnt + 1
			print 'Inserting a boss man' + @parent
			INSERT INTO #tempObjects (ID) VALUES(@parent)
			set @prev = @parent
			set @parent = null
			print 'Now Finding the boss of ' + @prev
			SELECT @parent = BOSS FROM A_V_PEOPLE_APPROVED_DATA p WHERE ID = @prev
			print 'Boss= ' + isNULL(@parent,'NULL')
			end
		print 'Done Adding all bosses'
		end
	
	if @OBJECT_TYPE = 'A_COMPANIES_HISTORY'
		begin
		set @cnt = 1
		SELECT @parent = PARENT FROM A_V_COMPANIES_APPROVED_DATA p WHERE ID = @it
		while @parent is not null and @cnt < 50
			begin
			set @cnt = @cnt + 1
			INSERT INTO #tempObjects (ID) VALUES(@parent)
			set @prev = @parent
			set @parent = null
			SELECT @parent = PARENT FROM A_V_COMPANIES_APPROVED_DATA p WHERE ID = @prev
			print 'Parent Co = ' + isNULL(@parent,'NULL')
			end
		end
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs
--SELECT '--aftobjParent',* FROM #tempObjects

print 'Now Delete all the old values we had'

DELETE FROM A_PROJECT_ITEM_LINK WHERE ITEM_ID = @strID AND ITEM_TYPE = @strType
DELETE FROM A_BUSINESS_PURPOSES_ITEM_LINK WHERE ITEM_ID = @strID AND ITEM_TYPE = @strType
DELETE FROM A_OBJECT_ITEM_LINK WHERE ITEM_ID = @strID AND ITEM_TYPE = @strType

create TABLE #tempObjects2 (IT varchar(50))
create TABLE #tempPurposes2 (IT varchar(50))
create TABLE #tempProjects2 (IT varchar(50))

INSERT INTO #tempObjects2 SELECT DISTINCT ID FROM #tempObjects
INSERT INTO #tempPurposes2 SELECT DISTINCT ID FROM #tempPurposes
INSERT INTO #tempProjects2 SELECT DISTINCT ID FROM #tempProjects

INSERT INTO A_PROJECT_ITEM_LINK (ID,ITEM_ID,ITEM_TYPE,PROJECT_ID,DRCM,MODBY)
	SELECT newID(),@strID,@strType,lTrim(IT),getDate(),@strNTLogin  FROM #tempProjects2 
INSERT INTO A_OBJECT_ITEM_LINK (ID,ITEM_ID,ITEM_TYPE,OBJECT_ID,DRCM,MODBY)
	SELECT newID(),@strID,@strType,lTrim(IT),getDate(),@strNTLogin FROM #tempObjects2 
INSERT INTO A_BUSINESS_PURPOSES_ITEM_LINK(ID,ITEM_ID,ITEM_TYPE,BUSINESS_PURPOSE_ID,DRCM,MODBY)
	SELECT newID(),@strID,@strType,lTrim(IT),getDate(),@strNTLogin FROM #tempPurposes2


/*


*/