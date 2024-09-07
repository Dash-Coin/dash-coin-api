# coin-api

### docker-compose
Para subir o docker use: </br>
`docker-compose up -d` 

Para remover os contêineres e os recursos criados use: </br>
`docker-compose down`

Para acessar database pelo CMD: </br>
`docker exec -it dash-coin-api-db-1 psql -U <user> -d <nome-do-banco>`


### migrations
Certifique-se que está insalado o dotnet ef, se não estiver instalado pode use o comando </br>
`dotnet tool install --global dotnet-ef`

Com ele instalado rode o comando </br>
`dotnet ef migrations add <nome-migration>`

Criar projeto da sulotion
`dotnet build coin-api.generated.sln`

Agora com a migration pode dar update no banco de dados com: </br>
`dotnet ef database update`


### dotnet
Para executar o projeto use: </br>
`dotnet run`

Para executar o projeto em WatchMode use: </br>
`dotnet watch run`

