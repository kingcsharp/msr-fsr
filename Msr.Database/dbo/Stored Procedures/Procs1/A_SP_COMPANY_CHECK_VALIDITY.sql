


CREATE   procedure dbo.A_SP_COMPANY_CHECK_VALIDITY
	@returnVal nvarchar(2000) OUTPUT,
	@ID nvarchar(50),
	@strNTLogin nvarchar(50)
as
print 'Entering A_SP_COMPANY_CHECK_VALIDITY'
declare @name as nvarchar(1000), @retVal as nvarchar(2000), @objID as nvarchar(50),
	@parentID as varchar(50)
select @objID = OBJECT_ID,@name = name, @parentID = PARENT FROM A_COMPANIES_HISTORY WHERE ID = @ID
set @retVal = ''
--first check to make sure the company has a name
if (@name = '' or @name is null)
	begin 
	print 'Company has no name'
	set @retVal = @retVal + 'NO_NAME_FOR_COMPANY,'
	end
--now check to make sure that if they have 
--no parent ID and they are rev 1 
--then they need to have a person in the new person table
declare @login as nvarchar(200),@password as nvarchar(100),@rev as smallint
SELECT @rev = REV  FROM A_OBJECTS WHERE ID = @objID
if @rev = 1 and @parentID is null
	begin
	select @login = LOGIN_NAME, @password = PASSWORD FROM A_COMPANIES_NEW_PERSON_DATA WHERE CO_ID = @ID
	if @login is null
		set @retVal = @retVal + 'NO_LOGIN_FOR_ADMIN_PERSON,'
	if @password is null
		set @retVal = @retVal + 'NO_PASSWORD_FOR_ADMIN_PERSON,'
	end
		

print 'returnVal = ' + isNull(@retVal,'NULL')
set @returnVal = @retVal
--Finished


fin:

