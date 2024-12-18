set WORKSPACE=..
set LUBAN_DLL=%WORKSPACE%\Tools\Luban\Luban.dll
set LUBAN_TEMPLATE_DIR=%WORKSPACE%\Tools\LubanClientTemplate
set CONF_ROOT=.

set OUTPUT_DATA_DIR_LAUNCHER=%WORKSPACE%\Client\Assets\GameLauncher\DataTable
set OUTPUT_CODE_DIR_LAUNCHER=%WORKSPACE%\Client\Assets\GameMain\Scripts\GameMono\DataTable\AutoGen

dotnet %LUBAN_DLL% ^
    --customTemplateDir %LUBAN_TEMPLATE_DIR% ^
    -t launcher ^
    -d bin ^
    --conf %CONF_ROOT%\luban_localization.conf ^
    -x outputDataDir=%OUTPUT_DATA_DIR_LAUNCHER% ^
    -x outputCodeDir=%OUTPUT_CODE_DIR_LAUNCHER%

pause