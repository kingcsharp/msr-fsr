

/*
stored procedure called: locations/viewHierarchy.asp    

*/

create          PROCEDURE A_SP_LOCATION_HIERARCHY_GET_CHILD_LOCATION
@ID varchar(50),
@strNTLogin varchar(50)
AS
SELECT ID, NAME 
FROM A_APPROVED_LOCATIONS 
WHERE PARENT_LOCATION = @ID

