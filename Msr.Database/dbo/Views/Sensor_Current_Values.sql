CREATE VIEW [dbo].[Sensor_Current_Values]
	AS SELECT        S.Site, S.SensorName, m.ItemCurrentValue
FROM            dbo.Sensor_Mapping AS S INNER JOIN
                         EquipmentMonitoring.dbo.Hillsboro_UpdateTable AS m ON m.ItemID = S.MonitoringID
