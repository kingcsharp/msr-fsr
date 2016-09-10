

/*
STORED PROCEDURE CALLED IN companies/editHolidays.asp
*/

CREATE      PROCEDURE dbo.A_SP_COMPANIES_GET_HOLIDAY_DATA_FOR_COMPANY
	@objID nvarchar(50),
	@strNTLogin nvarchar(50)
AS
declare @CO_ID as varchar(50)
SELECT @CO_ID = ID FROM A_COMPANIES_HISTORY WHERE OBJECT_ID = @objID
SELECT *,convert(nvarchar(4),YR) + '-' + convert(nvarchar(2),MO) + '-' + convert(nvarchar(2),DA) AS HOLIDAY_DATE FROM A_COMPANY_HOLIDAYS WHERE CO_ID = @CO_ID ORDER BY YR,MO,DA


