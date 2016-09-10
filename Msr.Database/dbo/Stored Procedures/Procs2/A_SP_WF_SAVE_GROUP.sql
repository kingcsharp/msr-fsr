


CREATE  procedure A_SP_WF_SAVE_GROUP
	@result nvarchar(100) OUTPUT,
	@ID nvarchar(50),
	@NAME nvarchar (50),
	@strNTLogin nvarchar(50),
	@DRCM dateTime,
	@WF_GROUP_PEOPLE nvarchar(800),
	@WF_GROUP_SPECIALS nvarchar(500),
	@WF_GROUP_ROLES nvarchar(800)
	
as
print 'inputed values are'
print 'id = ' + (@ID)
print 'Name = ' + (@NAME)
print 'NTLogin = ' + (@strNTLogin)
if (@ID is NULL)
--If it is new then we need to create it 
--Creating it will also execute the trigger
--which will make the object and make it checked
--out to this person.
	begin
		print 'This is a new one'
--Which company is inserting this part
		declare @CREATING_CO as nvarchar(50)
		SELECT @CREATING_CO = CO_ID FROM A_V_PEOPLE_WITH_CO WHERE ID = @strNTLogin
--get a unique ID for this one
		declare @myID as nvarchar(50)
		exec sp_GetUniqueID3 @myID OUTPUT
--insert this group
		INSERT INTO A_WF_GROUPS
		(ID,NAME,DRCM,MODBY)
		Values
		(@myID,@NAME,getDate(),@strNTLogin)
	end
else
--We are just saving one that should be already checked to us
--so check to make sure it is checked to us and update
	begin
		declare @personID as nvarchar(50)
		select @personID = LOCKED_BY FROM A_V_WF_GROUPS WHERE ID = @ID
		if @personID != @strNTLogin
			begin
				UPDATE A_WF_GROUPS SET
				NAME = @NAME,
				DRCM = getDate(),
				MODBY = @strNTLogin 
				WHERE ID = @ID
			end
		else
			begin
				print @strNTLogin + ' is not the owner.  The owner is ' + @personID
				
			end
	end
--Now that we did that we need to change the people set up to approve this Group
--We will call a recursive procedure for each list.
--First the people allowed to approve



