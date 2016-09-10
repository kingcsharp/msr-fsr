

CREATE   PROCEDURE dbo.A_SP_PROCEDURE_STEPS_FIX_RECEIVE_STEP_FOR_MSR
AS
declare @curs as cursor,@it as varchar(50),@pid varchar(50),@newID varchar(50),@strNTLogin varchar(50)
set @strNTLogin = '7'
Set @curs = cursor for SELECT STEP_ID,PROC_HIST_ID 
				FROM A_V_PROCEDURE_STEPS_WITH_PROC_DATA 
				WHERE CREATING_CO = '310' AND SYSTEM_TASK = 'SYS_RECEIVE' AND 
						STEP_TEXT LIKE '%Move From Shop%'
open @curs
fetch next from @curs into @it,@pid
while @@fetch_status = 0
	begin
		exec sp_GetUniqueID3 @newID OUTPUT
		INSERT INTO A_PROCEDURE_STEPS 
		(ID,PROCEDURE_ID,STEP_TEXT,START_ON_COUNTER,SYSTEM_TASK,DESTINATION,DRCM,MODBY,PRINT_ORDER,DURATION,DURATION_TYPE)
		SELECT
			@newID,
			PROCEDURE_ID,replace(replace(STEP_TEXT,'Move From Shop','Receive at Customer'),'Location of part changes from MSR''s address back to customer''s address.  Can''t confirm it is actually there yet.','Location of part changes back to customer''s address.'),START_ON_COUNTER,'SYS_RECEIVE',DESTINATION,DRCM,MODBY,PRINT_ORDER + 1,DURATION,DURATION_TYPE 
		FROM A_PROCEDURE_STEPS WHERE ID = @it
		INSERT INTO A_PROCEDURE_OBJECT_LINK (ID,PROCEDURE_ID,STEP_ID,APPROVED_OBJECT_ID,QTY,QTY_TYPE,RELATIONSHIP,DRCM,MODBY,LABOR_ROLE)
		SELECT newID(),PROCEDURE_ID,@newID,APPROVED_OBJECT_ID,QTY,QTY_TYPE,RELATIONSHIP,getDate(),@strNTLogin,LABOR_ROLE FROM
				A_PROCEDURE_OBJECT_LINK WHERE STEP_ID = @it
		UPDATE A_PROCEDURE_STEPS SET SYSTEM_TASK = 'SYS_SEND',STEP_TEXT = replace(STEP_TEXT,'Location of part changes from MSR''s address back to customer''s address.  Can''t confirm it is actually there yet.','Location of part changes to unknown because it is in transit.') WHERE ID = @it
		exec A_SP_PROCEDURE_FIX_PRECEDING_STEPS_BASED_ON_PRINT_ORDER @pid,@strNTLogin		
		fetch next from @curs into @it,@pid
	end




