
CREATE  PROCEDURE dbo.A_SP_SERVICE_CALLS_MASS_IMPORT_TIME_CARD 
@userID varchar(50),
@startDate dateTime,
@workTypeName varchar(2000),
@normHours varchar(200),
@importID varchar(50),
@strNTLogin varchar(50)
AS
declare @repID varchar(50),@workTypeID varchar(50)
SELECT @workTypeID = ID FROM A_SERVICE_CALLS_WORK_TYPES WHERE WORK_TYPE_NAME = @workTypeName
if @workTypeID is null
	begin
	set @importID = 'WORK_TYPE_NULL'
	print 'WORK TYPE NULL---- FAILING'
	goto FIN
	end
if @importID is null
	exec sp_GetUniqueID3 @importID OUTPUT
print 'The import ID = ' + @importID
DELETE FROM A_SERVICE_CALLS_WEEKLY_REPORTS WHERE IMPORT_ID IS NOT NULL AND IMPORT_ID <> @importID AND ACTUAL_START_DATE = @startDate
exec A_SP_SERVICE_CALL_UPDATE_ONE_WEEKLY_REPORT
@repID OUTPUT,
null,
null, --@weeklyID varchar(50),
@startDate, -- datetime,
null, --@machineName varchar(1000),
@workTypeID, -- varchar(50),
null, --@expensesDescription nvarchar(4000),
null, --@expensesAmount nvarchar(50),
null, --@orderNumber varchar(50),
null, --@comment varchar(4000),
null, --@refFiles varchar(4000),
@normHours, --@allNormalHours varchar (1000),
null, --@allOverHours varchar (1000),
@userID,-- varchar(50),
@strNTLogin -- varchar(50)

UPDATE A_SERVICE_CALLS_WEEKLY_REPORTS SET IMPORT_ID = @importID WHERE ID = @repID
UPDATE A_SERVICE_CALLS_WEEKLY_REPORTS SET STATUS = 'CLOSED' WHERE ID = @repID


FIN:
SELECT @importID AS IMPORT_ID,@repID as REPORT_ID


