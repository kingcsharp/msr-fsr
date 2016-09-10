

















CREATE     PROCEDURE dbo.A_SP_PROPOSALS_UPDATE_ONE_PROPOSAL
@newID varchar(50) OUTPUT,
@messages varchar(500) OUTPUT,
@objID varchar(50),
@NAME varchar(100),
@CUSTOMER_CO varchar(50),
@SUPPLIER_CO varchar(50),
@PROP_DATE datetime,
@PROP_VALID_DATE datetime,
@OPENING varchar(4000),
@ADD_NOTES varchar(4000),
@CLOSING varchar(4000),
@QUOTE_LIST varchar(4000),
@CUSTOMER_CONTACTS varchar(4000),
@strNTLogin varchar(50)
AS
set @PROP_DATE = dbo.timeToGrenich(@PROP_DATE,@strNTLogin)
set @PROP_VALID_DATE = dbo.timeToGrenich(@PROP_VALID_DATE,@strNTLogin)

declare @ID as varchar(50)
declare @ptest as varchar(50)
print 'Updating a Proposal'
if @objID is null
	begin
		print 'ID is Null we need to create this Proposal'
		exec sp_GetUniqueID3 @newID OUTPUT
		set @ID = @newID
		INSERT INTO A_PROPOSALS_HISTORY(CREATOR_ID,ID,NAME,MODBY,DRCM,CUSTOMER_CO,SUPPLIER_CO) VALUES(@strNTLogin,@newID,@NAME,@strNTLogin,getDATE(),@CUSTOMER_CO,@SUPPLIER_CO)
		SELECT @newID = OBJECT_ID FROM A_PROPOSALS_HISTORY WHERE ID = @newID
	end
else
	begin
		print 'The ID is not null so we are just updating Company objectID='+@objID
		set @newID = @objID
		SELECT @ID = ID FROM A_PROPOSALS_HISTORY WHERE OBJECT_ID = @objID
	end


UPDATE A_PROPOSALS_HISTORY SET
NAME = @NAME,
CUSTOMER_CO = @CUSTOMER_CO,
SUPPLIER_CO = @SUPPLIER_CO,
PROP_DATE = @PROP_DATE,
PROP_VALID_DATE = @PROP_VALID_DATE,
OPENING = @OPENING,
ADD_NOTES = @ADD_NOTES,
CLOSING = @CLOSING,
MODBY = @strNTLogin,
DRCM = getDate(),
CREATOR_TITLE = (SELECT POSITION_NAME FROM A_PEOPLE_SEARCH_TABLE pst WHERE pst.OBJ_ID = A_PROPOSALS_HISTORY.CREATOR_ID)
WHERE OBJECT_ID = @newID



print 'Deleting all of this proposals customer contact Links'
DELETE FROM A_PROPOSAL_CONTACTS WHERE PROP_HIST_ID = @ID 
CREATE TABLE #TempItems	(IT varchar(50))
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @CUSTOMER_CONTACTS,', '
Declare @it nvarchar(50)
Declare @curs Cursor
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	set @it = ltrim(@it)
	INSERT INTO A_PROPOSAL_CONTACTS (ID,PROP_HIST_ID,CONTACT_ID,DRCM,MODBY)
		VALUES (newID(),@ID,@IT,getDate(),@strNTLogin)
	Fetch Next from @curs Into @it
End
close @curs

print 'Updating proposal quote link'
print 'making a cursor to go through the quote list'
DELETE FROM A_PROPOSALS_QUOTE_LINK WHERE PROP_HIST_ID = @ID
DELETE FROM #TempItems
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @QUOTE_LIST,', '
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	set @it = ltrim(@it)
	print 'Adding Quote ID = ' + @it
	INSERT INTO A_PROPOSALS_QUOTE_LINK (ID,PROP_HIST_ID,QUOTE_ID,DRCM,MODBY)
		VALUES (newID(),@ID,@IT,getDate(),@strNTLogin)
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs

DROP TABLE #TempItems







