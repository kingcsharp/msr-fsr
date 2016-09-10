
CREATE  PROCEDURE dbo.A_SP_ACTUAL_PART_UPDATE_BY_GE_NANYA_NUM 
@GE_NUM varchar(2000),
@NANYA_NUM varchar(2000),
@SN varchar(2000)
AS
declare @cnt int,@res varchar(50),@combo varchar(500)
set @combo = @GE_NUM + ' / ' + @NANYA_NUM
SELECT @cnt = COUNT(ID) FROM A_ACTUAL_PARTS_HISTORY WHERE SERIAL = @combo
if @cnt = 1
	begin
	UPDATE A_ACTUAL_PARTS_HISTORY SET NICK_NAME = @SN WHERE SERIAL = @combo
	set @res = 'Update ' + @combo + ' SUCCESSFULLY'
	goto fin
	end
if @cnt = 0
	begin
	set @res = 'FAILED TO UPDATE (count = 0) ' + @combo
	goto fin
	end

if @cnt > 1
	begin
	set @res = 'FAILED TO UPDATE (TOO MANY) ' + @combo
	goto fin
	end

fin:
SELECT @res as RES	


