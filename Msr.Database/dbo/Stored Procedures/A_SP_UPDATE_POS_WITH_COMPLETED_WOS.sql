
-- =============================================
-- Author:		<Mike Harvey>
-- Create date: <7/5/2019>
-- Description:	<Called by SQL Agent to populate Table every dat>
-- =============================================
CREATE PROCEDURE [dbo].[A_SP_UPDATE_POS_WITH_COMPLETED_WOS] 

AS
BEGIN
	
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	TRUNCATE TABLE A_POS_WITH_COMPLETED_WOS
	print 'Deleted previous rows from A_POS_WITH_COMPLETED_WOS';
	INSERT INTO A_POS_WITH_COMPLETED_WOS
	EXEC A_SP_GET_POS_WITH_WOS
	print 'Update complete.'
END