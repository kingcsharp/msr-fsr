



CREATE  PROCEDURE A_SP_FAVORITES_VERIFY_EXISTS
	@exists AS nvarchar(10) OUTPUT,
	@fav AS nvarchar(50),
	@strNTLogin AS nvarchar(50)
as

SELECT @exists = ID FROM A_PEOPLES_FAVORITE_GROUPS WHERE ID = @fav and PERSON = @strNTLogin
if @exists is null
	set @exists = 'FALSE'
else
	set @exists = 'TRUE'
--SELECT FROM A_PEOPLES_FAVORITES WHERE ID = @ID