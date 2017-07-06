CREATE PROCEDURE [dbo].[Portal_WorkItemImagesById]
	-- Add the parameters for the stored procedure here
	@fillId varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @ActualPartId varchar(50)
    -- Insert statements for procedure here
	SELECT @ActualPartId = ActualPartId FROM [dbo].[Portal_WorkOrders] where FillId = @fillId
	SELECT * FROM A_V_ACTUAL_PARTS_RELATED_FILES WHERE ACTUAL_PART_ID = @ActualPartId AND STATUS = 'ACTIVE' ORDER BY FILE_NAME,FILE_DESCRIPTION
END
