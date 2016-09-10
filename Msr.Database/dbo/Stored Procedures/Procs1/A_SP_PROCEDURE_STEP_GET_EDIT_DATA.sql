



CREATE    PROCEDURE A_SP_PROCEDURE_STEP_GET_EDIT_DATA
@ID nvarchar(50),
@strNTLogin nvarchar(50)
AS
declare @firstPrevStep as varchar(50)
SELECT top 1 @firstPrevStep = PREV_STEP FROM A_PROCEDURE_STEP_PRECEDING_STEPS 
WHERE MY_STEP = @ID

declare @myProcID as varchar(50)
SELECT @myProcID = PROCEDURE_ID FROM A_PROCEDURE_STEPS WHERE ID = @ID

declare @stepsAllowed as smallInt
SELECT @stepsAllowed = STEPS_IN_AP FROM A_PROCEDURES_HISTORY WHERE ID = @myProcID

SELECT *,@firstPrevStep as FIRST_PREV_STEP,@stepsAllowed as STEPS_IN_AP FROM A_V_PROCEDURE_STEPS WHERE ID = @ID





