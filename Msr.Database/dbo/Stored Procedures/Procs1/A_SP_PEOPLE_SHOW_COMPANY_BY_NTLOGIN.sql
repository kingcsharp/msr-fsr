





/*
 
STORED PROCEDURE CALLED IN

Module: people/searchPeople.asp
Module: parts/editPart.asp
Module: discussion/searchDiscussion.asp



*/



CREATE     PROCEDURE A_SP_PEOPLE_SHOW_COMPANY_BY_NTLOGIN
@ID nvarchar(50),
@strNTLogin nvarchar(50)
as
SELECT CO_ID,CO_NAME FROM A_V_CO_BY_NTLOGIN WHERE ID = @ID









