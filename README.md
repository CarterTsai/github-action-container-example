### Github Action Docker DB Example

>>> 此範例主要是目的是Push Code到Github的時候可以在Github Action會建立測試用的DB來做專案的Unit Test。
>>> 更多說明 https://hamisme.blogspot.com/2021/12/githubaction-github-action.html

### Create docker-compose.yaml
此專案的範例是使用MSSQL2019

```yaml
version: '3.9'

services:
  MSSQL2019Test:
    image: mcr.microsoft.com/mssql/server:latest
    restart: always
    container_name: MSSQL2019Test
    ports:
      - 1433:1433
    volumes:
      - db-test:/var/opt/mssql
    environment:
      - ACCEPT_EULA=Y
      - SA_PASSWORD=1qaz@WSX
      - MSSQL_COLLATION=Chinese_Taiwan_Stroke_CI_AS
    networks:
      - db

volumes:
  db-test:

networks:
  db:
```

### Create Github Action Workflows dotnet.yml
建立github action的檔案，位置在/.github/workflows/dotnet.yml
```yaml
name: dotnet package

on: [push]

jobs:
  build:
    timeout-minutes: 10
    runs-on: ubuntu-latest
    
    strategy:
      matrix:
        dotnet-version: ['6.0.x' ]

    steps:
      - uses: actions/checkout@v2
      - name: Start containers
        working-directory: ./docker/
        run: docker-compose -f "docker-compose.yml" up -d --build
      
      - name: Install mssql tools 
        run: sudo apt-get install mssql-tools

      - name: Install mssql tools path 
        run: echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bash_profile

      - name: Create Database
        working-directory: ./sql/
        run: sqlcmd -S localhost -U sa -P 1qaz@WSX -i create_db.sql

      - name: Create table
        working-directory: ./sql/
        run: sqlcmd -S localhost -U sa -P 1qaz@WSX -d testDB -i create_table.sql
        
      - name: Setup .NET Core SDK ${{ matrix.dotnet-version }}
        uses: actions/setup-dotnet@v1.7.2
        with:
          dotnet-version: ${{ matrix.dotnet-version }}

      - name: Install dependencies
        run: dotnet restore github-action-container-example

      - name: Build
        run: dotnet build --configuration Release --no-restore github-action-container-example
      - name: Test
        run: dotnet test --no-restore --verbosity normal github-action-container-example

      - name: Stop containers
        working-directory: ./docker/
        if: always()
        run: docker-compose -f "docker-compose.yml" down
```

### Update DBContext
```
$> cd ./infrastructure
$> dotnet ef dbcontext scaffold "data source=127.0.0.1;initial catalog=testDB;persist security info=True;user id=sa;password=1qaz@WSX;MultipleActiveResultSets=True;" Microsoft.EntityFrameworkCore.SqlServer -c TestDBContext --context-namespace infrastructure --context-dir . --output-dir Models -f
```

### Reference
* https://docs.github.com/en/actions/creating-actions/creating-a-docker-container-action
* https://docs.microsoft.com/zh-tw/sql/tools/sqlcmd-utility?view=sql-server-ver15

### Test
