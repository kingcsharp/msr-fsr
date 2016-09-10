CREATE PROCEDURE dbo.A_SP_PART_GET_SUB_PART_LOCATION_LIST
@partID varchar(50),
@strNTLogin varchar(50)
AS
declare @pHistID varchar(50)
SELECT @pHistID = HISTORY_REF_ID FROM A_V_PARTS_APPROVED_DATA WHERE ID =@partID

create table #subPartList (PART_ID varchar(50),QTY float)
INSERT INTO #subPartList SELECT PART_ID,qTY FROM A_PARTS_SUB_PARTS WHERE PARENT = @pHistID
create table #locationList (LOC_ID varchar(50),MAX_MAKES float)

declare @curs2 cursor,@lID varchar(50),@mq float,@myMin float
declare @curs cursor,@pid varchar(50),@q float,@cnt int
set @curs = cursor for SELECT PART_ID,QTY FROM #subPartList
open @curs
fetch next from @curs  into @pID,@q
set @cnt = 0
while @@fetch_status = 0 
	begin
	if @cnt = 0
		begin
		INSERT INTO #locationList (LOC_ID,MAX_MAKES)
			SELECT LOCATION,isnull((sum(QTY)/@q),0) 
				FROM A_V_ACTUAL_PARTS_APPROVED_DATA 
				WHERE PART_ID = @pID AND AP_STATUS = 'ap_available' AND LOCATION IS NOT NULL
				GROUP BY LOCATION
		end
	else
		begin
			set @curs2 = cursor for SELECT LOC_ID,MAX_MAKES FROM #locationList
			open @curs2
			fetch next from @curs2  into @lID,@mq
			while @@fetch_status = 0
				begin 
				select @myMin = isNull((SELECT (SUM(QTY)/@q) FROM A_V_ACTUAL_PARTS_APPROVED_DATA 
					WHERE PART_ID = @pID AND LOCATION = @lID AND AP_STATUS = 'ap_available'
					GROUP BY LOCATION),0)
				print 'Found ' + isnull(convert(nvarchar(50),@myMin),'NULL') + ' items for part ID = ' + isNull(@pID,'NULL') + ' at loc = ' + isNull(@lID,'NULL')
				if @myMin > @mq
					set @myMin = @mq
				UPDATE #locationList SET MAX_MAKES = @myMin WHERE LOC_ID = @lID
				fetch next from @curs2  into @lID,@mq
				end
		end
	set @cnt = @cnt + 1
	fetch next from @curs into @pID,@q
	end	
deallocate @curs
SELECT vl.*,l.MAX_MAKES from
	#locationList l, A_V_LOCATIONS_APPROVED_DATA vl WHERE
	l.LOC_ID = vl.ID AND MAX_MAKES > 0


