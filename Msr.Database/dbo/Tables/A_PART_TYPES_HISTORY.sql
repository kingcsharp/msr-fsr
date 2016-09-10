CREATE TABLE [dbo].[A_PART_TYPES_HISTORY] (
    [ID]                   VARCHAR (50)    NOT NULL,
    [NAME]                 NVARCHAR (4000) NOT NULL,
    [SPARE]                VARCHAR (10)    NULL,
    [CONSUMABLE]           VARCHAR (10)    NULL,
    [DRCM]                 DATETIME        NULL,
    [MODBY]                CHAR (10)       NULL,
    [OBJECT_ID]            VARCHAR (50)    NULL,
    [UNIT]                 NVARCHAR (50)   NULL,
    [UNIT_SHIPPING_WEIGHT] NUMERIC (18)    NULL,
    CONSTRAINT [PK_A_PART_TYPES_HISTORY] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE   TRIGGER A_PART_TYPES_HISTORY_INSERT
ON dbo.A_PART_TYPES_HISTORY
AFTER INSERT
AS
declare @ID as nvarchar(50)
declare @NAME as nvarchar(50)
declare @MODBY as nvarchar(50)
SELECT @ID = ID,
	@MODBY = MODBY,
	@NAME = NAME FROM INSERTED
exec A_SP_OBJECT_ADD 'A_PART_TYPES_HISTORY',@ID,@NAME,@MODBY,null,null,null

GO
CREATE    TRIGGER A_PART_TYPES_HISTORY_UPDATE
ON dbo.A_PART_TYPES_HISTORY
AFTER UPDATE
AS


if update(NAME)
	begin
		declare @OBJ_ID as nvarchar(50)
		declare @NAME as nvarchar(50)
		declare @MODBY as nvarchar(50)
		SELECT
		@OBJ_ID = OBJECT_ID,
		@NAME = NAME,
		@MODBY = MODBY
		FROM INSERTED
		UPDATE A_OBJECTS SET
		OBJ_DESC = @NAME,
		MODBY = @MODBY,
		DRCM = getDATE()
		WHERE ID = @OBJ_ID
	end
