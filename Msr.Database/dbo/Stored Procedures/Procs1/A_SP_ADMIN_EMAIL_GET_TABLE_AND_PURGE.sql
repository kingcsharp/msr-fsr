

CREATE   PROCEDURE [dbo].[A_SP_ADMIN_EMAIL_GET_TABLE_AND_PURGE]
@strNTLogin varchar(50),@send varchar(50)
AS
print 'getting and purging the email list'
CREATE TABLE #TempItems	 (
	[ID] varchar(50),
	[EMAIL_ADDRESS] varchar(50) ,
	[BODY] varchar(8000) ,
	[DRCM] datetime,
	[MODBY] varchar(50) ,
	[PERSON_ID] varchar(50) ,
	[SUBJECT] nvarchar(500) 
)

INSERT INTO #TempItems SELECT ID,EMAIL_ADDRESS,BODY,DRCM,MODBY,PERSON_ID,SUBJECT FROM A_ADMIN_EMAIL_QUE
DELETE FROM A_ADMIN_EMAIL_QUE WHERE ID IN (SELECT ID FROM #TempItems)
if @send = 'True'
	SELECT * FROM #TempItems
else
	begin
	DELETE FROM #TempItems
	SELECT * FROM #TempItems
	end









