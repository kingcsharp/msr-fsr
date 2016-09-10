









CREATE    procedure A_SP_PROPOSALS_COPY_ONE
	@newObjID varchar(50) OUTPUT,
	@strID varchar(50),
	@copyPrefix varchar(50),
	@strNTLogin varchar(50)
as
--Make a new ID for the copy
declare @newID as varchar(50)
exec sp_getUniqueID3 @newID OUTPUT
--Insert the new one
INSERT INTO A_PROPOSALS_HISTORY (
ID,NAME,
CUSTOMER_CO,SUPPLIER_CO,PROP_DATE,PROP_VALID_DATE,OPENING,ADD_NOTES,CLOSING,
DRCM,MODBY,CREATOR_ID)
SELECT @newID as ID,@copyPrefix + NAME as NAME,
CUSTOMER_CO,SUPPLIER_CO,PROP_DATE,PROP_VALID_DATE,OPENING,ADD_NOTES,CLOSING,
getDate(),@strNTLogin,@strNTLogin
FROM A_PROPOSALS_HISTORY WHERE ID = @strID
--find out what object id the new one got
SELECT @newObjID = OBJECT_ID FROM A_PROPOSALS_HISTORY WHERE ID = @newID
print 'Now copying all my contacts'
INSERT INTO A_PROPOSAL_CONTACTS (
	ID,DRCM,MODBY,PROP_HIST_ID,
	CONTACT_ID,ADDRESS_TO
	)
SELECT 
	newID(),getDate(),@strNTLogin,@newID,
	CONTACT_ID,ADDRESS_TO
FROM 	A_PROPOSAL_CONTACTS WHERE PROP_HIST_ID = @strID

print 'Now copying all the quote links'
INSERT INTO A_PROPOSALS_QUOTE_LINK (
	ID,DRCM,MODBY,PROP_HIST_ID,
	QUOTE_ID
	)
SELECT 
	newID(),getDate(),@strNTLogin,@newID,
	QUOTE_ID
FROM 	A_PROPOSALS_QUOTE_LINK WHERE PROP_HIST_ID = @strID








