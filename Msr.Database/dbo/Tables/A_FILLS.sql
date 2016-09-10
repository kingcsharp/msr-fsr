CREATE TABLE [dbo].[A_FILLS] (
    [ID]            VARCHAR (50) NOT NULL,
    [FILL_OBJ_ID]   VARCHAR (50) NULL,
    [FILL_QTY]      REAL         NULL,
    [FILLER]        VARCHAR (50) NULL,
    [DRCM]          DATETIME     NULL,
    [MODBY]         VARCHAR (50) NULL,
    [FILL_BY]       VARCHAR (50) NULL,
    [PURCH_ITEM_ID] VARCHAR (50) NULL,
    [TASK_ID]       VARCHAR (50) NULL,
    [SUB_FILL_FOR]  VARCHAR (50) NULL,
    [DONT_BILL]     TINYINT      NULL,
    [PRICE]         MONEY        NULL,
    [PURCH_HIST_ID] VARCHAR (50) NULL,
    [BATCH_PARENT]  VARCHAR (50) NULL,
    [BATCHED]       TINYINT      NULL,
    [BATCH_FILL]    TINYINT      NULL,
    CONSTRAINT [PK_A_FILLS] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO


CREATE  TRIGGER [dbo].[A_FILLS_INSERT]
ON [dbo].[A_FILLS]
AFTER INSERT
AS
declare @ID varchar(50)
SELECT @ID = ID FROM INSERTED
exec A_SP_FILL_CREATE_TASK_FOR_FILLER @ID

GO






CREATE       TRIGGER [dbo].[A_FILLS_UPDATE]
ON [dbo].[A_FILLS]
AFTER UPDATE
AS
print 'In A_FILLS_UPDATE'
declare @ID as varchar(50),@purchHistID varchar(50),@fillQ float,@PURCH_ITEM_ID varchar(50),@totQ float,
	@totPrice money,@strNTLogin varchar(50)
 if UPDATE(FILL_OBJ_ID)
 	begin
	SELECT @PURCH_ITEM_ID = PURCH_ITEM_ID,@fillQ = FILL_QTY,@ID = ID,@strNTLogin = modby FROM INSERTED
	if @PURCH_ITEM_ID is not null
		begin
	 	SELECT @totPrice = TOTAL_PRICE,@totQ = TOTAL_QTY,
				@purchHistID = PURCHASE_HIST_ID 
			FROM A_ORDER_ITEMS WHERE ID = @PURCH_ITEM_ID
	  	exec A_SP_PURCHASE_UPDATE_FILL_STATUS @purchHistID,@strNTLogin
		UPDATE A_FILLS SET
			PRICE = (@fillQ / @totQ) * @totPrice
			WHERE ID = @ID
		end
 	end
print 'Out of A_FILLS_UPDATE'






