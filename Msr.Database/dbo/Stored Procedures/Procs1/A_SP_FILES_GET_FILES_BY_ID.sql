











CREATE          PROCEDURE A_SP_FILES_GET_FILES_BY_ID
@docID varchar(1000),
@strNTLogin varchar(50)
as
declare @sql as nvarchar(4000)

set @sql='SELECT * FROM A_V_FILES_WITH_SOURCE WHERE ID='+ @docID   
print @sql

EXEC(@SQL)









