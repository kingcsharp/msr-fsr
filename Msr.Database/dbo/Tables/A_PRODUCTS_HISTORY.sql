CREATE TABLE [dbo].[A_PRODUCTS_HISTORY] (
    [ID]               VARCHAR (50)    NOT NULL,
    [NAME]             NVARCHAR (1000) NULL,
    [SUPPLIER_ID]      VARCHAR (50)    NULL,
    [COMMENTS]         NVARCHAR (1000) NULL,
    [PROCEDURE_ID]     VARCHAR (50)    NULL,
    [APP_OBJECT]       VARCHAR (50)    NULL,
    [SHIP_OR_LABOR]    SMALLINT        NULL,
    [CUSTOMIZABLE]     SMALLINT        NULL,
    [REQ_FORM]         VARCHAR (50)    NULL,
    [MGR_TEAM]         VARCHAR (50)    NULL,
    [SALES_TAX]        SMALLINT        NULL,
    [DRCM]             DATETIME        NULL,
    [MODBY]            VARCHAR (50)    NULL,
    [OBJECT_ID]        VARCHAR (50)    NULL,
    [PARENT_ID]        VARCHAR (50)    NULL,
    [SYSTEM_PROCEDURE] VARCHAR (50)    NULL,
    [CUST_MGR_ROLE]    VARCHAR (50)    NULL,
    [PERSON_SUPPLIER]  VARCHAR (50)    NULL,
    [AVAILABILITY]     TINYINT         NULL,
    [OEM]              VARCHAR (200)   NULL,
    [MODEL]            VARCHAR (200)   NULL,
    [AREA]             VARCHAR (200)   NULL,
    [CU]               TINYINT         NULL,
    [MM]               VARCHAR (50)    NULL,
    CONSTRAINT [PK_A_PRODUCT_HISTORY] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO


CREATE        TRIGGER A_PRODUCTS_HISTORY_INSERT
ON dbo.A_PRODUCTS_HISTORY
AFTER INSERT
AS
declare @ID as nvarchar(50)
declare @NAME as nvarchar(50)
declare @MODBY as nvarchar(50)
SELECT @ID = ID,
	@MODBY = MODBY,
	@NAME = isNull(NAME,'') FROM INSERTED
exec A_SP_OBJECT_ADD 'A_PRODUCTS_HISTORY',@ID,@NAME,@MODBY,null,null,null



GO



CREATE           TRIGGER A_PRODUCTS_HISTORY_UPDATE
ON dbo.A_PRODUCTS_HISTORY
AFTER UPDATE
AS

declare @OBJ_ID as nvarchar(50)
declare @ID as nvarchar(50)
declare @NAME as nvarchar(50)
declare @MODBY as nvarchar(50)
print 'In the Products History Update Trigger'
if update(NAME)
begin
SELECT
@ID = ID,
@OBJ_ID = OBJECT_ID,
@NAME = isNull(NAME,''),
@MODBY = MODBY 
FROM INSERTED

UPDATE A_OBJECTS SET
OBJ_DESC = @NAME,
MODBY = @MODBY,
DRCM = getDATE()
WHERE ID = @OBJ_ID
end



