



CREATE  procedure A_SP_EMAILS_ADD_FOR_OBJECT
	@objID nvarchar(50),
	@strNTLogin nvarchar(50)

as
print 'Adding an email for the object ' + @objID
declare @ID as nvarchar(50)
exec sp_GetUniqueID3 @ID OUTPUT
INSERT INTO A_EMAILS ([ID], [MODBY], [DRCM], [OBJECT_ID])
VALUES (@ID,@strNTLogin,getDate(),@objID)






