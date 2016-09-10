


CREATE   proc A_SP_Z_SPLIT
@i varchar(8000), --input string
@del varchar(8000) = ',' --delimiter
as
set nocount on
declare 
@p int,
@p2 int,
@tok varchar(8000)

CREATE TABLE #TempItems
	(
	IT varchar(1000)
	)

set @p=1

if (CHARINDEX(@del,@i,@p)=0)
begin
  exec('INSERT INTO #TempItems (IT) values (ltrim('''+@i+'''))')
end
else
begin
  while CHARINDEX(@del,@i,@p)>0
  begin
    set @p2 = CHARINDEX(@del,@i,@p)
    set @tok = SUBSTRING(@i,@p,@p2-@p)
    if Len(@tok)>0
      exec('INSERT INTO  #TEMPITEMS (IT) values (ltrim('''+@tok+'''))')
    set @p = @p2+Len(@del)
  end
  if @p<=Len(@i)
  begin
    set @tok = SUBSTRING(@i,@p,Len(@i)-(@p-1))
      exec('INSERT INTO  #TEMPITEMS (IT) values (ltrim('''+@tok+'''))')
  end
end
SELECT * FROM #TEMPITEMS
set nocount off




