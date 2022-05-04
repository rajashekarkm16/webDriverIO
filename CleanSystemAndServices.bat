
Rem : Clear cache
rem del /q/f/s %TEMP%\*
rem del /q/f/s %windir%\temp\*

Rem : Kill all existing tasks
taskkill /F /IM chromedriver.exe
taskkill /F /IM geckodriver.exe
taskkill /F /IM chrome.exe
taskkill /F /IM chrome_screenster.exe
taskkill /F /IM firefox.exe
taskkill /F /IM firefox_screenster.exe
taskkill /F /IM devenv.exe
rem taskkill /F /IM sourcetree.exe
taskkill /F /IM teams.exe
rem taskkill /F /IM slack.exe