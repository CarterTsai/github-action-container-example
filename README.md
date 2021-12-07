### Github Action Docker DB Example

>>> 此範例主要是目的是Push Code到Github的時候可以在Github Action會建立測試用的DB來做專案的Unit Test。


### Update DBContext
```
$> cd ./infrastructure
$> dotnet ef dbcontext scaffold "data source=127.0.0.1;initial catalog=testDB;persist security info=True;user id=sa;password=1qaz@WSX;MultipleActiveResultSets=True;" Microsoft.EntityFrameworkCore.SqlServer -c TestDBContext --context-namespace infrastructure --context-dir . --output-dir Models -f
```
