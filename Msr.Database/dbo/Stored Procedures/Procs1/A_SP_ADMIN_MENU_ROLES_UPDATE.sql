CREATE PROCEDURE dbo.A_SP_ADMIN_MENU_ROLES_UPDATE
@menuID varchar(50),
@roleList varchar(4000),
@strNTLogin varchaR(50)
AS
declare @myCo varchar(50)
SELECT @myCo = ROOT_COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @strNTLogin
DELETE FROM A_MENU_ROLES WHERE MENU_ID = @menuID AND CO = @myCo
CREATE TABLE #tt (ID varchar(50))
INSERT INTO #tt Exec A_SP_Z_SPLIT @roleList,', '
INSERT INTO A_MENU_ROLES (MENU_ID,ROLE_ID,ID,DRCM,MODBY,CO)
	SELECT @menuID,ID,newID(),getdate(),@strNTLogin,@myCo FROM #tt

