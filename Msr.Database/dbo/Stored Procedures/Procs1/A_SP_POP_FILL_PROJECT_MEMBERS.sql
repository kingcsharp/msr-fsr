

/*
STORED PROCEDURE CALLED IN projects/editProjects.asp

*/

Create    PROCEDURE A_SP_POP_FILL_PROJECT_MEMBERS
@projectID varchar(50),
@strNTLogin nvarchar(50)
AS
SELECT * 
FROM A_V_PROJECT_MEMBER_DATA
WHERE PROJECT_ID =@projectID


