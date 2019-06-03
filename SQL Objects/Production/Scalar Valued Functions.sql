USE [Answer2_Prod]
GO

/****** Object:  UserDefinedFunction [dbo].[xmlEncode]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[xmlEncode]
GO

/****** Object:  UserDefinedFunction [dbo].[timeToLocal]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[timeToLocal]
GO

/****** Object:  UserDefinedFunction [dbo].[timeToGrenich]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[timeToGrenich]
GO

/****** Object:  UserDefinedFunction [dbo].[md]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[md]
GO

/****** Object:  UserDefinedFunction [dbo].[leadingZeros]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[leadingZeros]
GO

/****** Object:  UserDefinedFunction [dbo].[leadingSpaces]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[leadingSpaces]
GO

/****** Object:  UserDefinedFunction [dbo].[isParentTask]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[isParentTask]
GO

/****** Object:  UserDefinedFunction [dbo].[isChildLocation]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[isChildLocation]
GO

/****** Object:  UserDefinedFunction [dbo].[isBoss]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[isBoss]
GO

/****** Object:  UserDefinedFunction [dbo].[getUniqueID]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[getUniqueID]
GO

/****** Object:  UserDefinedFunction [dbo].[getTaskParentList]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[getTaskParentList]
GO

/****** Object:  UserDefinedFunction [dbo].[getRootURL]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[getRootURL]
GO

/****** Object:  UserDefinedFunction [dbo].[getEmailURL]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[getEmailURL]
GO

/****** Object:  UserDefinedFunction [dbo].[getCompany]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[getCompany]
GO

/****** Object:  UserDefinedFunction [dbo].[FN_ROLE_GET_COMPANY]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[FN_ROLE_GET_COMPANY]
GO

/****** Object:  UserDefinedFunction [dbo].[findFillForTask]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[findFillForTask]
GO

/****** Object:  UserDefinedFunction [dbo].[dateToVarchar]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[dateToVarchar]
GO

/****** Object:  UserDefinedFunction [dbo].[d2v]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[d2v]
GO

/****** Object:  UserDefinedFunction [dbo].[A_SP_PART_FIND_MY_RELATED_PART]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_SP_PART_FIND_MY_RELATED_PART]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_TASK_MAKE_PARENT_LIST]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_TASK_MAKE_PARENT_LIST]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_TASK_GET_LEVEL]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_TASK_GET_LEVEL]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_TASK_CREATE_NAME]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_TASK_CREATE_NAME]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_SURVEYS_GET_STANDARD_SEARCH_FLAG]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_SURVEYS_GET_STANDARD_SEARCH_FLAG]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_SERVICE_CALL_GET_STANDARD_SEARCH_FLAG]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_SERVICE_CALL_GET_STANDARD_SEARCH_FLAG]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_ROLES_CHECK_IF_MEMBER]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_ROLES_CHECK_IF_MEMBER]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_QUOTE_ITEM_GET_PRECEDENT_LIST]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_QUOTE_ITEM_GET_PRECEDENT_LIST]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_QUOTE_ITEM_GET_PARENT_USING_ORDER_ITEM]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_QUOTE_ITEM_GET_PARENT_USING_ORDER_ITEM]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_QUOTE_ITEM_GET_PARENT_LIST]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_QUOTE_ITEM_GET_PARENT_LIST]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_PURCHSES_GET_QUOTE_ACCOUNT_STATUS]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_PURCHSES_GET_QUOTE_ACCOUNT_STATUS]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_PRODUCT_WHERE_USED_CHECK]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_PRODUCT_WHERE_USED_CHECK]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_PRODUCT_INSTALLED_STATUS]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_PRODUCT_INSTALLED_STATUS]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_PROD_PROCE_LIST_FIGURE_CHILD_TOTAL_COST]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_PROD_PROCE_LIST_FIGURE_CHILD_TOTAL_COST]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_PROCEDURE_STEPS_MAKE_PRECEDING_LIST]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_PROCEDURE_STEPS_MAKE_PRECEDING_LIST]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_PROCEDURE_GET_SYSTEM_PROC_ID]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_PROCEDURE_GET_SYSTEM_PROC_ID]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_PROCEDURE_GET_PROVIDE_AND_TAKE_BACK_ID]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_PROCEDURE_GET_PROVIDE_AND_TAKE_BACK_ID]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_PROCEDURE_GET_PROVIDE_AND_STAY_ID]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_PROCEDURE_GET_PROVIDE_AND_STAY_ID]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_PROCEDURE_GET_E_ACCESS_ID]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_PROCEDURE_GET_E_ACCESS_ID]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_ORDER_ITEM_GET_PRECEDENT_LIST]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_ORDER_ITEM_GET_PRECEDENT_LIST]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_ORDER_ITEM_GET_PARENT_LIST]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_ORDER_ITEM_GET_PARENT_LIST]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_ORDER_ITEM_GET_LEVEL]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_ORDER_ITEM_GET_LEVEL]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_NEEDS_HAS_DOCUMENT]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_NEEDS_HAS_DOCUMENT]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_NEEDS_GET_STANDARD_SEARCH_FLAG]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_NEEDS_GET_STANDARD_SEARCH_FLAG]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_MESSAGES_IS_SENDER]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_MESSAGES_IS_SENDER]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_MESSAGES_IS_RECIPIENT]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_MESSAGES_IS_RECIPIENT]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_MESSAGES_IS_READ]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_MESSAGES_IS_READ]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_MESSAGES_HAS_CHILD_MESSAGE]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_MESSAGES_HAS_CHILD_MESSAGE]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_MEETING_GET_STANDARD_SEARCH_FLAG]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_MEETING_GET_STANDARD_SEARCH_FLAG]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_MAKE_ZERO_NULL]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_MAKE_ZERO_NULL]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_FILES_SHOW_ALL_XML_DATA_FOR_OBJECT]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_FILES_SHOW_ALL_XML_DATA_FOR_OBJECT]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_EE_GET_OVERALL_SHOW]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_EE_GET_OVERALL_SHOW]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_DISCUSSION_GET_STANDARD_SEARCH_FLAG]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_DISCUSSION_GET_STANDARD_SEARCH_FLAG]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_DISCUSSION_GET_LAST_VIEW_DATE]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_DISCUSSION_GET_LAST_VIEW_DATE]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_DATE_TIME_GET_LOCAL_TIME_FOR_PERSON]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_DATE_TIME_GET_LOCAL_TIME_FOR_PERSON]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_DATE_TIME_GET_GREENWITCH_TIME_FOR_PERSON]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_DATE_TIME_GET_GREENWITCH_TIME_FOR_PERSON]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_DATE_TIME_ADD_USING_UNITS]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_DATE_TIME_ADD_USING_UNITS]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_COMPANY_HAS_CHILD]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_COMPANY_HAS_CHILD]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_COMPANY_GET_TOP_COMPANY]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_COMPANY_GET_TOP_COMPANY]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_COMPANY_GET_PATH_NAME_FROM_APPROVED_ID]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_COMPANY_GET_PATH_NAME_FROM_APPROVED_ID]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_COMPANY_GET_PATH_NAME]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_COMPANY_GET_PATH_NAME]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_COMPANY_GET_PARENT_LIST_XML]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_COMPANY_GET_PARENT_LIST_XML]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_COMPANY_APPROVED_GET_TOP_COMPANY]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_COMPANY_APPROVED_GET_TOP_COMPANY]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_ACTUAL_PART_HAS_CHILD]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_ACTUAL_PART_HAS_CHILD]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_ACTUAL_PART_GET_NAME_FROM_HIST_ID_OR_ID]    Script Date: 6/3/2019 1:45:15 PM ******/
DROP FUNCTION [dbo].[A_FN_ACTUAL_PART_GET_NAME_FROM_HIST_ID_OR_ID]
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_ACTUAL_PART_GET_NAME_FROM_HIST_ID_OR_ID]    Script Date: 6/3/2019 1:45:15 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE       FUNCTION [dbo].[A_FN_ACTUAL_PART_GET_NAME_FROM_HIST_ID_OR_ID] (@ID varchar(50),@histID varchar(50))
RETURNS nvarchar(3000)
as
BEGIN
if @histID is null SELECT @histID = HISTORY_REF_ID FROM A_ACTUAL_PARTS WHERE ID = @ID
declare @so nvarchar(3000),@pID varchar(50)

if exists(SELECT * FROM A_OBJECTS WHERE OBJ_ID = @histID)
	begin
	SELECT @so = ISNULL('Actual Part # ' + ROOT + ', ', '') FROM A_OBJECTS WHERE OBJ_ID = @histID
	end
else
	begin
	SELECT @so = ISNULL('Actual Part # ' + ID + ', ', '') FROM A_ACTUAL_PARTS_HISTORY WHERE ID = @histID
	end

SELECT @so = @so +  ISNULL('Nick: ' + NICK_NAME + ', ', '') + ISNULL('s/n:' + SERIAL + ', ', ''),@pID = PART_ID
	FROM A_ACTUAL_PARTS_HISTORY WHERE ID = @histID


SELECT @so = @so +  ISNULL(NAME + ' ', '') + ISNULL('(p/n:' + ID + ')', '')
	FROM A_V_PARTS_APPROVED_DATA WHERE ID = @pID

	

return(@so) 
END
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_ACTUAL_PART_HAS_CHILD]    Script Date: 6/3/2019 1:45:15 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO


CREATE FUNCTION [dbo].[A_FN_ACTUAL_PART_HAS_CHILD] (@ID varchar(50))
RETURNS smallint
as
BEGIN
declare @c as smallint
SELECT @c = count(c.ID) FROM A_ACTUAL_PARTS_HISTORY c,A_OBJECTS o WHERE c.PARENT_ID = @ID AND 
c.OBJECT_ID = o.ID AND o.STATUS = 'APPROVED'
return(@c) 
END
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_COMPANY_APPROVED_GET_TOP_COMPANY]    Script Date: 6/3/2019 1:45:15 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE FUNCTION [dbo].[A_FN_COMPANY_APPROVED_GET_TOP_COMPANY](@ID varchar(50))
RETURNS varchar(50)
AS
BEGIN
declare @supRootCo varchar(50)
declare @tRoot as varchar(50)
set @tRoot = @ID

	begin
	set @supRootCo = @tRoot
	SELECT @tRoot = PARENT FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @supRootCo
	end
return(@supRootCo)
end
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_COMPANY_GET_PARENT_LIST_XML]    Script Date: 6/3/2019 1:45:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE    FUNCTION [dbo].[A_FN_COMPANY_GET_PARENT_LIST_XML](@ID varchar(50))
RETURNS nvarchar(4000)
AS
BEGIN
declare @p as varchar(50),
@pd as nvarchar(200)
declare @so as nvarchar(4000)
SELECT @p = PARENT FROM A_COMPANIES_HISTORY WHERE ID = @ID
SELECT @pd = NAME FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @p
while @p is not null
	begin
	set @so =  '<i><f i="id">' + isNull(@p,'') + '</f><n>' + isNull(@pd,'') + '</n></i>' + isNull(@so,'')
	set @p = NULL
	SELECT @p = PARENT FROM A_COMPANIES_HISTORY WHERE ID = @p
	SELECT @pd = NAME FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @p
	end
return(@so)
END
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_COMPANY_GET_PATH_NAME]    Script Date: 6/3/2019 1:45:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO








CREATE    FUNCTION [dbo].[A_FN_COMPANY_GET_PATH_NAME](@ID varchar(4000))
RETURNS varchar(4000)
AS
BEGIN
declare @p as varchar(50)
declare @pOld as varchar(50)
declare @t as varchar(50)
declare @pathName as varchar(4000)
SELECT @p = PARENT FROM A_COMPANIES_HISTORY WHERE ID = @ID
SELECT @pOld = ROOT FROM A_OBJECTS WHERE OBJ_ID = @ID AND OBJ_TABLE = 'A_COMPANIES_HISTORY'
SELECT @pathName = NAME FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @pOld
while @p is not null
	begin
	SELECT @pathName = NAME + '/' + @pathName FROM A_v_COMPANIES_APPROVED_DATA WHERE ID = @p
	set @pOld = @p
	set @t = NULL
	SELECT @t = HISTORY_REF_ID FROM A_COMPANIES WHERE ID = @p
--	print ' Got the t = ' +@t
	set @p = NULL
	SELECT @p = PARENT FROM A_COMPANIES_HISTORY WHERE ID = @t
	end
return(@pathName)
END
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_COMPANY_GET_PATH_NAME_FROM_APPROVED_ID]    Script Date: 6/3/2019 1:45:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO









CREATE     FUNCTION [dbo].[A_FN_COMPANY_GET_PATH_NAME_FROM_APPROVED_ID]
	(@approvedID varchar(50))
RETURNS varchar(4000)
AS
BEGIN
declare @p as varchar(50), @pOld as varchar(50), @t as varchar(50), @pathName as varchar(4000),
	@ID varchar(50)
SELECT @ID = HISTORY_REF_ID FROM A_COMPANIES WHERE ID = @approvedID
SELECT @p = PARENT FROM A_COMPANIES_HISTORY WHERE ID = @ID
SELECT @pOld = ROOT FROM A_OBJECTS WHERE OBJ_ID = @ID AND OBJ_TABLE = 'A_COMPANIES_HISTORY'
SELECT @pathName = NAME FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @pOld
while @p is not null
	begin
	SELECT @pathName = NAME + '/' + @pathName FROM A_v_COMPANIES_APPROVED_DATA WHERE ID = @p
	set @pOld = @p
	set @t = NULL
	SELECT @t = HISTORY_REF_ID FROM A_COMPANIES WHERE ID = @p
--	print ' Got the t = ' +@t
	set @p = NULL
	SELECT @p = PARENT FROM A_COMPANIES_HISTORY WHERE ID = @t
	end
return(@pathName)
END
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_COMPANY_GET_TOP_COMPANY]    Script Date: 6/3/2019 1:45:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE  FUNCTION [dbo].[A_FN_COMPANY_GET_TOP_COMPANY](@ID varchar(50))
RETURNS varchar(50)
AS
BEGIN
declare @p as varchar(50)
declare @pOld as varchar(50)
declare @t as varchar(50)

SELECT @p = PARENT FROM A_COMPANIES_HISTORY WHERE ID = @ID
SELECT @pOld = ROOT FROM A_OBJECTS WHERE OBJ_ID = @ID AND OBJ_TABLE = 'A_COMPANIES_HISTORY'
while @p is not null
	begin
--	print 'Currently looking at parent id = ' + @p
	set @pOld = @p
	set @t = NULL
	SELECT @t = HISTORY_REF_ID FROM A_COMPANIES WHERE ID = @p
--	print ' Got the t = ' +@t
	set @p = NULL
	SELECT @p = PARENT FROM A_COMPANIES_HISTORY WHERE ID = @t
	end
SELECT @pOld = ID FROM A_COMPANIES WHERE HISTORY_REF_ID = @pOld
return(@pOld)
END
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_COMPANY_HAS_CHILD]    Script Date: 6/3/2019 1:45:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO

CREATE      FUNCTION [dbo].[A_FN_COMPANY_HAS_CHILD] (@ID varchar(50))
RETURNS smallint
as
BEGIN
declare @c as smallint
SELECT @c = count(o.ID) FROM A_COMPANIES_HISTORY c,A_OBJECTS o WHERE c.PARENT = @ID AND 
c.OBJECT_ID = o.ID AND o.STATUS = 'APPROVED'
return(@c) 
END
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_DATE_TIME_ADD_USING_UNITS]    Script Date: 6/3/2019 1:45:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE  FUNCTION [dbo].[A_FN_DATE_TIME_ADD_USING_UNITS] 
	(@units varchar(50),@tm datetime,@num float)
RETURNS datetime
as
BEGIN
set @num = isNull(@num,0)
declare @myDate dateTime

	select @myDate = 
	case
		when @units = 'TIME_SYS_SECONDS'
			then dateAdd(ss,@num,@tm)
		when @units = 'TIME_SYS_MINUTES'
			then dateAdd(mi,@num,@tm)
		when @units = 'TIME_SYS_HOURS'
			then dateAdd(hh,@num,@tm)
		when @units = 'TIME_SYS_DAYS'
			then dateAdd(d,@num,@tm)
		when @units = 'TIME_SYS_WEEKS'
			then dateAdd(wk,@num,@tm)
		when @units = 'TIME_SYS_MONTHS'
			then dateAdd(m,@num,@tm)
		when @units = 'TIME_SYS_YEARS'
			then dateAdd(yy,@num,@tm)
		else dateAdd(yy,0,@tm)
	end
				



	return(@myDate)
END
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_DATE_TIME_GET_GREENWITCH_TIME_FOR_PERSON]    Script Date: 6/3/2019 1:45:17 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE  FUNCTION [dbo].[A_FN_DATE_TIME_GET_GREENWITCH_TIME_FOR_PERSON]
	(@strNTLogin varchar(50),@tm datetime)
RETURNS datetime
as
BEGIN
declare @myTZ varchar(50),@gDiff float,@myTime dateTime
SELECT @myTZ = TIME_ZONE FROM A_V_PEOPLE_APPROVED_DATA
	WHERE ID = @strNTLogin
SELECT @myTime = DATEADD(hh,-(G_DIFF),@tm) FROM A_TIME_ZONES WHERE ID = @myTZ
return(@myTime)
END
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_DATE_TIME_GET_LOCAL_TIME_FOR_PERSON]    Script Date: 6/3/2019 1:45:17 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[A_FN_DATE_TIME_GET_LOCAL_TIME_FOR_PERSON]
	(@strNTLogin varchar(50),@tm datetime)
RETURNS datetime
as
BEGIN
declare @myTZ varchar(50),@gDiff float,@myTime dateTime
SELECT @myTZ = TIME_ZONE FROM A_V_PEOPLE_APPROVED_DATA
	WHERE ID = @strNTLogin
SELECT @myTime = DATEADD(hh,G_DIFF,@tm) FROM A_TIME_ZONES WHERE ID = @myTZ
return(@myTime)
END
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_DISCUSSION_GET_LAST_VIEW_DATE]    Script Date: 6/3/2019 1:45:17 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







CREATE  FUNCTION [dbo].[A_FN_DISCUSSION_GET_LAST_VIEW_DATE](@strNTLogin varchar(50),@DID varchar(50))
RETURNS dateTime
AS
BEGIN
declare @d as datetime
SELECT @d = DRCM FROM A_DISCUSSION_VIEWED_BY_PEOPLE WHERE PERSON_ID = @strNTLogin AND DISCUSSION_ID = @DID
return(@d)
end
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_DISCUSSION_GET_STANDARD_SEARCH_FLAG]    Script Date: 6/3/2019 1:45:17 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE      FUNCTION [dbo].[A_FN_DISCUSSION_GET_STANDARD_SEARCH_FLAG](@STATUS varchar(50))
RETURNS smallint
AS
BEGIN
if @STATUS = 'ACTIVE'
	return(1)
if @STATUS = 'CLOSED'
	return(2)
return(0)
END
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_EE_GET_OVERALL_SHOW]    Script Date: 6/3/2019 1:45:17 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO



CREATE  FUNCTION [dbo].[A_FN_EE_GET_OVERALL_SHOW] (@o smallint)
RETURNS varchar(50)
as
BEGIN
declare @ret as varchar(50)
if @o = 0 set @ret = 'EE_OVERALL_NONE'
if @o >= 1 and @o <=5 set @ret = 'EE_OVERALL_LOW'
if @o >= 6 and @o <=10 set @ret = 'EE_OVERALL_MED'
if @o >= 11 and @o <=15 set @ret = 'EE_OVERALL_HIGH'
return(@ret) 
END
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_FILES_SHOW_ALL_XML_DATA_FOR_OBJECT]    Script Date: 6/3/2019 1:45:17 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[A_FN_FILES_SHOW_ALL_XML_DATA_FOR_OBJECT] (@objID varchar(50),@type as varchar(50))
RETURNS nvarchar(4000)
as
BEGIN
	declare @so as nvarchar(4000)
	Declare @ID as varchar(50),@NAME nvarchar(50)
	Declare @cur Cursor
	if @type is null
		begin
		set @cur = Cursor For SELECT LINKED_DOC_ID,SERVER_PATH FROM A_V_DOCUMENTS_WITH_LINKED_ITEM WHERE OBJECT_ID = @objID and TYPE is null
		end
	else
		begin
		set @cur = Cursor For SELECT LINKED_DOC_ID,SERVER_PATH FROM A_V_DOCUMENTS_WITH_LINKED_ITEM WHERE OBJECT_ID = @objID and TYPE = @type
		end
	open @cur
	Fetch Next from @cur Into @ID,@NAME
	while (@@fetch_status = 0)
		Begin
		set @so = isNull(@so,'') + '<r><i>' + @ID + '</i><n>' + @NAME + '</n></r>'
		Fetch Next from @cur Into @ID,@NAME
		End
	close @cur
	Deallocate @cur
	return(@so)
END
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_MAKE_ZERO_NULL]    Script Date: 6/3/2019 1:45:18 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO









CREATE  FUNCTION [dbo].[A_FN_MAKE_ZERO_NULL] (@cnt float)
RETURNS float
as
BEGIN
declare @res float
if @cnt <> 0
	set @res = @cnt
return(@res)

END
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_MEETING_GET_STANDARD_SEARCH_FLAG]    Script Date: 6/3/2019 1:45:18 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE     FUNCTION [dbo].[A_FN_MEETING_GET_STANDARD_SEARCH_FLAG](@START_DATE datetime,@END_DATE datetime,@curDate dateTime)
RETURNS smallint
AS
BEGIN
if @START_DATE < @curDate AND @END_DATE > @curDate
	return(1)
if @START_DATE > @curDate
	return(2)
if @START_DATE < @curDate
	return(3)
return(0)
END
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_MESSAGES_HAS_CHILD_MESSAGE]    Script Date: 6/3/2019 1:45:18 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE       FUNCTION [dbo].[A_FN_MESSAGES_HAS_CHILD_MESSAGE] (@ID varchar(50))
RETURNS smallInt
AS
BEGIN
	DECLARE @tester varchar(50),
	@ret as smallint
	SELECT top 1 @tester = ID FROM 
	A_MESSAGES l WHERE l.PARENT_ID = @ID AND STATUS ='SENT'
	if @tester is null
		begin
		set @ret =  0
		end
	else
		begin
		set @ret = 1
		end
return(@ret)
END
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_MESSAGES_IS_READ]    Script Date: 6/3/2019 1:45:18 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE        FUNCTION [dbo].[A_FN_MESSAGES_IS_READ] (@messageID varchar(50),@strNTlogin varchar(50))
RETURNS smallInt
AS
BEGIN
	DECLARE @tester varchar(50),
	@ret as smallint
	SELECT @tester = IS_READ FROM 
	A_MESSAGES_PEOPLE_LINK  
	WHERE MESSAGE_ID = @messageID
	AND PERSON_ID = @strNTlogin
	if @tester = 0
		begin
		set @ret =  0
		end
	else
		begin
		set @ret = 1
		end
return(@ret)
END
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_MESSAGES_IS_RECIPIENT]    Script Date: 6/3/2019 1:45:18 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE        FUNCTION [dbo].[A_FN_MESSAGES_IS_RECIPIENT] (@messageID varchar(50),@strNTlogin varchar(50))
RETURNS smallInt
AS
BEGIN
	DECLARE @tester varchar(50),
	@ret as smallint
	SELECT @tester = PERSON_ID FROM 
	A_MESSAGES_PEOPLE_LINK  
	WHERE MESSAGE_ID = @messageID
	AND PERSON_ID = @strNTlogin
	if @tester is null
		begin
		set @ret =  0
		end
	else
		begin
		set @ret = 1
		end
return(@ret)
END
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_MESSAGES_IS_SENDER]    Script Date: 6/3/2019 1:45:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE        FUNCTION [dbo].[A_FN_MESSAGES_IS_SENDER] (@messageID varchar(50),@strNTlogin varchar(50))
RETURNS smallInt
AS
BEGIN
	DECLARE @tester varchar(50),
	@ret as smallint
	SELECT @tester = SENDER FROM 
	A_MESSAGES  
	WHERE ID = @messageID
	AND SENDER = @strNTlogin
	if @tester is null
		begin
		set @ret =  0
		end
	else
		begin
		set @ret = 1
		end
return(@ret)
END
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_NEEDS_GET_STANDARD_SEARCH_FLAG]    Script Date: 6/3/2019 1:45:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE      FUNCTION [dbo].[A_FN_NEEDS_GET_STANDARD_SEARCH_FLAG](@START_DATE datetime,@STOP_DATE datetime,@curDate dateTime)
RETURNS smallint
AS
BEGIN
if @STOP_DATE < @curDate
	return(2)
if @START_DATE > @curDate
	return(1)
return(0)
END
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_NEEDS_HAS_DOCUMENT]    Script Date: 6/3/2019 1:45:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







CREATE   FUNCTION [dbo].[A_FN_NEEDS_HAS_DOCUMENT] (@objID varchar(50))
RETURNS smallInt
AS
BEGIN
	DECLARE @tester varchar(50),
	@ret as smallint
	SELECT @tester = ID FROM 
	A_DOCUMENT_LINK l WHERE l.OBJECT_ID = @objID 
	if @tester is null
		begin
		set @ret =  0
		end
	else
		begin
		set @ret = 1
		end
return(@ret)
END
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_ORDER_ITEM_GET_LEVEL]    Script Date: 6/3/2019 1:45:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







CREATE FUNCTION [dbo].[A_FN_ORDER_ITEM_GET_LEVEL](@ID varchar(50))
RETURNS int
AS
BEGIN
declare @p as varchar(50),
@pd as nvarchar(200)
SELECT @p = PARENT FROM A_ORDER_ITEMS WHERE ID = @ID
declare @c as int
set @c = 0
while @p is not null
	begin
	set @c = @c + 1
	set @p = NULL
	SELECT @p = PARENT FROM A_ORDER_ITEMS WHERE ID = @p
	end
return(@c)
END
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_ORDER_ITEM_GET_PARENT_LIST]    Script Date: 6/3/2019 1:45:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







CREATE FUNCTION [dbo].[A_FN_ORDER_ITEM_GET_PARENT_LIST](@ID varchar(50))
RETURNS nvarchar(4000)
AS
BEGIN
declare @p as varchar(50),
@pd as nvarchar(200)
declare @so as nvarchar(4000)
SELECT @p = PARENT FROM A_ORDER_ITEMS WHERE ID = @ID
while @p is not null
	begin
	set @so =  dbo.leadingSpaces(@p,12) + isNull('-->' + @so,'')
	set @p = NULL
	SELECT @p = PARENT FROM A_ORDER_ITEMS WHERE ID = @p
	end
return(@so)
END
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_ORDER_ITEM_GET_PRECEDENT_LIST]    Script Date: 6/3/2019 1:45:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







CREATE FUNCTION [dbo].[A_FN_ORDER_ITEM_GET_PRECEDENT_LIST](@ID varchar(50))
RETURNS varchar(8000)
AS
BEGIN
declare @so as varchar(8000)
Declare @it nvarchar(50)
Declare @curs Cursor
set @curs = Cursor For SELECT PREV FROM A_ORDER_ITEM_PRECEDENTS WHERE FOL = @ID
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	set @so = isnull(@so + ',','') + @it
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs
return(@so)
END
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_PROCEDURE_GET_E_ACCESS_ID]    Script Date: 6/3/2019 1:45:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO



CREATE FUNCTION [dbo].[A_FN_PROCEDURE_GET_E_ACCESS_ID] (@strNTLogin as varchar(50))
RETURNS varchar(50)
as
BEGIN
	declare @so as  varchar(50)
	SELECT @so = ID FROM A_V_PROCEDURES_APPROVED_DATA WHERE SYSTEM_ID = 'SYS_E_ACCESS' AND CREATING_CO = dbo.getCompany(@strNTLogin)
	return(@so)
END
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_PROCEDURE_GET_PROVIDE_AND_STAY_ID]    Script Date: 6/3/2019 1:45:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO


CREATE   FUNCTION [dbo].[A_FN_PROCEDURE_GET_PROVIDE_AND_STAY_ID] (@strNTLogin as varchar(50))
RETURNS varchar(50)
as
BEGIN
	declare @so as  varchar(50)
	SELECT @so = ID FROM A_V_PROCEDURES_APPROVED_DATA WHERE SYSTEM_ID = 'SYS_PROVIDE_AND_STAY' AND CREATING_CO = dbo.getCompany(@strNTLogin)
	return(@so)
END
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_PROCEDURE_GET_PROVIDE_AND_TAKE_BACK_ID]    Script Date: 6/3/2019 1:45:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO



CREATE FUNCTION [dbo].[A_FN_PROCEDURE_GET_PROVIDE_AND_TAKE_BACK_ID] (@strNTLogin as varchar(50))
RETURNS varchar(50)
as
BEGIN
	declare @so as  varchar(50)
	SELECT @so = ID FROM A_V_PROCEDURES_APPROVED_DATA WHERE SYSTEM_ID = 'SYS_PROVIDE_TAKE_BACK' AND CREATING_CO = dbo.getCompany(@strNTLogin)
	return(@so)
END
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_PROCEDURE_GET_SYSTEM_PROC_ID]    Script Date: 6/3/2019 1:45:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE  FUNCTION [dbo].[A_FN_PROCEDURE_GET_SYSTEM_PROC_ID] (@co varchar(50),@SYS_ID varchar(50))
RETURNS varchar(50)
as
BEGIN
	declare @so as  varchar(50)
	SELECT @so = ID FROM A_V_PROCEDURES_APPROVED_DATA WHERE SYSTEM_ID = @SYS_ID AND CREATING_CO = dbo.A_FN_COMPANY_GET_TOP_COMPANY(@co)
	return(@so)
END
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_PROCEDURE_STEPS_MAKE_PRECEDING_LIST]    Script Date: 6/3/2019 1:45:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE    FUNCTION [dbo].[A_FN_PROCEDURE_STEPS_MAKE_PRECEDING_LIST] (@stepID varchar(50))
RETURNS nvarchar(4000)
as
BEGIN
	Declare @so as nvarchar(4000)
	Declare @ID as varchar(50),
	@TXT as varchar(4000),
	@STEP_ORDER as int
	Declare @cur Cursor
	set @cur = Cursor For SELECT s.ID,s.PRINT_ORDER,convert(nvarchar(10),s.STEP_TEXT) AS TXT
		FROM A_PROCEDURE_STEP_PRECEDING_STEPS p,
			A_PROCEDURE_STEPS s
		WHERE MY_STEP = @stepID AND p.PREV_STEP = s.ID
		ORDER BY s.PRINT_ORDER

	open @cur
	declare @cnt smallint
	set @cnt = 0
	Fetch Next from @cur Into @ID,@STEP_ORDER,@TXT
	while (@@fetch_status = 0) AND @cnt < 20
		Begin
		set @cnt = @cnt + 1
		set @TXT = dbo.xmlEncode(@TXT)
		set @TXT = replace(@TXT,CHAR(13),'')
		if CHARINDEX(@TXT,CHAR(13)) > 0
			begin
			set @TXT = LEFT(@TXT,CHARINDEX(@TXT,CHAR(13)))
			end
		set @TXT = left(@TXT,5) + '...'
		set @so = isNull(@so,'') + '<s><i>' + isNULL(@ID,'') + '</i><t>' + isNull(@ID,'') + '</t><o>' + convert(nvarchar(50),isNull(@STEP_ORDER,'')) + '</o></s>'
		Fetch Next from @cur Into @ID,@STEP_ORDER,@TXT
		End
	close @cur
	Deallocate @cur
	return(@so)
END
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_PROD_PROCE_LIST_FIGURE_CHILD_TOTAL_COST]    Script Date: 6/3/2019 1:45:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







CREATE  FUNCTION [dbo].[A_FN_PROD_PROCE_LIST_FIGURE_CHILD_TOTAL_COST]
(@REL varchar(50),
@DUR REAL,
@DUR_TYPE varchar(50),
@QTY REAL,
@QTY_TYPE varchar(50),
@UNIT_PRICE REAL
)
RETURNS REAL
AS
BEGIN
declare @tot as REAL
if @REL = 'PARTS_PROVIDE_TAKE_BACK'
	begin
		set @tot = @DUR * @UNIT_PRICE
	end
if @REL = 'PARTS_PROVIDE_STAY' OR @REL = 'PARTS_PROVIDE_CONSUMED'
	begin
		set @tot = @QTY * @UNIT_PRICE
	end
return(@tot)




end
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_PRODUCT_INSTALLED_STATUS]    Script Date: 6/3/2019 1:45:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO


CREATE      FUNCTION [dbo].[A_FN_PRODUCT_INSTALLED_STATUS] (@PRO_PART_LINK_ID varchar(50))
RETURNS varchar(50)
AS
BEGIN
declare @so varchar(50)
SELECT @so = CASE WHEN (@PRO_PART_LINK_ID IS NULL) 
 THEN 'PROD_NOT_INSTALLED' ELSE 'PROD_INSTALLED' END
return @so
END
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_PRODUCT_WHERE_USED_CHECK]    Script Date: 6/3/2019 1:45:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO

CREATE     FUNCTION [dbo].[A_FN_PRODUCT_WHERE_USED_CHECK] (@ID varchar(50))
RETURNS smallInt
AS
BEGIN
declare @t as varchar(50)
SELECT Top 1 @t = ID FROM A_PRODUCT_OBJ_USED_ON_LINK WHERE PRODUCT_ID = @ID
if @t is null return 0
return 1
END
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_PURCHSES_GET_QUOTE_ACCOUNT_STATUS]    Script Date: 6/3/2019 1:45:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[A_FN_PURCHSES_GET_QUOTE_ACCOUNT_STATUS]
	(@PURCHASE_ID varchar(50),@QUOTE_ID varchar(50))
RETURNS varchar(50)
AS
BEGIN
	declare @tester varchar(50), @ret varchar(50)
	SELECT @tester = ID FROM A_V_PURCHASE_ITEM_COMPLETE_DATA WHERE 
		PURCHASE_ID = @PURCHASE_ID AND QUOTE_ID = @QUOTE_ID AND ACCT_ID is NULL
	if @tester is not null	set @ret = 'ITEM_NEEDS_ACCOUNT'
	else set @ret = 'ALL_ITEMS_HAVE_ACCOUNTS'
return @ret
END
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_QUOTE_ITEM_GET_PARENT_LIST]    Script Date: 6/3/2019 1:45:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO








CREATE  FUNCTION [dbo].[A_FN_QUOTE_ITEM_GET_PARENT_LIST](@ID varchar(50))
RETURNS nvarchar(4000)
AS
BEGIN
declare @p as varchar(50),
@pd as nvarchar(200)
declare @so as nvarchar(4000)
SELECT @p = PARENT FROM A_QUOTE_ITEMS WHERE ID = @ID
while @p is not null
	begin
	set @so =  dbo.leadingSpaces(@p,12) + isNull('-->' + @so,'')
	set @p = NULL
	SELECT @p = PARENT FROM A_QUOTE_ITEMS WHERE ID = @p
	end
return(@so)
END
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_QUOTE_ITEM_GET_PARENT_USING_ORDER_ITEM]    Script Date: 6/3/2019 1:45:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO








CREATE   FUNCTION [dbo].[A_FN_QUOTE_ITEM_GET_PARENT_USING_ORDER_ITEM](@ID varchar(50))
RETURNS varchar(50)
AS
BEGIN
declare @parent varchar(50)
SELECT @parent = PARENT_ID FROM A_V_QUOTE_ITEM_PARENTS_USING_ORDER_ITEM_FOR_RELATIONSHIP 
	WHERE CHILD_ID = @ID
return(@parent)
end
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_QUOTE_ITEM_GET_PRECEDENT_LIST]    Script Date: 6/3/2019 1:45:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO








CREATE FUNCTION [dbo].[A_FN_QUOTE_ITEM_GET_PRECEDENT_LIST](@ID varchar(50))
RETURNS varchar(8000)
AS
BEGIN
declare @so as varchar(8000)
Declare @it nvarchar(50)
Declare @curs Cursor
set @curs = Cursor For SELECT PREV FROM A_QUOTE_ITEM_PRECEDENTS WHERE FOL = @ID
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	set @so = isnull(@so + ',','') + @it
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs
return(@so)
END
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_ROLES_CHECK_IF_MEMBER]    Script Date: 6/3/2019 1:45:22 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  FUNCTION [dbo].[A_FN_ROLES_CHECK_IF_MEMBER](@ROLE_ID varchar(50),@P_ID varchar(50))
RETURNS smallInt
AS
BEGIN
declare @res as smallint
declare @tester varchar(50)
SELECT @tester = ID FROM A_V_ROLES_WITH_ASSIGNEES WHERE PERSON_ID = @P_ID AND ROOT = @ROLE_ID
if @tester is null set @res = 0
else set @res = 1
return(@res)
end
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_SERVICE_CALL_GET_STANDARD_SEARCH_FLAG]    Script Date: 6/3/2019 1:45:22 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







CREATE         FUNCTION [dbo].[A_FN_SERVICE_CALL_GET_STANDARD_SEARCH_FLAG](@STATUS varchar(100))
RETURNS smallint
AS
BEGIN
if @STATUS = 'WORKER'
	return(1)
if @STATUS = 'BOSS'
	return(2)
if @STATUS = 'CUSTOMER_REVIEW_GROUP'
	return(3)
if @STATUS = 'CUSTOMER_AP_GROUP'
	return(4)
if @STATUS = 'AR_GROUP'
	return(5)
if @STATUS = 'CLOSED'
	return(6)
return(0)
END
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_SURVEYS_GET_STANDARD_SEARCH_FLAG]    Script Date: 6/3/2019 1:45:22 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE      FUNCTION [dbo].[A_FN_SURVEYS_GET_STANDARD_SEARCH_FLAG](@STATUS varchar(50))
RETURNS smallint
AS
BEGIN
if @STATUS = 'OPEN'
	return(1)
if @STATUS = 'CREATING'
	return(2)
if @STATUS = 'CLOSED'
	return(3)
return(0)
END
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_TASK_CREATE_NAME]    Script Date: 6/3/2019 1:45:22 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE   FUNCTION [dbo].[A_FN_TASK_CREATE_NAME] (@ID varchar(50))
RETURNS nvarchar(3000)
as
BEGIN
declare @so nvarchar(3000),@procID varchar(50),@procStepID varchar(50),@objID varchar(50),@toID varchar(50),@fromID varchar(50)
SELECT @procID = PROCEDURE_ID,@procStepID = PROCEDURE_STEP_ID FROM A_TASKS WHERE ID = @ID
SELECT @so = isNull(NAME + '. ','No Procedure Name. ') FROM A_V_PROCEDURES_APPROVED_DATA WHERE ID = @procID
SELECT @objID = OBJECT_ID FROM A_TASK_OBJECT_LINK WHERE TASK_ID = @ID
--SELECT @so = @so + isNull('Part = ' + OBJ_DESC + '. ','') FROM A_OBJECTS WHERE ID = @objID
SELECT @toID = ACTUAL_TO_LOC,@fromID = ACTUAL_FROM_LOC FROM A_TASK_ORDER_INFORMATION WHERE TASK_ID = @ID
SELECT @so = @so + isNull('Source Location = ' + NAME + '. ','') FROM A_V_LOCATIONS_APPROVED_DATA WHERE ID = @fromID
SELECT @so = @so + isNull('Destination Location = ' + NAME + '. ','') FROM A_V_LOCATIONS_APPROVED_DATA WHERE ID = @toID


return(@so) 
END
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_TASK_GET_LEVEL]    Script Date: 6/3/2019 1:45:22 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[A_FN_TASK_GET_LEVEL](@ID varchar(50))
RETURNS int
AS
BEGIN
declare @p as varchar(50),
@pd as nvarchar(200)
SELECT @p = PARENT_ID FROM A_TASKS WHERE ID = @ID
declare @c as int
set @c = 0
while @p is not null
	begin
	set @c = @c + 1
	SELECT @p = PARENT_ID FROM A_TASKS WHERE ID = @p
	end
return(@c)
END
GO

/****** Object:  UserDefinedFunction [dbo].[A_FN_TASK_MAKE_PARENT_LIST]    Script Date: 6/3/2019 1:45:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE   FUNCTION [dbo].[A_FN_TASK_MAKE_PARENT_LIST] (@ID varchar(50))
RETURNS nvarchar(3000)
as
BEGIN
declare @parentID varchar(100)
declare @res varchar(3000)
SELECT @parentID = parent_id from A_TASKS WHERE ID = @ID
set @res = ''
while @parentID is not null
	begin
	set @res = @parentID + @res
	SELECT @parentID = parent_id from A_TASKS WHERE ID = @parentID
	end

return(@res) 
END
GO

/****** Object:  UserDefinedFunction [dbo].[A_SP_PART_FIND_MY_RELATED_PART]    Script Date: 6/3/2019 1:45:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE function [dbo].[A_SP_PART_FIND_MY_RELATED_PART](
@supPartNum varchar(50),
@strNTLogin varchar(50))
RETURNS varchar(50)
AS
BEGIN
declare @myCo varchar(50),@phID varchar(50)
SELECT @myCo = ROOT_COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @strNTLogin
declare @myPartID varchar(50)
SELECT @myPartID = p.ID FROM
A_O_PARTS_HISTORY p, A_PARTS_EXTERNAL_EQUALS e
WHERE p.ID = e.PART_ID AND
EQUAL_PART_ID = @supPartNum AND
p.CREATING_CO = @myCo AND
p.STATUS LIKE 'APPROVED%'

if @myPartID is Null
	begin
	select @phID = PARTS_HISTORY_ID FROM A_PARTS WHERE ID = @supPartNum
	SELECT @myPartID = p.ID FROM
	A_O_PARTS_HISTORY p, A_PARTS_EXTERNAL_EQUALS e
	WHERE p.ROOT = e.EQUAL_PART_ID AND
	e.PART_ID = @phID AND
	p.CREATING_CO = @myCo AND
	p.STATUS LIKE 'APPROVED%'
	end



return(@myPartID)
END
GO

/****** Object:  UserDefinedFunction [dbo].[d2v]    Script Date: 6/3/2019 1:45:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







CREATE  FUNCTION [dbo].[d2v](@d datetime)
RETURNS varchar(50)
AS
BEGIN
declare @op as varchar(50)
set @op = 
	convert(varchar(50),month(@d)) + '/' +
	convert(varchar(50),day(@d)) + '/' +
	convert(varchar(50),year(@d))


return(@op)
end
GO

/****** Object:  UserDefinedFunction [dbo].[dateToVarchar]    Script Date: 6/3/2019 1:45:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







CREATE  FUNCTION [dbo].[dateToVarchar](@d datetime)
RETURNS varchar(50)
AS
BEGIN
declare @op as varchar(50)
set @op = 
	convert(varchar(50),month(@d)) + '/' +
	convert(varchar(50),day(@d)) + '/' +
	convert(varchar(50),year(@d))


return(@op)
end
GO

/****** Object:  UserDefinedFunction [dbo].[findFillForTask]    Script Date: 6/3/2019 1:45:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE FUNCTION [dbo].[findFillForTask](@taskID varchar(50))
RETURNS nvarchar(4000)
AS
BEGIN
declare @pID varchar(50),@fillID varchar(50), @cnt int
SELECT @pID = PARENT_ID FROM A_TASKS WHERE ID = @taskID
set @cnt = 0
while @pID is not null and @fillID is null and @cnt < 20
begin
set @cnt = @cnt + 1
SELECT @fillID = FILL_ITEM_ID FROM A_TASK_ORDER_INFORMATION WHERE TASK_ID = @pID
SELECT @pID = PARENT_ID FROM A_TASKS WHERE ID = @pID
end
return(@fillID)
END
GO

/****** Object:  UserDefinedFunction [dbo].[FN_ROLE_GET_COMPANY]    Script Date: 6/3/2019 1:45:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE       FUNCTION [dbo].[FN_ROLE_GET_COMPANY](@roleID varchar(50))
RETURNS varchar(50)
AS
BEGIN
	declare @ret varchar(50)
	SELECT @ret = CREATING_CO FROM A_APPROVED_ROLES WHERE ID = @roleID
	return(@ret)
END
GO

/****** Object:  UserDefinedFunction [dbo].[getCompany]    Script Date: 6/3/2019 1:45:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE   FUNCTION [dbo].[getCompany](@strNTLogin nvarchar(50))
RETURNS nvarchar(50)
AS
BEGIN
	DECLARE @myCO nvarchar(50)
	SELECT @myCO = ROOT_COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @strNTLogin
	RETURN @myCO
END
GO

/****** Object:  UserDefinedFunction [dbo].[getEmailURL]    Script Date: 6/3/2019 1:45:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







CREATE  FUNCTION [dbo].[getEmailURL]()
RETURNS varchar(200)
AS
BEGIN
	DECLARE @myVal nvarchar(50)
	SELECT @myVal = VAL FROM A_ADMIN_CONFIGURATION WHERE NAME = 'EMAIL_URL'
	return (@myVal)
END
GO

/****** Object:  UserDefinedFunction [dbo].[getRootURL]    Script Date: 6/3/2019 1:45:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE FUNCTION [dbo].[getRootURL]()
RETURNS varchar(200)
AS
BEGIN
	DECLARE @myVal nvarchar(50)
	SELECT @myVal = VAL FROM A_ADMIN_CONFIGURATION WHERE NAME = 'ROOT_URL'
	return (@myVal)
END
GO

/****** Object:  UserDefinedFunction [dbo].[getTaskParentList]    Script Date: 6/3/2019 1:45:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE    FUNCTION [dbo].[getTaskParentList](@ID varchar(50))
RETURNS nvarchar(4000)
AS
BEGIN
declare @p as varchar(50),
@pd as nvarchar(50)
declare @so as varchar(8000)
SELECT @p = PARENT_ID FROM A_TASKS WHERE ID = @ID
SELECT @pd = DESCRIPTION FROM A_TASKS WHERE ID = @p
set @pd = dbo.xmlEncode(@pd)
declare @cnt int
set @cnt = 0
while @p is not null and @cnt < 15
	begin
	set @cnt = @cnt + 1
	set @so =  '<i><f i="id">' + isNull(@p,'') + '</f><n>' + isNull(@pd,'') + '</n></i>' + isNull(@so,'')
	SELECT @p = PARENT_ID FROM A_TASKS WHERE ID = @p
	SELECT @pd = DESCRIPTION FROM A_TASKS WHERE ID = @p
	end
set @so = @so
return(@so)
END
GO

/****** Object:  UserDefinedFunction [dbo].[getUniqueID]    Script Date: 6/3/2019 1:45:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE   FUNCTION [dbo].[getUniqueID]()
RETURNS nvarchar(50)
AS
BEGIN
	DECLARE @myVal nvarchar(50)
	exec sp_getUniqueID3 @myVal OUTPUT
	RETURN @myVal
END
GO

/****** Object:  UserDefinedFunction [dbo].[isBoss]    Script Date: 6/3/2019 1:45:25 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE   FUNCTION [dbo].[isBoss](@ID varchar(50))
RETURNS smallInt
AS
BEGIN
	DECLARE @tester varchar(50),
	@ret as smallint
	SELECT top 1 @tester = ID FROM 
	A_V_PEOPLE_APPROVED_DATA p WHERE p.BOSS = @ID
	if @tester is null
		begin
		set @ret =  0
		end
	else
		begin
		set @ret = 1
		end
return(@ret)
END
GO

/****** Object:  UserDefinedFunction [dbo].[isChildLocation]    Script Date: 6/3/2019 1:45:25 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE    FUNCTION [dbo].[isChildLocation](@ID varchar(50))
RETURNS smallInt
AS
BEGIN
	DECLARE @tester varchar(50),
	@ret as smallint
	SELECT top 1 @tester = ID FROM 
	A_APPROVED_LOCATIONS l WHERE l.PARENT_LOCATION = @ID
	if @tester is null
		begin
		set @ret =  0
		end
	else
		begin
		set @ret = 1
		end
return(@ret)
END
GO

/****** Object:  UserDefinedFunction [dbo].[isParentTask]    Script Date: 6/3/2019 1:45:25 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE    FUNCTION [dbo].[isParentTask](@ID varchar(50))
RETURNS smallInt
AS
BEGIN
	DECLARE @tester varchar(50),
	@ret as smallint
	SELECT top 1 @tester = ID FROM 
	A_TASKS t WHERE t.PARENT_ID = @ID
	if @tester is null
		begin
		set @ret =  0
		end
	else
		begin
		set @ret = 1
		end
return(@ret)
END
GO

/****** Object:  UserDefinedFunction [dbo].[leadingSpaces]    Script Date: 6/3/2019 1:45:25 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE    FUNCTION [dbo].[leadingSpaces](@val varchar(50),@len int)
RETURNS varchar(50)
AS
BEGIN
	
	DECLARE @myLen int,
		@so varchar(50),
		@cnt int
	set @myLen = len(@val)
	if @len > @myLen
		begin
		set @cnt = 1
		while @cnt < (@len - @myLen)
			begin
			set @so = isnull(@so,'') + ' '
			set @cnt = @cnt + 1
			end
		end
	return(isNull(@so,'') + @val)

END
GO

/****** Object:  UserDefinedFunction [dbo].[leadingZeros]    Script Date: 6/3/2019 1:45:25 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO








CREATE     FUNCTION [dbo].[leadingZeros](@val varchar(50),@len int)
RETURNS varchar(50)
AS
BEGIN
	
	DECLARE @myLen int,
		@so varchar(50),
		@cnt int
	set @myLen = len(@val)
	if @len > @myLen
		begin
		set @cnt = 1
		while @cnt <= (@len - @myLen)
			begin
			set @so = isnull(@so,'') + '0'
			set @cnt = @cnt + 1
			end
		end
	return(isNull(@so,'') + @val)

END
GO

/****** Object:  UserDefinedFunction [dbo].[md]    Script Date: 6/3/2019 1:45:25 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE    FUNCTION [dbo].[md]()
RETURNS tinyInt
AS
BEGIN
	declare @ret tinyInt
	if exists (SELECT * FROM A_ADMIN_CONFIGURATION WHERE NAME = 'SP_DEBUG' AND VAL = 1)
		set @ret = 1
	else
		set @ret = 0
	return @ret
END
GO

/****** Object:  UserDefinedFunction [dbo].[timeToGrenich]    Script Date: 6/3/2019 1:45:26 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE   FUNCTION [dbo].[timeToGrenich](@time datetime,@strNTLogin nvarchar(50))
RETURNS datetime
AS
BEGIN
	DECLARE @myOffset int
	SELECT @myOffset = isNull(p.G_DIFF,0) FROM A_APPROVED_PEOPLE p WHERE p.ID = @strNTLogin
	declare @newDate as dateTime
	set @newDate = DATEADD(hh,-(@myOffset),@time)
	return (@newDate)
END
GO

/****** Object:  UserDefinedFunction [dbo].[timeToLocal]    Script Date: 6/3/2019 1:45:26 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE     FUNCTION [dbo].[timeToLocal](@time datetime,@strNTLogin nvarchar(50))
RETURNS datetime
AS
BEGIN
	DECLARE @myOffset int
	SELECT @myOffset = isNull(-(p.G_DIFF),0) FROM A_APPROVED_PEOPLE p WHERE p.ID = @strNTLogin
	declare @newDate as dateTime
	set @newDate = DATEADD(hh,-(@myOffset),@time)
	return (@newDate)
END
GO

/****** Object:  UserDefinedFunction [dbo].[xmlEncode]    Script Date: 6/3/2019 1:45:26 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO









CREATE    FUNCTION [dbo].[xmlEncode](@str nvarchar(4000))
RETURNS nvarchar(4000)
AS
BEGIN
declare @so nvarchar(4000)
set @so = @str
set @so = replace(@so,'&','&amp;')
set @so = replace(@so,'<','&lt;')
set @so = replace(@so,'>','&gt;')
set @so = replace(@so,'"','&quot;')

return(@so)
END
GO

