










/*
STORED PROCEDURE CALLED IN projects/processProject.asp
*/


CREATE          PROCEDURE A_SP_PROJECT_DELETE_ONE_PROJECT
@projectID nvarchar(50),
@strNTLogin nvarchar(50)
AS
print 'Deletions only work for copies. The only person that can delete is the initiator of the project.'

declare @initiator varchar(50)

SELECT @initiator = INITIATOR
FROM A_PROJECTS
WHERE ID = @projectID AND
INITIATOR = @strNTLogin 

if @initiator is not null 
	begin
		print 'i am the initator so deleting my copy'
		DELETE FROM A_PROJECTS WHERE 
		ID = @projectID
	end 

















