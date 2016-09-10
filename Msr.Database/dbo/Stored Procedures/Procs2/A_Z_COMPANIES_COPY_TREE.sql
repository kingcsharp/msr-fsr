CREATE PROCEDURE dbo.A_Z_COMPANIES_COPY_TREE
@from varchar(50),
@to varchar(50)
AS
print 'Copying the company tree ' + @from
print 'as a child to ' + @to
declare @toName varchar(50), @fromName varchar(50),@fromPhone varchar(50),@fromLoc varchar(50),@fromType varchar(50)



SELECT @toName = NAME FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @to
SELECT @fromPhone = PHONE,@fromLoc = LOCATION,@fromType = CO_TYPE,@fromName = NAME FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @from
print 'Copying the company tree ' + @fromName
print 'as a child to ' + @toName
declare @adminID varchar(50)
exec A_SP_COMPANY_GET_AN_ADMIN_ID_FROM_THIS_COMPANY
	@adminID OUTPUT,@to
print 'The admin we are using is ' + @adminID
declare @adminName varchar(100)
SELECT @adminName = FULL_NAME FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @adminID
print 'His Name is ' + @adminName
declare @newLocID varchar(50)
if @fromLoc IS NOT NULL
	begin
	print 'We need to make a location to match this ones location ' + @fromLoc
	exec A_Z_LOCATION_COPY_ACROSS_SILOS @newLocID OUTPUT,@fromLoc,@to

	end

declare @childID varchar(50), @msgs nvarchar(600)
exec dbo.A_SP_COMPANIES_UPDATE_ONE_COMPANY
@childID OUTPUT,@msgs OUTPUT,
null, --@objID
@fromName, --NAME
@fromType, --TYPE
@to, --PARENT
@fromPhone, --
null,
@newLocID, -- varchar(50),
null,
null,
null,
null,
null,
null,
null,
null,
null,
@adminID

print 'Created a location and the new object ID is ' + @childID
UPDATE A_OBJECTS SET STATUS = 'APPROVED',LOCKED_BY = NULL, 
	LOCKED_BY_NAME = NULL 
	WHERE ID = @childID
declare @newCoID varchar(50)
SELECT @newCoID = OBJ_ID FROM A_OBJECTS WHERE ID = @childID
declare @myRoot as nvarchar(50)
SELECT @myRoot = ROOT FROM A_OBJECTS WHERE ID = @childID
print 'The Root is ' + @myRoot
INSERT INTO A_COMPANIES(ID,HISTORY_REF_ID,DRCM,MODBY,STATUS) VALUES 
		(@myRoot,@newCoID,getDate(),@adminID,'APPROVED')

declare @curs as CURSOR, @cID varchar(50)
set @curs = CURSOR FOR SELECT ID FROM A_V_COMPANIES_APPROVED_DATA WHERE PARENT = @from
open @curs
fetch next from @curs into @cID
while @@fetch_status = 0
	begin
	exec A_Z_COMPANIES_COPY_TREE @cID,@childID
	fetch next from @curs into @cID
	end

close @curs
deallocate @curs
