





CREATE  FUNCTION dbo.A_FN_DISCUSSION_GET_LAST_VIEW_DATE(@strNTLogin varchar(50),@DID varchar(50))
RETURNS dateTime
AS
BEGIN
declare @d as datetime
SELECT @d = DRCM FROM A_DISCUSSION_VIEWED_BY_PEOPLE WHERE PERSON_ID = @strNTLogin AND DISCUSSION_ID = @DID
return(@d)
end







