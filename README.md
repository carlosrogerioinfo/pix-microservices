# Pix Microservices Backend

## Sobre o projeto

O projeto **Pix Microservices Backend** é um exemplo de estrutura de **aplicação distribuída** com utilização de **API**, **Gateways** para isolamento das APIs, possui uma implementação funcional do conceito do uso de **CRQS** (Command Query Responsibility Segregation), uso de **VO** (Value Objects) e uso de **Notification Pattern** e muito mais, este projeto tem a intenção de servir de modelo para desenvolvedores tanto iniciantes quanto avançados que desejem desenvolver um projeto corporativos de backend robusto com padrões de projetos bem definidos.

Este modelo de projeto está compatível e pronto para ser conectado com dois tipos de bancos de dados, **SQL Server** e **PostgreSQL**. Tem diversas características e recursos importantes como por exemplo:

- [ ] Paginação (Se não for especificado page e pageSize, por padrão ele define page = 1 e pageSize = 10).
- [ ] Filtros dinâmicos (Realiza filtragem dinâmica por qualquer campo da entidade).
- [ ] Duas opções de conexão a banco de dados (**SQL Server** e **PostgreSQL**)
- [ ] Repositório genérico avançado (Para maximizar a produtividade)
- [ ] Autenticação via JWT Token
- [ ] Entity Framework Core
- [ ] Notification Pattern
- [ ] Asp Net Core 7.0
- [ ] API Gateway


## Arquitetura
![image_a](https://github.com/carlosrogerioinfo/pix-microservices/assets/72615280/ba4b790f-cf8f-4911-8aa8-a3baf829dca1)


## Configurações iniciais

Este modelo de projeto baseado no conceito de microsserviços está compatível e pronto para ser conectado com dois tipos de bancos de dados, **SQL Server** e **PostgreSQL**. Para isso é necessário que algumas configurações iniciais e ações sejam realizadas. São elas:

- [ ] Setar o valor da chave **UseSqlServer** para **true**, caso use o banco de dados **SQL Server**.
- [ ] Setar o valor da chave **UseSqlServer** para **false**, caso use o banco de dados **PostgreSQL**.
- [ ] Informar o valor da connection strings na chave **DatabaseConnection**.
- [ ] Alterar as constantes de tipos de dados
- [ ] Descomentar a opção do banco de dados para realizar a migration no método **OnConfiguring** no **PixDataContext**.

### Configurando o arquivo appsettings

#### Para SQL Server

```
"UseSqlServer": "true",
"ConnectionStrings": {
  "DatabaseConnection": "Server=url_server;Database=database_name;User ID=username;Password=password;"
}
```

#### Para PostgreSQL

```
"UseSqlServer": "false",
"ConnectionStrings": {
  "DatabaseConnection": "Host=url_server;Port=port;Pooling=true;Database=database_name;User Id=username;Password=password;"
}
```

### Alterando constantes de tipos de dados

#### Para Sql Server
Pressionar as teclas CTRL + SHIFT + H, para realizar uma busca e substituição em todo o projeto das seguintes expressões

```
(Constants.BooleanPostgreSql)  => (Constants.Boolean)
(Constants.DateTimePostgreSql) => (Constants.DateTime)
(Constants.DateTimePostgreSql) => (Constants.DateTime)
```

#### Para PostgreSql
Pressionar as teclas CTRL + SHIFT + H, para realizar uma busca e substituição em todo o projeto das seguintes expressões

```
(Constants.Boolean)  => (Constants.BooleanPostgreSql)
(Constants.DateTime) => (Constants.DateTimePostgreSql)
(Constants.DateTime) => (Constants.DateTimePostgreSql)
```

### Configurando o contexto para migration

#### Para SQL Server

```
protected override void OnConfiguring(DbContextOptionsBuilder options)
{
    /* Descomentar abaixo quando precisar gerar a migration ou realizar alguma alteração no banco de dados */

    //SQL Server 
    options.UseSqlServer("Server=url_server;Database=database_name;User ID=username;Password=password;");

    //Postgre SQL
    //options.UseNpgsql("Host=url_server;Port=port;Pooling=true;Database=database_name;User Id=username;Password=password;");
}
```

#### Para Postgre SQL

```
protected override void OnConfiguring(DbContextOptionsBuilder options)
{
    /* Descomentar apenas quando precisar gerar a migration ou realizar alguma alteração no banco de dados */

    //SQL Server 
    //options.UseSqlServer("Server=url_server;Database=database_name;User ID=username;Password=password;");

    //Postgre SQL
    options.UseNpgsql("Host=url_server;Port=port;Pooling=true;Database=database_name;User Id=username;Password=password;");
}
```

### Criando o banco de dados pela primeira vez

Para criarmos o banco de dados pela primeira vez, ou para alterarmos o banco de dados, é necessário executar migration, para isso, você deve setar o projeto de infraestrutura como Set as Startup Project e no Package Manager Console setar o projeto de infraestrutura no dropdown de seleção, após feito isso, execute os seguintes comandos na ordem abaixo:

```
Add-Migration start-database
```
![image_d1-1](https://github.com/carlosrogerioinfo/pix-microservices/assets/72615280/659806d6-828c-4d25-b847-91f59f9e1f5f)

Se deu tudo certo você deverá ver algo como a imagem a seguir
![image_d2-2](https://github.com/carlosrogerioinfo/pix-microservices/assets/72615280/ddbe8743-8e32-450e-b814-3e607e9713b4)

```
Update-Database
```
![image_d3-3](https://github.com/carlosrogerioinfo/pix-microservices/assets/72615280/168c6736-f71f-44ff-913c-e493a41d5f24)

Se deu tudo certo você deverá ver algo como a imagem a seguir
![image_d4-4](https://github.com/carlosrogerioinfo/pix-microservices/assets/72615280/cd4f450e-7e94-417d-858d-2e7090b82048)

Use o comando abaixo caso deseje gerar um **script de banco de dados** ao invés de criar ou aplicar as alterações diretamente no banco de dados com o comando **Update-Database**
```
Script-Migration
```

Lembre-se de descomentar a opção do banco de dados para realizar a migration no método **OnConfiguring** no **PixDataContext**, conforme já mencionado acima na seção de configurações iniciais. Depois de executar sua migration, comente novamente a string de conexão, para que ela não sobrescreva a string de conexão que está no appsettings.

![image_d](https://github.com/carlosrogerioinfo/pix-microservices/assets/72615280/03a1d708-9fe1-4e14-82a7-22a5dd5e176e)


## Hospedagem gratuita (Asp.Net Core / SQL Server)

Hospedagem gratuita para aplicações asp.net e asp.net core e com banco de dados sql server incluídos gratuitamente.
<a href="https://somee.com/" target="_blank">https://somee.com/</a>

## Links úteis
<a href="https://www.markdownguide.org/basic-syntax/#overview" target="_blank">Sintaxe Básica Markdown</a>

## Contato
E-mail: carlosrogerio.info@gmail.com <br/>

## Arquitetura

![image_a](https://github.com/carlosrogerioinfo/pix-microservices/assets/72615280/ba4b790f-cf8f-4911-8aa8-a3baf829dca1)

![image_b](https://github.com/carlosrogerioinfo/pix-microservices/assets/72615280/383812dd-1086-45bf-a53a-91c051c4d303)

