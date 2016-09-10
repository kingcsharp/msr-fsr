




CREATE     PROCEDURE A_SP_THEORY_PARAGRAPH_CREATE_FILE_LINK
@paragraphID nvarchar(50),
@fileID nvarchar(50),
@strNTLogin nvarchar(50)
AS

INSERT INTO A_THEORY_PARAGRAPH_FILE_LINK (ID,PARAGRAPH_ID,FILE_ID,DRCM,MODBY)
VALUES (newID(),@paragraphID,@fileID,getDate(),@strNTLogin)



