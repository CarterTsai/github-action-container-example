### Github Action Docker DB Example

### Update DBContext
```
$> cd ./infrastructure
$> dotnet ef dbcontext scaffold "data source=127.0.0.1;initial catalog=testDB;persist security info=True;user id=sa;password=1qaz@WSX;MultipleActiveResultSets=True;" Microsoft.EntityFrameworkCore.SqlServer -c TestDBContext --context-namespace infrastructure --context-dir . --output-dir Models -f
```