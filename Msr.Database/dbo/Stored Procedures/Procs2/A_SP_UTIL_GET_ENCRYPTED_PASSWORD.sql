






CREATE procedure dbo.A_SP_UTIL_GET_ENCRYPTED_PASSWORD
@retPass varchar(50) OUTPUT,
@inPass varchar(50)
as
print 'Encrypting'
declare @x int,@so varchar(50)
set @so = ''
if @inPass = 'passWord'
	set @retPass = @inPass
else
	begin
	print 'here we go'
	set @x = 1
	while @x <= len(@inPass)
		begin
		print 'doing it'
		set @so = @so + char(77 + ascii(substring(@inPass,@x,1)) % 128)
		set @x = @x + 1
		end
	set @retPass = @so

	end








