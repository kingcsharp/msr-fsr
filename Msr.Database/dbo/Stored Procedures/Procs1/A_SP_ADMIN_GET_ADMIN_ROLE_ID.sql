

CREATE     PROCEDURE A_SP_ADMIN_GET_ADMIN_ROLE_ID
@strNTLogin as varchar(50)
AS
--Get my company
declare @myCo as varchar(50)
exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCo OUTPUT
print 'getting the admin role for the company = ' + @myCo
--Find out what the role ID is
SELECT ID FROM A_APPROVED_ROLES WHERE IS_ADMIN = 1 AND CREATING_CO = @myCo







