/*
STORED PROCEDURE CALLED IN A_SP_DISCUSSION_UPDATE_INV_DEPARTMENT
*/
CREATE    PROCEDURE A_SP_DISCUSSION_INSERT_REALLY_INVITED_COMPANY_CHILD
@discussionID nvarchar(50),
@companyID nvarchar(2000),
@strNTLogin nvarchar(50)
AS
declare @myDeptChild varchar(50)
Declare @curs Cursor
Declare @it varchar(50), @test varchar(50)
Declare @childName varchar(50) 
--cursor call
print 'inside A_SP_DISCUSSION_INSERT_REALLY_INVITED_COMPANY_CHILD '
set @curs = Cursor For 
SELECT ID, NAME 
FROM A_APPROVED_COMPANIES 
WHERE PARENT=@companyID 
open @curs
Fetch Next from @curs Into @it, @childName
while (@@fetch_status = 0)
	Begin
	SELECT @test = ID  
    	FROM A_DISCUSSION_REALLY_INIVTED_COMPANY
    	WHERE COMPANY_ID = @it AND DISCUSSION_ID = @discussionID 
    if @test is null
	    begin  
	 	INSERT INTO A_DISCUSSION_REALLY_INIVTED_COMPANY
    	    ([ID], [DISCUSSION_ID],[COMPANY_ID], [DRCM],[MODBY])
 		VALUES(newID(),@discussionID,@it, getDate() ,@strNTLogin)
       	print 'Insert child' + @it + 'into A_DISCUSSION_REALLY_INVITED_COMPANY of company' + @companyID  
		end 
	exec A_SP_DISCUSSION_INSERT_REALLY_INVITED_COMPANY_CHILD  @discussionID, @it , @strNTLogin 
	Fetch Next from @curs Into @it, @childName
	End
close @curs
Deallocate @curs
--end cursor call

