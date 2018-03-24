


CREATE    PROCEDURE dbo.A_SP_PROJECT_GENERAL_UPDATE_PURPOSES_AND_OBJECTS
@myID varchar(50),
@strItemType varchar(50),
@strPurposes varchar(4000),
@strObjects varchar(4000),
@strProjects varchar(4000),
@strNTLogin varchar(50)
AS
CREATE TABLE #tempItems (IT varchar(50))
print 'Entering the PROJECT_GENERAL_UPDATE_PURPOSES_AND_OBJECTS'
print 'Deleting all the Projects,Objects, and Purposes'
DELETE FROM A_PROJECT_ITEM_LINK WHERE ITEM_ID = @myID AND ITEM_TYPE = @strItemType
DELETE FROM A_BUSINESS_PURPOSES_ITEM_LINK WHERE ITEM_ID = @myID AND ITEM_TYPE = @strItemType
DELETE FROM A_OBJECT_ITEM_LINK WHERE ITEM_ID = @myID AND ITEM_TYPE = @strItemType
print 'Purposes'
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @strPurposes,', '
INSERT INTO A_BUSINESS_PURPOSES_ITEM_LINK(ID,ITEM_ID,ITEM_TYPE,BUSINESS_PURPOSE_ID,DRCM,MODBY)
	SELECT newID(),@myID,@strItemType,lTrim(IT),getDate(),@strNTLogin FROM #TempItems
DELETE FROM #TempItems

print 'Objects'
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @strObjects,','
INSERT INTO A_OBJECT_ITEM_LINK (ID,ITEM_ID,ITEM_TYPE,OBJECT_ID,DRCM,MODBY)
SELECT newID(),@myID,@strItemType,lTrim(IT),getDate(),@strNTLogin FROM #tempITems 
DELETE FROM #TempItems

print 'Projects'
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @strProjects,','
INSERT INTO A_PROJECT_ITEM_LINK (ID,ITEM_ID,ITEM_TYPE,PROJECT_ID,DRCM,MODBY)
SELECT newID(),@myID,@strItemType,lTrim(IT),getDate(),@strNTLogin  FROM #tempITems 
DELETE FROM #tempItems

exec A_SP_PROJECT_ITEM_CLEAN_UP_ALL_TAGS @myID,@strItemType,@strNTLogin