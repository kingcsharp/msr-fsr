

CREATE  PROCEDURE DBO.A_SP_NOTE_SEND_EMAIL
@ID varchar(50),
@strNTLogin varchar(50)
AS
print 'Sending an email about note = ' + isnull(@ID,'NULL')
declare @linkPath varchar (1000), @emailSubject varchar(1000),@emailBody varchar(8000),
	@author varchar(50),@txt varchar(4000),@authorName varchar(200),
	@emailBodyID varchar(50)

select @linkPath = dbo.xmlEncode('asp/notes/edit.asp?ID=' + @ID),
	@txt = TXT,
	@author = AUTHOR
	FROM A_NOTES WHERE ID = @ID

SELECT @authorName = FULL_NAME FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @author

set @emailBodyID = newID()
exec addToEmailBody @emailBodyID,1,
'A NOTE HAS BEEN SENT TO YOU'
declare @meBody varchar(7000)
set @meBody = 
'<obj type="table">
	<obj type="row">
		<obj type="col"> <attribute name="style" value="vertical-align:top" />
			<obj type="text" dontUsePutText="true"><attribute name="value" value="Author:" /></obj>
		</obj>
		<obj type="col"><attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="dontUsePutText" value="true" /><attribute name="value" value="' + isNull(dbo.xmlEncode(@authorName),'') + '" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col"> <attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="Link:" /></obj>
		</obj>
		<obj type="col"><attribute name="style" value="vertical-align:top" />
			<obj type="link">
				<attribute name="url" value="' +  isNull(dbo.getEmailURL(),'NULLEMAILURL') + '/' + isNull(@linkPath,'NULLPATH') + '" />
				<obj type="text"><attribute name="value" value="Link to information" /></obj>
			</obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col"> <attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="Note:" /></obj>
		</obj>
		<obj type="col"><attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="dontUsePutText" value="true" /><attribute name="value" value="
'
exec addToEmailBody @emailBodyID,0,@meBody
exec addToEmailBody @emailBodyID,0,@txt
exec addToEmailBody @emailBodyID,0,
				'"  />
			</obj>
		</obj>
	</obj>
</obj>
'
INSERT INTO A_ADMIN_EMAIL_SEND_LIST (ID,ADDY,EMAIL_BODY_ID)
	SELECT newID(),e.ADDY,@emailBodyID FROM A_NOTE_PEOPLE_TO_VIEW v,A_V_PEOPLE_APPROVED_DATA p,A_EMAILS e WHERE
	v.NOTE_ID = @ID AND v.PERSON_ID = p.ID AND p.OBJECT_ID = e.OBJECT_ID

INSERT INTO A_ADMIN_EMAIL_SEND_LIST (ID,ADDY,EMAIL_BODY_ID)
	SELECT newID(),e.ADDY,@emailBodyID FROM 
		A_NOTE_ROLES_TO_VIEW v,
		A_V_ROLES_APPROVED_WITH_PEOPLE_IDS rp,
  		A_V_PEOPLE_APPROVED_DATA p,
		A_EMAILS e 
	WHERE
		v.NOTE_ID = @ID AND 
		v.ROLE_ID = rp.ROLE_ID AND
		rp.PERSON = p.ID AND
		p.OBJECT_ID = e.OBJECT_ID

CREATE TABLE #cos (ID varchar(50))
INSERT INTO #cos SELECT COMPANY_ID FROM A_NOTE_COMPANIES_TO_VIEW WHERE NOTE_ID = @ID
INSERT INTO #cos SELECT CHILD_COMPANY FROM A_COMPANIES_CHILD_LOOKUP_TABLE WHERE COMPANY IN (SELECT COMPANY_ID FROM A_NOTE_COMPANIES_TO_VIEW WHERE NOTE_ID = @ID)
INSERT INTO A_ADMIN_EMAIL_SEND_LIST (ID,ADDY,EMAIL_BODY_ID)
	SELECT newID(),e.ADDY,@emailBodyID FROM 
  		A_V_PEOPLE_APPROVED_DATA p,
		A_EMAILS e 
	WHERE
		p.OBJECT_ID = e.OBJECT_ID
		AND 
		p.COMPANY IN (SELECT ID FROM #cos)

UPDATE A_ADMIN_EMAIL_BODIES SET STAT = 'READY_TO_SEND' WHERE ID = @emailBodyID

print'finished sending email about a service call'
fin:


