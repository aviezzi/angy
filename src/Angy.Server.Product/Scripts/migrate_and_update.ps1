Set-Location -Path ..\..\Angy.Server.Data

dotnet ef --startup-project ..\angy.Server.Product migrations add RenameMicroCategoryToCategory
dotnet ef --startup-project ..\angy.Server.Product database update

Set-Location -Path ..\Angy.Server.Product\Scripts