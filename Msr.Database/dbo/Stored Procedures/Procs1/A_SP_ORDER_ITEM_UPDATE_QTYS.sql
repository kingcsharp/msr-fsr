






CREATE   PROCEDURE dbo.A_SP_ORDER_ITEM_UPDATE_QTYS
@ID varchar(50)
AS
declare @QTY_FILLED as float
declare @QTY as float
declare @QTY_NEEDS_FILLED as float
SELECT @QTY = QTY FROM A_ORDER_ITEMS WHERE ID = @ID
print @QTY
SELECT @QTY_FILLED = isNull(SUM(isNull(FILL_QTY,0)),0)
	FROM A_FILLS WHERE PURCH_ITEM_ID = @ID

print 'QTY = ' + isNull(convert(varchar(50),@QTY),'NULL')
print 'QTY_FILLED = ' + isNull(convert(varchar(50),@QTY_FILLED),'NULL')
set @QTY_NEEDS_FILLED = @QTY - @QTY_FILLED
print 'QTY_To Fill = ' + isNull(convert(varchar(50),@QTY_NEEDS_FILLED),'NULL')

--print 'QTY_FILLED = ' + isNull(convert(varchar(50),@QTY_FILLED),'NULL')




UPDATE A_ORDER_ITEMS SET
QTY_FILLED = @QTY_FILLED,
QTY_NEEDS_FILLING = @QTY_NEEDS_FILLED
WHERE ID = @ID
 

fin:
return 0
problem:
print 'THERE WAS A PROBLEM'
return 1







