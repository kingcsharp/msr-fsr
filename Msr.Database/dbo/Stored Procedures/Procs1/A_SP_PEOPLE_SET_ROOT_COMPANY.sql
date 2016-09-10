





CREATE    PROCEDURE A_SP_PEOPLE_SET_ROOT_COMPANY
@ID nvarchar(50)
as
print 'Setting Root Company for  ' + @ID
declare @curCo as nvarchar(50)
declare @parentCo as nvarchar(50)
SELECT @curCo = COMPANY FROM A_PEOPLE_HISTORY WHERE ID = @ID
SELECT @parentCo = PARENT FROM A_APPROVED_COMPANIES WHERE ID = @curCo
while @parentCo is not null
	begin
		set @curCo = @parentCo
		SELECT @parentCo = PARENT FROM A_APPROVED_COMPANIES WHERE ID = @curCo
	end
update A_PEOPLE_HISTORY SET ROOT_COMPANY = @curCo WHERE ID = @ID
		









