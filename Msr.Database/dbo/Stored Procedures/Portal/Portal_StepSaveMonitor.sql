CREATE PROCEDURE [dbo].[Portal_StepSaveMonitor]
	@id nvarchar(50),
	@failAction varchar(50),
	@result varchar(50),
	@comment varchar(1000),
	@target REAL, -- varchar(50),
	@tolerance FLOAT, -- varchar(50),
	@theSaurusId varchar(50),
	@SENSOR_MAPPING_ID INT,
	@strNTLogin varchar(50)
AS
	declare @newId nvarchar(50)
	set @newId=NULL
	declare @messages nvarchar(2000)
	set @messages=NULL
	
	exec A_SP_MONITOR_UPDATE_RESULT_AND_COMMENT @newId output, @messages output, @id, @failAction, @result, @comment, @target,
	 @tolerance, @theSaurusId, @SENSOR_MAPPING_ID, @strNTLogin
	SELECT @newId, @messages

	GO