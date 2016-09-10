


CREATE   PROCEDURE A_SP_TASK_COUNTER_UPDATE
@taskID varchar(50),
@type varchar(50),
@val numeric,
@strNTLogin varchar(50)
AS
--print 'In SP A_SP_TASK_COUNTER_UPDATE'
if @val is not null
	begin
	declare @tester numeric
	SELECT @tester = VAL FROM A_TASK_COUNTERS
	WHERE COUNTER_TYPE = @type AND TASK_ID = @taskID AND IS_CURRENT = 1
--	print 'Tested to find the ' + @type + ' for task ' + @taskID
--	print 'It is '
	print isNull(str(@tester),'NULL')
	if @tester is NULL or @tester != @val
		begin
--		print 'Need to add this count and make it the current one'
		UPDATE A_TASK_COUNTERS SET IS_CURRENT = 0 WHERE TASK_ID = @taskID AND COUNTER_TYPE = @type
		INSERT INTO A_TASK_COUNTERS (ID,COUNTER_TYPE,VAL,DRCM,MODBY,TASK_ID,IS_CURRENT)
		VALUES (newID(),@type,@val,getDAte(),@strNTLogin,@taskID,1)
		end
	end
else
	begin
--	print 'Since the new value is null we need to just make sure there is no value by setting all to null'
	UPDATE A_TASK_COUNTERS SET IS_CURRENT= 0 WHERE TASK_ID = @taskID AND COUNTER_TYPE = @type
	end
--print 'Out of SP A_SP_TASK_COUNTER_UPDATE'



