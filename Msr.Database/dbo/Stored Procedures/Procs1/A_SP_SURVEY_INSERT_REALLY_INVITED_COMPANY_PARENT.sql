
/*
STORED PROCEDURE CALLED IN A_SP_
*/
create     PROCEDURE A_SP_SURVEY_INSERT_REALLY_INVITED_COMPANY_PARENT
@SURVEYID nvarchar(50),
@companyID nvarchar(2000),
@strNTLogin nvarchar(50)
AS
declare @myDeptParent varchar(50)
print 'inside A_SP_SURVEY_INSERT_REALLY_INVITED_COMPANY_PARENT '
SELECT @myDeptParent = PARENT 
FROM A_APPROVED_COMPANIES 
WHERE ID=@companyID
print 'myDepartParent is' + isnull(@myDeptParent,'null')
declare @test varchar(50)
if @myDeptParent is not null
	begin
	SELECT @test = ID  
    	FROM A_SURVEY_REALLY_INIVTED_COMPANY
    	WHERE COMPANY_ID = @myDeptParent AND SURVEY_ID = @surveyID 
    if @test is null
	    begin  
		INSERT INTO A_SURVEY_REALLY_INIVTED_COMPANY  
		 			(ID,[SURVEY_ID],[COMPANY_ID], [DRCM],[MODBY])
			VALUES(newid(),@surveyID,@myDeptParent, getDate() ,@strNTLogin)
		print 'Insert parent' + @myDeptParent + 'into A_SURVEY_INV_DEPARTMENTS of company' + @companyID  
	    end
     exec A_SP_SURVEY_INSERT_REALLY_INVITED_COMPANY_PARENT @surveyID, @myDeptParent , @strNTLogin
	end  

