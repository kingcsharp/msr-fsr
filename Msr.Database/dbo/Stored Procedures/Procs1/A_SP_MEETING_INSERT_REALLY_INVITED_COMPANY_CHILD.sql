

/*
STORED PROCEDURE CALLED IN A_SP_MEETING_UPDATE_INV_DEPARTMENT
*/
CREATE      PROCEDURE A_SP_MEETING_INSERT_REALLY_INVITED_COMPANY_CHILD
@meetingID nvarchar(50),
@companyID nvarchar(2000),
@strNTLogin nvarchar(50)
AS
declare @myDeptChild varchar(50)
Declare @curs Cursor
Declare @it varchar(50), @test varchar(50)
Declare @childName varchar(50) 
--cursor call
print 'inside A_SP_MEETING_INSERT_REALLY_INVITED_COMPANY_CHILD '
set @curs = Cursor For 
SELECT ID, NAME 
FROM A_APPROVED_COMPANIES 
WHERE PARENT=@companyID 
open @curs
Fetch Next from @curs Into @it, @childName
while (@@fetch_status = 0)
	Begin
	SELECT @test = ID  
    	FROM A_MEETING_REALLY_INIVTED_COMPANY
    	WHERE COMPANY_ID = @it AND MEETING_ID = @meetingID 
    if @test is null
	    begin  
       -- optinal value 1 means it optional
	 	INSERT INTO A_MEETING_REALLY_INIVTED_COMPANY
    	    ([ID], [MEETING_ID],[COMPANY_ID], OPTIONAL, [DRCM],[MODBY])
 		VALUES(newID(),@meetingID,@it, 1, getDate() ,@strNTLogin)
       	print 'Insert child' + @it + 'into A_MEETING_REALLY_INVITED_COMPANY of company' + @companyID  
		end 
	exec A_SP_MEETING_INSERT_REALLY_INVITED_COMPANY_CHILD  @meetingID, @it , @strNTLogin 
	Fetch Next from @curs Into @it, @childName
	End
close @curs
Deallocate @curs
--end cursor call


