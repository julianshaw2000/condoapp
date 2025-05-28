dotnet ef migrations add InitialCreate --project CondoApp.Data   --startup-project CondoApp.WebApi

dotnet ef database update --project CondoApp.Data   --startup-project CondoApp.WebApi

2️⃣ Open the generated migration file and replace this block:
 
migrationBuilder.AddForeignKey(
    name: "FK_Apartments_AspNetUsers_OwnerId",
    table: "Apartments",
    column: "OwnerId",
    principalTable: "AspNetUsers",
    principalColumn: "Id",
    onDelete: ReferentialAction.SetNull);


// replace With a raw SQL command:

 
migrationBuilder.Sql(@"
ALTER TABLE ""Apartments""
    DROP CONSTRAINT IF EXISTS ""FK_Apartments_AspNetUsers_OwnerId"",
    ADD CONSTRAINT ""FK_Apartments_AspNetUsers_OwnerId""
    FOREIGN KEY (""OwnerId"") REFERENCES ""AspNetUsers"" (""Id"") ON DELETE SET NULL;
");