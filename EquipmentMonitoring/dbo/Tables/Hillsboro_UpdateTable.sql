CREATE TABLE [dbo].[Hillsboro_UpdateTable] (
    [ItemID]           VARCHAR (255)  NOT NULL,
    [ItemCurrentValue] VARCHAR (2000) NULL,
    [ItemTimeStamp]    DATETIME       NULL,
    [ItemQuality]      VARCHAR (255)  NULL,
    [ServerProgId]     VARCHAR (100)  NULL,
    [ServerAddress]    VARCHAR (100)  NULL,
    [GroupName]        VARCHAR (100)  NULL,
    [ReadMode]         VARCHAR (50)   NULL,
    [ItemDataType]     VARCHAR (100)  NULL,
    [ItemAccessRights] VARCHAR (50)   NULL,
    PRIMARY KEY CLUSTERED ([ItemID] ASC)
);


GO

CREATE TRIGGER dbo.AlarmStateChangeTrigger 
   ON  [dbo].[Hillsboro_UpdateTable]
AFTER INSERT,UPDATE
AS


DECLARE @ItemID NVARCHAR(255)
DECLARE @ItemCurrentValue NVARCHAR(2000)
DECLARE @ItemTimeStamp DATETIME
DECLARE @RequestorUserID VARCHAR(50)
-- Change this tp Prod Equipment Maintenance User ID when deploying to Prod
SET @RequestorUserID= '173117'

PRINT 'RUNNING Hillsboro_UpdateTable TRIGGER'

SELECT  @ItemID = ItemID
       ,@ItemCurrentValue = ItemCurrentValue
       ,@ItemTimeStamp = ItemTimeStamp
FROM INSERTED

PRINT 'ItemID UPDATED IS ' + @ItemID

-- CASE 1: If we have an alarm state change and the value is 1 (assumed), we will create a new EM Ticket
IF UPDATE(ItemCurrentValue) 
  AND @ItemID LIKE '%Data.AlarmState'
  -- Assuming the alarm value = 1 as we do not yet see alarms in the update table
  AND @ItemCurrentValue = '1'

PRINT 'AlarmState CHANGED TO' + @ItemCurrentValue

BEGIN
	SET NOCOUNT ON;
     -- GetLighthouse ID from Items ID and find the Location ID to create the EM Record in Locations Table


	 DECLARE @LighthouseLocationID NVARCHAR(255)
     DECLARE @LocationID VARCHAR(50)
	 DECLARE @MonitoringSystemID VARCHAR(255)

     SET @LighthouseLocationID = (SELECT SUBSTRING(@ItemID,0,PATINDEX('%.Data.AlarmState%',@ItemID))) 

     PRINT 'LightHouseLocationID TO MATCH IN Locations TABLE IS ' + @LighthouseLocationID

     SET @LocationID = (SELECT ID FROM Answer2_Test.dbo.A_LOCATIONS_HISTORY lh
     -- assuming we will have a comma delimited list of monitoring IDS for a location if it is monitored by multiple systems. This WHERE Clause could be wrong.
     WHERE @LighthouseLocationID IN (lh.MONITORING_SYSTEM_ID))

     INSERT INTO [Answer2_Test].[dbo].[Portal_EquipmentMaintenance] (
       [RoomEquipment]
      ,[DateTime]
      ,[RequestedById]
      ,[TroubleState]
      ,[MaintenanceTask]
      ,[Comments]
      ,[Status]
     )
     VALUES (
       @LocationID
      ,@ItemTimeStamp
      ,@RequestorUserID
      ,1
      ,'Repair'
      ,'New Alarm from Lighthouse Monitoring System.'
      ,'REQUESTED'
     )
END
 
-- CASE 2: If we have an alarm state change and the value is NOT 1 (assumed), we will close the corresponding EM Ticket if it's open
IF UPDATE(ItemCurrentValue) 
  AND @ItemID LIKE '%Data.AlarmState'
  -- Assuming the alarm value = 1 as we do not yet see alarms in the update table
  AND @ItemCurrentValue != '1'

PRINT 'AlarmState CHANGED TO' + @ItemCurrentValue

BEGIN
     SET NOCOUNT ON;
	 -- GetLighthouse ID from Items ID and find the Location ID to create the EM Record in Locations Table

     SET @LighthouseLocationID = (SELECT SUBSTRING(@ItemID,0,PATINDEX('%.Data.AlarmState%',@ItemID))) 

     PRINT 'LightHouseLocationID TO MATCH IN Locations TABLE IS ' + @LighthouseLocationID

     SET @LocationID = (SELECT ID FROM Answer2_Test.dbo.A_LOCATIONS_HISTORY lh
     -- assuming we will have a comma delimited list of monitoring IDS for a location if it is monitored by multiple systems. This WHERE Clause could be wrong.
     WHERE @LighthouseLocationID IN (lh.MONITORING_SYSTEM_ID))

     -- CLose the Ticket if it's still OPEN
     UPDATE [Answer2_Test].[dbo].[Portal_EquipmentMaintenance]
     SET DateTime = @ItemTimeStamp
        ,Status = 'COMPLETED'
        ,Comments = 'Closed by Monitoring System'
     WHERE RoomEquipment = @LocationID AND Status != 'COMPLETED'

END
GO
DISABLE TRIGGER [dbo].[AlarmStateChangeTrigger]
    ON [dbo].[Hillsboro_UpdateTable];

