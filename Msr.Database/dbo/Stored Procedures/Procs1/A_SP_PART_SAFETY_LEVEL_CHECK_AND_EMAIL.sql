




CREATE      PROCEDURE  dbo.A_SP_PART_SAFETY_LEVEL_CHECK_AND_EMAIL
@safetyLevelID varchar(50),
@strNTLogin varchar(50)
AS
print 'Checking and emailing'
declare @curLevel float,@minLevel float,@minWLevel float,
	@maxLevel float, @maxWLevel float,@partID varchar(50),
	@partName nvarchar(1000),@locID varchar(50), @locName nvarchar(1000),
	@SUBJECT nvarchar(4000),@direction varchar(50)
SELECT  @curLevel =CUR_LEVEL,@minLevel = MIN_LEVEL,@minWLevel = MIN_WARNING_LEVEL,
	@maxLevel = MAX_LEVEL, @maxWLevel = MAX_WARNING_LEVEL,
	@locID = LOCATION_ID,@partID = PART_ID,@locName = LOCATION_COMPLETE_NAME,
	@partName = PART_NAME,
	@direction = 
	case
		when CUR_LEVEL > OLD_LEVEL then 'INCREASED'
		else 'DECREASED'
	end
	FROM A_V_PARTS_SAFETY_STOCK_LEVELS WHERE ID = @safetyLevelID

declare @emailID varchar(50)
exec A_SP_ZZ_EMAIL_CREATE @emailID OUTPUT,1,1,null
declare @emailType varchar(50)
if @curLevel <= @minLevel
	begin
	set @SUBJECT = '<obj type="span"><attribute name="value" value="is at or below the minimum value of" /></obj>'
	set @SUBJECT = @subject + '<obj type="span"><attribute name="dontUsePutText" value="true" /><attribute name="value" value="' + convert(varchar(50),@minLevel) + '" /></obj>'
	set @emailType = 'FAIL'
	goto createSubject
	end
if @curLevel <= @minWLevel
	begin
	set @SUBJECT = '<obj type="span"><attribute name="value" value="is at or below minimum warning value of" /></obj>'
	set @SUBJECT = @subject + '<obj type="span"><attribute name="dontUsePutText" value="true" /><attribute name="value" value="' + convert(varchar(50),@minWLevel) + '" /></obj>'
	set @emailType = 'WARN'
	goto createSubject
	end
if @curLevel >= @maxLevel
	begin
	set @SUBJECT = '<obj type="span"><attribute name="value" value="is at or above the maximum value of" /></obj>'
	set @SUBJECT = @subject + '<obj type="span"><attribute name="dontUsePutText" value="true" /><attribute name="value" value="' + convert(varchar(50),@maxLevel) + '" /></obj>'
	set @emailType = 'FAIL'
	goto createSubject
	end
if @curLevel >= @maxWLevel
	begin
	set @SUBJECT = '<obj type="span"><attribute name="value" value="is at or above the maximum warning value of" /></obj>'
	set @SUBJECT = @subject + '<obj type="span"><attribute name="dontUsePutText" value="true" /><attribute name="value" value="' + convert(varchar(50),@maxWLevel) + '" /></obj>'
	set @emailType = 'WARN'
	goto createSubject
	end
goto fin

--Since we got here we must have someting to say.  We will create the subject now
createSubject:
declare @qs1 nvarchar(4000),@qs2 nvarchar(4000)
set @qs1 = '<obj type="span"><attribute name="value" value="ANSWER PART STOCK SAFETY LEVEL ALERT -" /></obj>'
set @qs1 = @qs1 + '<obj type="span"><attribute name="value" value="' + isnull(@partName,'NO NAME PART') + '" /><attribute name="dontUsePutText" value="true" /></obj>'
set @qs1 = @qs1 + '<obj type="span"><attribute name="value" value="Has ' + @direction + '" /></obj>'
set @qs1 = @qs1 + '<obj type="span"><attribute name="dontUsePutText" value="true" /><attribute name="value" value="' + convert(varchar(50),@curLevel) + '" /></obj>'
exec A_SP_ZZ_EMAIL_ADD_TEXT @emailID,'SUBJECT',@qs1,@strNTLogin
exec A_SP_ZZ_EMAIL_ADD_TEXT @emailID,'SUBJECT',@SUBJECT,@strNTLogin
set @qs2 = '<obj type="span"><attribute name="value" value="at the location" /></obj>'
set @qs2 = @qs2 + '<obj type="span"><attribute name="value" value="' + isnull(@locName,'NO NAME PART') + '" /><attribute name="dontUsePutText" value="true" /></obj>'
exec A_SP_ZZ_EMAIL_ADD_TEXT @emailID,'SUBJECT',@qs2,@strNTLogin

--Now we need to make a body for this email.
createBody:
exec A_SP_ZZ_EMAIL_ADD_TEXT @emailID,'TEXT_BODY',@qs1,@strNTLogin
exec A_SP_ZZ_EMAIL_ADD_TEXT @emailID,'TEXT_BODY',@SUBJECT,@strNTLogin
exec A_SP_ZZ_EMAIL_ADD_TEXT @emailID,'TEXT_BODY',@qs2,@strNTLogin

exec A_SP_ZZ_EMAIL_ADD_TEXT @emailID,'HTML_BODY',@qs1,@strNTLogin
exec A_SP_ZZ_EMAIL_ADD_TEXT @emailID,'HTML_BODY',@SUBJECT,@strNTLogin
exec A_SP_ZZ_EMAIL_ADD_TEXT @emailID,'HTML_BODY',@qs2,@strNTLogin

declare @bod nvarchar(4000)
set @bod = '<obj type="link">
				<attribute name="url" value="' +  isNull(dbo.getEmailURL(),'NULLEMAILURL') + '/asp/actualParts/searchActualParts.asp?PART_ID=' + 
					@partID + '&amp;LOCATION=' + @locID + '&amp;STATUS=APPROVED%2C+APPROVED_BUT_REVISING%2C+APPROVED_BUT_DELETING" />
				<obj type="text"><attribute name="value" value="Click here to view the Actual Parts at the location." /></obj>
			</obj>'
exec A_SP_ZZ_EMAIL_ADD_TEXT @emailID,'HTML_BODY',@bod,@strNTLogin
--Now add the roles to email to the list
print 'Email Type = ' + @emailType
INSERT INTO A_Z_EMAILS_TO_SEND_ROLES_TO_EMAIL 
	(ID,EMAIL_ID,ROLE_ID,DRCM,MODBY,PAGE)
	SELECT newID(),@emailID,ROLE_ID,getDate(),@strNTLogin,'1'
		FROM A_PARTS_SAFETY_STOCK_ROLES WHERE SAFETY_STOCK_ID = @safetyLevelID AND EMAIL_TYPE = @emailType

--Finished making an email
exec A_SP_ZZ_EMAIL_SUBMIT @emailID


fin:






