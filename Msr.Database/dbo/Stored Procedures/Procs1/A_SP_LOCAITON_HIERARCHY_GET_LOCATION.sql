
/*
stored procedure called: location/viewHierarchyPeople.asp    

*/

CREATE           PROCEDURE A_SP_LOCAITON_HIERARCHY_GET_LOCATION
@ID varchar(50),
@strNTLogin varchar(50)
AS
declare @hasChild varchar(50)
SELECT top 1 @hasChild=ID  
FROM A_APPROVED_LOCATIONS
WHERE PARENT_LOCATION = @ID

SELECT ID, NAME, @hasChild as IS_A_CHILD_LOCATION
FROM A_APPROVED_LOCATIONS 
WHERE ID = @ID

