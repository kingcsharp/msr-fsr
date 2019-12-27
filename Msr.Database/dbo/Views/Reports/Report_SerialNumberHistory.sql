
CREATE VIEW [dbo].[Report_SerialNumberHistory]
AS

SELECT DISTINCT
ROW_NUMBER() OVER(ORDER BY Serial ASC) AS Id,
pwo.Serial AS SerialNumber,
pwo.CustPurchNum AS WONumber,
pwo.StartDate AS WOCreationDate,
pwo.DueDate AS DueDate,
pwo.OrigDueDate AS ShipDate, 
pwo.LocationName AS MSRFSRFacility,
pwo.CustomerName,
pwo.ProcName AS specno,
pwo.WoItem AS KitName,
'Not sure where to get' AS NCRNumber,
pwo.ReferencePo AS pono,
pwo.CustMttn AS mttn,
PTLCount.CycleCount
FROM dbo.Portal_WorkOrders pwo 
INNER JOIN PartsTransactionLog AS pt ON pt.PartId = pwo.PartId AND pt.SerialNumber=pwo.Serial
OUTER APPLY(
			SELECT count(SerialNumber) AS CycleCount 
			FROM PartsTransactionLog AS pt
			WHERE pt.PartId = pwo.PartId AND pt.SerialNumber=pwo.Serial
			) PTLCount
WHERE pwo.CustPurchNum IS NOT NULL AND pwo.Serial IS NOT NULL
GO