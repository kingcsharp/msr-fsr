
CREATE  PROCEDURE A_SP_PROJECT_ADD_REMOVE_EXISTING_ITEMS
@newID varchar(50) OUTPUT,
@msg varchar(8000) OUTPUT,
@projID varchar(50),
@tasks varchar(4000),
@meetings varchar(4000),
@discussions varchar(4000),
@surveys varchar(4000),
@projects varchar(4000),
@messages varchar(4000),
@strNTLogin varchar(50)
as
exec A_SP_PROJECT_ITEM_UPDATE @tasks,@projID,'A_TASKS',@strNTLogin
exec A_SP_PROJECT_ITEM_UPDATE @meetings,@projID,'A_MEETINGS',@strNTLogin
exec A_SP_PROJECT_ITEM_UPDATE @discussions,@projID,'A_DISCUSSIONS',@strNTLogin
exec A_SP_PROJECT_ITEM_UPDATE @surveys,@projID,'A_SURVEYS',@strNTLogin
exec A_SP_PROJECT_ITEM_UPDATE @projects,@projID,'A_PROJECTS',@strNTLogin
exec A_SP_PROJECT_ITEM_UPDATE @messages,@projID,'A_MESSAGES',@strNTLogin