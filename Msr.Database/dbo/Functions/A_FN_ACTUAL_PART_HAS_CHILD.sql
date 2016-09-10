
CREATE FUNCTION dbo.A_FN_ACTUAL_PART_HAS_CHILD (@ID varchar(50))
RETURNS smallint
as
BEGIN
declare @c as smallint
SELECT @c = count(c.ID) FROM A_ACTUAL_PARTS_HISTORY c,A_OBJECTS o WHERE c.PARENT_ID = @ID AND 
c.OBJECT_ID = o.ID AND o.STATUS = 'APPROVED'
return(@c) 
END






