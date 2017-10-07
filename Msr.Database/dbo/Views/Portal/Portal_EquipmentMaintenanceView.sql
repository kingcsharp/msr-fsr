
CREATE VIEW [dbo].[Portal_EquipmentMaintenanceView]
AS
SELECT        
em.Id,
em.ObjectId,
em.ScanBarcode,
(SELECT        Name FROM            dbo.Portal_LocationsView WHERE        (ObjectId = em.ParentLocation)) AS ParentLocation,
(SELECT        Name FROM            dbo.Portal_LocationsView WHERE        (ObjectId = em.SubLocationFirst)) AS SubLocationFirst,
(SELECT        Name FROM            dbo.Portal_LocationsView WHERE        (ObjectId = em.SubLocationSecond)) AS SubLocationSecond, 
em.DateTime,
ap.FULL_NAME AS RequestedBy, 
em.RequestedById, 
em.TroubleState,
em.MaintenanceTask,
em.Comments,
em.Status,
abyp.FULL_NAME ApprovedBy,
em.ApprovedById
FROM            dbo.Portal_EquipmentMaintenance em
INNER JOIN A_APPROVED_PEOPLE ap ON ap.Id = em.RequestedById
LEFT JOIN A_APPROVED_PEOPLE abyp ON abyp.ID = em.ApprovedById

GO


