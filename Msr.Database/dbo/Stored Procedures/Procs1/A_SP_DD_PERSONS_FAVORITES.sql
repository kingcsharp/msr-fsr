


CREATE PROCEDURE A_SP_DD_PERSONS_FAVORITES
@strNTLogin nvarchar(50),
@FavType nvarchar(50)
as
SELECT 
GROUP_NAME AS show,
ID AS value from 
A_PEOPLES_FAVORITE_GROUPS 
WHERE PERSON = @strNTLogin and
FAV_TYPE = @FavType



