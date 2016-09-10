CREATE TABLE [dbo].[A_MACHINE_PARTS] (
    [ID]                    VARCHAR (50)  NOT NULL,
    [PARENT_ID]             NVARCHAR (50) NULL,
    [MACHINE_PART_LOCATION] NVARCHAR (50) NOT NULL,
    [SN]                    NVARCHAR (50) NULL,
    [PART_ID]               VARCHAR (50)  NULL,
    [CURRENT_STATUS]        NVARCHAR (50) NOT NULL,
    [DRCM]                  DATETIME      NULL,
    [MODBY]                 NVARCHAR (50) NULL,
    [NICK_NAME]             NVARCHAR (50) NULL,
    [PURCHASER]             NVARCHAR (50) NULL,
    [PURCHASING_COMPANY]    VARCHAR (50)  NULL,
    [OBJECT_ID]             NVARCHAR (50) NULL,
    CONSTRAINT [PK_A_MACHINE_PARTS] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO

CREATE  TRIGGER A_MACHINE_PARTS_INSERT
ON dbo.A_MACHINE_PARTS
AFTER INSERT
AS
declare @ID as nvarchar(50)
declare @NAME as nvarchar(50)
SELECT  @ID = ID,@NAME = CO_PART_NAME + 
		isNull(' (' + CO_PART_NUM + ')','') + 
		isNull(' [' + SN + ']','')
from A_V_MACHINE_PARTS WHERE ID = (SELECT ID FROM INSERTED)
exec A_SP_OBJECT_ADD 'A_MACHINE_PARTS',@ID,@NAME,'SP',NULL,NULL,NULL

