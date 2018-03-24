

/*
STORED PROCEDURE CALLED IN : discussion/replyDicussions.asp
*/
CREATE     PROCEDURE A_SP_DISCUSSION_COLOR_LIST
@discussionID varchar(50),
@strNTLogin varchar(50)
AS
declare @myColor varchar(50)

if @discussionID is null
begin
   print 'This is the opening statment color list'
   SELECT RGB_CODE, NAME
	FROM A_COLORS
    ORDER BY NUM
goto finished
end

print 'doing response color processing'
SELECT @myColor = COLOR  
FROM A_DISCUSSION_COLORS
WHERE DISCUSSION_ID = @discussionID AND PERSON_ID = @strNTLogin 
AND COLOR IN (SELECT RGB_CODE FROM A_COLORS)

print 'found my color ' + @myColor
if @myColor is null
begin
	print '@myColor is null so getting the colors noone used'
	SELECT RGB_CODE, NAME
	FROM A_COLORS
	WHERE RGB_CODE NOT IN 
	(SELECT COLOR FROM A_DISCUSSION_COLORS WHERE DISCUSSION_ID = @discussionID) 
	ORDER BY NUM
    goto finished
end 
begin 
    print 'i have a color so getting myColor'
	SELECT RGB_CODE, NAME
	FROM A_COLORS
	WHERE  RGB_CODE= @myColor
	ORDER BY NUM
end 
finished: