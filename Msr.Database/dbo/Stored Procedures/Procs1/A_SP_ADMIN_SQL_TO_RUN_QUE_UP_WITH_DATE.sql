





CREATE PROCEDURE dbo.A_SP_ADMIN_SQL_TO_RUN_QUE_UP_WITH_DATE
@mySQL varchar(7500),
@myDate dateTime,
@strNTLogin varchar(50)
AS
if @mySQL is null 
	goto fin
print 'Queing up some SQL with a date'
declare @sqlQueSwitch varchar(50)
SELECT @sqlQueSwitch = VAL FROM A_ADMIN_CONFIGURATION WHERE NAME = 'SQL_QUE'
if @sqlQueSwitch is null or @sqlQueSwitch = 'QUE'
	INSERT INTO A_ADMIN_SQL_TO_RUN (ID,CODE,DRCM,MODBY,STATUS,RUN_AT_DATE)
		VALUES(newID(),@mySQL,getdate(),@strNTLogin,'WAITING',@myDate)
else
	begin
	print 'Running the SQL = '
	print @mySQL
	exec(@mySQL)
	end


fin:




