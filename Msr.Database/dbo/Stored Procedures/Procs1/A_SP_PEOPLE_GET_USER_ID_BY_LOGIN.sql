



CREATE PROCEDURE A_SP_PEOPLE_GET_USER_ID_BY_LOGIN
@strLogin nvarchar(50),
@strNTLogin nvarchar(50)
as
--build the sql for the query
SELECT * FROM A_APPROVED_PEOPLE WHERE LOGIN = @strLogin




