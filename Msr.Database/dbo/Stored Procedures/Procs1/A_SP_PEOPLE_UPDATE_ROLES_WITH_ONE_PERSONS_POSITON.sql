CREATE PROCEDURE dbo.A_SP_PEOPLE_UPDATE_ROLES_WITH_ONE_PERSONS_POSITON
@pID varchar(50),
@strNTLogin varchar(50)
AS
print 'In the procedure A_SP_PEOPLE_UPDATE_ROLES_WITH_ONE_PERSONS_POSITON'
declare @myPosition as varchar(50),
	@myHistID as varchar(50)
SELECT @myPosition = CO_POSITION FROM A_APPROVED_PEOPLE WHERE ID = @pID
if @myPosition is null
	begin
	print 'This person has no official position'
	goto fin
	end
declare @myPos as varchar(50)
SELECT @myPos = HISTORY_REF_ID FROM A_ROLES WHERE ID = @myPosition
print 'Now we need to get the current position role assignee'
UPDATE A_ROLE_ASSIGNEE SET STATUS = 'OLD' WHERE PERSON = @pID AND TYPE = 'POSITION'
INSERT INTO A_ROLE_ASSIGNEE 
		([ID], [ROLE], [PERSON], [STATUS], [SOURCE], [TYPE], [DRCM], [MODBY])
		VALUES 
		(newID(), @myPos, @pID, 'ACTIVE', 'A_SP_PEOPLE_UPDATE_ROLES_WITH_ONE_PERSONS_POSITON', 
		'POSITION', getDate(), @strNTLogin)

fin:
