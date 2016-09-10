
/*
STORED PROCEDURE CALLED IN A_SP_MEETING_UPDATE_REALLY_INVITED_COMPANY
*/
CREATE       PROCEDURE A_SP_MEETING_INSERT_REALLY_INVITED_COMPANY_PARENT
@meetingID varchar(50),
@companyID varchar(50),
@strNTLogin varchar(50)
AS
declare @myDeptParent varchar(50)
print 'inside A_SP_MEETING_INSERT_REALLY_INVITED_COMPANY_PARENT '
SELECT @myDeptParent = PARENT 
FROM A_APPROVED_COMPANIES 
WHERE ID=@companyID
print 'myDepartParent is' + isnull(@myDeptParent,'null')
declare @test varchar(50)
if @myDeptParent is not null
	begin
	SELECT @test = ID  
    	FROM A_MEETING_REALLY_INIVTED_COMPANY
    	WHERE COMPANY_ID = @myDeptParent AND MEETING_ID = @meetingID 
    if @test is null
	    begin  
		 -- optional value of 1 means its  optional 
		INSERT INTO A_MEETING_REALLY_INIVTED_COMPANY  
		 			(ID,[MEETING_ID],[COMPANY_ID],OPTIONAL, [DRCM],[MODBY])
			VALUES(newid(),@meetingID,@myDeptParent,1,getDate() ,@strNTLogin)
		print 'Insert parent' + @myDeptParent + 'into A_MEETING_INV_DEPARTMENTS of company' + @companyID  
	    end
     exec A_SP_MEETING_INSERT_REALLY_INVITED_COMPANY_PARENT @meetingID, @myDeptParent , @strNTLogin
	end  
