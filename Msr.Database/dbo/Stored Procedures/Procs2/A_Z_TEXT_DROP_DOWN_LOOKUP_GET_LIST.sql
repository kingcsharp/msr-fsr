



CREATE     PROCEDURE DBO.A_Z_TEXT_DROP_DOWN_LOOKUP_GET_LIST
@curVal varchar(50),
@strNTLogin varchar(50),
@lookup varchar(50)
AS
SELECT VAL AS VAL, VAL AS SHOW 
FROM A_Z_TEXT_DROP_DOWN_LOOK_UP 
WHERE LOOKUP = @lookup and PERSON_ID = @strNTLogin and VAL <> isNull(@curVal,'') and VAL <> '' AND VAL IS NOT NULL
ORDER BY VAL





