
/*
STORED PROCEDURE CALLED IN people/editPeople.asp
*/
CREATE         PROCEDURE A_SP_PEOPLE_UPDATE_PASSWORD_BY_OBJ_ID
@objID varchar(50),
@password varchar(50),
@strNTLogin varchar(50)
AS
if @password is not null
	UPDATE A_PEOPLE_HISTORY SET PASSWORD = @PASSWORD WHERE OBJECT_ID = @objID

