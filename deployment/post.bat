echo "Copy Web.config from old to new"
xcopy /y D:\MSRFSR\answer2-admin.old\Web.config D:\MSRFSR\answer2-admin\

echo "Start IIS"
iisreset /start