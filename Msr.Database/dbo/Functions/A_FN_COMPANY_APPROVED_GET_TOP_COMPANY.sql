




CREATE FUNCTION dbo.A_FN_COMPANY_APPROVED_GET_TOP_COMPANY(@ID varchar(50))
RETURNS varchar(50)
AS
BEGIN
declare @supRootCo varchar(50)
declare @tRoot as varchar(50)
set @tRoot = @ID

	begin
	set @supRootCo = @tRoot
	SELECT @tRoot = PARENT FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @supRootCo
	end
return(@supRootCo)
end






