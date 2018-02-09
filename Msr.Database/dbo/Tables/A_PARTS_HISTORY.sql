CREATE TABLE [dbo].[A_PARTS_HISTORY] (
    [ID]                        VARCHAR (50)   NOT NULL,
    [UNIT]                      VARCHAR (50)   NULL,
    [NAME]                      NVARCHAR (200) NULL,
    [PART_TYPE]                 VARCHAR (50)   NULL,
    [SPARE]                     VARCHAR (10)   NULL,
    [CONSUMABLE]                VARCHAR (10)   NULL,
    [TRACK_FROM_START]          SMALLINT       NULL,
    [DRCM]                      DATETIME       NULL,
    [MODBY]                     VARCHAR (50)   NULL,
    [COMPANY]                   VARCHAR (50)   NULL,
    [OBJECT_ID]                 VARCHAR (50)   NULL,
    [COMPANY_NAME]              NVARCHAR (100) NULL,
    [PART_TYPE_NAME]            NVARCHAR (100) NULL,
    [UNIT_SHIPPING_WEIGHT]      FLOAT (53)     NULL,
    [COMPANY_PART_NUMBER]       NVARCHAR (50)  NULL,
    [SUPPLIER_SEE_INSTALL_BASE] SMALLINT       NULL,
    [SUPPLIER_SEE_AVAILABILITY] SMALLINT       NULL,
    [CUSTOMER_SEE_AVAILABILITY] SMALLINT       NULL,
    [WEIGHT_TYPE]               VARCHAR (50)   NULL,
    [CREATE_PROD]               TINYINT        NULL,
    [SUPPLIER_CO]               VARCHAR (50)   NULL,
    [PRODUCT_TYPE]              VARCHAR (10)   NULL,
    [PROC_VERB]                 NVARCHAR (100) NULL,
    [PRICE]                     MONEY          NULL,
    CONSTRAINT [PK_A_PARTS_HISTORY] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO


CREATE    TRIGGER A_PARTS_HISTORY_INSERT
ON dbo.A_PARTS_HISTORY
AFTER INSERT
AS
declare @ID as nvarchar(50)
declare @NAME as nvarchar(2000)
declare @MODBY as nvarchar(50)
SELECT @ID = ID,
	@MODBY = MODBY,
	@NAME = ISNULL(NAME,'') + ISNULL('(' + COMPANY_PART_NUMBER + ')','') 
	FROM INSERTED
exec A_SP_OBJECT_ADD 'A_PARTS_HISTORY',@ID,@NAME,@MODBY,null,null,null



GO


CREATE       TRIGGER A_PARTS_HISTORY_UPDATE
ON dbo.A_PARTS_HISTORY
AFTER UPDATE
AS

declare @OBJ_ID as nvarchar(50)
declare @ID as nvarchar(50)
declare @NAME as nvarchar(2000)
declare @MODBY as nvarchar(50)
declare @LOCATION_ID as nvarchar(50)
SELECT
@ID = ID,
@OBJ_ID = OBJECT_ID,
@NAME = ISNULL(NAME,'') + ISNULL(' (' + COMPANY_PART_NUMBER + ')',''),
@MODBY = MODBY
FROM INSERTED
print 'Checking to see if we need to update the objDesc for the part'
if (SELECT OBJ_DESC FROM A_OBJECTS WHERE ID = @OBJ_ID) != @NAME 
begin
print 'We Do'
UPDATE A_OBJECTS SET
OBJ_DESC = @NAME,
MODBY = @MODBY,
DRCM = getDATE()
WHERE ID = @OBJ_ID
print 'Updated it'
end
print 'Done worrying about the name'


