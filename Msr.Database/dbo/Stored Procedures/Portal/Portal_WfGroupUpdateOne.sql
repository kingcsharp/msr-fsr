-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Portal_WfGroupUpdateOne]
	-- Add the parameters for the stored procedure here
	@NEWID nvarchar(50) OUTPUT,
	@NAME nvarchar(50),
	@ID nvarchar(50),
	@strNTLogin nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @OutPutTable TABLE(Id varchar(50) NULL)

	INSERT INTO @OutPutTable EXEC A_SP_WF_GROUP_UPDATE_ONE @NAME,@ID,@strNTLogin
	
	SELECT @NewId = Id from @OutPutTable
END