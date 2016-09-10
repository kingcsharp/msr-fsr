






CREATE  FUNCTION dbo.A_FN_QUOTE_ITEM_GET_PARENT_LIST(@ID varchar(50))
RETURNS nvarchar(4000)
AS
BEGIN
declare @p as varchar(50),
@pd as nvarchar(200)
declare @so as nvarchar(4000)
SELECT @p = PARENT FROM A_QUOTE_ITEMS WHERE ID = @ID
while @p is not null
	begin
	set @so =  dbo.leadingSpaces(@p,12) + isNull('-->' + @so,'')
	set @p = NULL
	SELECT @p = PARENT FROM A_QUOTE_ITEMS WHERE ID = @p
	end
return(@so)
END









