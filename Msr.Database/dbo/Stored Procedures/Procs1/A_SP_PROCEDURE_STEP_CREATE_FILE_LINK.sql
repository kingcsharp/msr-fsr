




CREATE    PROCEDURE A_SP_PROCEDURE_STEP_CREATE_FILE_LINK
@stepID nvarchar(50),
@fileID nvarchar(50),
@strNTLogin nvarchar(50)
AS

INSERT INTO A_PROCEDURE_STEP_FILE_LINK (ID,STEP_ID,FILE_ID,DRCM,MODBY)
VALUES (newID(),@stepID,@fileID,getDate(),@strNTLogin)



