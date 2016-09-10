
CREATE  PROCEDURE dbo.A_SP_PROCEDURE_STEPS_CREATE_ALL_RELATED_STEPS
@procHistID varchar(50)
as
delete FROM A_PROCEDURE_STEPS_RELATED_STEPS WHERE 
	(
	STEP_ID in (SELECT ID FROM A_PROCEDURE_STEPS WHERE PROCEDURE_ID = @procHistID)
	OR
	REL_STEP_ID in (SELECT ID FROM A_PROCEDURE_STEPS WHERE PROCEDURE_ID = @procHistID)
	)
declare @curs as cursor,@stepID varchar(50)
set @curs = CURSOR FOR SELECT ID FROM A_PROCEDURE_STEPS s WHERE OLD_STEP_ID is null and 
	exists(SELECT ID FROM A_PROCEDURE_STEPS WHERE OLD_STEP_ID = s.ID)
	and PROCEDURE_ID = @procHistID
open @curs
declare @cnt as int
set @cnt = 1
fetch NEXT FROM @curs into @stepID
while @@fetch_status = 0
begin
	print @cnt
	set @cnt = @cnt + 1
	print 'Finding all related steps to step = ' + @stepID
	exec A_SP_PROCEDURE_STEPS_CREATE_RELATED_STEPS_FOR_ONE @stepID
	fetch NEXT FROM @curs into @stepID
end

