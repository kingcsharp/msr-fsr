

CREATE   PROCEDURE dbo.A_SP_FORECASTS_VIEW_FORECAST_ADD_TOTAL_ROWS_PLACE_HOLDER
@foreCastID varchar(50),
@firstMonth datetime,
@lastMonth datetime,
@viewItem int OUTPUT,
@lev int
AS
declare @forecastName varchar(1000)
SELECT @forecastName = NAME FROM A_FORECASTS_HISTORY WHERE ID = @forecastID
print 'ADDING TOTALS For Forecast' + @foreCastID
INSERT INTO #tempForecast (USE_PUT_TEXT,VIEW_ITEM,ID,PARENT_ID,NAME,TREE_LEVEL,FORECAST_ID,HIDDEN,TREE_HAS_CHILD,EXPANDED)
VALUES(1,@viewItem,@foreCastID + '_TSR',NULL,'Total Sales Revenue ',@lev,@foreCastID,0,0,0)
set @viewITem = @viewItem + 1
INSERT INTO #tempForecast (USE_PUT_TEXT,VIEW_ITEM,ID,PARENT_ID,NAME,TREE_LEVEL,FORECAST_ID,HIDDEN,TREE_HAS_CHILD,EXPANDED)
VALUES(1,@viewItem,@foreCastID + '_TCS',NULL,'Total Cost Of Sales ',@lev,@foreCastID,0,0,0)
set @viewITem = @viewItem + 1
INSERT INTO #tempForecast (USE_PUT_TEXT,VIEW_ITEM,ID,PARENT_ID,NAME,TREE_LEVEL,FORECAST_ID,HIDDEN,TREE_HAS_CHILD,EXPANDED)
VALUES(1,@viewItem,@foreCastID + '_GM',NULL,'Gross Margin ',@lev,@foreCastID,0,0,0)
set @viewITem = @viewItem + 1
INSERT INTO #tempForecast (USE_PUT_TEXT,VIEW_ITEM,ID,PARENT_ID,NAME,TREE_LEVEL,FORECAST_ID,HIDDEN,TREE_HAS_CHILD,EXPANDED)
VALUES(1,@viewItem,@foreCastID + '_OE',NULL,'Total Operating Expenses ',@lev,@foreCastID,0,0,0)
set @viewITem = @viewItem + 1
INSERT INTO #tempForecast (USE_PUT_TEXT,VIEW_ITEM,ID,PARENT_ID,NAME,TREE_LEVEL,FORECAST_ID,HIDDEN,TREE_HAS_CHILD,EXPANDED)
VALUES(1,@viewItem,@foreCastID + '_EBIT',NULL,'EBIT ',@lev,@foreCastID,0,0,0)
set @viewITem = @viewItem + 1
INSERT INTO #tempForecast (USE_PUT_TEXT,VIEW_ITEM,ID,PARENT_ID,NAME,TREE_LEVEL,FORECAST_ID,HIDDEN,TREE_HAS_CHILD,EXPANDED)
VALUES(1,@viewItem,@foreCastID + '_INT',NULL,'Total Interest ',@lev,@foreCastID,0,0,0)
set @viewITem = @viewItem + 1
INSERT INTO #tempForecast (USE_PUT_TEXT,VIEW_ITEM,ID,PARENT_ID,NAME,TREE_LEVEL,FORECAST_ID,HIDDEN,TREE_HAS_CHILD,EXPANDED)
VALUES(1,@viewItem,@foreCastID + '_EBT',NULL,'EBT ',@lev,@foreCastID,0,0,0)
set @viewITem = @viewItem + 1
INSERT INTO #tempForecast (USE_PUT_TEXT,VIEW_ITEM,ID,PARENT_ID,NAME,TREE_LEVEL,FORECAST_ID,HIDDEN,TREE_HAS_CHILD,EXPANDED)
VALUES(1,@viewItem,@foreCastID + '_TAX',NULL,'Total Taxes ',@lev,@foreCastID,0,0,0)
set @viewITem = @viewItem + 1
INSERT INTO #tempForecast (USE_PUT_TEXT,VIEW_ITEM,ID,PARENT_ID,NAME,TREE_LEVEL,FORECAST_ID,HIDDEN,TREE_HAS_CHILD,EXPANDED)
VALUES(1,@viewItem,@foreCastID + '_NET',NULL,'Net Profit ',@lev,@foreCastID,0,0,0)
set @viewITem = @viewItem + 1





