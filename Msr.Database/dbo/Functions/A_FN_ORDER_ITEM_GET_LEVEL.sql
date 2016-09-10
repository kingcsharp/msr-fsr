





CREATE FUNCTION dbo.A_FN_ORDER_ITEM_GET_LEVEL(@ID varchar(50))
RETURNS int
AS
BEGIN
declare @p as varchar(50),
@pd as nvarchar(200)
SELECT @p = PARENT FROM A_ORDER_ITEMS WHERE ID = @ID
declare @c as int
set @c = 0
while @p is not null
	begin
	set @c = @c + 1
	set @p = NULL
	SELECT @p = PARENT FROM A_ORDER_ITEMS WHERE ID = @p
	end
return(@c)
END








