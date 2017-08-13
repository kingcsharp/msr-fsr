CREATE VIEW dbo.Portal_PartsView
AS
SELECT        P.ID AS PartId, o.LOCKED_BY AS LockedBy, o.UNLOCKED_BY AS UnLockedBy, o.CREATED_BY AS CreatedBy, o.CREATE_DATE AS CreateDate, o.ROOT, o.REV_INFO AS RevInfo, o.CREATING_CO AS CreatingCo, o.STATUS, 
                         o.REV, o.WFS_ID AS WfsId, o.LOCKED_BY_NAME AS LockedByName, o.CREATING_CO_NAME AS CreatingCoName, o.APPROVAL_ACTIVITY AS ApprovalActivity, PH.OBJECT_ID AS ObjectId, PH.ID, PH.UNIT, PH.NAME, 
                         PH.PART_TYPE AS PartType, PH.TRACK_FROM_START AS TrackFromStart, PH.COMPANY, PH.UNIT_SHIPPING_WEIGHT AS UnitShippingWeight, PH.COMPANY_PART_NUMBER AS CompanyPartNumber, 
                         PH.SUPPLIER_SEE_INSTALL_BASE AS SupplierSeeInstallBase, PH.SUPPLIER_SEE_AVAILABILITY AS SupplierSeeAvailability, PH.CUSTOMER_SEE_AVAILABILITY AS CustomerSeeAvailability, c.NAME AS CompanyName, 
                         PT.NAME AS PartTypeName, PH.SPARE, PH.CONSUMABLE, PH.WEIGHT_TYPE AS WeightType, PH.CREATE_PROD AS CreateProd, PH.PRODUCT_TYPE AS ProductType, PH.PROC_VERB AS ProcVerb, 
                         PH.SUPPLIER_CO AS SupplierCo, PARENT_CO.NAME AS ParentCoName, dbo.A_UNIT_TYPES.NAME AS WeightTypeName
FROM            dbo.A_PARTS_HISTORY AS PH INNER JOIN
                         dbo.A_PARTS AS P ON P.PARTS_HISTORY_ID = PH.ID INNER JOIN
                         dbo.A_OBJECTS AS o ON PH.OBJECT_ID = o.ID INNER JOIN
                         dbo.A_V_COMPANIES_APPROVED_DATA_QUICK AS c ON PH.COMPANY = c.ID LEFT OUTER JOIN
                         dbo.A_V_COMPANIES_APPROVED_DATA_QUICK AS PARENT_CO ON c.PARENT = PARENT_CO.ID LEFT OUTER JOIN
                         dbo.A_UNIT_TYPES ON PH.WEIGHT_TYPE = dbo.A_UNIT_TYPES.ID LEFT OUTER JOIN
                         dbo.A_APPROVED_PART_TYPES AS PT ON PH.PART_TYPE = PT.ROOT

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Portal_PartsView';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N' = 0
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Portal_PartsView';


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
         Begin Table = "PH"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 290
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "o"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 244
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 270
               Left = 38
               Bottom = 400
               Right = 225
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PARENT_CO"
            Begin Extent = 
               Top = 270
               Left = 263
               Bottom = 400
               Right = 450
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "A_UNIT_TYPES"
            Begin Extent = 
               Top = 138
               Left = 282
               Bottom = 268
               Right = 452
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PT"
            Begin Extent = 
               Top = 402
               Left = 38
               Bottom = 532
               Right = 260
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "P"
            Begin Extent = 
               Top = 6
               Left = 328
               Bottom = 136
               Right = 521
            End
            DisplayFlags = 280
            TopColumn', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Portal_PartsView';

