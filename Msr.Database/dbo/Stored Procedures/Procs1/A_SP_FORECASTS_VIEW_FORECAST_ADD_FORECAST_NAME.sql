






CREATE        PROCEDURE A_SP_FORECASTS_VIEW_FORECAST_ADD_FORECAST_NAME
@forecastID varchar(50),
@grouperNumber int OUTPUT,
@itemType varchar(50),
@lev int
AS
declare @fName varchar(1000),@fID varchar(50)
SELECT @fID = ID,@fname = NAME FROM A_FORECASTS_HISTORY WHERE ID = @forecastID
INSERT INTO #tempForecast(USE_PUT_TEXT,VIEW_ITEM,NAME,ID,TREE_LEVEL,FORECAST_ID,TREE_HAS_CHILD,EXPANDED,HIDDEN)
	VALUES(0,@grouperNumber,'FORECAST/BUDGET = ' +@fname,@fID,@lev,@fID,0,0,0)
set @grouperNumber = @grouperNumber + 1


