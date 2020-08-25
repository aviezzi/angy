Set-Location -Path ..\..\Angy.BackEnd.Kharonte.Data

dotnet ef --startup-project ..\Angy.BackEnd.Kharonte migrations add AddPendingPhotosColumns
dotnet ef --startup-project ..\Angy.BackEnd.Kharonte database update

Set-Location -Path ..\Angy.BackEnd.Kharonte\Scripts