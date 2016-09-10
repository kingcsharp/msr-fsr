CREATE PROCEDURE dbo.A_SP_PROCEDURE_STEPS_COMPARE_TWO_STEPS
@hasChanged tinyint OUTPUT,
@step1 varchar(50),
@step2 varchar(50)
AS
print 'Looking for differences between ' + @step1 + ' and ' + @step2
if @step1 = @step2
	goto isSame
print 'Checking text'
if not exists (SELECT ID FROM A_PROCEDURE_STEPS table1 
				WHERE ID = @step1 AND 
					EXISTS(
						SELECT * FROM A_PROCEDURE_STEPS table2 
						WHERE ID = @step2 AND table2.STEP_TEXT = table1.STEP_TEXT
					)
				)
	begin
	print 'These are different'
	goto isDifferent
	end





isSame:
set @hasChanged = 0
goto fin
isDifferent:
set @hasChanged = 1
goto fin


fin:
