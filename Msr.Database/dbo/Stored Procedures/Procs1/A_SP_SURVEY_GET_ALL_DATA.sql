









/*
STORED PROCEDURE CALLED IN survey/editSurey.asp
*/

CREATE   PROCEDURE A_SP_SURVEY_GET_ALL_DATA
@ID nvarchar(50),
@strNTLogin nvarchar(50)

AS
SELECT * FROM A_V_SURVEY_BY_ID 
WHERE ID=@ID








