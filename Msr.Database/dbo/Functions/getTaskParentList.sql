




CREATE    FUNCTION [dbo].[getTaskParentList](@ID varchar(50))
RETURNS nvarchar(4000)
AS
BEGIN
declare @p as varchar(50),
@pd as nvarchar(50)
declare @so as varchar(8000)
SELECT @p = PARENT_ID FROM A_TASKS WHERE ID = @ID
SELECT @pd = DESCRIPTION FROM A_TASKS WHERE ID = @p
set @pd = dbo.xmlEncode(@pd)
declare @cnt int
set @cnt = 0
while @p is not null and @cnt < 15
	begin
	set @cnt = @cnt + 1
	set @so =  '<i><f i="id">' + isNull(@p,'') + '</f><n>' + isNull(@pd,'') + '</n></i>' + isNull(@so,'')
	SELECT @p = PARENT_ID FROM A_TASKS WHERE ID = @p
	SELECT @pd = DESCRIPTION FROM A_TASKS WHERE ID = @p
	end
set @so = @so
return(@so)
END







