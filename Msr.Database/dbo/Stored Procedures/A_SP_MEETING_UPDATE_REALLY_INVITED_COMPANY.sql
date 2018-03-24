

/*
STORED PROCEDURE CALLED IN A_SP_MEETINGS_UPDATE_INV_COMPANY
*/
CREATE          PROCEDURE A_SP_MEETING_UPDATE_REALLY_INVITED_COMPANY
@meetingID varchar(50),
@companyID varchar(50),
@strNTLogin varchar(50)
AS
Declare @curs Cursor
Declare @it varchar(50)
declare @test varchar(50)
print 'delete the companys from the really inivted table form meeting' +@meetingID
DELETE FROM A_MEETING_REALLY_INIVTED_COMPANY WHERE  --call this really_invited_company
MEETING_ID = @meetingID
--cursor call
set @curs = Cursor For 
SELECT COMPANY_ID 
FROM A_MEETING_INV_COMPANY 
WHERE MEETING_ID=@meetingID 
open @curs
Declare @parentName varchar(50) 
Fetch Next from @curs Into @it 
while (@@fetch_status = 0)
	Begin
	print 'Got an invited company ' + @it
	SELECT @test = ID  
    	FROM A_MEETING_REALLY_INIVTED_COMPANY
    	WHERE COMPANY_ID = @it AND MEETING_ID = @meetingID  AND OPTIONAL= '1' 
    if @test is null
	begin
      -- optional value of 0 means its not optional
	 	INSERT INTO A_MEETING_REALLY_INIVTED_COMPANY  
    	    (ID,[MEETING_ID],[COMPANY_ID], [OPTIONAL],[DRCM],[MODBY])
		VALUES(newid(),@meetingID,@it, 0, getDate() ,@strNTLogin)
	end 
-- find the parent company
    print 'just inserted my really invited company' 
	exec A_SP_MEETING_INSERT_REALLY_INVITED_COMPANY_PARENT @meetingID, @it , @strNTLogin 
        -- find the child companies 
    exec A_SP_MEETING_INSERT_REALLY_INVITED_COMPANY_CHILD  @meetingID, @it , @strNTLogin 
Fetch Next from @curs Into @it
	End
close @curs
Deallocate @curs
--end cursor call