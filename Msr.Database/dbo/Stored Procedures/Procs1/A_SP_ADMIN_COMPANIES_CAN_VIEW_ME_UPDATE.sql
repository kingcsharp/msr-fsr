



CREATE        PROCEDURE dbo.A_SP_ADMIN_COMPANIES_CAN_VIEW_ME_UPDATE
@coID nvarchar(50),
@list varchar(50),
@strNTLogin varchar(50)
AS
print 'Delete the old one'
DELETE FROM A_COMPANIES_THAT_CAN_VIEW_ME WHERE ME = @coID
CREATE TABLE #TempItems	(IT varchar(50))
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @list,', '
INSERT INTO A_COMPANIES_THAT_CAN_VIEW_ME (ID,ME,CO_CAN_SEE_ME,DRCM,MODBY)
	SELECT newID(),@coID,ltrim(IT),getDate(),@strNTLogin FROM #TempItems







