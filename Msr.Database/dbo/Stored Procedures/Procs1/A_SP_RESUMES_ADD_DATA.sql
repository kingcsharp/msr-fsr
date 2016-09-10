CREATE PROCEDURE dbo.A_SP_RESUMES_ADD_DATA
@data nvarchar(2000),
@resumeID varchar(50),
@dataType varchar(50),
@strNTLogin varchar(50)
AS
declare @cnt int
SELECT @cnt = (1 + COUNT(ID)) FROM A_RESUME_DATA WHERE RESUME_ID = @resumeID AND DATA_TYPE = @dataType
INSERT INTO A_RESUME_DATA (ID,CNT,RESUME_ID,DATA_TYPE,DATA,DRCM,MODBY)
	VALUES (newID(),@cnt,@resumeID,@dataType,@data,getDate(),@strNTLogin)

