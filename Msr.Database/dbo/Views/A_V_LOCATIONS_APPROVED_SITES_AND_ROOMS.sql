CREATE VIEW dbo.A_V_LOCATIONS_APPROVED_SITES_AND_ROOMS
AS
SELECT        dbo.A_LOCATIONS.ID, dbo.A_LOCATIONS.HISTORY_REF_ID, dbo.A_LOCATIONS_HISTORY.NAME, dbo.A_LOCATIONS_HISTORY.PARENT_LOCATION, 
                         dbo.A_LOCATIONS_HISTORY.PARENT_LOCATION_NAME, dbo.A_LOCATIONS_HISTORY.ADDRESS_1, dbo.A_LOCATIONS_HISTORY.ADDRESS_2, 
                         dbo.A_LOCATIONS_HISTORY.FULL_ADDRESS, dbo.A_LOCATIONS_HISTORY.CITY, dbo.A_LOCATIONS_HISTORY.STATE, dbo.A_LOCATIONS_HISTORY.COUNTRY, 
                         dbo.A_LOCATIONS_HISTORY.POSTAL_CODE, dbo.A_LOCATIONS_HISTORY.REGION, dbo.A_LOCATIONS_HISTORY.REGION_NAME, 
                         dbo.A_LOCATIONS_HISTORY.INTERNAL_ADDRESS, dbo.A_LOCATIONS_HISTORY.OBJECT_ID, dbo.A_LOCATIONS_HISTORY.PARENT_PATH, 
                         dbo.A_LOCATIONS_HISTORY.COMPLETE_NAME
FROM            dbo.A_LOCATIONS_HISTORY INNER JOIN
                         dbo.A_LOCATIONS ON dbo.A_LOCATIONS_HISTORY.ID = dbo.A_LOCATIONS.HISTORY_REF_ID
WHERE        (LEN(dbo.A_LOCATIONS_HISTORY.INTERNAL_ADDRESS) <= 4) AND (dbo.A_LOCATIONS.STATUS = 'APPROVED')
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'A_V_LOCATIONS_APPROVED_SITES_AND_ROOMS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "A_LOCATIONS_HISTORY"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 274
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "A_LOCATIONS"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 267
               Right = 215
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'A_V_LOCATIONS_APPROVED_SITES_AND_ROOMS';





