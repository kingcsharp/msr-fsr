
CREATE PROCEDURE [dbo].[sp_CopyProdToStage]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--backup Prod
	exec msdb.dbo.rds_backup_database 
	@source_db_name='Answer2_Prod', 
	@s3_arn_to_backup_to='arn:aws:s3:::msrfsr/Answer2_Stage.bak', 
	@overwrite_S3_backup_file=1,
	@type='FULL';

	--drop stage and restore from Prod
	drop database Answer2_Stage;

	exec msdb.dbo.rds_restore_database 
	@restore_db_name='Answer2_Stage', 
	@s3_arn_to_restore_from='arn:aws:s3:::msrfsr/Answer2_Stage.bak';

	/*
		exec msdb.dbo.rds_task_status @db_name='Answer2_Prod'
		exec msdb.dbo.rds_task_status @db_name='Answer2_Stage'

		exec msdb..rds_task_status @task_id= 9	
	*/

END