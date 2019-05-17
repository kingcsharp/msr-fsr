echo "Shutdown IIS"
iisreset /stop

echo "Deleting old backup"
rmdir /s /q D:\MSRFSR\answer2-admin.old

echo "Backing up files"
ren D:\MSRFSR\answer2-admin answer2-admin.old

echo "Deleting old backup"
rmdir /s /q D:\MSRFSR\answer2-admin

echo "Creating new directory
md D:\MSRFSR\answer2-admin