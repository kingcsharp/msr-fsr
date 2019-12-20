CREATE VIEW [dbo].[Portal_ProductionPlanningView]
	AS
SELECT        cr.Id, cr.Respresentative, cr.Company, cr.DivisionFab, cr.PartKitNo, cr.SubmittedDate, cr.Status, ap.FULL_NAME AS SubmittedBy, cr.ShortDescription
FROM            dbo.Portal_CustomerRequirement AS cr INNER JOIN
                         dbo.A_APPROVED_PEOPLE AS ap ON cr.SubmittedBy = ap.ID