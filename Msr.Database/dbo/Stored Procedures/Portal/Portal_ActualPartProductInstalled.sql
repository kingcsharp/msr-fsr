-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Portal_ActualPartProductInstalled
	-- Add the parameters for the stored procedure here
		@ID varchar(50),
	    @strNTLogin varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @OutPutTable TABLE(Name varchar(500) NULL,Id varchar(500) NULL)

	INSERT INTO @OutPutTable EXEC A_SP_ACTUAL_PARTS_SHOW_PRODUCTS_INSTALLED @ID,@strNTLogin
	
	SELECT Id from @OutPutTable
END