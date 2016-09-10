CREATE PROCEDURE dbo.A_SP_PRODUCT_GET_PART_AVAILABILITY_FOR_LOCATION 
@ID varchar(50) OUTPUT,
@msgs varchar(50) OUTPUT,
@locID varchar(50),
@prodID varchar(50),
@PPL_ID varchar(50),
@strNTLogin varchar(50)
AS
print 'Getting the product parts availibility for the loc = ' + @locID


declare @pplHistID varchar(50)
SELECT @pplHistID = HISTORY_REF_ID FROM A_PROD_PRICE_LIST WHERE ID = @PPL_ID

CREATE TABLE #partsList (
	PART_ID varchar(50),
	QTY float
)

INSERT INTO #partsList (QTY,PART_ID)
SELECT sl.QTY,o.ROOT FROM 
A_PROD_PRICE_LIST_SUB_PRICE_LISTS sl,
A_APPROVED_OBJECTS ao,
A_OBJECTS o
WHERE 
sl.PARENT = @pplHistID 
and sl.RELATIONSHIP = 'PARTS_PROVIDE_STAY'
and ao.ID = APPROVED_OBJ_ID
and o.ID = ao.OBJ_REF_ID 
and o.OBJ_TABLE = 'A_PARTS_HISTORY'

CREATE TABLE #partsListSummary (
	ID int Identity,
	PART_ID varchar(50),
	PART_NAME nvarchar(50),
	QTY float,
	LOCATION_NAME nvarchar(1000),
	QTY_HAVE float,
	QTY_DIFF float,
	MAX_MAKE float
)

INSERT INTO #partsListSummary (PART_ID,PART_NAME,QTY,LOCATION_NAME,QTY_HAVE)
SELECT prod.PART_ID,ap.PART_DESC,prod.QTY,LOCATION_NAME,SUM(ap.QTY) 
	FROM #partsList prod LEFT OUTER JOIN
                      A_V_ACTUAL_PARTS_APPROVED_DATA ap ON prod.PART_ID = ap.PART_ID
	WHERE ap.LOCATION = @locID
	GROUP BY prod.PART_ID,ap.PART_DESC,prod.QTY,LOCATION_NAME


UPDATE #partsListSummary SET MAX_MAKE = round(QTY_HAVE / QTY,0)

SELECT @ID = convert(varchar(50),min(MAX_MAKE)) FROM #partsListSummary
INSERT INTO #partsListSummary (PART_NAME,QTY)
	VALUES('ZZZZZZZZZZZZZZZ_NO_NAME',@ID)

print 'ID = ' + isnull(@ID,'NULL')
SELECT * FROM #partsListSummary


