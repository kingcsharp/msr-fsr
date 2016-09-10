

CREATE   PROCEDURE A_SP_COMPANIES_UPDATE_ONE_COMPANY_CHILD_TABLE
@ID varchar(50)
AS
print 'First Delete wherever the id is a child  ' + @ID
DELETE FROM A_COMPANIES_CHILD_LOOKUP_TABLE WHERE CHILD_COMPANY = @ID
print 'fixing to run through the list and add all the parent to the top for id = ' + @ID
declare @curCompany as varchar(50)
select @curCompany = PARENT FROM A_APPROVED_COMPANIES WHERE ID = @ID
while @curCompany is not null
	begin
	INSERT INTO A_COMPANIES_CHILD_LOOKUP_TABLE (ID,COMPANY,CHILD_COMPANY)
	VALUES (newID(),@curCompany,@ID)
	exec A_SP_COMPANIES_UPDATE_ONE_COMPANY_CHILD_TABLE @curCompany
	SELECT @curCompany = PARENT FROM A_APPROVED_COMPANIES WHERE ID = @curCompany
	end


