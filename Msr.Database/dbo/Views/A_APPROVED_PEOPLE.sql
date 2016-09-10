CREATE VIEW dbo.A_APPROVED_PEOPLE
AS
SELECT     P.ID, PH.LOGIN, PH.NAME, PH.PASSWORD, PH.BOSS, PH.SOURCE, PH.LAST_NAME, PH.MIDDLE_NAME, PH.NICK_NAME, PH.LANG, PH.HIRE_DATE, PH.DRCM, 
                      PH.MODBY, PH.OBJECT_ID, PH.COMPANY, PH.TIME_ZONE, PH.FULL_NAME, PH.SYSTEM_STATUS, PH.CO_POSITION, PH.ROOT_COMPANY, 
                      ROOT_CO.NAME AS ROOT_CO_NAME, CO.NAME AS CO_NAME, dbo.A_TIME_ZONES.G_DIFF, PH.IS_HEAD, P.STATUS, PH.TOOL_BOX, PH.INFO_BOX, 
                      PH.ADV_SEARCH, PH.COLOR_KEY, PH.SCREEN_TYPE, PH.CHANGE_PASS, dbo.A_V_ROLES_APPROVED_DATA_QUICK.NAME AS POSITION_NAME, 
                      P.HISTORY_REF_ID
FROM         dbo.A_PEOPLE AS P INNER JOIN
                      dbo.A_PEOPLE_HISTORY AS PH ON P.HISTORY_REF_ID = PH.ID LEFT OUTER JOIN
                      dbo.A_V_ROLES_APPROVED_DATA_QUICK ON PH.CO_POSITION = dbo.A_V_ROLES_APPROVED_DATA_QUICK.ID LEFT OUTER JOIN
                      dbo.A_TIME_ZONES ON PH.TIME_ZONE = dbo.A_TIME_ZONES.ID LEFT OUTER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA AS CO ON PH.COMPANY = CO.ID LEFT OUTER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA AS ROOT_CO ON PH.ROOT_COMPANY = ROOT_CO.ID
WHERE     (P.STATUS = 'APPROVED')

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
         Begin Table = "P"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 132
               Right = 204
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PH"
            Begin Extent = 
               Top = 6
               Left = 242
               Bottom = 114
               Right = 406
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "A_V_ROLES_APPROVED_DATA_QUICK"
            Begin Extent = 
               Top = 114
               Left = 38
               Bottom = 222
               Right = 204
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "A_TIME_ZONES"
            Begin Extent = 
               Top = 114
               Left = 242
               Bottom = 222
               Right = 393
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CO"
            Begin Extent = 
               Top = 222
               Left = 38
               Bottom = 330
               Right = 204
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ROOT_CO"
            Begin Extent = 
               Top = 222
               Left = 242
               Bottom = 330
               Right = 408
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
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'A_APPROVED_PEOPLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'     Alias = 900
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'A_APPROVED_PEOPLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'A_APPROVED_PEOPLE';

