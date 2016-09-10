CREATE PROCEDURE dbo.A_SP_COMPANY_GET_PURCHASING_TREE_BY_PERSON_ID
@strID varchar(50),
@strNTLogin varchar(50)
AS
print 'In A_SP_COMPANY_GET_PURCHASING_TREE_BY_PERSON_ID'
declare @rootCo varchar(50)
SELECT @rootCo = COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @strID
if @strNTLogin <> @strID
	begin
	print 'If the person doing this is not the one the order is for then we just return the orderers main dept'
	SELECT * FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @rootCo
	end
else
	begin
	print 'If the person doing this is not the one the order is for then we just return the orderers main dept'
	exec A_SP_COMPANIES_SHOW_TREE @rootCo,NULL,@rootCo,@strNTLogin
	end	

