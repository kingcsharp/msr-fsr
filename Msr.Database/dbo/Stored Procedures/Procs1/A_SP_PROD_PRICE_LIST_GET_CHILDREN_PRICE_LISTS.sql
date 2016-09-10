CREATE PROCEDURE dbo.A_SP_PROD_PRICE_LIST_GET_CHILDREN_PRICE_LISTS
-- '5936','PARTS_PROVIDE_TAKE_BACK','5203' 
@PPL_ID varchar(50),
@rel varchar(50),
@strNTLogin varchar(50)
AS
print ' Getting all the child price lists for rel = ' + @rel
SELECT * FROM A_V_PROD_PRICE_LIST_CHILDREN_PROD_FILLS 
WHERE PARENT = @PPL_ID AND RELATIONSHIP = @rel








