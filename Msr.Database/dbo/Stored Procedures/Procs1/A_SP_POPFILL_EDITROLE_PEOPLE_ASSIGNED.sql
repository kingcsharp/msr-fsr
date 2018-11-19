







CREATE      PROCEDURE A_SP_POPFILL_EDITROLE_PEOPLE_ASSIGNED
	@ID nvarchar(50),
	@strNTLogin nvarchar(50)
AS

SELECT p.FULL_NAME as SHOW, p.ID AS VALUE,ra.ROLE,ra.startDate as StartDate,ra.EndDate AS EndDate   FROM A_ROLE_ASSIGNEE ra,A_APPROVED_PEOPLE p 
WHERE ra.ROLE = @ID  AND ra.PERSON = p.ID AND TYPE is NULL









