


CREATE      PROCEDURE A_SP_POPFILL_PROJECT_BY_ID
@projectID varchar(50),
@strNTLogin varchar(50)
AS
SELECT ID, NAME  
FROM A_PROJECTS
WHERE @projectID = ID



