

CREATE PROCEDURE dbo.DBA_GetDBChanges_sp

@s sysname,
@t sysname

AS

/* This script will provide DDL differences between two databases*/

DECLARE 
@sql nvarchar(4000)


Select '-------************* NEW TABLES **************----------'
SET @sql = 'Select Source_Table.Name As New_Table_Name,
Source_Columns.Name As
New_Column_Name,
Source_Type.Name As
New_Column_Type,
Source_Columns.Prec As
New_Column_Precision,
Source_Columns.Scale As
New_Column_Scale,
Source_Comments.Text As
Default_Value,
Case When Source_Columns.status & 8 = 8
Then ''Yes'' Else ''No'' END As AllowNulls
From 
' + @s + '.dbo.SysObjects
Source_Table Left Join 
' + @t +
'.dbo.sysobjects Target_Table ON Source_Table.Name = Target_Table.Name
INNER JOIN
' + @s + '.dbo.syscolumns
Source_Columns ON Source_Table.ID = Source_Columns.ID INNER JOIN
' + @t + '.dbo.systypes
Source_Type ON Source_Columns.xtype = Source_Type.xusertype LEFT OUTER
JOIN 
' + @s +
'.dbo.syscomments Source_Comments ON Source_Columns.CDefault =
Source_Comments.ID
Where 
Source_Table.Type = ''U'' AND 
Target_Table.ID IS NULL
Order by Source_Table.Name'
exec sp_executesql @sql
select '------************* NEW COLUMNS **************----------'
SET @sql = 'Select Source_Table.Name As Source_TableName,
Source_Column.Name As
Source_ColumnName,
SysTypes.Name As ColumnType,
Source_Column.Prec As
ColumnPrec,
Source_Column.Scale As
ColumnScale, 
SysComments.Text As
Default_Value,
Case When Source_Column.status &
8 = 8 Then ''Yes'' Else ''No'' END As AllowNulls
From 
' + @s + '.dbo.SysObjects
Source_Table INNER JOIN 
' + @s + '.dbo.SysColumns
Source_Column ON Source_Table.ID = Source_Column.ID LEFT OUTER JOIN
' + @s + '.dbo.SysComments
SysComments ON SysComments.ID = Source_Column.CDefault INNER JOIN
' + @t +
'.dbo.SysObjects Target_Table ON Target_Table.Name = Source_Table.Name
LEFT OUTER JOIN
' + @t + '.dbo.SysColumns
Target_Column ON Target_Column.ID = Target_Table.ID AND
Target_Column.Name = Source_Column.Name INNER JOIN
' + @s + '.dbo.Systypes SysTypes
ON Systypes.XUserType = Source_Column.XType
Where Source_Table.Type = ''U'' AND 
Target_Column.ID IS NULL AND 
Source_Table.Name not like
''SyMailMerge%''
Order by Source_Table.Name, Source_Column.Name'
exec sp_executesql @sql
select '-------************* NEW STORED PROCEDURES**************----------'
SET @sql = 'Select Source_Table.NAme as Procedure_Name ,
Target_table.Name as Still_here, *
From 
' + @s + '.dbo.sysobjects source_table 
Left Outer Join 
' + @t + '.dbo.sysobjects target_table 
ON Source_Table.Name = Target_table.Name
WHERE 
Source_Table.type = ''P'' 
AND 
Target_table.NAme is null'

exec SP_executesql @sql

select '-------************* New Indexes **************----------'
SET @sql = 'Select 
Source.Table_Name AS Table_Name, 
Source.Index_Name AS Index_Name, 
CASE (Source.Index_Status & 2) WHEN 2
THEN ''YES'' ELSE ''NO'' END AS UniqueInd,
CASE (Source.Index_Status & 2) WHEN 2
THEN CASE (Source.Index_Status & 1) WHEN 1 THEN ''YES'' ELSE ''NO'' END
ELSE ''N/A'' END AS IgnoreDupKey,
INDEX_COL(''' + @s + '.dbo.''' +
' + Source.Table_Name, Source.Index_ID, Keys.KeyNO) AS Index_Column,
CASE INDEXKEY_PROPERTY(Source.ID,
Source.Index_ID, Keys.KeyNO, ''IsDescending'') WHEN 1 THEN ''YES'' ELSE
''NO'' END AS DescendingSort
From 
(
SELECT so.ID, so.Name AS Table_Name,
si.Name AS Index_Name, 
si.IndID AS Index_ID,
si.Status AS Index_Status 
FROM ' + @s + '.dbo.SysObjects
so JOIN 
' + @s +
'.dbo.SysIndexes si ON so.ID=si.ID 
WHERE so.xtype=''U'' AND (si.Status &
64) = 0
) Source LEFT OUTER JOIN
(
SELECT so.ID, so.Name AS Table_Name,
si.Name AS Index_Name, 
si.IndID AS Index_ID,
si.Status AS Index_Status 
FROM ' + @t +
'.dbo.SysObjects so JOIN 
' + @t +
'.dbo.SysIndexes si ON so.ID=si.ID 
WHERE so.xtype=''U'' AND (si.Status &
64) = 0
) Target ON Source.ID = Target.ID
AND 
Source.Table_Name = Target.Table_Name AND 
Source.Index_Name = Target.Index_Name INNER JOIN 
' + @t + '.dbo.SysIndexKeys Keys
ON Source.ID = Keys.ID AND 
Source.Index_ID = Keys.IndID
WHERE Target.ID IS NULL
Order by Source.Table_Name, Source.Index_Name'

exec sp_executesql @sql
select '-------************* New Triggers **************----------'
SET @sql = 'Select Source_Parent.Name AS Table_Name, 
Source.Name AS Trigger_Name
From
' + @s + '.dbo.SysObjects
Source LEFT OUTER JOIN
' + @t +
'.dbo.SysObjects Target ON Source.Name = Target.Name INNER JOIN
' + @s + '.dbo.SysObjects
Source_Parent ON Source.Parent_Obj = Source_Parent.ID
WHERE Source.xtype=''TR'' AND Target.ID IS
NULL
ORDER BY Trigger_Name'
exec sp_executesql @sql
select '-------************* NEW VIEWS **************----------'

SET @sql = 'Select Source_Table.NAme as View_Name , *
From 
' + @s + '.dbo.sysobjects source_table 
Left Outer Join 
' + @t + '.dbo.sysobjects target_table 
ON Source_Table.Name = Target_table.Name
WHERE 
Source_Table.type = ''v'' 
AND 
Target_table.NAme is null'
exec SP_executesql @sql 
select '-------************* DELETED TABLES **************----------'

SET @sql = 'select Source_Table.NAme as Old_Table_Name , * From 
' + @t + '.dbo.sysobjects source_table 
Left Outer Join 
' + @s + '.dbo.sysobjects target_table 
ON Source_Table.Name = Target_table.NAme
WHERE 
Source_Table.type = ''U'' 
AND 
Target_table.NAme is null'

exec SP_executesql @sql
select '-------************* DELETED INDEXES **************----------'

SET @sql = 'SELECT Source.Table_Name AS Table_Name, 
Source.Index_Name AS Index_Name, 
CASE (Source.Index_Status & 2) 
WHEN 2 
THEN ''YES'' 
ELSE ''NO'' 
END AS UniqueInd,
CASE (Source.Index_Status & 2) 
WHEN 2 
THEN CASE (Source.Index_Status &
1) 
WHEN 1 
THEN ''YES'' 
ELSE ''NO'' 
END 
ELSE ''N/A'' 
END AS IgnoreDupKey,
INDEX_COL(''' + @s + '.dbo.''' + ' +
Source.Table_Name,Source.Index_ID, Keys.KeyNO) AS Index_Column,
CASE INDEXKEY_PROPERTY(Source.ID,
Source.Index_ID, Keys.KeyNO, ''IsDescending'') 
WHEN 1 
THEN ''YES''
ELSE ''NO'' 
END AS DescendingSort
From 
(
SELECT so.ID, so.Name AS Table_Name,
si.Name AS Index_Name, 
si.IndID AS Index_ID,
si.Status AS Index_Status 
FROM ' + @s +
'.dbo.SysObjects so JOIN 
' + @s +
'.dbo.SysIndexes si ON so.ID=si.ID 
WHERE so.xtype=''U'' AND (si.Status &
64) = 0
) 
Source LEFT OUTER JOIN
(
SELECT so.ID, so.Name AS Table_Name,
si.Name AS Index_Name, 
si.IndID AS Index_ID,
si.Status AS Index_Status 
FROM ' + @t + '.dbo.SysObjects
so JOIN 
' + @t +
'.dbo.SysIndexes si ON so.ID=si.ID 
WHERE so.xtype=''U'' AND (si.Status &
64) = 0) 
Target ON 
Source.Table_Name = Target.Table_Name AND 
Source.Index_Name = Target.Index_Name INNER JOIN 
' + @t + '.dbo.SysIndexKeys
Keys ON Source.ID = Keys.ID AND 
Source.Index_ID = Keys.IndID
WHERE Target.ID IS NULL
Order by Source.Table_Name, Source.Index_Name'

exec SP_executesql @sql
select '-------************* DELETED TRIGGERS **************----------'












