CREATE    PROCEDURE Portal_GetUserModulePermissions
@strNTLogin as nvarchar(50)
as

SELECT 
m.MENU_GROUP AS MenuGroup
FROM A_MENUS m WHERE (
					exists(
							SELECT mr.ROLE_ID FROM A_MENU_ROLES mr,A_PERSON_ROLES rp
							WHERE mr.MENU_ID = m.ID 
							AND mr.ROLE_ID = rp.ROLE_ID 
							AND rp.PERSON_ID = @strNTlogin)
				 )
		






