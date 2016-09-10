
CREATE PROCEDURE dbo.A_SP_PURCHASES_UPDATE_ALL_STATUSES
AS
declare @curs as cursor,@pid varchar(50)
set @curs = CURSOR FOR SELECT ID FROM A_PURCHASES_HISTORY WHERE PURCHASE_STATUS <> 'CLOSED'
open @curs
fetch next from @curs into @pid
while @@fetch_status = 0
	begin
	exec A_SP_PURCHASE_UPDATE_PURCHASE_STATUS @pid,'SYSTEM'
	fetch next from @curs into @pid
	end
