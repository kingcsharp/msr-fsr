CREATE VIEW [dbo].[Portal_LatestApprovedFilesView]
AS 
SELECT * FROM (
    SELECT
		l.LINKED_DOC_ID, 
		d.[NAME],
		d.CONTENTTYPE,
		d.SERVER_PATH,
		row_number() over(partition by d.[NAME] order by d.[NAME] ASC) as rn

	FROM dbo.A_DOCUMENT_LINK AS l INNER JOIN
          dbo.A_DOCUMENTS AS d ON l.LINKED_DOC_ID = d.ID 
) t
where t.rn = 1
