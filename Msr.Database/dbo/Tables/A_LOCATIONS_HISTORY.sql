CREATE TABLE [dbo].[A_LOCATIONS_HISTORY] (
    [ID]                   VARCHAR (50)    NOT NULL,
    [NAME]                 NVARCHAR (100)  NULL,
    [PARENT_LOCATION]      NVARCHAR (50)   NULL,
    [PARENT_LOCATION_NAME] NVARCHAR (200)  NULL,
    [ADDRESS_1]            NVARCHAR (100)  NULL,
    [ADDRESS_2]            NVARCHAR (100)  NULL,
    [FULL_ADDRESS]         NVARCHAR (200)  NULL,
    [CITY]                 NVARCHAR (50)   NULL,
    [STATE]                NVARCHAR (50)   NULL,
    [COUNTRY]              NVARCHAR (50)   NULL,
    [POSTAL_CODE]          NVARCHAR (15)   NULL,
    [REGION]               NVARCHAR (50)   NULL,
    [REGION_NAME]          NVARCHAR (50)   NULL,
    [INTERNAL_ADDRESS]     NVARCHAR (200)  NULL,
    [OBJECT_ID]            VARCHAR (50)    NULL,
    [DRCM]                 DATETIME        NULL,
    [MODBY]                NVARCHAR (50)   NULL,
    [PARENT_PATH]          NVARCHAR (500)  NULL,
    [COMPLETE_NAME]        NVARCHAR (2000) NULL,
    CONSTRAINT [PK_A_LOCATIONS_HISTORY] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO

CREATE TRIGGER A_LOCATIONS_HISTORY_INSERT
ON dbo.A_LOCATIONS_HISTORY
AFTER INSERT
AS
print ' IN A_LOCATIONS_HISTORY_INSERT TRIGGER'
declare @ID as nvarchar(50)
declare @NAME as nvarchar(50)
declare @MODBY as nvarchar(50)
SELECT @ID = ID,
	@MODBY = MODBY,
	@NAME = NAME
from INSERTED
print 'Calling the ADD Object Procedure'
exec A_SP_OBJECT_ADD 'A_LOCATIONS_HISTORY',@ID,@NAME,@MODBY,null,null,null


GO
CREATE      TRIGGER A_LOCATIONS_HISTORY_UPDATE
ON dbo.A_LOCATIONS_HISTORY
AFTER UPDATE
AS
print 'INSIDE A_LOCATIONS_HISTORY_UPDATE TRIGGER'
declare @ID as nvarchar(50)
declare @NAME as nvarchar(50)
declare @MODBY as nvarchar(50)
declare @objID as nvarchar(50)
SELECT @ID = ID,
	@MODBY = MODBY,
	@NAME = NAME,
	@objID = OBJECT_ID
from INSERTED
--Update the name in the object
if update(NAME)
	begin
	print 'The location Name was updated so we need to update it in the object'
	UPDATE A_OBJECTS SET OBJ_DESC = @NAME WHERE ID = @objID
	end

--Do the general Locations update for this record
print 'Calling procedure A_SP_LOCATIONS_UPDATE_ALL_DATA_FOR_ONE'
exec A_SP_LOCATIONS_UPDATE_ALL_DATA_FOR_ONE @ID
exec A_LOCATION_UPDATE_PARENT_PATH @ID
print 'Finished with A_LOCATIONS_UPDATE'
