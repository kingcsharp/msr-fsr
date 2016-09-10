CREATE PROCEDURE A_SP_PROJECT_ITEM_UPDATE
@list varchar(4000),
@projID varchar(50),
@itemType varchar(50),
@strNTLogin varchar(50)
AS
print 'We are updating a list of type = ' + @itemType
print 'The list looks like this ' + @list
print 'The Project ID - ' + @projID

print 'Deleting the surveys from the project link table'
DELETE FROM A_PROJECT_ITEM_LINK WHERE PROJECT_ID = @projID AND ITEM_TYPE = @itemType
CREATE TABLE #TempItems	(IT varchar(50))
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @list,', '
print 'Inserting the items into the project table'
INSERT INTO A_PROJECT_ITEM_LINK (ID,ITEM_ID,ITEM_TYPE,PROJECT_ID,DRCM,MODBY)
	SELECT newID(),ltrim(IT),@itemType,@projID,getDate(),@strNTLogin
	FROM #TempItems
print 'Now we need to make sure it still has all the objects and purposes that all the projects it is linked to have'
DELETE FROM #TempItems
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @list,', '
Declare @it nvarchar(50)
Declare @curs Cursor
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
	Begin
	print 'first the purposes'
	INSERT INTO A_BUSINESS_PURPOSES_ITEM_LINK(ID,ITEM_ID,ITEM_TYPE,BUSINESS_PURPOSE_ID,DRCM,MODBY)
		SELECT DISTINCT newID(),Ltrim(@it),@itemType,BUSINESS_PURPOSE_ID,getDate(),@strNTLogin
		FROM A_BUSINESS_PURPOSES_ITEM_LINK
		WHERE ITEM_ID IN (SELECT PROJECT_ID FROM A_PROJECT_ITEM_LINK WHERE ITEM_ID = ltrim(@it) AND ITEM_TYPE = @itemType)
		AND BUSINESS_PURPOSE_ID NOT IN (SELECT BUSINESS_PURPOSE_ID FROM A_BUSINESS_PURPOSES_ITEM_LINK WHERE ITEM_ID = ltrim(@it) AND ITEM_TYPE = @itemType)

	print 'Now the Objects'
	INSERT INTO A_OBJECT_ITEM_LINK(ID,ITEM_ID,ITEM_TYPE,OBJECT_ID,DRCM,MODBY)
		SELECT DISTINCT newID(),Ltrim(@it),@itemType,OBJECT_ID,getDate(),@strNTLogin
		FROM A_OBJECT_ITEM_LINK
		WHERE ITEM_ID IN (SELECT PROJECT_ID FROM A_PROJECT_ITEM_LINK WHERE ITEM_ID = Ltrim(@it) AND ITEM_TYPE = @itemType)
		AND OBJECT_ID NOT IN (SELECT OBJECT_ID FROM A_OBJECT_ITEM_LINK WHERE ITEM_ID = Ltrim(@it) AND ITEM_TYPE = @itemType)
	Fetch Next from @curs Into @it
	End
close @curs
Deallocate @curs


 
