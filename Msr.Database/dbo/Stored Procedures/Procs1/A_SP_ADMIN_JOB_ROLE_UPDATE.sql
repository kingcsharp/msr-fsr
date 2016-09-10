




CREATE        PROCEDURE dbo.A_SP_ADMIN_JOB_ROLE_UPDATE
@JOB varchar(50),
@coID varchar(50),
@role varchar(50),
@strNTLogin varchar(50)
AS
print 'Delete the old one'
DELETE FROM A_ADMIN_ROLE_JOBS WHERE JOB = @JOB AND CO_ID  = @coID
print 'insert the new one'
INSERT INTO A_ADMIN_ROLE_JOBS (ID,CO_ID,ROLE_ID,JOB,DRCM,MODBY)
	VALUES (newID(),@coID,@role,@JOB,getDate(),@strNTLogin)






