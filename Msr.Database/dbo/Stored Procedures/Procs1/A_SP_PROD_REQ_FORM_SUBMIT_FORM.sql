





CREATE         PROCEDURE dbo.A_SP_PROD_REQ_FORM_SUBMIT_FORM
@newID nvarchar(50) OUTPUT,
@messages nvarchar(2000) OUTPUT,
@id varchar(50),
@strNTLogin varchar(50)
AS
declare @upLoader as varchar(50)
SELECT @upLoader = UPLOADER FROM A_PROD_REQ_FORMS WHERE ID = @id
if @upLoader <> @strNTLogin
	begin
	set @newID = 'You are not the uploader so you can not submit this one'
	goto fin
	end

UPDATE A_PROD_REQ_FORMS SET PROGRESS = 'PRQ_SUBMITTED' WHERE ID = @id
exec A_SP_PROD_REQ_FORM_UPDATE_DATE @ID,@strNTLogin
fin:






