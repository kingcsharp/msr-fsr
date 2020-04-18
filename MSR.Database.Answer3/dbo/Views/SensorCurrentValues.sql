
CREATE VIEW [dbo].[SensorCurrentValues]
AS

SELECT DISTINCT valuemap.SensorMappingId
	, valuemap.[Site] AS 'Site'
	, slocation.InternalAddress
	, valuemap.SensorName
	, svalue.ItemCurrentValue AS SensorCurrentValue
FROM SensorValueMap AS valuemap
INNER JOIN SensorsValue AS svalue ON svalue.ItemId = valuemap.MonitoringId
LEFT OUTER JOIN SensorLocationMap AS slocation ON slocation.SensorMappingId = valuemap.SensorMappingId
