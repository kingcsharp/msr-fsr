
-- =============================================
-- Author:		<Mike Harvey>
-- Create date: <8/5/2019>
-- Description:	<Called as EXEC by A_SP_UPDATE_POS_WITH_COMPLETED_WOS no params>
-- =============================================
CREATE PROCEDURE [dbo].[A_SP_GET_POS_WITH_WOS] 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT DISTINCT 
			[Extent1].[ReferencePo] AS [ReferencePo]
			FROM   [dbo].[Portal_WorkOrders] AS [Extent1]
			INNER JOIN [dbo].[Portal_PurchaseOrders] AS [Extent2] ON ([Extent1].[ReferencePo] = [Extent2].[ReferencePo]) OR (([Extent1].[ReferencePo] IS NULL) AND ([Extent2].[ReferencePo] IS NULL))
			INNER JOIN [dbo].[Portal_PeopleView] AS [Extent3] ON ([Extent1].[Purchaser] = [Extent3].[ObjectId]) OR (([Extent1].[Purchaser] IS NULL) AND ([Extent3].[ObjectId] IS NULL))
			WHERE (N'FINISHED' = [Extent1].[Status]) AND ([Extent1].[ReferencePo] IS NOT NULL)
		
END