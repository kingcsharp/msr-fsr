ren D:\websites\*.nupkg  Publish.zip
echo a | d:\tools\7-Zip\7z x D:\MSRFSR\Publish.zip -oD:\MSRFSR\answer2-admin\

echo "Copy Web.config from old to new"
xcopy /v D:\MSRFSR\answer2-admin.old\Web.config D:\MSRFSR\answer2-admin\Web.config

echo "Start IIS"
iisreset /start

echo "Delete Publish.zip"
del "D:\MSRFSR\Publish.zip" /s /f /q