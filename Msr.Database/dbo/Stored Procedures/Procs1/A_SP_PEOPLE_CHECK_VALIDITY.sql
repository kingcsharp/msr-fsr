



CREATE    procedure A_SP_PEOPLE_CHECK_VALIDITY
	@returnVal nvarchar(50) OUTPUT,
	@ID nvarchar(50),
	@strNTLogin nvarchar(50)
as
print 'Entering A_SP_PEOPLE_CHECK_VALIDITY'
declare @login as nvarchar(50)
declare @retVal as nvarchar(2000)
declare @password as nvarchar(50)
declare @company as nvarchar(50)
declare @objID as nvarchar(50)
select @objID = OBJECT_ID,@login = LOGIN, @password = PASSWORD,@company = COMPANY FROM
A_PEOPLE_HISTORY WHERE ID = @ID
set @retVal = ''

--first check to make sure they have a login
if (@login = '' or @login is null)
	begin
	print 'This is just an ANSWER Contact so make sure the password is null'
	goto answerContact
	end
--now check to make sure that the login is not in use by someone else
--first get the root
declare @root as nvarchar(50)
SELECT @root = ROOT FROM A_OBJECTS WHERE ID = @objID
print 'root = ' + isNUll(@root,'NULL')
--Now check the approved people table for a login like this one
--and that does not have the same ID as me
declare @tester as nvarchar(50)
SELECT @tester = OBJECT_ID FROM A_APPROVED_PEOPLE WHERE LOGIN = @login and ID <> @root
if not(@tester is NULL)
	set @retVal = @retVal + 'Login in use by:[' + @tester + '],'
--Now make sure the password is good
if (@password = '' or @password is null)
	begin
		print 'Password is not Valid'
		set @retVal = @retVal + 'Password,'
	end



answerContact:
--Now check to make sure the company is not empty
if (@company = '' or @company is null)
	set @retVal = @retVal + 'COMPANY is Not Valid,'
print 'returnVal = ' + isNull(@retVal,'NULL')
set @returnVal = @retVal
--Finished





