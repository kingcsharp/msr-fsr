





CREATE  FUNCTION dbo.A_FN_PROD_PROCE_LIST_FIGURE_CHILD_TOTAL_COST
(@REL varchar(50),
@DUR REAL,
@DUR_TYPE varchar(50),
@QTY REAL,
@QTY_TYPE varchar(50),
@UNIT_PRICE REAL
)
RETURNS REAL
AS
BEGIN
declare @tot as REAL
if @REL = 'PARTS_PROVIDE_TAKE_BACK'
	begin
		set @tot = @DUR * @UNIT_PRICE
	end
if @REL = 'PARTS_PROVIDE_STAY' OR @REL = 'PARTS_PROVIDE_CONSUMED'
	begin
		set @tot = @QTY * @UNIT_PRICE
	end
return(@tot)




end







