












CREATE               PROCEDURE dbo.A_SP_COMPANIES_UPDATE_ONE_COMPANY
@newID nvarchar(50) OUTPUT,
@messages nvarchar(500) OUTPUT,
@objID nvarchar(50),
@NAME nvarchar(100),
@TYPE nvarchar(50),
@PARENT_COMPANY nvarchar(50),
@PHONE nvarchar(50),
@HEAD_PEOPLE varchar(8000),
@LOCATION_ID varchar(50),
@CO_SUP_PRODS varchar(8000),
@PIC_FILES varchar(8000),
@LOGO_FILES varchar(8000),
@REFERENCE_FILES varchar(8000),
@NEW_PERSON_LOGIN nvarchar(200),
@NEW_PERSON_PASSWORD nvarchar(50),
@NEW_PERSON_EMAIL nvarchar(200),
@NEW_PERSON_FIRST_NAME nvarchar(200),
@NEW_PERSON_LAST_NAME nvarchar(200),
@strNTLogin nvarchar(50)
AS
declare @ID as varchar(50)
declare @ptest as varchar(50)
print 'Updating a Company'
if @objID is null
	begin
		print 'ID is Null we need to create this Company'
		exec sp_GetUniqueID3 @newID OUTPUT
		set @ID = @newID
		INSERT INTO A_COMPANIES_HISTORY(ID,NAME,MODBY,DRCM) VALUES(@newID,@NAME,@strNTLogin,getDATE())
		SELECT @newID = OBJECT_ID FROM A_COMPANIES_HISTORY WHERE ID = @newID
		print '^^^^^^^^^^^^^^NEW OBJECT ID = ' + @newID
	end
else
	begin
		print 'The ID is not null so we are just updating Company objectID='+@objID
		set @newID = @objID
	end


UPDATE A_COMPANIES_HISTORY SET
NAME = @NAME, 
CO_TYPE = @TYPE, 
PARENT = @PARENT_COMPANY,
MODBY = @strNTLogin,
PHONE = @PHONE,
LOCATION = @LOCATION_ID,
DRCM = getDate()
WHERE OBJECT_ID = @newID

print 'Deleting all of this companies File Links'
DELETE FROM A_DOCUMENT_LINK WHERE OBJECT_ID = @newID 

print 'Updating reference File List for the Company'
print 'making a cursor to go through the ref files string'
CREATE TABLE #TempItems	(IT varchar(50))
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @REFERENCE_FILES,', '
Declare @it nvarchar(50)
Declare @curs Cursor
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	set @it = ltrim(@it)
	print 'Adding Reference File = ' + @it
	exec A_SP_FILES_CREATE_LINK @newID,@it,null,@strNTLogin
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs

print 'Updating logo File List for the Company'
print 'making a cursor to go through the ref files string'
DELETE FROM #TempItems
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @LOGO_FILES,', '
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	set @it = ltrim(@it)
	print 'Adding Logo File = ' + @it
	exec A_SP_FILES_CREATE_LINK @newID,@it,'LOGO',@strNTLogin
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs

print 'Updating picture File List for the Company'
print 'making a cursor to go through the ref files string'
DELETE FROM #TempItems
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @PIC_FILES,', '
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	set @it = ltrim(@it)
	print 'Adding Logo File = ' + @it
	exec A_SP_FILES_CREATE_LINK @newID,@it,'PICTURE',@strNTLogin
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs

print 'Now it is time to add the department heads'
declare @CO_ID as varchar(50)
SELECT @CO_ID = ID FROM A_COMPANIES_HISTORY WHERE OBJECT_ID = @newID
print 'First delete all the previous heads of ' + isNull(@CO_ID,'NULL')
DELETE FROM A_COMPANY_HEAD_PEOPLE WHERE CO_ID = @CO_ID
DELETE FROM #TempItems
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @HEAD_PEOPLE,', '
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	set @it = ltrim(@it)
	print 'Adding Head Person = ' + @it
	INSERT INTO A_COMPANY_HEAD_PEOPLE (ID,CO_ID,PERSON_ID,DRCM,MODBY)
	VALUES (newID(),@CO_ID,@it,getDate(),@strNTLogin)
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs


print 'Now it is time to add the products for the people'
print 'First delete all the previous products of ' + isNull(@CO_ID,'NULL')
DELETE FROM A_COMPANY_EMPLOYEE_PRODUCT_LIST WHERE CO_ID = @CO_ID
DELETE FROM #TempItems
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @CO_SUP_PRODS,', '
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	set @it = ltrim(@it)
	print 'Adding Company Emp Product = ' + @it
	INSERT INTO A_COMPANY_EMPLOYEE_PRODUCT_LIST (ID,CO_ID,PRODUCT_ID,DRCM,MODBY)
	VALUES (newID(),@CO_ID,@it,getDate(),@strNTLogin)
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs



SELECT @ptest = ID FROM A_COMPANIES_NEW_PERSON_DATA WHERE CO_ID = @CO_ID

if @ptest is null
	begin
	INSERT INTO  A_COMPANIES_NEW_PERSON_DATA (ID,LOGIN_NAME,EMAIL_ADDRESS,
		CO_ID,FIRST_NAME,LAST_NAME,DRCM,MODBY,PASSWORD)
	VALUES (newID(),@NEW_PERSON_LOGIN,@NEW_PERSON_EMAIL,@CO_ID,
	@NEW_PERSON_FIRST_NAME,@NEW_PERSON_LAST_NAME,
	getDate(),@strNTLogin,@NEW_PERSON_PASSWORD)
	end
else
	begin
	UPDATE A_COMPANIES_NEW_PERSON_DATA SET LOGIN_NAME = @NEW_PERSON_LOGIN, EMAIL_ADDRESS = @NEW_PERSON_EMAIL,
		FIRST_NAME = @NEW_PERSON_FIRST_NAME, LAST_NAME = @NEW_PERSON_LAST_NAME, DRCM = getDate(), MODBY = @strNTLogin
	WHERE CO_ID = @CO_ID
	IF @NEW_PERSON_PASSWORD is not null
		begin
		UPDATE A_COMPANIES_NEW_PERSON_DATA SET PASSWORD= @NEW_PERSON_PASSWORD WHERE CO_ID = @CO_ID
		end

	end



