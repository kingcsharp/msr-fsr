::ren D:\websites\*.nupkg  Publish.zip
::echo a | d:\tools\7-Zip\7z x D:\websites\publish.zip -oD:\websites\answer2-admin\

echo "Rename Answer.Web to answer2-admin"
ren D:\Answer.Web answer2-admin

echo "Copy Web.config from old to new"
xcopy /y D:\websites\answer2-admin.old\Web.config D:\websites\answer2-admin\

echo "Start IIS"
iisreset /start

::echo "Delete Publish.zip"
::del "D:\websites\Publish.zip" /s /f /q