::set folder="D:\codedeploy"
::cd /d %folder%
::for /F "delims=" %%i in ('dir /b') do (rmdir "%%i" /s/q || del "%%i" /s/q)

echo "Shutdown IIS"
iisreset /stop

echo "Deleting old backup"
rmdir /s /q D:\websites\answer2-admin.old

echo "Backing up files"
ren D:\websites\answer2-admin answer2-admin.old

echo "Creating new directory
md D:\websites\answer2-admin