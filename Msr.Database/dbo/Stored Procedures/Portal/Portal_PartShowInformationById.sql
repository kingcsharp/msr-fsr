-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Portal_PartShowInformationById
	-- Add the parameters for the stored procedure here
	@ID varchar(50),
    @strNTLogin varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
  SELECT ID AS Id FROM A_V_PART_DATA_BY_APPROVED_DATA WHERE ID = @ID
END