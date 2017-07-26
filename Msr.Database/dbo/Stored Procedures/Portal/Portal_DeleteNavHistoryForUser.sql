CREATE PROCEDURE [dbo].[Portal_DeleteNavHistoryForUser]
	@userId nvarchar(50)
AS
	DELETE FROM A_NAV_HISTORY WHERE PAGE = 'Input Monitor Results' AND USER_ID = @userId

	DELETE FROM A_NAV_HISTORY WHERE PAGE = 'ViewTask' AND USER_ID = @userId

	DELETE FROM A_NAV_HISTORY WHERE PAGE = 'DNR Procedure Select' AND USER_ID = @userId

	DELETE FROM A_NAV_HISTORY WHERE PAGE = 'Input Monitor Results' AND USER_ID = @userId
