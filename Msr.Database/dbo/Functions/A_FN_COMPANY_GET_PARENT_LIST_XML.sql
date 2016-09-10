




CREATE    FUNCTION dbo.A_FN_COMPANY_GET_PARENT_LIST_XML(@ID varchar(50))
RETURNS nvarchar(4000)
AS
BEGIN
declare @p as varchar(50),
@pd as nvarchar(200)
declare @so as nvarchar(4000)
SELECT @p = PARENT FROM A_COMPANIES_HISTORY WHERE ID = @ID
SELECT @pd = NAME FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @p
while @p is not null
	begin
	set @so =  '<i><f i="id">' + isNull(@p,'') + '</f><n>' + isNull(@pd,'') + '</n></i>' + isNull(@so,'')
	set @p = NULL
	SELECT @p = PARENT FROM A_COMPANIES_HISTORY WHERE ID = @p
	SELECT @pd = NAME FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @p
	end
return(@so)
END







