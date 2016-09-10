CREATE        PROCEDURE dbo.A_SP_COMPANIES_SHOW_HIERARCHY
@strRootCo varchar(50),
@strListToexpand varchar(8000),
@strExpandAllList varchar(8000),
@strNTLogin nvarchar(50)
AS

exec A_SP_COMPANIES_SHOW_TREE @strRootCo,
@strListToexpand,@strExpandAllList,@strNTLogin

--SELECT * FROM #tempCoTree







