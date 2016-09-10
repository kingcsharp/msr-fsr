
/*
STORED PROCEDURE CALLED IN meetings/saveAgendaMeeting.asp
*/
CREATE                    PROCEDURE A_SP_MEETING_AGENDA_ITEM_UPDATE_ONE_ITEM
@newID varchar(50) OUTPUT,
@msg varchar(1000) OUTPUT,
@agendaID varchar(50),
@ROOT varchar(50),
@TEXT varchar(4000),
@RELATED_ITEM varchar(50),
@ITEM real,
@FACILITATOR varchar(50),
@START_DATE varchar(50),
@DURATION int,
@strNTLogin varchar(50)
AS
--declare @newID as nvarchar(50)
if @agendaID is null
begin
	exec SP_GETUNIQUEID3 @newID OUTPUT 
	INSERT INTO A_MEETING_AGENDA_ITEMS ([ID],[ROOT],[MODBY],[DRCM])
	VALUES
	(@newID, @ROOT,@strNTLogin, getDate())
        set @agendaID=@newID

end
	print 'updating item'
	UPDATE A_MEETING_AGENDA_ITEMS 
		set TEXT = @TEXT,
		ITEM = @ITEM,
		RELATED_ITEM = @RELATED_ITEM,
		FACILITATOR = @FACILITATOR,
		DURATION = @DURATION,
		DRCM = getDate(),
		MODBY = @strNTLogin
	WHERE ID = @agendaID
exec A_SP_MEETING_ORDER_AGENDA_ITEMS @ROOT, @strNTLogin    

