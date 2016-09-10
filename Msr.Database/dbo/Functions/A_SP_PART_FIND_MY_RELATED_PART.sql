CREATE function dbo.A_SP_PART_FIND_MY_RELATED_PART(
@supPartNum varchar(50),
@strNTLogin varchar(50))
RETURNS varchar(50)
AS
BEGIN
declare @myCo varchar(50),@phID varchar(50)
SELECT @myCo = ROOT_COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @strNTLogin
declare @myPartID varchar(50)
SELECT @myPartID = p.ID FROM
A_O_PARTS_HISTORY p, A_PARTS_EXTERNAL_EQUALS e
WHERE p.ID = e.PART_ID AND
EQUAL_PART_ID = @supPartNum AND
p.CREATING_CO = @myCo AND
p.STATUS LIKE 'APPROVED%'

if @myPartID is Null
	begin
	select @phID = PARTS_HISTORY_ID FROM A_PARTS WHERE ID = @supPartNum
	SELECT @myPartID = p.ID FROM
	A_O_PARTS_HISTORY p, A_PARTS_EXTERNAL_EQUALS e
	WHERE p.ROOT = e.EQUAL_PART_ID AND
	e.PART_ID = @phID AND
	p.CREATING_CO = @myCo AND
	p.STATUS LIKE 'APPROVED%'
	end



return(@myPartID)
END

