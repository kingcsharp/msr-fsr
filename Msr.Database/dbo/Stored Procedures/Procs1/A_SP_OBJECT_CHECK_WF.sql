


CREATE  PROCEDURE A_SP_OBJECT_CHECK_WF
@objID nvarchar(50),
@strNTLogin nvarchar(50)
as
declare @myWFSID as nvarchar(50)
SELECT @myWFSID = WFS_ID FROM A_OBJECTS WHERE ID = @objID
print 'The object ID = ' + @objID
print 'Its wf started is ' + @myWFSID
exec A_SP_WORKFLOW_STARTED_UPDATE @myWFSID,@strNTLogin



