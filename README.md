dotnet ef migrations add InitialCreate --project CondoApp.Data   --startup-project CondoApp.WebApi

dotnet ef database update --project CondoApp.Data   --startup-project CondoApp.WebApi

