Remove-Item -Recurse -Force .\CondoApp.Data\Persistence\Migrations


dotnet ef migrations add InitialCreate `
  --project CondoApp.Data `
  --startup-project CondoApp.WEBAPI `
  --output-dir Data\Migrations


dotnet ef database update `
  --project CondoApp.Data `
  --startup-project CondoApp.WEBAPI


dotnet run  --project webapi  -o -watch