


CREATE    PROCEDURE DBO.A_SP_PROCEDURE_CREATE_PLACE_HOLDER
@procObjID varchar(50) OUTPUT,
@procName varchar(2000),
@ROLE_ID varchar(50),
@strNTLogin varchar(50)
AS
declare @creatingCo varchar(50)
SELECT @creatingCo = ROOT_COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @strNTLogin
exec A_SP_PROCEDURES_UPDATE_ONE_PROCEDURE
	@procObjID OUTPUT,
	null,				--@messages nvarchar(500) OUTPUT,
	null,				--@objID
	@creatingCo,		--COMPANY
	null,				--VERB
	@procName,			--NAME
	null,				--COMMENTS
	1,					--STEPS IN AP
	0,					--WIP_MSG
	3,					--SECURITY_LEVEL
	null,				--SYS_ID
	0,					--Duration
	'TIME_SYS_MINUTES', --Duration Type
	@strNTlogin			--strNTLogin
declare @procHistID varchar(50)
SELECT ID FROM A_PROCEDURES_HISTORY WHERE OBJECT_ID = @procObjID
declare @procStepName varchar(2000)
set @procStepName = 'In the short term, manually manage this activity since procedure steps aren''t written yet.  In the long term, write the procedure steps.  Probably manually transfer from ESCA.  By the way, the first step usually should be a "Move To Shop" and the last step usually should be a "Move From Shop”.'
declare @procStepID varchar(50)
exec A_SP_PROCEDURE_STEP_UPDATE_ONE_STEP
@procStepID OUTPUT, 							--@newID nvarchar(50) OUTPUT,
null, 							--@messages nvarchar(500) OUTPUT,
null,							--@ID nvarchar(50),
@procStepName,		--@STEP_TEXT nvarchar(2000),
@procObjID,						--@PROC_OBJ_ID nvarchar(50),
null,							--@COMMENTS nvarchar(2000),
0,								--@START_ON_COUNTER nvarchar(50),
0,								--@COUNTER_VALUE nvarchar(50),
null,							--@COUNTER_UNIT nvarchar(50),
null,							--@FROM_START_OR_STOP nvarchar(50),
null,							--@REL_OR_ABS nvarchar(50),
null,							--@SYSTEM_TASK nvarchar(50),
null,							--@DESTINATION nvarchar(50),
null,							--@SPECIFIC_LOCATION nvarchar(50),
null,							--@REFERENCE_VERB nvarchar(50),
null,							--@REFERENCE_OBJECT nvarchar(50),
null,							--@REFERENCE_THEORIES nvarchar(50),
null,							--@GOTO_STEP nvarchar(50),
null,							--@GOTO_STEP_ID nvarchar(50),
null,							--@CYCLES nvarchar(50),
null,							--@CYCLE_ON_COUNTER nvarchar(50),
null,							--@CYCLE_COUNT nvarchar(50),
null,							--@CYCLE_UNIT nvarchar(50),
null,							--@ReferenceProcs varchar(8000),
null,							--@precedingSteps varchar(8000),
1,								--@DURATION float,
'TIME_SYS_MINUTES',				--@DURATION_TYPE nvarchar(50),
@strNTLogin 


INSERT INTO A_PROCEDURE_OBJECT_LINK
(ID,PROCEDURE_ID,STEP_ID,APPROVED_OBJECT_ID,QTY,QTY_TYPE,RELATIONSHIP,DRCM,MODBY,LABOR_ROLE)
values
(newID(),@procHistID,@procStepID,@ROLE_ID,1,'TIME_SYS_MINUTES','LABOR_PROVIDE_TAKE_BACK',getDate(),@strNTLogin,'LABOR_OWNER')

-- exec A_SP_PROCEDURE_OBJECT_LINK_ADD 
-- null, 					--@newID OUTPUT,
-- null,					--@msg OUTPUT, --22:00 -  
-- null,					--ID
-- @procObjID,				--Procedure_obj_id
-- NULL, 					--PROC_ID
-- NULL,					--PROCEDURE_STEP_ID
-- @ROLE_ID,				--ROLE_ID
-- '1', 					--ID
-- 'TIME_SYS_MINUTES', 	--QTY Type
-- 'LABOR_PROVIDE_TAKE_BACK', --ITEM Type
-- 'LABOR_OWNER', 			--Labor Role
-- '7'						--strNTlogin
--  


	



