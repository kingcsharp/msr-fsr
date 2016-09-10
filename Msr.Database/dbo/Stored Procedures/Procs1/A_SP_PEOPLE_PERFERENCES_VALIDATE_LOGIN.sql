





/*
STORED PROCEDURE CALLED IN people/editMyAccount.asp
created by Jerry McMillan
*/

CREATE      PROCEDURE A_SP_PEOPLE_PERFERENCES_VALIDATE_LOGIN
@boolLogInExits varchar(50) OUTPUT,
@msg varchar(1000) OUTPUT,
@login varchar(100),
@strNTLogin varchar(50)
AS

set @boolLogInExits='false'
Declare @curs Cursor
Declare @it varchar(100)

if exists(SELECT * FROM A_V_PEOPLE_APPROVED_DATA WHERE LOGIN = @login and ID <> @strNTLogin)
	begin
	set @boolLogInExits = 'true'
	end


print 'boolLogin is' + @boolLogInExits




