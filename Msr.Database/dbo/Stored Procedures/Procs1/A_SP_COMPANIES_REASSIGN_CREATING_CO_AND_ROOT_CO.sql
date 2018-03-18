--sarb

CREATE PROCEDURE A_SP_COMPANIES_REASSIGN_CREATING_CO_AND_ROOT_CO
@objectID varchar(50),
@strNTLogin varchar(50)
AS
declare @parentID as varchar(50),
	@myRoot as varchar(50),
	@parentName as nvarchar(200)

SELECT @parentID = PARENT,@myRoot = ROOT FROM A_O_COMPANIES WHERE OBJECT_ID = @objectID
SELECT @parentName = NAME FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @parentID
if @parentID is not null
	begin
		UPDATE A_OBJECTS SET CREATING_CO = @parentID,CREATING_CO_NAME = @parentName WHERE CREATING_CO = @myRoot
		UPDATE A_PEOPLE_HISTORY SET ROOT_COMPANY = @parentID WHERE ROOT_COMPANY = @myRoot
	end


