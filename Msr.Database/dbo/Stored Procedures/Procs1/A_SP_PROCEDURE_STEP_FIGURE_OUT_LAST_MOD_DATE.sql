CREATE procedure dbo.A_SP_PROCEDURE_STEP_FIGURE_OUT_LAST_MOD_DATE
@stepID varchar(50)
AS
print '****************'
declare @firstStep varchar(50),@curStep varchar(50),@curProcedureID varchar(50),
	@curObjID varchar(50),@hasChanged tinyInt, @changeDate datetime
set @curStep = @stepID
declare @curModDate datetime
SELECT @curProcedureID = PROCEDURE_ID FROM A_PROCEDURE_STEPS WHERE ID = @curStep
SELECT @curObjID = OBJECT_ID FROM A_PROCEDURES_HISTORY WHERE ID = @curProcedureID
SELECT @curModDate = APPROVAL_DATE FROM A_OBJECTS WHERE ID = @curObjID
print 'looking to figure out last mod date for step ' + @stepID
print 'My Mod date is'
print @curModDate
select @curStep = OLD_STEP_ID FROM A_PROCEDURE_STEPS WHERE ID = @curStep
while @curStep is not null and @changeDate is null
	begin
	exec A_SP_PROCEDURE_STEPS_COMPARE_TWO_STEPS @hasChanged OUTPUT, @stepID, @curStep	
	if @hasChanged = 1
		begin
		print 'This one has changed so I am setting the change date to '
		print @curModDate
		set @changeDate = @curModDate
		end
	else
		begin
		print 'No Change moving to next step'
		SELECT @curProcedureID = PROCEDURE_ID FROM A_PROCEDURE_STEPS WHERE ID = @curStep
		SELECT @curObjID = OBJECT_ID FROM A_PROCEDURES_HISTORY WHERE ID = @curProcedureID
		SELECT @curModDate = APPROVAL_DATE FROM A_OBJECTS WHERE ID = @curObjID
		select @curStep = OLD_STEP_ID FROM A_PROCEDURE_STEPS WHERE ID = @curStep
		end
	end

if @changeDate is null
	set @changeDate = @curModDate


UPDATE A_PROCEDURE_STEPS SET DATE_LAST_MODIFIED = @changeDate WHERE ID = @stepID


print '****************'
