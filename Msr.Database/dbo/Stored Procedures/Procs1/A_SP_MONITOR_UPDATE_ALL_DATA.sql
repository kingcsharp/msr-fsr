





CREATE       PROCEDURE dbo.A_SP_MONITOR_UPDATE_ALL_DATA
@ID varchar(50)
AS
if dbo.MD() = 1 print 'Updating all the data about a monitor'
declare @mType varchar(50),@taskID varchar(50),@ynA varchar(50),@resID varchar(50),@ynRes varchar(50),
	@myTarget varchar(50),@corAnsID varchar(50),@targetID varchar(50),@targetTXT varchar(2000),
	@resTXT varchar(2000),@myANSWER varchar(50),@ALWAYS_PASS tinyint


SELECT @mType = MONITOR_TYPE,
	@taskID = TASK_ID, @ynA = YES_NO_ANSWER,@corAnsID = CORRECT_ANSWER_ID,@ALWAYS_PASS = ALWAYS_PASS
	FROM A_MONITOR_TEMPLATES WHERE ID = @ID
if @mType = 'YES_NO'
	begin
		if dbo.MD() = 1 print 'This is a yes no monitor'
		SELECT @myANSWER = TEXT_VAL,@resID = ID,@ynRes = TEXT_VAL from A_MONITOR_RESULTS WHERE MONITOR_TEMPLATE_ID = @ID
		if @ynRes = @ynA
			UPDATE A_MONITOR_TEMPLATES SET IS_PASSING = 1 WHERE ID = @ID
		else
			UPDATE A_MONITOR_TEMPLATES SET IS_PASSING = 0 WHERE ID = @ID
	end
if @mType = 'MULTIPLE'
	begin
		if dbo.MD() = 1 print 'This is a Multiple Choice monitor'
		SELECT @targetID = ID,@targetTXT = TXT from A_MONITOR_TEMPLATES_MULT_CHOICE 
			WHERE MONITOR_ID = @ID AND IS_ANSWER = 1
		SELECT  @resID = MULT_CHOICE_ANSWER, @resTXT = PRINT_RESULT  
			FROM A_MONITOR_RESULTS
			WHERE     (MONITOR_TEMPLATE_ID = @ID)
		SELECT @myANSWER=ROOT_ID FROM A_MONITOR_TEMPLATES_MULT_CHOICE WHERE ID = @resID
		UPDATE A_MONITOR_TEMPLATES SET TARGET_ANSWER_ID = @targetID,CORRECT_ANSWER_ID = @targetID
			WHERE ID = @ID
		if isNull(@resID,'') = isNull(@targetID,'')
			UPDATE A_MONITOR_TEMPLATES SET IS_PASSING = 1 WHERE ID = @ID
		else
			UPDATE A_MONITOR_TEMPLATES SET IS_PASSING = 0 WHERE ID = @ID
	end
declare @targetText nvarchar(2000)
if @mType = 'TEXT'
	begin
		if dbo.MD() = 1 print 'This is a TEXT monitor'
		SELECT @myANSWER=TEXT_VAL FROM A_MONITOR_RESULTS WHERE MONITOR_TEMPLATE_ID = @ID
		UPDATE A_MONITOR_RESULTS SET PRINT_RESULT = @myANSWER WHERE MONITOR_TEMPLATE_ID = @ID
		SELECT @targetText = TEXT_TARGET FROM A_MONITOR_TEMPLATES WHERE ID = @ID
		if isNull(@myANSWER,'') = isNull(@targetText,'')
			UPDATE A_MONITOR_TEMPLATES SET IS_PASSING = 1 WHERE ID = @ID
		else
			UPDATE A_MONITOR_TEMPLATES SET IS_PASSING = 0 WHERE ID = @ID
	end


declare @myNum float,@targNum float,@highNum float,@highestNum float,@lowNum float,
	@lowestNum float,@myType varchar(50),@myPass tinyInt

if @mType = 'NUMBER' OR @mType = 'USER_NUMBER'
	begin
		if dbo.MD() = 1 print 'This is a Number monitor'
		SELECT @myNUM=NUM_VAL FROM A_MONITOR_RESULTS WHERE MONITOR_TEMPLATE_ID = @ID
		SELECT @targNum = TARGET,
			@highNum = isNULL(HIGH_THRESHOLD,TARGET),
			@lowNum = isNULL(LOW_THRESHOLD,TARGET),
			@myType = SHOULD_BE
			FROM A_MONITOR_TEMPLATES WHERE ID = @ID

			SELECT
			@lowestNum = isNULL(LOWEST_THRESHOLD,@lowNum),
			@highestNum = isNULL(HIGHEST_THRESHOLD,@highNum)
			FROM A_MONITOR_TEMPLATES WHERE ID = @ID

		if @myType = 'EQUAL' OR @myType = 'BETWEEN'
			begin
			if @myNum > @highestNum 
				select @myPass = 0, @myANSWER = 'TOO_HIGH'
			else
				if @myNum < @lowestNum 
					select @myPass = 0, @myANSWER = 'TOO_LOW'
				else
					select @myPass = 1, @myANSWER = 'IN_RANGE'
			end			


		if @myType = 'BELOW'
			begin
			if @myNum > @highestNum 
				select @myPass = 0, @myANSWER = 'TOO_HIGH'
			else
					select @myPass = 1, @myANSWER = 'IN_RANGE'
			end			
		if @myType = 'ABOVE'
			begin
			if @myNum < @lowestNum 
				select @myPass = 0, @myANSWER = 'TOO_LOW'
			else
					select @myPass = 1, @myANSWER = 'IN_RANGE'
			end			



numFin:
			UPDATE A_MONITOR_TEMPLATES SET IS_PASSING = @myPass WHERE ID = @ID
	end

print 'Updating My Answer now'
UPDATE A_MONITOR_TEMPLATES SET MY_ANSWER = @myANSWER WHERE ID = @ID

if @ALWAYS_PASS = 1 
	UPDATE A_MONITOR_TEMPLATES SET IS_PASSING = 1 WHERE ID = @ID












