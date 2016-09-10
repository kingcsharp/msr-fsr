

CREATE PROCEDURE A_SP_PROCEDURE_GET_LABOR_DATA
	@pID nvarchar(50),
	@procStepID nvarchar(50),
	@strNTLogin nvarchar(50)
AS

if @procStepID is null
	begin
		SELECT * FROM A_V_PROCEDURE_LABOR WHERE PROC_ID = @pID AND PROC_STEP is NULL
	end
else
	begin
		SELECT * FROM A_V_PROCEDURE_LABOR WHERE PROC_ID = @pID AND PROC_STEP = @procStepID
	end




