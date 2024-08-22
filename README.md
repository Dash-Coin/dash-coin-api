# coin-api

### docker-compose
Para subir o docker use:
`docker-compose up -d`
Para remover os contêineres e os recursos criados use:
`docker-compose down`


### migrations
Certifique-se que está insalado o dotnet ef, se não estiver instalado pode use o comando
`dotnet tool install --global dotnet-ef`

Com ele instalado rode o comando 
`dotnet ef migrations add <nome-migration>`

Agora com a migration pode dar update no banco de dados com:
`dotnet ef database update`

### dotnet
Para executar o projeto use: 
`dotnet run`

Para executar o projeto em WatchMode use: 
`dotnet watch run`

