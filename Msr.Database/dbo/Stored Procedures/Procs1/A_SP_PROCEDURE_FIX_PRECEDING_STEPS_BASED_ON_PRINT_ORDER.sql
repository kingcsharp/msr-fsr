CREATE PROCEDURE dbo.A_SP_PROCEDURE_FIX_PRECEDING_STEPS_BASED_ON_PRINT_ORDER
@procHistID varchar(50),
@strNTlogin varchar(50)
AS
DELETE FROM A_PROCEDURE_STEP_PRECEDING_STEPS WHERE PROCEDURE_ID = @procHistID
declare @curs as CURSOR,@curStep varchar(50),@prevStep varchar(50)
set @curs = CURSOR FOR SELECT ID 
		FROM A_PROCEDURE_STEPS 
			WHERE PROCEDURE_ID = @procHistID 
				ORDER BY PRINT_ORDER
open @curs
fetch next from @curs into @curStep
while @@fetch_status = 0
	begin
	if @prevStep is not null
		begin
		INSERT INTO A_PROCEDURE_STEP_PRECEDING_STEPS
			(ID,MY_STEP,PREV_STEP,DRCM,MODBY,PROCEDURE_ID)
		VALUES
			(newID(),
			@curStep,
			@prevStep,
			getDate(),
			@strNTLogin,
			@procHistID
			)
		end
	set @prevStep = @curStep
	fetch next from @curs into @curStep
	end

