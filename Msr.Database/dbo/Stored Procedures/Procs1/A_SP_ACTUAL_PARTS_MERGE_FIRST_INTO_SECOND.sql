


CREATE   PROCEDURE DBO.A_SP_ACTUAL_PARTS_MERGE_FIRST_INTO_SECOND
@from varchar(50),
@to varchar(50),
@strNTLogin varchar(50)
AS
print 'Merging ' + @from + ' into ' + @to
declare @fhistID varchar(50),@thistID varchar(50),@fQty float,@tQty float
SELECT @fhistID = HISTORY_REF_ID FROM A_ACTUAL_PARTS WHERE ID = @from
SELECT @thistID = HISTORY_REF_ID FROM A_ACTUAL_PARTS WHERE ID = @to
SELECT @fQty = QTY FROM A_ACTUAL_PARTS_HISTORY WHERE ID = @fhistID
SELECT @tQty = QTY FROM A_ACTUAL_PARTS_HISTORY WHERE ID = @thistID
print 'From Qty = ' + convert(varchar(50),@fQty)
print 'To Qty = ' + convert(varchar(50),@tQty)
UPDATE A_TASK_OBJECT_LINK SET OBJECT_ID = (SELECT ROOT FROM A_OBJECTS WHERE OBJ_ID = @thistID) WHERE OBJECT_ID = (SELECT ROOT FROM A_OBJECTS WHERE OBJ_ID = @fhistID)
UPDATE A_ACTUAL_PARTS_HISTORY SET QTY = @tQty + @fQty where ID = @thistID
declare @myNewQty float
set @myNewQty = @tQty + @fQty
exec A_SP_ACTUAL_PARTS_ADJUST_CHILD_QTYS @to,@tQty,@myNewQty
exec A_SP_ACTUAL_PART_DELETE_APPROVED_PART @from,@strNTLogin


