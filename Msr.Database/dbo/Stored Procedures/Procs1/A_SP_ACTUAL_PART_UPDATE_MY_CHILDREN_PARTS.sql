
CREATE  PROCEDURE DBO.A_SP_ACTUAL_PART_UPDATE_MY_CHILDREN_PARTS
@ID varchar(50)
AS
declare @histRefID varchar(50),
@name varchar(50),
@modby varchar(50),
@oStat varchar(50),
@apStat varchar(50),
@locID varchar(50),
@objID varchar(50)
CREATE TABLE #TempIs	(IT varchar(50),HISTORY_REF_ID varchar(50))
INSERT INTO #TempIs(IT,HISTORY_REF_ID) SELECT ID,HISTORY_REF_ID FROM A_V_ACTUAL_PARTS_APPROVED_DATA WHERE PARENT_ID = @ID

print 'In here'


SELECT @histRefID = HISTORY_REF_ID FROM A_ACTUAL_PARTS WHERE ID = @ID
print 'The history ID = ' + isNULL(@histRefID,'NULL')
SELECT @apStat = AP_STATUS, @locID = LOCATION, @objID = OBJECT_ID 
	FROM A_ACTUAL_PARTS_HISTORY 
	WHERE ID = @histRefID
SELECT @oStat = STATUS FROM A_OBJECTS WHERE ID = @objID
	if @oStat = 'APPROVED'
		begin
		print 'This is an approved one, so I am going to update all the children'
		declare @tester varchar (50)
		SELECT @tester = ID FROM A_V_ACTUAL_PARTS_APPROVED_DATA WHERE PARENT_ID = @ID
		if @tester is null goto fin
		print 'making a cursor to go through the childs'
		Declare @it varchar(50),@hist varchar(50)
		Declare @curs Cursor
		set @curs = Cursor For SELECT * FROM #TempIs
		open @curs
		Fetch Next from @curs Into @it,@hist
		while (@@fetch_status = 0)
		Begin
			print 'Updating Child = ' + @hist
			UPDATE A_ACTUAL_PARTS_HISTORY SET LOCATION = @locID WHERE ID = @hist
			EXEC A_SP_ACTUAL_PART_UPDATE_MY_CHILDREN_PARTS @it
			Fetch Next from @curs Into @it,@hist
		End
		close @curs
		Deallocate @curs		
		end


return(0)
fin:
print 'Fin'
return(0)
