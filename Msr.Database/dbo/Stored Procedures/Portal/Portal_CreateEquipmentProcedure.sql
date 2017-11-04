CREATE procedure [dbo].[Portal_CreateEquipmentProcedure]

@newID varchar(50) OUTPUT,
@messages varchar(2000) OUTPUT,
@Id varchar(50),
@ObjectId varchar(50),
@ScanBarcode varchar(100),
@ParentLocation varchar(100),
@SubLocationFirst varchar(100),
@SubLocationSecond varchar(100),
@DateTime datetime,
@RequestedById varchar(100),
@ApprovedById varchar(100),
@TroubleState bit,
@MaintenanceTask varchar(100),
@Comments varchar(4000),
@Status varchar(100),
@strNTLogin varchar(50),
@PMLastCompletedDate datetime,
@FrequencyField int

AS BEGIN

DECLARE @myID AS NVARCHAR(50)

IF @Id is null
	BEGIN
		print 'This is a new location so inserting it now'
		exec sp_GetUniqueID3 @myID OUTPUT
		
		INSERT INTO Portal_EquipmentMaintenance (Id,ScanBarcode,ParentLocation,SubLocationFirst,SubLocationSecond, DateTime, RequestedById,ApprovedById, TroubleState, MaintenanceTask, Comments,Status,strNtLogin,PMLastCompletedDate, FrequencyField)
		VALUES (@myID,@ScanBarcode,@ParentLocation,@SubLocationFirst,@SubLocationSecond,@DateTime,@RequestedById,@ApprovedById,@TroubleState,@MaintenanceTask,@Comments,@Status,@strNTLogin,@PMLastCompletedDate,@FrequencyField)
		SELECT @newID = Id FROM Portal_EquipmentMaintenance WHERE ID = @myID 
	END
ELSE
	BEGIN
		print 'This is an update to the existing location ' + @myID

		UPDATE Portal_EquipmentMaintenance 
		SET ScanBarcode = @ScanBarcode,
		ParentLocation = @ParentLocation,
		SubLocationFirst = @SubLocationFirst,
		SubLocationSecond = @SubLocationSecond,
		[DateTime] = @DateTime,
		RequestedById = @RequestedById,
		ApprovedById = @ApprovedById,
		TroubleState = @TroubleState,
		MaintenanceTask = @MaintenanceTask,
		Comments = @Comments,
		Status = @Status,
		strNTLogin = @strNTLogin,
		PMLastCompletedDate = @PMLastCompletedDate,
		FrequencyField =@FrequencyField
		WHERE Id = @ObjectId
		
		SET @newID = @Id
	end
end