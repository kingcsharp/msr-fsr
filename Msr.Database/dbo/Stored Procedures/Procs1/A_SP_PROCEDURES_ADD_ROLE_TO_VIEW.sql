


CREATE      PROCEDURE A_SP_PROCEDURES_ADD_ROLE_TO_VIEW
	@roleID nvarchar(50),
	@procedureObjID nvarchar(50),	
	@strNTLogin nvarchar(50)
as
declare @newID as nvarchar(50), @procID as nvarchar(50), @tester as varchar(50)
SELECT @procID = ID FROM A_PROCEDURES_HISTORY WHERE OBJECT_ID = @procedureObjID
print 'The Proc ID = ' + @procID
SELECT @tester = ID FROM A_PROCEDURE_OBJECT_LINK WHERE 
	PROCEDURE_ID = @procID AND APPROVED_OBJECT_ID = @roleID AND RELATIONSHIP = 'ROLE_TO_VIEW'
if @tester is null 
	begin
	print 'Tester was not null so adding'
	exec sp_GetUniqueID3 @newID OUTPUT
	INSERT INTO A_PROCEDURE_OBJECT_LINK (ID,PROCEDURE_ID,APPROVED_OBJECT_ID,RELATIONSHIP,DRCM,MODBY)
	VALUES (@newID,@procID,@roleID,'ROLE_TO_VIEW',getDate(),@strNTLogin)
	end







