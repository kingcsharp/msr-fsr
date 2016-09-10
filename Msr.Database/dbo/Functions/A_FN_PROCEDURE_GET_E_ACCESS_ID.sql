

CREATE FUNCTION dbo.A_FN_PROCEDURE_GET_E_ACCESS_ID (@strNTLogin as varchar(50))
RETURNS varchar(50)
as
BEGIN
	declare @so as  varchar(50)
	SELECT @so = ID FROM A_V_PROCEDURES_APPROVED_DATA WHERE SYSTEM_ID = 'SYS_E_ACCESS' AND CREATING_CO = dbo.getCompany(@strNTLogin)
	return(@so)
END



