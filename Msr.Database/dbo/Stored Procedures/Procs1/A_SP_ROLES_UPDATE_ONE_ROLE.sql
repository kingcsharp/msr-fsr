


CREATE PROCEDURE [dbo].[A_SP_ROLES_UPDATE_ONE_ROLE]
	@returnID nvarchar(50) OUTPUT,
	@msg nvarchar(500) OUTPUT,	
	@NAME nvarchar(100),
	@ID nvarchar(50),
	@SECURITY_LEVEL nvarchar(50),
	@REFERENCE_FILES varchar(8000),
	@TrainingIDRev varchar(50),
	@Comments varchar(4000),
	@strNTLogin nvarchar(50)
AS

declare @objId as varchar(50)
if not(@ID is null)
begin
	declare @tester as nvarchar(50)
	SELECT @tester = ID FROM A_O_ROLES WHERE ID = @ID AND CREATING_CO = (select dbo.getCompany(@strNTLogin))
	if @tester is null
		set @msg =  'Error cannot find the ROLE ' + @ID + ' to update IT '
	else
		begin 
			UPDATE A_ROLES_HISTORY SET
			NAME = @NAME,
			SECURITY_LEVEL = @SECURITY_LEVEL,
			TRAININGIDREV = @TrainingIDRev,
			COMMENTS = @Comments,
			MODBY = @strNTLogin,
			DRCM = getDate()
			WHERE ID = @ID
			set @returnID = @ID
			
		set @objId = (select OBJECT_ID
		from A_ROLES_HISTORY
		where ID = @ID) 
		end
end
else
	begin
		exec sp_getUniqueID3 @ID OUTPUT
		INSERT INTO A_ROLES_HISTORY (ID,NAME,SOURCE,HIDDEN,SECURITY_LEVEL,TRAININGIDREV,COMMENTS,MODBY,DRCM) VALUES
		(@ID,@NAME,'A_SP_UPDATE_ROLE',0,@SECURITY_LEVEL,@TrainingIDRev,@Comments,@strNTLogin,getDate())
		set @returnID = @ID

		set @objId = (select OBJECT_ID
		from A_ROLES_HISTORY
		where ID = @ID) 
	end


print 'Deleting all of this companies File Links'
DELETE FROM A_DOCUMENT_LINK WHERE OBJECT_ID = @objId
DECLARE @pos INT
DECLARE @len INT
DECLARE @value varchar(8000)
DECLARE @cont BIT

set @cont = 1 
set @pos = 0
set @len = 0

WHILE @cont = 1
if CHARINDEX(',', @REFERENCE_FILES,@pos) = 0
	BEGIN
		
		set @len = LEN(@REFERENCE_FILES)

		
		set @value = LTRIM(RTRIM(SUBSTRING(@REFERENCE_FILES, @pos, @len)))

		exec A_SP_FILES_CREATE_LINK @objId,@value,null,@strNTLogin
        --print @value
		set @cont = 0
	END
else
BEGIN
    set @len = CHARINDEX(',', @REFERENCE_FILES, @pos+1) - @pos

    set @value = LTRIM(RTRIM(SUBSTRING(@REFERENCE_FILES, @pos, @len)))
    --print @value
	exec A_SP_FILES_CREATE_LINK @objId,@value,null,@strNTLogin
    
    set @pos = CHARINDEX(',', @REFERENCE_FILES, @pos+@len) +1
END





