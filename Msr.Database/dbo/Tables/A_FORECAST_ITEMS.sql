CREATE TABLE [dbo].[A_FORECAST_ITEMS] (
    [ID]                      VARCHAR (50)    NOT NULL,
    [FORECAST_ID]             VARCHAR (50)    NULL,
    [ACCOUNT_ID]              VARCHAR (50)    NULL,
    [F_TYPE]                  VARCHAR (50)    NULL,
    [QTY]                     FLOAT (53)      NULL,
    [F_AMT]                   MONEY           NULL,
    [PERCENT_OF_REV]          FLOAT (53)      NULL,
    [PROGRESS]                VARCHAR (50)    NULL,
    [DRCM]                    DATETIME        NULL,
    [MODBY]                   VARCHAR (50)    NULL,
    [AVG_MONTHLY]             MONEY           NULL,
    [CONFIDENCE]              FLOAT (53)      NULL,
    [NOTE]                    NVARCHAR (2000) NULL,
    [AMT_INVOICED]            MONEY           NULL,
    [DATE_ADDED]              DATETIME        NULL,
    [EST_QUAL_START_DATE]     DATETIME        NULL,
    [ACT_QUAL_START_DATE]     DATETIME        NULL,
    [EST_FIRST_PURCHASE_DATE] DATETIME        NULL,
    [ACT_FIRST_PURCHASE_DATE] DATETIME        NULL,
    [STATUS]                  NVARCHAR (2000) NULL,
    [PRIORITY]                VARCHAR (50)    NULL,
    [SUPPLIER_OWNER]          NVARCHAR (500)  NULL,
    [CUSTOMER_OWNER]          NVARCHAR (500)  NULL,
    [UNIT_PRICE]              MONEY           NULL,
    [UNIT_COST]               MONEY           NULL,
    CONSTRAINT [PK_A_FORECAST_ITEMS] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO

CREATE    TRIGGER A_FORECAST_ITEMS_UPDATE_INSERT
ON dbo.A_FORECAST_ITEMS
AFTER UPDATE,INSERT
AS
declare @ID as nvarchar(50)
declare @AMT as money
print 'In the A_FORECAST_ITEMS_UPDATE_INSERT Trigger'
SELECT
@ID = ID,
@AMT = F_AMT
FROM INSERTED

if UPDATE(F_AMT)
	begin
	declare @m as integer
	SELECT @m = COUNT(ID) FROM A_FORECAST_MONTHLY_BREAKDOWN WHERE FI_ID = @ID
	if (@m > 0)
		begin
		UPDATE A_FORECAST_ITEMS SET AVG_MONTHLY = (@AMT/@m) WHERE ID = @ID

		end
	

	end
print 'Out of the A_FORECAST_ITEMS_UPDATE_INSERT Trigger'
