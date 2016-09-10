
CREATE  procedure dbo.A_SP_ACTUAL_PART_ADD_PART_TO_PICK_LIST 
@parentID varchar(50),
@mergeIntoObjID varchar(50),
@childActualPartObjID varchar(50),
@PartID varchar(50),
@nickName varchar(50),
@strNTLogin varchar(50)
AS
print 'Adding this one to the pick list and merging it into the merge part.'
declare @childActualPart varchar(50)
SELECT @childActualPart = ROOT FROM A_OBJECTS WHERE ID = @childActualPartObjID

INSERT INTO A_ACTUAL_PARTS_PICK_LIST 
(ID,PARENT_ID,CHILD_ID,CHILD_LOCATION,CHILD_QTY,DATE_PICKED,MERGED_INTO,MERGED_INTO_NICK_NAME)
SELECT newID(),@parentID,ID,LOCATION,QTY,getDate(),@mergeIntoObjID,@nickName 
	FROM A_V_ACTUAL_PARTS_APPROVED_DATA WHERE ID = @childActualPart

declare @mID varchar(50),@cpID varchar(50)
if @mergeIntoObjID <> @childActualPartObjID
	begin
	print 'This part needs to be merged'
	SELECT @mID = ROOT FROM A_OBJECTS WHERE ID = @mergeIntoObjID
	SELECT @cpID = ROOT FROM A_OBJECTS WHERE ID = @childActualPartObjID
	exec A_SP_ACTUAL_PARTS_MERGE_FIRST_INTO_SECOND @cpID,@mID,@strNTLogin
	set @childActualPartObjID = @mergeIntoObjID
	end
else
	begin
	print 'This part is the merge part so we need not merge any'
	end

declare @pLoc varchar(50)
SELECT @pLoc = LOCATION FROM A_V_ACTUAL_PARTS_APPROVED_DATA WHERE ID= @parentID
UPDATE A_ACTUAL_PARTS_HISTORY 
	SET AP_STATUS = 'ap_installed', 
		PARENT_ID = @parentID,
		LOCATION = @pLoc 
	WHERE OBJECT_ID = @childActualPartObjID




