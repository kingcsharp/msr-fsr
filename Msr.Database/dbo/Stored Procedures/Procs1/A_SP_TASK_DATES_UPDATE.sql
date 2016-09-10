







CREATE        PROCEDURE A_SP_TASK_DATES_UPDATE
@taskID varchar(50),
@dateType varchar(50),
@dateValue datetime,
@strNTLogin varchar(50)
AS
declare @tester datetime
	SELECT @tester = THE_DATE FROM A_TASK_DATES
		WHERE DATE_TYPE = @dateType AND TASK_ID = @taskID AND IS_CURRENT = 1

if @dateValue is not null
	begin
	if @tester is NULL or @tester != @dateValue
		begin
		UPDATE A_TASK_DATES SET IS_CURRENT = 0 WHERE TASK_ID = @taskID AND DATE_TYPE = @dateType
		INSERT INTO A_TASK_DATES (ID,DATE_TYPE,THE_DATE,DRCM,MODBY,TASK_ID,IS_CURRENT)
		VALUES (newID(),@dateType,@dateValue,getDAte(),@strNTLogin,@taskID,1)
		end
	end
else
	begin
	if @tester is not null
		begin
		--	print 'Since the new value is null we need to just make sure there is no value by setting all to null'
		UPDATE A_TASK_DATES SET IS_CURRENT= 0 WHERE TASK_ID = @taskID AND DATE_TYPE = @dateType
		end
	end
declare @sql varchar(2000)
set @sql = 'UPDATE A_TASKS SET ' + @dateType + '_DATE = ''' + convert(varchar(50),@dateValue) + ''' WHERE ID = ''' + @taskID + ''''
exec(@sql)

if @dateType = 'ORIG_PLANNED_START'
	begin
	exec A_SP_TASK_DATES_UPDATE @taskID,'CUR_PLANNED_START',@dateValue,@strNTLogin
	set @sql = 'UPDATE A_TASKS SET ' + 'CUR_PLANNED_START' + '_DATE = ''' + convert(varchar(50),@dateValue) + ''' WHERE ID = ''' + @taskID + ''''
	exec(@sql)
	end

if @dateType = 'ORIG_PLANNED_STOP'
	begin
	exec A_SP_TASK_DATES_UPDATE @taskID,'CUR_PLANNED_STOP',@dateValue,@strNTLogin
	set @sql = 'UPDATE A_TASKS SET ' + 'CUR_PLANNED_STOP' + '_DATE = ''' + convert(varchar(50),@dateValue) + ''' WHERE ID = ''' + @taskID + ''''
	end





