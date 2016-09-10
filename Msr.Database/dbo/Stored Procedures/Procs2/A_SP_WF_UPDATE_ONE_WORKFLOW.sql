
CREATE     PROCEDURE A_SP_WF_UPDATE_ONE_WORKFLOW
	@msg varchar(500) OUTPUT,
	@objID varchar(50) OUTPUT,
	@newID varchar(50) OUTPUT,
	@NAME varchar(500),
	@ID varchar(50),
	@AP_STAMP varchar(500),
	@AP_STAMP_PIC varchar(50),
	@strNTLogin varchar(50)
AS
if not(@ID is null)
begin
	declare @tester as nvarchar(50)
	SELECT @tester = ID FROM A_O_WORKFLOWS WHERE ID = @ID AND CREATING_CO = (select dbo.getCompany(@strNTLogin))
	if @tester is null
		select 'Error cannot find the stage ' + @ID + ' to update them ' as ERROR
	else
		UPDATE A_WORKFLOWS SET
		NAME = @NAME,
		STAMP_NAME = @AP_STAMP,
		STAMP_ID = @AP_STAMP_PIC,
		MODBY = @strNTLogin,
		DRCM = getDate()
		WHERE ID = @ID
end
else
	begin
		exec sp_getUniqueID3 @ID OUTPUT
		INSERT INTO A_WORKFLOWS (ID,NAME,STAMP_NAME,STAMP_ID,MODBY,DRCM) VALUES
		(@ID,@NAME,@AP_STAMP,@AP_STAMP_PIC,@strNTLogin,getDate())
	end

select @objID = OBJECT_ID FROM A_WORKFLOWS WHERE ID = @ID
set @newID = @ID







