

CREATE VIEW [dbo].[Portal_EquipmentMaintenanceView]
AS
SELECT        
em.Id,
em.ObjectId,
em.ScanBarcode,
(SELECT        Name FROM            dbo.Portal_LocationsView WHERE        (ObjectId = em.RoomEquipment)) AS RoomEquipment, 
em.RoomEquipment AS RoomEquipmentId,
em.DateTime,
ap.FULL_NAME AS RequestedBy, 
em.RequestedById, 
em.TroubleState,
em.MaintenanceTask,
em.Comments,
em.Status,
abyp.FULL_NAME AssignedTo,
em.PemLastCompletedDate,
em.FrequencyField,
em.CreatedDate,
em.UpdatedDate
FROM            dbo.Portal_EquipmentMaintenance em
LEFT JOIN A_APPROVED_PEOPLE ap ON ap.Id = em.RequestedById
LEFT JOIN A_APPROVED_PEOPLE abyp ON abyp.ID = em.AssignedToId

GO


