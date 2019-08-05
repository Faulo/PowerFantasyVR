set project=%cd%
set unity="C:\Unity\2019.2.0b4\Editor\Unity.exe"
recycle %cd%\Build
mkdir %cd%\Build
%unity% -quit -accept-apiupdate -batchmode -nographics -logFile build.log -projectPath %project% -buildWindows64Player %project%\Build\PowerFantasyVR.exe