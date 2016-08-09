OpenCover\tools\OpenCover.Console.exe -skipautoprops -register:user -target:NUnitConsole\nunit3-console.exe -register:user "-targetargs:..\src\Domain.UnitTests\bin\Debug\Giacomelli.Unity.Metadata.Domain.UnitTests.dll ..\src\Infrastructure.Framework.UnitTests\bin\Debug\Infrastructure.Framework.UnitTests.dll" -filter:"+[Giacomelli.Unity.Metadata.Domain]* +[Giacomelli.Unity.Metadata.Infrastructure.Framework]*" -output:coverage.xml

ReportGenerator\ReportGenerator.exe -reports:coverage.xml -targetdir:CoverageReport

start CoverageReport\index.htm
