
CREATE VIEW [dbo].[Portal_EquipmentMaintenanceView]
AS
SELECT        
Id,
ObjectId,
ScanBarcode,
(SELECT        Name FROM            dbo.Portal_LocationsView WHERE        (ObjectId = dbo.Portal_EquipmentMaintenance.PrimaryLocationId)) AS PrimaryLocation,
(SELECT        Name FROM            dbo.Portal_LocationsView WHERE        (ObjectId = dbo.Portal_EquipmentMaintenance.SubLocationFirstId)) AS SubLocationFirst,
(SELECT        Name FROM            dbo.Portal_LocationsView WHERE        (ObjectId = dbo.Portal_EquipmentMaintenance.SubLocationSecondId)) AS SubLocationSecond, 
DateTime,
Technician, 
TroubleState,
MaintenanceTask,
Comments,
Status
FROM            dbo.Portal_EquipmentMaintenance


GO


