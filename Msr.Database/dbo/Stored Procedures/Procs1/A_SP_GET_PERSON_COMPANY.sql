


/*

STORED PROCEDURE CALLED IN

- Stored Procedure: A__SP_PEOPLE_SEARCH


*/


CREATE     PROCEDURE A_SP_GET_PERSON_COMPANY
@strNTLogin nvarchar(50),
@myCompany nvarchar(50) OUTPUT
as
SELECT @myCompany = ROOT_COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE 
	ID = @strNTLogin 






