


CREATE VIEW [dbo].[A_V_ENGINEER_SCREEN_DATA_WIP_ONE_STEP_Simplified]
AS
SELECT DISTINCT 
                      purch.ID AS PURCHASE_ID, toi.PURCHASE_HIST_ID, toi.PURCHASE_ITEM_ID, customer.NAME AS CUSTOMER_NAME, ISNULL(t.ACTUAL_STOP_DATE, 
                      purchItem.DUE_DATE) AS DUE_DATE, purchItem.ORIG_DUE_DATE, t.PROCEDURE_ID AS PROC_ID, customer.ID AS CUST_ID, 
                      dbo.A_FN_DATE_TIME_ADD_USING_UNITS(purchItem.PROD_TIME_UNIT, purchItem.DUE_DATE, - purchItem.PROD_TIME) AS START_DATE, t.STATUS, 
                      t.REQUESTEE_ID, t.GROUP_REQUESTEE_ID, Product.NAME AS PRODUCT_NAME, t.ID, purch.CUST_PURCH_NUM, purchItem.ACCOUNT_ID, Account.REFERENCE_PO, 
                      [PROC].NAME AS PROC_NAME, purchItem.QTY, dbo.A_V_ACTUAL_PARTS_QUICK.NICK_NAME, dbo.A_V_ACTUAL_PARTS_QUICK.SERIAL, 
                      dbo.A_V_ACTUAL_PARTS_QUICK.ID AS ACTUAL_PART_ID, t.CUR_PLANNED_START_DATE AS ST_DATE, ISNULL(t.ACTUAL_STOP_DATE, purchItem.DUE_DATE) 
                      AS ACTUAL_STOP_DATE, t.ACTUAL_START_DATE, purchItem.MT_NUM, toi.FILL_ITEM_ID, purch.DATE_CREATED, dbo.A_FILLS.BATCH_PARENT, 
                      dbo.A_FILLS.BATCHED, dbo.A_FILLS.BATCH_FILL, dbo.A_FILLS.ID AS FILL_ID, dbo.A_FILLS.FILL_QTY, dbo.A_TASK_COMPLETION_STATS.PERC_COMPLETE, 
                      dbo.A_TASK_COMPLETION_STATS.TIME_COMPLETE, dbo.A_TASK_COMPLETION_STATS.TOTAL_TIME, dbo.A_TASK_COMPLETION_STATS.NUM_SUB_TASKS, 
                      dbo.A_TASK_COMPLETION_STATS.MY_TOT_HOURS, dbo.A_TASK_COMPLETION_STATS.MY_COMP_HOURS, 
                      dbo.A_TASK_COMPLETION_STATS.NUM_SUB_TASKS_COMPLETE,  (CASE WHEN CHARINDEX('<<nl/>>', A_TASK_COMPLETION_STATS.CUR_STEP_TEXT) 
                      > 0 THEN SUBSTRING(A_TASK_COMPLETION_STATS.CUR_STEP_TEXT, 0, CHARINDEX('<<nl/>>', A_TASK_COMPLETION_STATS.CUR_STEP_TEXT )) 
                      ELSE A_TASK_COMPLETION_STATS.CUR_STEP_TEXT END) AS CUR_STEP_TEXT, dbo.A_V_ACTUAL_PARTS_QUICK.OBJECT_ID AS ACT_PART_OBJ_ID, t.HAS_FILE,
					  supp.ID as SUPPLIER_ID, supp.NAME AS SUPPLIER_NAME
FROM         dbo.A_V_COMPANIES_APPROVED_DATA_QUICK AS customer INNER JOIN
                      dbo.A_V_PURCHASES_APPROVED_DATA AS purch ON customer.ID = purch.CUSTOMER_CO RIGHT OUTER JOIN
                      dbo.A_TASK_COMPLETION_STATS RIGHT OUTER JOIN
                      dbo.A_TASK_OBJECT_LINK AS T_OBJ INNER JOIN
                      dbo.A_V_PROCEDURES_DATA_QUICK AS [PROC] INNER JOIN
                      dbo.A_TASK_ORDER_INFORMATION AS toi INNER JOIN
                      dbo.A_TASKS AS t ON toi.TASK_ID = t.ID ON [PROC].ID = t.PROCEDURE_ID ON T_OBJ.TASK_ID = t.ID INNER JOIN
                      dbo.A_FILLS ON toi.FILL_ITEM_ID = dbo.A_FILLS.ID INNER JOIN
                      dbo.A_V_PRODUCTS_APPROVED_DATA AS Product INNER JOIN
                      dbo.A_ORDER_ITEMS AS purchItem ON Product.ID = purchItem.PRODUCT_ID ON dbo.A_FILLS.PURCH_ITEM_ID = purchItem.ID ON 
                      dbo.A_TASK_COMPLETION_STATS.TASK_ID = t.ID LEFT OUTER JOIN
                      dbo.A_V_ACCOUNTS_APPROVED_DATA_QUICK AS Account ON purchItem.ACCOUNT_ID = Account.ID ON 
                      purch.HISTORY_REF_ID = toi.PURCHASE_HIST_ID LEFT OUTER JOIN
                      dbo.A_V_ACTUAL_PARTS_QUICK ON T_OBJ.OBJECT_ID = dbo.A_V_ACTUAL_PARTS_QUICK.ID
					  RIGHT JOIN dbo.A_V_COMPANIES_APPROVED_DATA_QUICK supp on supp.ID = Account.SUPPLIER_CO
WHERE     (t.STATUS IN ('REQUESTED', 'ACCEPTED', 'CLOSED', 'FINISHED')) AND (toi.PURCHASE_ITEM_ID IS NOT NULL)




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
         Begin Table = "customer"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 114
               Right = 204
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "purch"
            Begin Extent = 
               Top = 6
               Left = 242
               Bottom = 114
               Right = 423
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "A_TASK_COMPLETION_STATS"
            Begin Extent = 
               Top = 6
               Left = 461
               Bottom = 114
               Right = 683
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "T_OBJ"
            Begin Extent = 
               Top = 6
               Left = 721
               Bottom = 114
               Right = 872
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PROC"
            Begin Extent = 
               Top = 6
               Left = 910
               Bottom = 114
               Right = 1076
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "toi"
            Begin Extent = 
               Top = 114
               Left = 38
               Bottom = 222
               Right = 238
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "t"
            Begin Extent = 
               Top = 114
               Left = 276
               Bottom = 222
               Right = 525
            End
            DisplayFlags = 280
   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'A_V_ENGINEER_SCREEN_DATA_WIP_ONE_STEP_Simplified';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'         TopColumn = 0
         End
         Begin Table = "A_FILLS"
            Begin Extent = 
               Top = 6
               Left = 1114
               Bottom = 114
               Right = 1275
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Product"
            Begin Extent = 
               Top = 114
               Left = 563
               Bottom = 222
               Right = 748
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "purchItem"
            Begin Extent = 
               Top = 114
               Left = 786
               Bottom = 222
               Right = 1003
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Account"
            Begin Extent = 
               Top = 114
               Left = 1041
               Bottom = 222
               Right = 1266
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "A_V_ACTUAL_PARTS_QUICK"
            Begin Extent = 
               Top = 222
               Left = 38
               Bottom = 330
               Right = 231
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'A_V_ENGINEER_SCREEN_DATA_WIP_ONE_STEP_Simplified';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'A_V_ENGINEER_SCREEN_DATA_WIP_ONE_STEP_Simplified';

