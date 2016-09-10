




CREATE   PROCEDURE A_SP_MENUS_CREATE_MENU_ITEM
@n as nvarchar(100),
@g as nvarchar(100),
@h as nvarchar(800)
as
declare @o as integer
SELECT @o = (max(NUM) + 1) from A_MENUS WHERE MENU_GROUP = @g
if @o is null
	begin
		set @o = 1
	end
INSERT INTO A_MENUS([ID], [URL], [DRCM], [MODBY], [NUM], [NAME], [INFO], [MENU_GROUP])
VALUES(@g + '_' + str(@o,1) ,@h,getDate(),'SP',
@o,@n,@n+' information',@g)





