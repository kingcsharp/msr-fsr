


CREATE  PROCEDURE A_SP_PROCEDURE_GET_MONITOR_DATA
	@pID nvarchar(50),
	@procStepID nvarchar(50),
	@strNTLogin nvarchar(50)
AS

if @procStepID is null
	begin
		SELECT * FROM A_V_PROCEDURE_MONITORS WHERE PROCEDURE_ID = @pID AND STEP_ID is NULL
	end
else
	begin
		SELECT * FROM A_V_PROCEDURE_MONITORS WHERE PROCEDURE_ID = @pID AND STEP_ID = @procStepID
	end





