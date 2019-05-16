echo "Copy Web.config from old to new"
xcopy /y D:\MSRFSR\answer2-admin.old\Web.config D:\MSRFSR\answer2-admin\

::echo "Creating roslyn directory
::md D:\MSRFSR\answer2-admin\bin\roslyn

::echo "Copy bin\roslyn contents from old to new"
::xcopy /s D:\MSRFSR\answer2-admin_HOLD\bin\roslyn D:\MSRFSR\answer2-admin\bin\roslyn

echo "Start IIS"
iisreset /start