CREATE    PROCEDURE Portal_GetUserModulePermissions
@strNTLogin as nvarchar(50)
as

SELECT 
Id,
URL,
NAME,
INFO,
MENU_GROUP,
Icon,
GroupIcon AS GroupIcon,
MENU_GROUP AS GroupMenu,
OrderNumber AS OrderNumber,
IsParent
FROM A_MENUS m WHERE (
					exists(
							SELECT mr.ROLE_ID FROM A_MENU_ROLES mr,A_APPROVED_ROLE_ASSIGNEES rp
							WHERE mr.MENU_ID = m.ID 
							AND mr.ROLE_ID = rp.ROLE_ID 
							AND rp.PERSON = @strNTlogin)
				 )





