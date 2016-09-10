



CREATE    procedure A_SP_PAGE_HITS
	@strNTLogin as nvarchar(50),
	@url as nvarchar(500),
	@qs as varchar(200)
as

declare @tester as numeric
SELECT @tester = NUM FROM HITS WHERE ID = @url
if @tester is null
	begin
	INSERT INTO HITS (ID,LAST_USER,DATETIME,NUM) 
	VALUES(@url,@strNTLogin,getDate(),1)
	end
else
	begin
	UPDATE HITS SET 
	NUM = NUM + 1, 
	DATETIME = getDate(), 
	[LAST_USER] = @strNTLogin 
	WHERE ID = @url
	end
if @strNTLogin is not null
	begin
	INSERT INTO A_PEOPLE_PAGE_TRACKING (ID,USER_ID,PAGE,DRCM,QUERYSTRING)
		VALUES(newID(),@strNTLogin,@url,getDate(),@qs)
	end






select NUM FROM HITS WHERE ID = @URL







