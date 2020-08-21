Set-Location -Path ..\..\Angy.BackEnd.Kharonte.Data

dotnet ef --startup-project ..\Angy.BackEnd.Kharonte migrations add InitialMigration
dotnet ef --startup-project ..\Angy.BackEnd.Kharonte database update

Set-Location -Path ..\Angy.BackEnd.Kharonte\Scripts