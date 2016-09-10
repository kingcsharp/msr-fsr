








/*
STORED PROCEDURE CALLED IN survey/saveSuruvey.asp
*/

CREATE PROCEDURE A_SP_SURVEY_PUBLISH
@ID nvarchar(50),
@strNTLogin nvarchar(50)
as
UPDATE A_SURVEYS SET
STATUS = 'OPEN'
WHERE ID = @ID






