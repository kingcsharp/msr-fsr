


/*
STORED PROCEDURE CALLED IN meetings/editMeeting.asp

*/

create     PROCEDURE A_SP_MEETING_GET_PROJECT_MEMBERS
@projectID varchar(50),
@strNTLogin nvarchar(50)
AS
CREATE TABLE #TmemebersWithLeader (
		P_ID varchar(50),	
		FULL_NAME varchar(50))

print 'inserting leader'

INSERT INTO #TmemebersWithLeader 
SELECT LEADER, LEADER_NAME FROM A_V_PROJECT_DATA WHERE ID = @projectID

print 'Getting regular members'

INSERT INTO #TmemebersWithLeader 
SELECT MEMBER_ID, MEMBER_NAME FROM A_V_PROJECT_MEMBER_DATA WHERE PROJECT_ID =@projectID

SELECT * FROM #TmemebersWithLeader ORDER BY FULL_NAME


