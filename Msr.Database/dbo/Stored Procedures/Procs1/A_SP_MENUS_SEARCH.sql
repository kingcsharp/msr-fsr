




CREATE    PROCEDURE A_SP_MENUS_SEARCH
@g as nvarchar(100),
@strNTLogin as nvarchar(50)
as
declare @rootCo varchar(50)
SELECT @rootCo = ROOT_COMPANY FROM A_APPROVED_PEOPLE WHERE ID = @strNTLogin

SELECT m.* FROM A_MENUS m
	WHERE MENU_GROUP = @g 
	AND 
		(
		not exists(SELECT ROLE_ID FROM A_MENU_ROLES WHERE CO = @rootCo AND MENU_ID = m.ID)
		OR
		exists(SELECT mr.ROLE_ID FROM A_MENU_ROLES mr,A_PERSON_ROLES rp
				 WHERE mr.MENU_ID = m.ID AND mr.ROLE_ID = rp.ROLE_ID AND rp.PERSON_ID = @strNTlogin AND mr.CO = @rootCo)
		)






