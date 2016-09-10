

CREATE   PROCEDURE dbo.A_SP_PROD_REQ_FORM_UPDATE_DATE
@ID varchar(50),
@strNTLogin varchar(50)
AS
declare @lastProg varchar(50),@curProg varchar(50)
SELECT @curProg = PROGRESS FROM A_PROD_REQ_FORMS WHERE ID = @ID
SELECT top 1 @lastProg = DATE_TYPE FROM A_PROD_REQ_FORM_DATES WHERE PRF = @ID ORDER BY DT DESC
if @curProg <> @lastProg
	begin
	print 'The Progress has changed'
	INSERT INTO A_PROD_REQ_FORM_DATES (ID,PRF,DATE_TYPE,DT,DRCM,MODBY,PERSON)
		VALUES(newID(),@ID,@curProg,getDate(),getDate(),@strNTLogin,@strNTLogin)
	exec A_SP_PROD_REQ_FORM_SEND_UPDATE_EMAIL @ID,@strNTLogin
	end


