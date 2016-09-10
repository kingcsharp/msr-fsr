

/*
STORED PROCEDURE CALLED IN A_SP_SURVEY_UPDATE_INVITED_COMPANY
*/
create        PROCEDURE A_SP_SURVEY_UPDATE_REALLY_INVITED_COMPANY
@surveyID nvarchar(50),
@companyID nvarchar(2000),
@strNTLogin nvarchar(50)
AS
Declare @curs Cursor
Declare @it varchar(50)
print 'delete the companys from the really inivted table form survey' +@surveyID
DELETE FROM A_SURVEY_REALLY_INIVTED_COMPANY WHERE  --call this really_invited_company
SURVEY_ID = @surveyID
--cursor call
set @curs = Cursor For 
SELECT COMPANY_ID 
FROM A_SURVEY_INV_COMPANY 
WHERE SURVEY_ID=@surveyID 
open @curs
Declare @parentName varchar(50) 
Fetch Next from @curs Into @it 
while (@@fetch_status = 0)
	Begin
	print 'Got an invited company ' + @it
 	INSERT INTO A_SURVEY_REALLY_INIVTED_COMPANY  
        (ID,[SURVEY_ID],[COMPANY_ID], [DRCM],[MODBY])
	VALUES(newid(),@surveyID,@it, getDate() ,@strNTLogin)
        -- find the parent company
    print 'just inserted my really invited company' 
	exec A_SP_SURVEY_INSERT_REALLY_INVITED_COMPANY_PARENT @surveyID, @it , @strNTLogin 
        -- find the child companies 
       	exec A_SP_SURVEY_INSERT_REALLY_INVITED_COMPANY_CHILD  @surveyID, @it , @strNTLogin 
Fetch Next from @curs Into @it
	End
close @curs
Deallocate @curs
--end cursor call






