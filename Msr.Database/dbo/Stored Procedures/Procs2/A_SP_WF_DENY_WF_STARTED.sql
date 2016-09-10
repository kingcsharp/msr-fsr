






CREATE   PROCEDURE A_SP_WF_DENY_WF_STARTED
@myWFS as nvarchar(50),
@myGS as nvarchar(50),
@denier as nvarchar(50),
@reason as nvarchar(4000),
@strNTLogin as nvarchar(50)
as
print 'Deying a WF'
print 'WFS_ID = ' + @myWFS
print 'Denier = ' + @denier
print 'Reason = ' + @reason
UPDATE A_WORKFLOWS_STARTED SET
DENIAL_REASON = @reason,
FINISHED = 1,
FINISHED_DATE = getDate(),
DRCM = getDate(),
MODBY = @strNTLogin
WHERE ID = @myWFS

--Now We Need to Deny the Object
declare @objID as nvarchar(50)
SELECT @objID = ID FROM A_OBJECTS WHERE WFS_ID = @myWFS
exec A_SP_OBJECT_DENIAL @objID,@strNTLogin





