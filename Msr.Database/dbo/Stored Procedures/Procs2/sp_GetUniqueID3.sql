





CREATE       procedure sp_GetUniqueID3
@ID numeric OUTPUT
as
print 'Getting a unique ID'
select @ID = (select max(ID) from A_UNIQUE_ID)
UPDATE A_UNIQUE_ID SET ID = ID + 1
return @ID






