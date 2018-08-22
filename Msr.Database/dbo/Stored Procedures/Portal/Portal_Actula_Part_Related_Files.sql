CREATE PROCEDURE [dbo].[Portal_Actula_Part_Related_Files]
@ActualPartId nvarchar(100),
@FileId nvarchar(100),
@ModBy nvarchar(100)
AS BEGIN
INSERT INTO A_ACTUAL_PARTS_RELATED_FILES (ID,ACTUAL_PART_ID,FILE_ID,STATUS,DRCM,MODBY) VALUES (newID(),@ActualPartId,@FileId,'ACTIVE',getDate(),@ModBy)
end 

Go