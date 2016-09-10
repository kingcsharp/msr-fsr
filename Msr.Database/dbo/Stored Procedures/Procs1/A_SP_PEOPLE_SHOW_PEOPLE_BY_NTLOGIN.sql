



/*
 
STORED PROCEDURE CALLED IN

Module: people/searchPeople.asp
Module: discussion/searchDiscussion.asp

*/




CREATE      PROCEDURE A_SP_PEOPLE_SHOW_PEOPLE_BY_NTLOGIN
@ID nvarchar(50),
@strNTLogin nvarchar(50)
as
SELECT P_ID,P_NAME FROM A_V_PEOPLE_BY_NTLOGIN WHERE P_ID = @ID










