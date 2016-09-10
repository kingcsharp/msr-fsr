



CREATE     PROCEDURE dbo.A_SP_PROD_REQ_FORM_ACCEPT
@newID nvarchar(50) OUTPUT,
@messages nvarchar(2000) OUTPUT,
@id varchar(50),
@strNTLogin varchar(50)
AS
declare @mgrGroup as varchar(50)
SELECT @mgrGroup = CUST_MGR_ROLE FROM A_V_PROD_REQUEST_FORMS_WITH_ALL_DATA WHERE ID = @id
print 'The group is ' + @mgrGroup

print 'SELECT @tester = ID FROM A_ROLE_ASSIGNEE WHERE PERSON = ''' + isNull(@strNTLogin,'NULL') + ''' AND ROLE = ''' + isNull(@mgrGroup,'NULL') + ''''


declare @t as smallInt
set @t = dbo.A_FN_ROLES_CHECK_IF_MEMBER(@mgrGroup,@strNTLogin)
if @t = 0
	begin
	set @newID = 'You are not in the cust mgr role so you can not accept'
	goto fin
	end

UPDATE A_PROD_REQ_FORMS SET PROGRESS = 'PRQ_ACCEPTED' WHERE ID = @id
exec A_SP_PROD_REQ_FORM_UPDATE_DATE @ID,@strNTLogin
fin:




