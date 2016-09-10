CREATE TABLE [dbo].[A_FORECASTS_HISTORY] (
    [ID]              VARCHAR (50)    NOT NULL,
    [OBJECT_ID]       VARCHAR (50)    NULL,
    [START_DATE]      NVARCHAR (50)   NULL,
    [STOP_DATE]       NVARCHAR (50)   NULL,
    [NAME]            NVARCHAR (2000) NULL,
    [START_MONTH]     INT             NULL,
    [START_YEAR]      INT             NULL,
    [DRCM]            DATETIME        NULL,
    [MODBY]           VARCHAR (50)    NULL,
    [CO]              VARCHAR (50)    NULL,
    [STOP_MONTH]      INT             NULL,
    [STOP_YEAR]       INT             NULL,
    [F_TYPE]          VARCHAR (50)    NULL,
    [PURCHASE_OFFSET] REAL            NULL,
    [SALES_OFFSET]    REAL            NULL,
    CONSTRAINT [PK_A_FORECASTS_HISTORY] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO

CREATE  TRIGGER A_FORECASTS_HISTORY_INSERT
ON dbo.A_FORECASTS_HISTORY
AFTER INSERT
AS
declare @ID as nvarchar(50)
declare @NAME as nvarchar(50)
declare @MODBY as nvarchar(50)
SELECT @ID = ID,
	@MODBY = MODBY,
	@NAME = NAME FROM INSERTED
exec A_SP_OBJECT_ADD 'A_FORECASTS_HISTORY',@ID,@NAME,@MODBY,null,null,null


GO



CREATE    TRIGGER A_FORECASTS_HISTORY_UPDATE
ON dbo.A_FORECASTS_HISTORY
AFTER UPDATE
AS
declare @OBJ_ID as nvarchar(50)
declare @ID as nvarchar(50)
declare @NAME as nvarchar(50)
declare @MODBY as nvarchar(50)
declare @START_DATE as nvarchar(50)
declare @STOP_DATE as nvarchar(50)
print 'In the A_FORECASTS_HISTORY Update Trigger'
SELECT
@ID = ID,
@OBJ_ID = OBJECT_ID,
@NAME = NAME,
@MODBY = MODBY, 
@START_DATE = CONVERT(nvarchar,START_MONTH) + '/' + CONVERT(nvarchar,START_YEAR),
@STOP_DATE = CONVERT(nvarchar,STOP_MONTH) + '/' + CONVERT(nvarchar,STOP_YEAR)
FROM INSERTED

declare @myObjName as nvarchar(100)
SELECT @myObjName = OBJ_DESC FROM A_OBJECTS WHERE ID = @OBJ_ID
if @myObjName <> @NAME
	begin
	UPDATE A_OBJECTS SET
	OBJ_DESC = @NAME,
	MODBY = @MODBY,
	DRCM = getDATE()
	WHERE ID = @OBJ_ID
	end

IF UPDATE(START_MONTH) OR UPDATE(START_YEAR)
	begin
	UPDATE A_FORECASTS_HISTORY SET START_DATE = @START_DATE WHERE ID = @ID
	end

IF UPDATE(STOP_MONTH) OR UPDATE(STOP_YEAR)
	begin
	UPDATE A_FORECASTS_HISTORY SET STOP_DATE = @STOP_DATE WHERE ID = @ID
	end

IF UPDATE(START_MONTH) OR UPDATE(START_YEAR) OR UPDATE(STOP_MONTH) OR UPDATE(STOP_YEAR)
	begin
	print 'The dates were modified so now we need to edit the distributions of all our Forecast Items'
	
	Declare @oneItem varchar(50)
	Declare @Cur Cursor
	set @Cur = Cursor For SELECT ID FROM A_FORECAST_ITEMS WHERE FORECAST_ID = @ID
	open @Cur
	Fetch Next from @Cur Into @oneItem
	while (@@fetch_status = 0)
		Begin
		print @oneItem
		exec A_SP_FORECAST_ITEM_FIX_DISTRIBUTION @oneItem,@MODBY
		Fetch Next from @Cur Into @oneItem
		End
	close @Cur
	Deallocate @Cur		
END




