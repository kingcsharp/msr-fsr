CREATE      FUNCTION dbo.A_FN_COMPANY_HAS_CHILD (@ID varchar(50))
RETURNS smallint
as
BEGIN
declare @c as smallint
SELECT @c = count(o.ID) FROM A_COMPANIES_HISTORY c,A_OBJECTS o WHERE c.PARENT = @ID AND 
c.OBJECT_ID = o.ID AND o.STATUS = 'APPROVED'
return(@c) 
END





