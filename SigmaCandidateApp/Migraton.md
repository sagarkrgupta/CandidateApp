## Migration Tips
1) Navigate to your API project folder (or ensure it is the startup project).
	`cd /path/to/your/api/project`
 
2) Run the migration command, specifying the project that contains the ApplicationDBContext
`dotnet ef migrations add InitialCreate --project /path/to/your/dataaccess/library --startup-project /path/to/your/api/project`

3) Apply the Migration to the Database 
`dotnet ef database update --project /path/to/your/dataaccess/library --startup-project /path/to/your/api/project`


---
