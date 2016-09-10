
CREATE     PROCEDURE A_SP_PROCEDURE_SET_PRINT_ORDER
@PID nvarchar(50),
@strNTLogin nvarchar(50)
AS

declare @PRINT_ORDER as int
set @PRINT_ORDER = 1

print 'so first we are going to set all the null steps to be print order 1'
UPDATE A_PROCEDURE_STEPS SET PRINT_ORDER = '1' WHERE PROCEDURE_ID = @PID
declare @maxCnt as integer
SELECT @maxCnt = count(ID) FROM A_PROCEDURE_STEPS WHERE PROCEDURE_ID = @PID

print 'Now that we have done that we should set the rest of them = to max until we set them really'
UPDATE A_PROCEDURE_STEPS SET PRINT_ORDER = @maxCnt
WHERE PROCEDURE_ID = @PID AND 
ID IN 
(SELECT MY_STEP 
	FROM A_PROCEDURE_STEP_PRECEDING_STEPS 
	WHERE PROCEDURE_ID = @PID AND PREV_STEP is not NULL)

print 'Now We are going to have to go through the steps exactly maxCnt times to make sure we set them properly'
declare @cnt as int
set @cnt = 1

while @cnt < @maxCnt
	begin
	UPDATE A_PROCEDURE_STEPS SET PRINT_ORDER = (@cnt + 1) 
	WHERE ID in
		(SELECT MY_STEP 
		FROM A_V_PROCEDURE_STEP_PREV_W_PRNT_ORDER 
		WHERE PROCEDURE_ID = @PID AND PRINT_ORDER =@cnt)
	set @cnt = @cnt + 1
	end

