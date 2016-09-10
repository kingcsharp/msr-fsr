
CREATE PROCEDURE DBO.A_SP_ADMIN_EMAIL_RECORD_ERROR 
@strTo nvarchar(100),
@strFrom nvarchar(100),
@strSubj nvarchar(1000),
@strBody nvarchar(4000),
@strToID varchar(50),
@strNTLogin varchar(50),
@strFileName varchar(100)
AS
INSERT INTO A_ADMIN_EMAIL_ERROR_LOG([ID], [TO_EMAIL], [FROM_EMAIL], [SUBJ], [BODY], [TO_ID], [FILE_MAILED_FROM], [DRCM], [MODBY])
VALUES(newID(),@strTo,@strFrom,@strSubj,@strBody,@strToID,@strFileName,getDate(),@strNTLogin)
