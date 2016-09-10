CREATE TABLE [dbo].[A_LOCATIONS] (
    [ID]             VARCHAR (50) NOT NULL,
    [DRCM]           DATETIME     NULL,
    [MODBY]          VARCHAR (50) NULL,
    [HISTORY_REF_ID] VARCHAR (50) NULL,
    [STATUS]         VARCHAR (50) NULL,
    CONSTRAINT [PK_A_LOCATIONS] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO

CREATE    TRIGGER A_LOCATIONS_UPDATE
ON dbo.A_LOCATIONS
AFTER UPDATE
AS
print 'INSIDE A_LOCATIONS_UPDATE TRIGGER'
declare @ID as nvarchar(50)
SELECT @ID = ID
from INSERTED
--Update the name in the object
print 'We are going to update where the name of this location is'
exec A_SP_LOCATIONS_UPDATE_WHERE_USED_NAME @ID

