

CREATE PROCEDURE A_SP_PEOPLE_UPDATE_ONE_PERSON_SUB_TABLE
@ID varchar(50)
AS

print 'First Delete wherever thid id is sub ' + @ID

DELETE FROM A_PEOPLE_SUB_LOOKUP_TABLE WHERE SUBORDINATE = @ID
print 'fixing to run through the list and add all the bosses to the top for id = ' + @ID

declare @curBoss as varchar(50)
select @curBoss = BOSS FROM A_APPROVED_PEOPLE WHERE ID = @ID
while @curBoss is not null
	begin
	INSERT INTO A_PEOPLE_SUB_LOOKUP_TABLE (ID,BOSS,SUBORDINATE)
	VALUES (newID(),@curBoss,@ID)
	exec A_SP_PEOPLE_UPDATE_ONE_PERSON_SUB_TABLE @curBoss
	SELECT @curBoss = BOSS FROM A_APPROVED_PEOPLE WHERE ID = @curBoss
	end




