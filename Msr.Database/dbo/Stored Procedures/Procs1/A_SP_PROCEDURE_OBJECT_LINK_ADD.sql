



CREATE     PROCEDURE A_SP_PROCEDURE_OBJECT_LINK_ADD
@newID nvarchar(50) OUTPUT,
@messages nvarchar(500) OUTPUT,
@ID nvarchar(50),
@PROCEDURE_OBJ_ID nvarchar(50),
@PROC_ID nvarchar(50),
@STEP_ID nvarchar(50),
@APPROVED_OBJECT_ID nvarchar(50),
@QTY nvarchar(50),
@QTY_TYPE nvarchar(50),
@RELATIONSHIP varchar(50),
@LABOR_ROLE varchar(50),
@strNTLogin nvarchar(50)
AS
-- MAKE SURE WE HAVE A procedure ID
if @PROC_ID is NULL
	begin
	print 'Proc ID was null'
	if not(@PROCEDURE_OBJ_ID is NULL)
		begin
		SELECT @PROC_ID = ID FROM A_PROCEDURES_HISTORY WHERE OBJECT_ID = @PROCEDURE_OBJ_ID
		end
	end
print 'The procedure ID = ' + @PROC_ID
--If it is new then go ahead and add it
if @ID is null
	begin
	exec sp_GetUniqueID3 @ID OUTPUT
	INSERT INTO A_PROCEDURE_OBJECT_LINK(ID) VALUES (@ID)
	end
--Go ahead and update it now
UPDATE A_PROCEDURE_OBJECT_LINK
SET 
	PROCEDURE_ID = @PROC_ID,
	STEP_ID = @STEP_ID,
	APPROVED_OBJECT_ID = @APPROVED_OBJECT_ID,
	QTY = @QTY,
	QTY_TYPE = @QTY_TYPE,
	RELATIONSHIP = @RELATIONSHIP,
	LABOR_ROLE = @LABOR_ROLE,
	DRCM = getDate(),
	MODBY = @strNTLogin
WHERE ID = @ID


if @RELATIONSHIP = 'LABOR_PROVIDE_TAKE_BACK' and @STEP_ID is not null
	begin
	declare @dur float
	SELECT @dur = DURATION FROM A_PROCEDURE_STEPS WHERE ID = @STEP_ID
	if @dur is null
		begin
		print 'The duration is null so we need to set it using the labor on this step'
		UPDATE A_PROCEDURE_STEPS SET DURATION = @QTY, DURATION_TYPE = @QTY_TYPE WHERE ID = @STEP_ID


		end


	end



