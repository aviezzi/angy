Set-Location -Path ..\..\Angy.Core

dotnet ef --startup-project ../angy.Server/ migrations add Initial
dotnet ef --startup-project ../angy.Server/ database update

Set-Location -Path ..\Angy.Server\Scripts