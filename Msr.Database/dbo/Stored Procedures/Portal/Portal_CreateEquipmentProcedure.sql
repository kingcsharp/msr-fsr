create procedure [dbo].[Portal_CreateEquipmentProcedure]

@newID varchar(50) OUTPUT,
@messages varchar(2000) OUTPUT,
@Id varchar(50),
@ObjectId varchar(50),
@ScanBarcode varchar(100),
@PrimaryLocationId varchar(100),
@SubLocationFirstId varchar(100),
@SubLocationSecondId varchar(100),
@DateTime datetime,
@Technician varchar(100),
@TroubleState bit,
@MaintenanceTask varchar(100),
@Comments varchar(4000),
@Status varchar(100),
@strNTLogin varchar(50)

AS BEGIN

DECLARE @myID AS NVARCHAR(50)

IF @Id is null
	BEGIN
		print 'This is a new location so inserting it now'
		exec sp_GetUniqueID3 @myID OUTPUT
		
		INSERT INTO Portal_EquipmentMaintenance (Id,ScanBarcode,PrimaryLocationId,SubLocationFirstId,SubLocationSecondId, DateTime, Technician, TroubleState, MaintenanceTask, Comments,Status,strNtLogin)
		VALUES (@myID,@ScanBarcode,@PrimaryLocationId,@SubLocationFirstId,@SubLocationSecondId,@DateTime,@Technician,@TroubleState,

		@MaintenanceTask,@Comments,@Status,@strNTLogin)
		SELECT @newID = Id FROM Portal_EquipmentMaintenance WHERE ID = @myID 
	END
ELSE
	BEGIN
		print 'This is an update to the existing location ' + @myID

		UPDATE Portal_EquipmentMaintenance 
		SET ScanBarcode = @ScanBarcode,
		PrimaryLocationId = @PrimaryLocationId,
		SubLocationFirstId = @SubLocationFirstId,
		SubLocationSecondId = @SubLocationSecondId,
		[DateTime] = @DateTime,
		Technician = @Technician,
		TroubleState = @TroubleState,
		MaintenanceTask = @MaintenanceTask,
		Comments = @Comments,
		Status = @Status,
		strNTLogin = @strNTLogin
		WHERE Id = @ObjectId
		
		SET @newID = @Id
	end
end