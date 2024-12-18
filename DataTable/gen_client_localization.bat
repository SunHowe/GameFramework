set WORKSPACE=..
set LUBAN_DLL=%WORKSPACE%\Tools\Luban\Luban.dll
set LUBAN_TEMPLATE_DIR=%WORKSPACE%\Tools\LubanClientTemplate
set CONF_ROOT=.

set OUTPUT_DATA_DIR_LAUNCHER=%WORKSPACE%\Client\Assets\GameLauncher\Localization
set OUTPUT_DATA_DIR_MAIN=%WORKSPACE%\Client\Assets\GameMain\Localization

dotnet %LUBAN_DLL% ^
    -t client ^
    -d bin ^
    --conf %CONF_ROOT%\luban_localization_launch.conf ^
    -x outputDataDir=%OUTPUT_DATA_DIR_LAUNCHER%

dotnet %LUBAN_DLL% ^
    -t client ^
    -d bin ^
    --conf %CONF_ROOT%\luban_localization.conf ^
    -x outputDataDir=%OUTPUT_DATA_DIR_MAIN%

pause