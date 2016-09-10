




CREATE  PROCEDURE A_SP_PEOPLE_GET_TIME_ZONE_INFO_BY_USER_ID
@strID nvarchar(50),
@strNTLogin nvarchar(50)
as
--build the sql for the query
SELECT * FROM A_V_TIME_ZONE_WITH_PERSON_ID WHERE ID = @strID





