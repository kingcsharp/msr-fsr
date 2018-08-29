Create View Portal_MenuView
AS
SELECT 
Id,
URL,
NAME,
INFO,
Num,
MENU_GROUP,
Icon,
GroupIcon AS GroupIcon,
MENU_GROUP AS GroupMenu,
OrderNumber AS OrderNumber,
IsParent
FROM A_MENUS