CREATE  PROCEDURE Portal_GetCurrentUser
@strID nvarchar(50)
as
--build the sql for the query
declare @logoFileID varchar(50),@myHistID varchar(50),@myObjID varchar(50), @strNTLogin varchar(50)
declare @myCo varchar(50)
SELECT @myCo = COMPANY, @strNTLogin =Id  FROM A_APPROVED_PEOPLE WHERE LOGIN = @strID
exec A_SP_COMPANY_GET_FIRST_LOGO_FILE @logoFileID OUTPUT,@myCo

declare @bg varchar(50)
SELECT @bg = LINKED_DOC_ID FROM A_DOCUMENT_LINK WHERE OBJECT_ID = @strNTLogin AND TYPE = 'BACKGROUND'

SELECT @myHistID = HISTORY_REF_ID FROM A_PEOPLE WHERE ID = @strNTLogin
SELECT @myObjID = OBJECT_ID FROM A_PEOPLE_HISTORY WHERE ID = @myHistID

declare @myPic varchar(50)
SELECT @myPic = LINKED_DOC_ID FROM A_DOCUMENT_LINK WHERE OBJECT_ID = @myObjID AND TYPE = 'PICTURE'


SELECT @myPic as MY_PICTURE, @bg as BG_IMAGE, @logoFileID as LOGO_ID,* FROM A_APPROVED_PEOPLE WHERE ID = @strNTLogin