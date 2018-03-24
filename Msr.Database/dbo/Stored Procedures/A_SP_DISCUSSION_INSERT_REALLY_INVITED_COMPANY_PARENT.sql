/*
STORED PROCEDURE CALLED IN A_SP_
*/
CREATE     PROCEDURE A_SP_DISCUSSION_INSERT_REALLY_INVITED_COMPANY_PARENT
@DISCUSSIONID nvarchar(50),
@companyID nvarchar(2000),
@strNTLogin nvarchar(50)
AS
declare @myDeptParent varchar(50)
print 'inside A_SP_DISCUSSION_INSERT_REALLY_INVITED_COMPANY_PARENT '
SELECT @myDeptParent = PARENT 
FROM A_APPROVED_COMPANIES 
WHERE ID=@companyID
print 'myDepartParent is' + isnull(@myDeptParent,'null')
declare @test varchar(50)
if @myDeptParent is not null
	begin
	SELECT @test = ID  
    	FROM A_DISCUSSION_REALLY_INIVTED_COMPANY
    	WHERE COMPANY_ID = @myDeptParent AND DISCUSSION_ID = @discussionID 
    if @test is null
	    begin  
		INSERT INTO A_DISCUSSION_REALLY_INIVTED_COMPANY  
		 			(ID,[DISCUSSION_ID],[COMPANY_ID], [DRCM],[MODBY])
			VALUES(newid(),@discussionID,@myDeptParent, getDate() ,@strNTLogin)
		print 'Insert parent' + @myDeptParent + 'into A_DISCUSSION_INV_DEPARTMENTS of company' + @companyID  
	    end
     exec A_SP_DISCUSSION_INSERT_REALLY_INVITED_COMPANY_PARENT @discussionID, @myDeptParent , @strNTLogin
	end