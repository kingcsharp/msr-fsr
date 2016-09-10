

CREATE PROCEDURE dbo.A_SP_PROCEDURE_STEPS_UPDATE_LAST_MODIFIED_DATE
@PH_ID varchar(50),
@P_OBJ_ID varchar(50)
--@strNTLogin varchar(50)
AS

declare @curs as cursor,@stepID varchar(50)
if @PH_ID is null
	SELECT @PH_ID = ID FROM A_PROCEDURES_HISTORY WHERE OBJECT_ID = @P_OBJ_ID
if @PH_ID IS NULL
	goto problem

set @curs = cursor for SELECT ID FROM A_PROCEDURE_STEPS WHERE PROCEDURE_ID = @PH_ID
open @curs
fetch next from @curs into @stepID
while @@fetch_status = 0
	begin
	print 'I am going to check Stepfor last mod date ' + @stepID
	exec A_SP_PROCEDURE_STEP_FIGURE_OUT_LAST_MOD_DATE @stepID



	fetch next from @curs into @stepID
	end





success:
goto fin

problem:
print 'Could not find a PH_ID to update the steps last modified dates'

fin:

