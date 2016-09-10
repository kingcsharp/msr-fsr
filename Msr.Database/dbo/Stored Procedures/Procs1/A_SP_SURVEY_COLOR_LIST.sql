

/*
STORED PROCEDURE CALLED IN : surveys/replySurveys.asp
*/
CREATE    PROCEDURE A_SP_SURVEY_COLOR_LIST
@surveyID varchar(50),
@strNTLogin varchar(50)
AS
declare @myColor varchar(50)

print 'doing reply color processing'

SELECT @myColor = COLOR 
FROM A_SURVEY_COLORS
WHERE SURVEY_ID = @surveyID AND PERSON_ID = @strNTLogin
AND COLOR IN (SELECT RGB_CODE FROM A_COLORS)

print 'found my color ' + @myColor

if @myColor is null
begin
	print '@myColor is null so getting the colors noone used for that survey'
	SELECT RGB_CODE, NAME
	FROM A_COLORS
	WHERE RGB_CODE NOT IN 
	(SELECT COLOR FROM A_SURVEY_COLORS WHERE SURVEY_ID = @surveyID)
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

