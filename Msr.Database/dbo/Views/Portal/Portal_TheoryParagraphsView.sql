CREATE VIEW dbo.Portal_TheoryParagraphsView
AS
SELECT        ID AS Id, NAME AS Name, CREATING_CO_NAME AS CreatingCoName, ROOT AS SendId, ROOT AS Root, STATUS AS Status
FROM            dbo.A_O_THEORY
GO
