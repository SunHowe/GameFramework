set WORKSPACE=..
set LUBAN_DLL=%WORKSPACE%\Tools\Luban\Luban.dll
set CONF_ROOT=.

set OUTPUT_DATA_DIR_SERVER=output_server
set OUTPUT_CODE_DIR_SERVER=output_code_server

set OUTPUT_DATA_DIR_CLIENT=%WORKSPACE%\Client\Assets\GameMain\DataTable
set OUTPUT_CODE_DIR_CLIENT=%WORKSPACE%\Client\Assets\GameMain\Scripts\GameLogic\DataTable\AutoGen

dotnet %LUBAN_DLL% ^
    -t client ^
    -c cs-gf-bin ^
    -d bin ^
    --conf %CONF_ROOT%\luban.conf ^
    -x outputDataDir=%OUTPUT_DATA_DIR_CLIENT% ^
    -x outputCodeDir=%OUTPUT_CODE_DIR_CLIENT%

@REM dotnet %LUBAN_DLL% ^
@REM     -t server ^
@REM     -c cs-bin ^
@REM     -d bin ^
@REM     --conf %CONF_ROOT%\luban.conf ^
@REM     -x outputDataDir=%OUTPUT_DATA_DIR_SERVER% ^
@REM     -x outputCodeDir=%OUTPUT_CODE_DIR_SERVER%

pause