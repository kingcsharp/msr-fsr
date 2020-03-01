CREATE VIEW [dbo].[Portal_LatestApprovedFilesView]
AS 
WITH AllFiles AS (
SELECT 	
	CAST(row_number() over( order by d.[NAME] ASC) AS INT) AS ALLID,
	l.LINKED_DOC_ID, 
	d.[NAME] AS 'NAME2',
	d.CONTENTTYPE,
	d.SERVER_PATH  
	FROM dbo.A_DOCUMENT_LINK AS l INNER JOIN
          dbo.A_DOCUMENTS AS d ON l.LINKED_DOC_ID = d.ID
), LastFiles AS (
SELECT
	CAST(row_number() over( order by d2.[NAME] ASC) AS INT) AS ID,
	MAX(l2.LINKED_DOC_ID) AS MAX_ID, d2.[NAME]
	FROM A_DOCUMENT_LINK l2 INNER JOIN
          A_DOCUMENTS d2 ON l2.LINKED_DOC_ID = d2.ID
	GROUP BY d2.[NAME]
)
SELECT 
LastFiles.ID,
LastFiles.MAX_ID AS LINKED_DOC_ID,
LastFiles.[NAME],
AllFiles.CONTENTTYPE,
AllFiles.SERVER_PATH
FROM LastFiles INNER JOIN
AllFiles ON AllFiles.ALLID = LastFiles.ID

