# hotelreservationapi

## migrations

### Nugget package manager console (selecting infrastructure project)

Add-Migration <NameOfMigration> -Context DatabaseContext -OutputDir Persistence/Migrations

Update-Database -Context DatabaseContext

Remove-Migration -c DatabaseContext

Script-Migration -Context DatabaseContext

### Powershell from solution dir

dotnet-ef migrations add <NameOfMigration> -c DatabaseContext --project Infrastructure --startup-project WebApi/ --output-dir Persistence/Migrations

dotnet-ef migrations remove -c DatabaseContext --project Infrastructure --startup-project WebApi/

dotnet-ef database update -c DatabaseContext --project Infrastructure --startup-project WebApi/

dotnet ef migrations script -c DatabaseContext

### Docker
Database image
```
docker run --name databasev1 -p 2000:5432 -e POSTGRES_PASSWORD=2209 -d postgres:15
```


```
docker build . -t demo
docker run -p 6969:5093 -e ASPNETCORE_URLS=http://+80 demo:v1
```

```
docker-compose down --rmi all
```