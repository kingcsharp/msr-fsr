






CREATE     procedure A_SP_OBJECT_COPY_ALL_OBJECT_ITEMS
	@oldObjID nvarchar(50),
	@newObjID nvarchar(50),
	@strNTLogin nvarchar(50)
	
as
print 'Copying all the object related stuff over from Object = ' + isNull(@oldObjID,'NULL')
print 'To the new Object = ' + isNull(@newObjID,'NULL')

print 'Copying all locations'
INSERT INTO A_LOCATIONS_OBJECT_LINK (ID,LOCATION_ID,OBJECT_ID,LOCATION_TYPE,MODBY,DRCM,LOCATION_NAME)
SELECT newID(),LOCATION_ID,@newObjID,LOCATION_TYPE,@strNTLogin,getDate(),LOCATION_NAME FROM
A_LOCATIONS_OBJECT_LINK WHERE OBJECT_ID = @oldObjID
print 'Done copying locations'

print 'Copying all Emails'
INSERT INTO A_EMAILS (ID,ADDY,TYPE,MODBY,DRCM,OBJECT_ID,EMAIL_TYPE)
SELECT newID(),ADDY,TYPE,@strNTLogin,getDate(),@newObjID,EMAIL_TYPE FROM
A_EMAILS WHERE OBJECT_ID = @oldObjID
print 'Done copying Emails'

print 'Copying all Phone Numbers'
INSERT INTO A_PHONE_NUMBERS (ID,PHONE_NUMBER,PHONE_TYPE,PHONE_PIN,PHONE_EXTENSION,MODBY,DRCM,OBJECT_ID)
SELECT newID(),PHONE_NUMBER,PHONE_TYPE,PHONE_PIN,PHONE_EXTENSION,@strNTLogin,getDate(),@newObjID FROM
A_PHONE_NUMBERS WHERE OBJECT_ID = @oldObjID
print 'Done copying Phone Numbers'

print 'Copying all Files'
INSERT INTO A_DOCUMENT_LINK (ID,LINKED_DOC_ID,OBJECT_ID,MODBY,DRCM,SHOW_IN_LINE,TYPE)
SELECT newID(),LINKED_DOC_ID,@newObjID,@strNTLogin,getDate(),SHOW_IN_LINE,TYPE FROM
A_DOCUMENT_LINK WHERE OBJECT_ID = @oldObjID
print 'Done copying Phone Numbers'











