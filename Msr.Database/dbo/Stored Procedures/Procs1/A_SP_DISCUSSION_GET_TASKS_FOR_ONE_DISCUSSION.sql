
create       PROCEDURE A_SP_DISCUSSION_GET_TASKS_FOR_ONE_DISCUSSION
@discussionID nvarchar(50),
@strNTLogin varchar(50)
AS
print 'Getting tasks for one discussion'
SELECT *
FROM A_V_DISCUSSION_TASKS
WHERE @discussionID= DISCUSSION_ID
