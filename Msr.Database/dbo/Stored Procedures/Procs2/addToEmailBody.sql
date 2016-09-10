CREATE Procedure dbo.addToEmailBody
@ID varchar(50),
@isSubject tinyint,
@txt varchar(3900)
AS
declare @cnt int
SELECT @cnt = isNull(CNT + 1,1) FROM A_ADMIN_EMAIL_BODIES WHERE ID = @ID
INSERT INTO A_ADMIN_EMAIL_BODIES (ID,IS_SUBJECT,TXT,CNT)
	VALUES(@ID,@isSubject,@txt,@cnt)

