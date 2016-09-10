



CREATE       PROCEDURE dbo.A_SP_PROD_REQ_FORM_UPDATE_FORM
@newID nvarchar(50) OUTPUT,
@messages nvarchar(2000) OUTPUT,
@id varchar(50),
@file varchar(50),
@KEYWORDS nvarchar(4000),
@CUSTOMER_PERSON_ID varchar(50),
@PRODUCT_ID varchar(50),
@PROGRESS varchar(50),
@RESULTING_PRODUCT varchar(50),
@saveType varchar(50),
@strNTLogin varchar(50)
AS
if @saveType = 'UPLOADER'
	begin
	if @file is null
		begin
			goto fin
			set @newID = 'Error no file'
		end
	
	print 'Updating a Product Request Form'
	if @ID is null
		begin
			print 'ID is Null we need to create this Product Request Form'
			exec sp_GetUniqueID3 @newID OUTPUT
			set @ID = @newID
			INSERT INTO A_PROD_REQ_FORMS(ID,DATE_UPLOADED,FILE_ID,MODBY,DRCM,UPLOADER,PROGRESS) 
			VALUES(@ID,getDate(),@file,@strNTLogin,getDATE(),@strNTLogin,'PRF_CREATING')
		end
	else
		begin
			set @newID = @ID
		end
	print 'I need to figure out what company is the customer company of this form'
	declare @custCo as varchar(50)
	SELECT @custCo = COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @CUSTOMER_PERSON_ID
	
	print 'Updating all the data in the product request form'
	UPDATE A_PROD_REQ_FORMS SET
	FILE_ID = @file,
	FILE_KEYWORDS = @KEYWORDS,
	CUST_PERSON_ID = @CUSTOMER_PERSON_ID,
	CUSTOMER_CO = @custCo,
	PRODUCT_ID = @PRODUCT_ID,
	PROGRESS = @PROGRESS,
	RESULT_PRODUCT = @RESULTING_PRODUCT,
	DRCM = getDate(),
	MODBY = @strNTLogin
	WHERE ID = @ID
	end
else
	begin
	print 'I am saving a Prod Requirement Form as a MGR'
	UPDATE A_PROD_REQ_FORMS SET
	PROGRESS = @PROGRESS,
	RESULT_PRODUCT = @RESULTING_PRODUCT,
	DRCM = getDate(),
	MODBY = @strNTLogin
	WHERE ID = @ID
	set @newID = @ID
	end

exec A_SP_PROD_REQ_FORM_UPDATE_DATE @ID,@strNTLogin

fin:




