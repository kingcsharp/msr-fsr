CREATE PROCEDURE dbo.A_NOTE_CREATE_FROM_TASK_COMMENT
@TASK_COMMENT_ID varchar(50)
AS
declare @writer varchar(50),@taskID varchar(50),
	@ALLOWED_TO_VIEW varchar(50),@writerCo varchar(50),
	@dateCreated datetime,@txt nvarchar(4000),
	@noteID varchar(50)

SELECT @writer = WRITER,@taskID = TASK_ID,@dateCreated = DATE_CREATED,
	@txt = COMMENT, @ALLOWED_TO_VIEW = ALLOWED_TO_VIEW, @writerCo = WRITER_COMPANY
	FROM A_TASK_COMMENT WHERE ID = @TASK_COMMENT_ID

print 'Got the data'
print 'Create the Note'
exec sp_GetUniqueID3 @noteID OUTPUT

INSERT INTO A_NOTES (ID,TXT,DRCM,MODBY,PEOPLE_SECURE)
	VALUES(@noteID,@txt,@dateCreated,@writer,1)

print 'Created teh note'
print 'Setting up people security'

INSERT INTO A_NOTE_PEOPLE_TO_VIEW (ID,NOTE_ID,PERSON_ID,DRCM,MODBY)
	values(newID(),@noteID,@writer,getdATE(),@writer)








