CREATE     FUNCTION dbo.A_FN_PRODUCT_WHERE_USED_CHECK (@ID varchar(50))
RETURNS smallInt
AS
BEGIN
declare @t as varchar(50)
SELECT Top 1 @t = ID FROM A_PRODUCT_OBJ_USED_ON_LINK WHERE PRODUCT_ID = @ID
if @t is null return 0
return 1
END








