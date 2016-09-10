







CREATE    FUNCTION dbo.xmlEncode(@str nvarchar(4000))
RETURNS nvarchar(4000)
AS
BEGIN
declare @so nvarchar(4000)
set @so = @str
set @so = replace(@so,'&','&amp;')
set @so = replace(@so,'<','&lt;')
set @so = replace(@so,'>','&gt;')
set @so = replace(@so,'"','&quot;')

return(@so)
END










