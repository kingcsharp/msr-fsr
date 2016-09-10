
CREATE  PROCEDURE A_SP_COMPANIES_SHOW_PERSONS_ROOT_COMPANY_TREE
@PERSON_ID varchar(50),
@strNTLogin varchar(50)
AS
declare @rootCo as varchar(50)
SELECT @rootCo = h.ROOT_COMPANY FROM A_PEOPLE a,A_PEOPLE_HISTORY h 
WHERE a.ID = @PERSON_ID AND a.HISTORY_REF_ID = h.ID
print 'The Persons Root Company is ' + isNull(@rootCo,'NULL')

exec A_SP_COMPANIES_SHOW_TREE @rootCo,NULL,@rootCo,@strNTLogin



