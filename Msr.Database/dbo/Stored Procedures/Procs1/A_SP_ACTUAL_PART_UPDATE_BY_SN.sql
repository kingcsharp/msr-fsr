CREATE PROCEDURE dbo.A_SP_ACTUAL_PART_UPDATE_BY_SN 
@oldSN varchar(2000),
@newSN varchar(2000),
@nickName varchar(2000)
AS
declare @cnt int,@res varchar(50)
SELECT @cnt = COUNT(ID) FROM A_ACTUAL_PARTS_HISTORY WHERE SERIAL = @oldSN
if @cnt = 1
	begin
	UPDATE A_ACTUAL_PARTS_HISTORY SET SERIAL = @newSN,@nickName = @nickName WHERE SERIAL = @oldSN
	set @res = 'Update ' + @oldSN + ' SUCCESSFULLY'
	goto fin
	end
if @cnt = 0
	begin
	set @res = 'FAILED TO UPDATE (count = 0) ' + @oldSN
	goto fin
	end

if @cnt > 1
	begin
	set @res = 'FAILED TO UPDATE (TOO MANY) ' + @oldSN
	goto fin
	end

fin:
SELECT @res as RES	

