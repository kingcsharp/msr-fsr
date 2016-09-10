

CREATE procedure A_SP_MONITOR_CHOICES_UPDATE_CHOICE
@ID nvarchar(50),
@TXT nvarchar(50),
@CNT nvarchar(50),
@IS_ANSWER nvarchar(50),
@strNTLogin nvarchar(50)
AS
UPDATE A_MONITOR_TEMPLATES_MULT_CHOICE SET
TXT = @TXT,
ORD = @CNT,
IS_ANSWER = @IS_ANSWER,
DRCM = getDate(),
MODBY = @strNTLogin
WHERE ID = @ID



