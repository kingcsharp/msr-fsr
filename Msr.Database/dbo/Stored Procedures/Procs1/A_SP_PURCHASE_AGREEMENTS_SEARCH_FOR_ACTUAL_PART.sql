
CREATE       PROCEDURE dbo.A_SP_PURCHASE_AGREEMENTS_SEARCH_FOR_ACTUAL_PART
	@strWhere nvarchar(2000),
	@strSort nvarchar(1000),
	@actPartID varchar(50),
	@strNTLogin varchar(50)
AS
declare @myCO varchar(50),@partID varchar(50),@partTypeID varchar(50)

exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCO OUTPUT
print 'My Co = ' + @myCO
SELECT @partID = PART_ID FROM A_V_ACTUAL_PARTS_APPROVED_DATA WHERE ID = @actPArtID
SELECT @partTypeID = PART_TYPE FROM A_V_PARTS_APPROVED_DATA WHERE ID = @partID
create table #tempPossibleParts(ID varchar(50))
INSERT INTO #tempPossibleParts (ID) VALUES (@partID)
INSERT INTO #tempPossibleParts (ID) 
	SELECT EXT_PART_ID FROM A_V_PARTS_EXTERNAL_PART_LOOKUP WHERE INT_PART_ID = @partID
INSERT INTO #tempPossibleParts (ID) 
	SELECT INT_PART_ID FROM A_V_PARTS_EXTERNAL_PART_LOOKUP WHERE EXT_PART_ID = @partID
declare @so varchar(8000)
exec A_SP_Z_UTIL_GET_COMMA_ID_LIST 'SELECT ID FROM #tempPossibleParts',@so OUTPUT

declare @sql nvarchar(4000)
set @sql = 
'SET QUOTED_IDENTIFIER OFF 
	SELECT DISTINCT * FROM A_V_ORDERS_WITH_ITEMS_AND_APPLICABLE_OBJECTS o1 WHERE 
(
	(
		(
		o1.APP_OBJECT IN (''' + replace(@so,',',''',''') + ''')
		) 
	AND PARENT IS NULL
	and not exists
	(SELECT * FROM A_V_ORDERS_WITH_ITEMS_AND_APPLICABLE_OBJECTS o2 WHERE 
		o1.ORDER_ID = o2.ORDER_ID 
		AND 
		NOT
			(
		o1.APP_OBJECT IN (''' + replace(@so,',',''',''') + ''')
			)

		AND PARENT IS NULL )  
	)
	or
	(
	o1.APP_OBJECT = ''' + isNull(@partTypeID,'')  + ''' 
	AND PARENT IS NULL
	and not exists
	(SELECT * FROM A_V_ORDERS_WITH_ITEMS_AND_APPLICABLE_OBJECTS o2 WHERE o1.ORDER_ID = o2.ORDER_ID AND o2.APP_OBJECT <> ''' + isNull(@partTypeID,'') + ''' AND PARENT IS NULL )  
	)
)
'
if len(@strWhere) > 0
		set @sql = @sql + ' AND ' + @strWhere + ' '

if len(@strSort) > 0
		set @sql = @sql + @strSort

print isNull(@sql,'NULL')
drop table #tempPossibleParts
exec (@sql)





















