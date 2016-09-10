

CREATE   PROCEDURE DBO.A_SP_INVOICE_GET_DATE 
@invDate dateTime OUTPUT,
@firstInvoiceDate dateTime,
@period varchar(50),
@periodNumber int
AS
set @periodNumber = isNull(@periodNumber,1)
set @period = isNull(@period,'MONTH')
declare @curDate datetime,@lastDate datetime,@intCnt int
set @curDate = isNull(@firstInvoiceDate,'1/1/2006')
print 'Period = ' + @period
print 'Cur Date = '
print @curDate
set @lastDate = @curDate
set @intCnt = 1
while @curDate < getDate()
	begin
	set @lastDate = @curDate
	select @curDate = 
		case when @period = 'DAYS' then dateAdd(dd,(@periodNumber * @intCnt),@firstInvoiceDate)
		when @period = 'MONTH' then dateAdd(mm,(@periodNumber * @intCnt),@firstInvoiceDate)
		else dateAdd(mm,(1 * @intCnt),@firstInvoiceDate)
	end
	print 'Now looking at Date = ' + convert(nvarchar(50),@curDate)
	set @intCnt = @intCnt + 1
	end


set @invDate = @curDate

print 'Returning a date of '
print @invDate
