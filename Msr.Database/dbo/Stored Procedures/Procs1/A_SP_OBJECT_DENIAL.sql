


CREATE  PROCEDURE A_SP_OBJECT_DENIAL
@OBJ_ID nvarchar(50),
@strNTLogin nvarchar(50)

AS
print 'Entering A_SP_OBJECT_DENIAL'
print 'We need to know who to deny this back to'
declare @starter as nvarchar(50)
declare @finalStat as nvarchar(50)
SELECT @starter = w.STARTED_BY,@finalStat = w.STATUS_ON_COMPLETION 
FROM A_WORKFLOWS_STARTED w,A_OBJECTS o
WHERE w.ID = o.WFS_ID AND o.ID = @OBJ_ID
print 'The Starter is ' + isNull(@starter,'NULL')
print 'The the Stat on Completion is ' + isNull(@finalStat,'NULL')
declare @fullName as nvarchar(50)
SELECT @fullName = FULL_NAME FROM A_PEOPLE P, A_PEOPLE_HISTORY H WHERE P.HISTORY_REF_ID = H.ID AND P.ID = @starter
--Now depending on the stat on completion we need to do different things
if @finalStat = 'APPROVED'
	begin
		print 'This was an approval so we need to check it back to the submitter'
		UPDATE A_OBJECTS SET 
			STATUS = 'DENIED',
			LOCKED_BY = @starter,
			LOCKED_BY_NAME = @fullName
		WHERE ID = @OBJ_ID
	end
if @finalStat = 'DELETED'
	begin
		print 'This was a deletion so we need to just set it back to approved and let it be'
		UPDATE A_OBJECTS SET 
			STATUS = 'APPROVED' 
		WHERE ID = @OBJ_ID
	end








