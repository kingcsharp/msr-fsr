








CREATE                               procedure A_SP_MENUS_CREATE_MENUS
as
DELETE FROM A_MENUS



--Projects
exec A_SP_MENUS_CREATE_MENU_ITEM 'Projects','Projects','asp/projects/searchproject.asp?firstTime=true'
exec A_SP_MENUS_CREATE_MENU_ITEM 'Discussions','Projects','asp/discussions/searchDiscussion.asp?firstTime=true'
exec A_SP_MENUS_CREATE_MENU_ITEM 'Surveys','Projects','asp/surveys/searchSurvey.asp?firstTime=true'
exec A_SP_MENUS_CREATE_MENU_ITEM 'Meetings','Projects','asp/meetings/searchMeeting.asp?firstTime=true'
exec A_SP_MENUS_CREATE_MENU_ITEM 'Action Plans','Projects','asp/ActualTasks/searchTasks.asp?firstTime=true'
exec A_SP_MENUS_CREATE_MENU_ITEM 'Messages','Projects','asp/Messages/searchMessages.asp?firstTime=true'

--DNR
exec A_SP_MENUS_CREATE_MENU_ITEM 'Diagnose And Repair','DNR','asp/actualTasks/searchTasks.asp?searchMethod=DNR'
--TimeLabor
exec A_SP_MENUS_CREATE_MENU_ITEM 'Actual Usage of Labor','TimeLabor','asp/laborUsage/searchLaborUsage.asp?firstTime=true'
exec A_SP_MENUS_CREATE_MENU_ITEM 'Actual Usage of Parts','TimeLabor','asp/partsUsage/searchPartsUsage.asp?firstTime=true'
exec A_SP_MENUS_CREATE_MENU_ITEM 'Time Cards','TimeLabor','asp/timeCards/searchTimeCards.asp?firstTime=true'
--Service Calls
exec A_SP_MENUS_CREATE_MENU_ITEM 'Weekly Service Call Reports','ServiceCalls','asp/serviceCalls/searchServiceCall2.asp?firstTime=true'
exec A_SP_MENUS_CREATE_MENU_ITEM 'Work Types','ServiceCalls','asp/serviceCallWorkTypes/searchWorkTypes.asp?firstTime=true'
exec A_SP_MENUS_CREATE_MENU_ITEM 'Time Report Data','ServiceCalls','asp/serviceCalls/dayTracker.asp'
--exec A_SP_MENUS_CREATE_MENU_ITEM 'Assign Accounts Recievable Role','ServiceCalls','asp/serviceCallAccRecievable/assignAccRecievableRole.asp'
--Dashboards
exec A_SP_MENUS_CREATE_MENU_ITEM 'Monitor Search','Dashboards','asp/Monitors/searchMonitors.asp?firstTime=true'
exec A_SP_MENUS_CREATE_MENU_ITEM 'Dashboards','Dashboards','asp/Dashboards/searchDashboards.asp?firstTime=true'
exec A_SP_MENUS_CREATE_MENU_ITEM 'Performance Appraisals','Dashboards','asp/Dashboards/searchPerfAppraisals.asp?firstTime=true'
exec A_SP_MENUS_CREATE_MENU_ITEM 'Equipment Experience','Dashboards','asp/equipmentExperience/search.asp?firstTime=true'
exec A_SP_MENUS_CREATE_MENU_ITEM 'Excel Reports','Dashboards','asp/excelReports/search.asp?firstTime=true'
--Favorites
--exec A_SP_MENUS_CREATE_MENU_ITEM 'Favorites','Favorites','asp/favorites/searchFavoriteGroups.asp?firstTime=true'
--Projects
--exec A_SP_MENUS_CREATE_MENU_ITEM 'Projects','Projects','asp/projects/searchproject.asp'
--Actions
--exec A_SP_MENUS_CREATE_MENU_ITEM 'Action Plans','Actions','asp/ActualTasks/preSearchTasks.asp'
--exec A_SP_MENUS_CREATE_MENU_ITEM 'Diagnose And Repair','Actions','asp/Actions/searchDiagNRep.asp?firstTime=true'
--Discussions
--exec A_SP_MENUS_CREATE_MENU_ITEM 'Meetings','Discussions','asp/meetings/presearchMeeting.asp'
--exec A_SP_MENUS_CREATE_MENU_ITEM 'Discussions','Discussions','asp/discussions/preSearchDiscussions.asp'
--exec A_SP_MENUS_CREATE_MENU_ITEM 'Surveys','Discussions','asp/surveys/preSearchSurvey.asp'
--Parts
exec A_SP_MENUS_CREATE_MENU_ITEM 'Parts','Parts','asp/Parts/searchParts.asp?firstTime=true'
exec A_SP_MENUS_CREATE_MENU_ITEM 'Part Types','Parts','asp/PartTypes/searchPartTypes.asp?firstTime=true'
exec A_SP_MENUS_CREATE_MENU_ITEM 'Actual Parts','Parts','asp/actualParts/searchActualParts.asp?firstTime=true&STATUS=CREATING, DENIED, APPROVED, APPROVED_BUT_REVISING, APPROVED_BUT_DELETING'
--Counters
exec A_SP_MENUS_CREATE_MENU_ITEM 'Counters','Counters','asp/Counters/searchCounters.asp?firstTime=true'
--Thesaurus
exec A_SP_MENUS_CREATE_MENU_ITEM 'Thesaurus','Thesaurus','asp/Thesaurus/searchThesaurus.asp?firstTime=true'
--Dashboards
--exec A_SP_MENUS_CREATE_MENU_ITEM 'Dashboards','Dashboards','asp/Dashboards/searchDashboards.asp?firstTime=true'
--exec A_SP_MENUS_CREATE_MENU_ITEM 'Performance Appraisals','Dashboards','asp/Dashboards/searchPerfAppraisals.asp?firstTime=true'
--Service Calls
--exec A_SP_MENUS_CREATE_MENU_ITEM 'Weekly Service Call Reports','ServiceCalls','asp/serviceCalls/searchServiceCall.asp?firstTime=true&START_DATE=ALL&STATUS=ALL&reasonType=ALL'
--exec A_SP_MENUS_CREATE_MENU_ITEM 'Work Types','ServiceCalls','asp/serviceCallWorkTypes/searchWorkTypes.asp?firstTime=true' --exec A_SP_MENUS_CREATE_MENU_ITEM 'Assign Accounts Recievable Role','ServiceCalls','asp/serviceCallAccRecievable/assignAccRecievableRole.asp'
--Procedures
exec A_SP_MENUS_CREATE_MENU_ITEM 'Procedures','Procedures','asp/Procedures/searchProcedures.asp?firstTime=true'
exec A_SP_MENUS_CREATE_MENU_ITEM 'Procedure Class Verbs','Procedures','asp/procedureVerbs/searchVerbs.asp?firstTime=true'
exec A_SP_MENUS_CREATE_MENU_ITEM 'Procedures-Theory Noun Hier','Procedures','asp/nounHierarchy/searchHier.asp?firstTime=true'
exec A_SP_MENUS_CREATE_MENU_ITEM 'Procedure Step PrePop Text','Procedures','asp/ProcedurePrepop/searchPrepop.asp?firstTime=true'
--Job Descriptions
exec A_SP_MENUS_CREATE_MENU_ITEM 'Job Descriptions','JobDescriptions','asp/jobDescription/searchJobDescription.asp?firstTime=true'
--Theory
exec A_SP_MENUS_CREATE_MENU_ITEM 'Theory','Theory','asp/Theory/searchTheory.asp?firstTime=true'
--Manuals
exec A_SP_MENUS_CREATE_MENU_ITEM 'Manuals','Manuals','asp/Manuals/searchManuals.asp?firstTime=true'
--Files
exec A_SP_MENUS_CREATE_MENU_ITEM 'Files','Files','asp/Files/SearchFiles.asp?firstTime=true'
--People
exec A_SP_MENUS_CREATE_MENU_ITEM 'People','People','asp/People/searchPeople.asp?firstTime=true&STATUS=CREATING%2C+DENIED%2C+APPROVED%2C+APPROVED_BUT_REVISING%2C+APPROVED_BUT_DELETING'
exec A_SP_MENUS_CREATE_MENU_ITEM 'Roles','People','asp/Roles/searchRoles.asp?firstTime=true'
exec A_SP_MENUS_CREATE_MENU_ITEM 'Companies Dept','People','asp/Companies/searchCompanies.asp?firstTime=true'
exec A_SP_MENUS_CREATE_MENU_ITEM 'Resumes','People','asp/Resumes/search.asp?firstTime=true'

--Locations
exec A_SP_MENUS_CREATE_MENU_ITEM 'Locations','Locations','asp/Locations/searchLocations.asp?firstTime=true'
exec A_SP_MENUS_CREATE_MENU_ITEM 'Regions','Locations','asp/Regions/searchRegions.asp?firstTime=true'
--Products
exec A_SP_MENUS_CREATE_MENU_ITEM 'Products','Products','asp/Products/searchProducts.asp?firstTime=true'
exec A_SP_MENUS_CREATE_MENU_ITEM 'Product Prices','Products','asp/prodListPrice/searchPriceList.asp?firstTime=true'
exec A_SP_MENUS_CREATE_MENU_ITEM 'ProductReqConfig Forms','Products','asp/prodReqForm/searchReqForm.asp?firstTime=true'
exec A_SP_MENUS_CREATE_MENU_ITEM 'Customer Needs','Products','asp/needs/search.asp?firstTime=true'
exec A_SP_MENUS_CREATE_MENU_ITEM 'Proposals','Products','asp/proposals/search.asp?firstTime=true'
--Forecast
exec A_SP_MENUS_CREATE_MENU_ITEM 'Accounts','Forecast','asp/Accounts/search.asp?firstTime=true'
exec A_SP_MENUS_CREATE_MENU_ITEM 'Invoices','Forecast','asp/Accounts/invoiceSearch.asp?firstTime=true'
exec A_SP_MENUS_CREATE_MENU_ITEM 'Forecasting','Forecast','asp/Forecasts/search.asp?firstTime=true'
--exec A_SP_MENUS_CREATE_MENU_ITEM 'Budgets','Forecast','asp/Budgets/searchBudgets.asp?firstTime=true'
--Quotes
exec A_SP_MENUS_CREATE_MENU_ITEM 'Shop for Products','Quotes','asp/Products/searchProductsToPurchase.asp?firstTime=true'
exec A_SP_MENUS_CREATE_MENU_ITEM 'Orders','Quotes','asp/orders/search.asp?firstTime=true'
exec A_SP_MENUS_CREATE_MENU_ITEM 'Quotes','Quotes','asp/quotes/search.asp?firstTime=true'
exec A_SP_MENUS_CREATE_MENU_ITEM 'Purchases','Quotes','asp/Purchases/search.asp?firstTime=true'
--exec A_SP_MENUS_CREATE_MENU_ITEM 'Fills','Quotes','asp/Fills/search.asp?firstTime=true'
--exec A_SP_MENUS_CREATE_MENU_ITEM 'Provides','Quotes','asp/Provides/searchProvides.asp?firstTime=true'
--exec A_SP_MENUS_CREATE_MENU_ITEM 'Payments','Quotes','asp/Payments/searchPayments.asp?firstTime=true'
--Approvals
exec A_SP_MENUS_CREATE_MENU_ITEM 'Pending Approvals','Approvals','asp/Approvals/approvalSearch.asp'
exec A_SP_MENUS_CREATE_MENU_ITEM 'Approval WorkFlows','Approvals','asp/ApprovalWF/searchApprovalWorkFlows.asp'
exec A_SP_MENUS_CREATE_MENU_ITEM 'Approval Stages','Approvals','asp/ApprovalStages/searchApprovalStages.asp'
exec A_SP_MENUS_CREATE_MENU_ITEM 'Approval Groups','Approvals','asp/ApprovalGroups/searchApprovalGroups.asp'
--Administration
exec A_SP_MENUS_CREATE_MENU_ITEM 'Administration','Administration','asp/Administration/AdminSetup.asp'


 



















































