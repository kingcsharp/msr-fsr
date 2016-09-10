





CREATE   FUNCTION A_FN_NEEDS_HAS_DOCUMENT (@objID varchar(50))
RETURNS smallInt
AS
BEGIN
	DECLARE @tester varchar(50),
	@ret as smallint
	SELECT @tester = ID FROM 
	A_DOCUMENT_LINK l WHERE l.OBJECT_ID = @objID 
	if @tester is null
		begin
		set @ret =  0
		end
	else
		begin
		set @ret = 1
		end
return(@ret)
END





