

CREATE              PROCEDURE A_SP_PROD_PRICE_LIST_FINISH_APPROVAL_WF 
@myID nvarchar(50),
@objID nvarchar(50),
@strNTLogin nvarchar(50)
as
print 'Finishing the Prod Price List Approval WF'
--We need to make sure that we got this part in the A_PROD_PRICE_LIST TAble
declare @myRoot as nvarchar(50) --get the root which is the ID of A_PROD_PRICE_LIST
declare @ID as nvarchar(50) --get my ID  in the A_PROD_PRICE_LIST_HISTORY table
SELECT @myRoot = ROOT,@ID = OBJ_ID FROM A_OBJECTS WHERE ID = @objID
declare @tester as nvarchar(50) --Test to see if it is in the table
SELECT @tester = ID FROM A_PROD_PRICE_LIST WHERE ID = @myRoot --it is not so add it
print 'got the data i needed'
if @tester is Null
	begin
		print 'tester was null so i need to go ahead and make a record'
		INSERT INTO A_PROD_PRICE_LIST(ID,HISTORY_REF_ID,DRCM,MODBY) VALUES (@myRoot,@ID,getDate(),@strNTLogin)
	end
--so now make sure that the value in A_PROD_PRICE_LIST is Approved
declare @curID as nvarchar(50)
SELECT @curID = OBJ_ID FROM A_OBJECTS WHERE ROOT = @myROOT AND STATUS IN
('APPROVED','APPROVED_BUT_REVISING','APPROVED_BUT_DELETING')

if @curID is null UPDATE A_PROD_PRICE_LIST SET STATUS = 'DELETED' WHERE ID = @myRoot
else UPDATE A_PROD_PRICE_LIST SET STATUS = 'APPROVED' WHERE ID = @myRoot


exec A_SP_UPDATE_TABLE_FIELD @curID,@myRoot,'A_PROD_PRICE_LIST','HISTORY_REF_ID'

exec A_SP_PROD_PRICE_LIST_SET_UP_AUTO_ACCOUNT @myRoot,@strNTLogin









