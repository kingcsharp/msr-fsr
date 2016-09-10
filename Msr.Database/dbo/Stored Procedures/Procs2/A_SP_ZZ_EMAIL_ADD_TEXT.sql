CREATE PROCEDURE dbo.A_SP_ZZ_EMAIL_ADD_TEXT
@ID varchar(50),
@txtType varchar(50),
@TXT nvarchar(4000),
@strNTLogin varchar(50)
AS
declare @myCnt int
SELECT @myCnt = count(ID) FROM A_Z_EMAILS_TO_SEND_TEXT WHERE TXT_TYPE = @txtType and EMAIL_ID = @ID
INSERT INTO A_Z_EMAILS_TO_SEND_TEXT (ID,EMAIL_ID,TXT_TYPE,TXT,DRCM,MODBY,CNT)
	VALUES (newID(),@ID,@txtType,@TXT,getDate(),@strNTLogin,isNull(@myCnt + 1,1))

