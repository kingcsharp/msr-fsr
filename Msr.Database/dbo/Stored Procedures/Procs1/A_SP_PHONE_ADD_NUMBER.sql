




CREATE   procedure A_SP_PHONE_ADD_NUMBER
	@objID nvarchar(50),
	@strNTLogin nvarchar(50)

as
print 'Adding a phone number for the object ' + @objID
declare @ID as nvarchar(50)
exec sp_GetUniqueID3 @ID OUTPUT
INSERT INTO A_PHONE_NUMBERS ([ID], [MODBY], [DRCM], [OBJECT_ID])
VALUES (@ID,@strNTLogin,getDate(),@objID)







