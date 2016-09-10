
CREATE PROCEDURE dbo.A_SP_COMPANY_GET_LOGO_FILE
@ID varchar(50)
AS
declare @objID varchar(50),@logoID varchar(50)
SELECT @objID = OBJECT_ID FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @ID
print 'Trying to find the logo for obj = ' + @objID
SELECT @logoID = LINKED_DOC_ID FROM A_DOCUMENT_LINK WHERE OBJECT_ID = @objID AND TYPE='LOGO'
if @logoID is null
	begin
	declare @parentID varchar(50)
	SELECT @parentID = PARENT FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @ID
	if @parentID is not null
		exec A_SP_COMPANY_GET_FIRST_LOGO_FILE @logoID OUTPUT,@parentID
	end

SELECT @logoID AS LOGO_ID




