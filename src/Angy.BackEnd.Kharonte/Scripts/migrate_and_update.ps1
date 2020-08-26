Set-Location -Path ..\..\Angy.BackEnd.Kharonte.Data

dotnet ef --startup-project ..\Angy.BackEnd.Kharonte migrations add AddNodaTime 
dotnet ef --startup-project ..\Angy.BackEnd.Kharonte database update

#dotnet ef --startup-project ..\Angy.BackEnd.Kharonte migrations remove

Set-Location -Path ..\Angy.BackEnd.Kharonte\Scripts