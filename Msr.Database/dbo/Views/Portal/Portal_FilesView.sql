CREATE VIEW [dbo].[Portal_FilesView]
	AS 
	SELECT DISTINCT 
	ID AS Id, 
	SORT_ID AS SortId, 
	NAME AS Name, 
	DESCRIPTION AS Description, 
	SRC_NAME AS SrcName, 
	SRC_ID AS SrcId, 
	CREATOR_ID AS CreatorId, 
	BOSS AS Boss
FROM            dbo.A_V_FILES_SEARCH_BY_SUBORDINATE
