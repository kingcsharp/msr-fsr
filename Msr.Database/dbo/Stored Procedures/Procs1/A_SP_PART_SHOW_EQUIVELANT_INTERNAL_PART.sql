
CREATE  PROCEDURE dbo.A_SP_PART_SHOW_EQUIVELANT_INTERNAL_PART
	@strID nvarchar(50),
	@strNTLogin nvarchar(50)
as
SELECT e.EQUAL_PART_ID AS ID,
p.NAME 
+ isNull(' [' + p.COMPANY_PART_NUMBER + ']','')
+ isNull(' (' + p.ID + ')','')
+ isNull(' (' + p.ROOT_CO_NAME + '/' + p.COMPANY_NAME + ')','')



as NAME 
FROM A_PARTS_INTERNAL_EQUALS e,A_V_PART_DATA_BY_APPROVED_DATA p
WHERE e.PART_ID = @strID AND e.EQUAL_PART_ID = p.ID 

