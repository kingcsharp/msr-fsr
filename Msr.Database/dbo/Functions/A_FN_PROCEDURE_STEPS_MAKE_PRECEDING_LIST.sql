

CREATE    FUNCTION dbo.A_FN_PROCEDURE_STEPS_MAKE_PRECEDING_LIST (@stepID varchar(50))
RETURNS nvarchar(4000)
as
BEGIN
	Declare @so as nvarchar(4000)
	Declare @ID as varchar(50),
	@TXT as varchar(4000),
	@STEP_ORDER as int
	Declare @cur Cursor
	set @cur = Cursor For SELECT s.ID,s.PRINT_ORDER,convert(nvarchar(10),s.STEP_TEXT) AS TXT
		FROM A_PROCEDURE_STEP_PRECEDING_STEPS p,
			A_PROCEDURE_STEPS s
		WHERE MY_STEP = @stepID AND p.PREV_STEP = s.ID
		ORDER BY s.PRINT_ORDER

	open @cur
	declare @cnt smallint
	set @cnt = 0
	Fetch Next from @cur Into @ID,@STEP_ORDER,@TXT
	while (@@fetch_status = 0) AND @cnt < 20
		Begin
		set @cnt = @cnt + 1
		set @TXT = dbo.xmlEncode(@TXT)
		set @TXT = replace(@TXT,CHAR(13),'')
		if CHARINDEX(@TXT,CHAR(13)) > 0
			begin
			set @TXT = LEFT(@TXT,CHARINDEX(@TXT,CHAR(13)))
			end
		set @TXT = left(@TXT,5) + '...'
		set @so = isNull(@so,'') + '<s><i>' + isNULL(@ID,'') + '</i><t>' + isNull(@ID,'') + '</t><o>' + convert(nvarchar(50),isNull(@STEP_ORDER,'')) + '</o></s>'
		Fetch Next from @cur Into @ID,@STEP_ORDER,@TXT
		End
	close @cur
	Deallocate @cur
	return(@so)
END







