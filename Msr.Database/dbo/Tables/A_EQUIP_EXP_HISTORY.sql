CREATE TABLE [dbo].[A_EQUIP_EXP_HISTORY] (
    [ID]                      VARCHAR (50)    NOT NULL,
    [OBJECT_ID]               VARCHAR (50)    NULL,
    [PERSON_ID]               VARCHAR (50)    NULL,
    [PART_ID]                 VARCHAR (50)    NULL,
    [FIRST_EXPOSURE_DATE]     DATETIME        NULL,
    [LAST_EXPOSURE_DATE]      DATETIME        NULL,
    [HW_INSTALL_EXP_LEVEL]    SMALLINT        NULL,
    [PROCESS_SETUP_EXP_LEVEL] SMALLINT        NULL,
    [OPERATION_EXP_LEVEL]     SMALLINT        NULL,
    [SM_EXP_LEVEL]            SMALLINT        NULL,
    [UM_EXP_LEVEL]            SMALLINT        NULL,
    [FORMALLY_TRAINED]        VARCHAR (50)    NULL,
    [CERTIFIED]               VARCHAR (50)    NULL,
    [COMMENTS]                NVARCHAR (4000) NULL,
    [DRCM]                    VARCHAR (50)    NULL,
    [MODBY]                   VARCHAR (50)    NULL,
    CONSTRAINT [PK_A_EQUIP_EXP_HISTORY] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO

CREATE           TRIGGER A_EQUIP_EXP_HISTORY_INSERT
ON dbo.A_EQUIP_EXP_HISTORY
AFTER INSERT
AS
declare @ID as nvarchar(50)
declare @NAME as nvarchar(50)
declare @P_ID as nvarchar(50)
declare @E_ID as nvarchar(50)
declare @P_NAME as nvarchar(50)
declare @E_NAME as nvarchar(50)
declare @MODBY as nvarchar(50)
SELECT @ID = ID,
	@P_ID = PERSON_ID,
	@E_ID = PART_ID,
	@MODBY = MODBY
	FROM INSERTED
SELECT @P_NAME = FULL_NAME FROM A_V_PEOPLE_APPROVED_DATA WHERE ID= @P_ID
SELECT @E_NAME = NAME FROM A_V_PART_DATA_BY_APPROVED_DATA WHERE ID= @E_ID
set @NAME = 'Equipment Experience for ' + isNull(@P_NAME,'NULL') + ' on ' + isNull(@E_NAME,'NULL')
exec A_SP_OBJECT_ADD 'A_EQUIP_EXP_HISTORY',@ID,@NAME,@MODBY,null,null,null


GO

CREATE  TRIGGER A_EQUIP_EXP_HISTORY_UPDATE
ON dbo.A_EQUIP_EXP_HISTORY
AFTER UPDATE
AS
declare @OBJ_ID as nvarchar(50)
declare @ID as nvarchar(50)
declare @NAME as nvarchar(50)
declare @P_ID as nvarchar(50)
declare @E_ID as nvarchar(50)
declare @P_NAME as nvarchar(50)
declare @E_NAME as nvarchar(50)
declare @MODBY as nvarchar(50)
print 'In the Equip Exp History Update Trigger'
SELECT
@ID = ID,
@OBJ_ID = OBJECT_ID,
@P_ID = PERSON_ID,
@E_ID = PART_ID,
@MODBY = MODBY 
FROM INSERTED
SELECT @P_NAME = FULL_NAME FROM A_V_PEOPLE_APPROVED_DATA WHERE ID= @P_ID
SELECT @E_NAME = NAME FROM A_V_PART_DATA_BY_APPROVED_DATA WHERE ID= @E_ID
set @NAME = 'Equipment Experience for ' + isNull(@P_NAME,'NULL') + ' on ' + isNull(@E_NAME,'NULL')

declare @myObjName as nvarchar(1000)
SELECT @myObjName = OBJ_DESC FROM A_OBJECTS WHERE ID = @OBJ_ID
if @myObjName <> @NAME
	begin
	UPDATE A_OBJECTS SET
	OBJ_DESC = @NAME,
	MODBY = @MODBY,
	DRCM = getDATE()
	WHERE ID = @OBJ_ID
	end

