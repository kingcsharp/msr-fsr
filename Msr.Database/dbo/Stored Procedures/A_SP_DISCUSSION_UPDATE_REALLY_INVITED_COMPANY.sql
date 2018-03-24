
/*
STORED PROCEDURE CALLED IN A_SP_DISCUSSION_UPDATE_INVITED_COMPANY
*/
CREATE        PROCEDURE A_SP_DISCUSSION_UPDATE_REALLY_INVITED_COMPANY
@discussionID nvarchar(50),
@companyID nvarchar(2000),
@strNTLogin nvarchar(50)
AS
Declare @curs Cursor
Declare @it varchar(50)
print 'delete the companys from the really inivted table form discussion' +@discussionID
DELETE FROM A_DISCUSSION_REALLY_INIVTED_COMPANY WHERE  --call this really_invited_company
DISCUSSION_ID = @discussionID
--cursor call
set @curs = Cursor For 
SELECT COMPANY_ID 
FROM A_DISCUSSION_INV_COMPANY 
WHERE DISCUSSION_ID=@discussionID 
open @curs
Declare @parentName varchar(50) 
Fetch Next from @curs Into @it 
while (@@fetch_status = 0)
	Begin
	print 'Got an invited company ' + @it
 	INSERT INTO A_DISCUSSION_REALLY_INIVTED_COMPANY  
        (ID,[DISCUSSION_ID],[COMPANY_ID], [DRCM],[MODBY])
	VALUES(newid(),@discussionID,@it, getDate() ,@strNTLogin)
        -- find the parent company
    print 'just inserted my really invited company' 
	exec A_SP_DISCUSSION_INSERT_REALLY_INVITED_COMPANY_PARENT @discussionID, @it , @strNTLogin 
        -- find the child companies 
       	exec A_SP_DISCUSSION_INSERT_REALLY_INVITED_COMPANY_CHILD  @discussionID, @it , @strNTLogin 
Fetch Next from @curs Into @it
	End
close @curs
Deallocate @curs
--end cursor call