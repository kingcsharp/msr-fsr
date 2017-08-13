CREATE VIEW dbo.Portal_ActualPartsView
AS
SELECT        PH.ID, PH.NICK_NAME AS NickName, AP.SYS_NAME AS SysName, PH.SERIAL, PH.PARENT_NAME AS ParentName, PH.LOCATION, PH.OBJECT_ID AS ObjectId, PH.MERGABLE, PH.PARENT_ID AS ParentId, 
                         PH.PART_ID AS PartId, PH.QTY, PH.CUR_OWNER AS CurOwner, PH.ASSEMBLY_WT AS AssemblyWT, PH.AP_STATUS AS ApStatus, PH.ROOT_ID AS RootId, PH.ROOT_STATUS AS RootStatus, PH.PART_TYPE AS PartType, 
                         PH.PART_TYPE_NAME AS PartTypeName, PH.PART_DESC AS PartDesc, PH.UNIT, PH.SUPPLIER_SEE_INSTALL_BASE AS SupplierSeeInstallBase, PH.SUPPLIER_SEE_AVAILABILITY AS SupplierSeeAvaliability, 
                         PH.CUSTOMER_SEE_AVAILABILITY AS CustomerSeeAvailability, PH.LOCATION_OBJECT_ID AS LocationObjectId, PH.COMPANY_PART_NUMBER AS CompanyPartNumber, PH.LOCKED_BY AS LockedBy, 
                         PH.UNLOCKED_BY AS UnlockedBy, PH.CREATED_BY AS CreatedBy, PH.CREATE_DATE AS CreateDate, PH.ROOT, PH.REV_INFO AS RevInfo, PH.CREATING_CO AS Creating, PH.STATUS, PH.REV, PH.WFS_ID AS WfsId, 
                         PH.LOCKED_BY_NAME AS LockedByName, PH.CREATING_CO_NAME AS CreatingCoName, PH.DRCM, PH.MODBY, PH.APPROVAL_ACTIVITY AS ApprovalActivity, PH.APPROVAL_DATE AS ApprovalDate, 
                         PH.CURRENT_OWNER_NAME AS CurrentOwnerName, PH.HAS_CHILD AS HasChild, PH.RESPONSIBLE_PERSON AS ResponsibleName, PH.RESP_PERSON_FULL_NAME AS RespPersonFullName, 
                         PH.LOCATION_NAME AS LocationName, PH.OBJECT_ID AS ObjId
FROM            dbo.A_O_ACTUAL_PARTS_HISTORY AS PH LEFT OUTER JOIN
                         dbo.A_V_ACTUAL_PARTS_APPROVED_DATA_QUICK AS AP ON PH.ID = AP.HISTORY_REF_ID
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Portal_ActualPartsView';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'gin ColumnWidths = 11
         Column = 2865
         Alias = 1440
         Table = 1425
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Portal_ActualPartsView';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[26] 4[35] 2[16] 3) )"
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
         Begin Table = "AP"
            Begin Extent = 
               Top = 6
               Left = 328
               Bottom = 136
               Right = 538
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
      Begin ColumnWidths = 47
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
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
      Be', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Portal_ActualPartsView';

