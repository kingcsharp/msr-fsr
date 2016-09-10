





CREATE     PROCEDURE dbo.A_SP_PROD_REQ_FORM_DELETE
@RET_STATUS as varchar(50) OUTPUT,
@MSGS as varchar(50) OUTPUT,
@ID as varchar(50),
@strNTLogin as varchar(50)
AS
print 'Deleting a Product Requirements form'
print 'Verifying that I am the person who can Close it'
declare @origReq as varchar(50)
SELECT @origReq = UPLOADER FROM A_PROD_REQ_FORMS WHERE ID = @ID
if @origReq = @strNTLogin
	begin
		DELETE FROM A_PROD_REQ_FORMS WHERE ID = @ID
	end
else
	begin
		set @RET_STATUS = 'ERROR --  You can not delete anything you did not upload'
	end
fin: 






