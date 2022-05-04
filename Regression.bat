rem cd D:\Automation\dnata.travelrepublic.mobileweb\Dnata.TravelRepublic.MobileWeb.UI.Tests
rem json -I -f appsettings.json -e "this.Environment='Prod'"

rem dotnet test --filter TestCategory=hotelregression  "C:\Automation\dnata.travelrepublic.mobileweb\Dnata.TravelRepublic.MobileWeb.UI.Tests" "--where:MobileOS=IOS" --logger trx
rem dotnet test --filter TestCategory=hotelregression  "C:\Automation\dnata.travelrepublic.mobileweb\Dnata.TravelRepublic.MobileWeb.UI.Tests" --logger trx
rem dotnet test --filter TestCategory=holidayregression  "C:\Automation\dnata.travelrepublic.mobileweb\Dnata.TravelRepublic.MobileWeb.UI.Tests" --logger trx

rem dotnet test --filter "TestCategory=v3regression123&TestCategory!=NotIE" "d:\Automation\dnata.travelrepublic.mobileweb\Dnata.TravelRepublic.MobileWeb.UI.Tests" --logger trx
rem SET Environment=BugFix

call CleanSystemAndServices.bat
SETLOCAL
set datetimef=%date:~0,2%%date:~3,2%%date:~6,4%
set reportportal_enabled=true
SET Environment=PreProd
REM set ukcat="TestCategory=v3regression&TestCategory!=DesktopOnly"
REM set iecat="TestCategory=v3regression&TestCategory!=DesktopOnly&TestCategory!=UKOnly"
SET isSaveResultsToTestRail=false
REM set Browser=Chrome_Mobile

REM SET Domain=UK
REM SET DeviceName=iPhone X
REM SET TestRunId=0
REM set reportportal_launch_name=%datetimef%_%Environment%_Mobile_V3_%Domain%_%DeviceName%
REM dotnet test --filter %ukcat% "C:\Automation\Dnata.TravelRepublic.MobileWeb\Dnata.TravelRepublic.MobileWeb.UI.Tests" --logger trx

REM Timeout /T 90

REM SET Domain=IE
REM SET DeviceName=Pixel 2
REM SET TestRunId=0
REM set reportportal_launch_name=%datetimef%_%Environment%_Mobile_V3_%Domain%_%DeviceName%
REM dotnet test --filter %iecat% "C:\Automation\Dnata.TravelRepublic.MobileWeb\Dnata.TravelRepublic.MobileWeb.UI.Tests" --logger trx

set ukcat="TestCategory=v3regression&TestCategory!=MobileOnly"
set iecat="TestCategory=v3regression&TestCategory!=holidayregression&TestCategory!=UKOnly&TestCategory!=MobileOnly"
set Browser=Chrome

set Domain=UK
SET TestRunId=0
set reportportal_launch_name=%datetimef%_%Environment%_Adaptive_V3_%Domain%
dotnet test --filter %ukcat% "E:\Automation\Dnata.TravelRepublic.MobileWeb\Dnata.TravelRepublic.MobileWeb.UI.Tests" --logger trx

set Domain=IE
SET TestRunId=0
set reportportal_launch_name=%datetimef%_%Environment%_Adaptive_V3_%Domain%
dotnet test --filter %iecat% "E:\Automation\Dnata.TravelRepublic.MobileWeb\Dnata.TravelRepublic.MobileWeb.UI.Tests" --logger trx