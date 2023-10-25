# Pix Microservices Backend

## Sobre o projeto

O projeto **Pix Microservices Backend** é um exemplo de estrutura de **aplicação distribuída** com utilização de **API**, **Gateways** para isolamento das APIs, possui uma implementação funcional do conceito do uso de **CRQS** (Command Query Responsibility Segregation), uso de **VO** (Value Objects) e uso de **Notification Pattern** e muito mais, este projeto tem a intenção de servir de modelo para desenvolvedores tanto iniciantes quanto avançados que desejem desenvolver um projeto corporativos de backend robusto com padrões de projetos bem definidos.

Este modelo de projeto está compatível e pronto para ser conectado com dois tipos de bancos de dados, **SQL Server** e **PostgreSQL**. Tem diversas características e recursos importantes como por exemplo:

- [ ] Filtros dinâmicos (Todos os endpoints tem possibilidade de realizar filtragem dinâmica por qualquer campo da entidade).
- [ ] Paginação (Em todos os endpoints, se não for especificado page e pageSize, por padrão ele define page = 1 e pageSize = 10).
- [ ] Duas opções de conexão a banco de dados (**SQL Server** e **PostgreSQL**)
- [ ] Repositório genérico avançado (Para maximizar a produtividade)
- [ ] Autenticação via JWT Token
- [ ] Entity Framework Core
- [ ] Notification Pattern
- [ ] Asp Net Core 7.0
- [ ] API Gateway


## Arquitetura
![image_a](https://github.com/carlosrogerioinfo/pix-microservices/assets/72615280/bd8c0670-81ea-49ad-8cd7-29652224de2a)


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

```
Update-Database
```

![image](https://github.com/carlosrogerioinfo/pix-microservices/assets/72615280/e26fba4f-f129-4772-9957-0d48cae62f12)

Lembre-se de descomentar a opção do banco de dados para realizar a migration no método **OnConfiguring** no **PixDataContext**, conforme já mencionado acima na seção de configurações iniciais. Depois de executar sua migration, comente novamente a string de conexão, para que ela não sobrescreva a string de conexão que está no appsettings.

![image](https://github.com/carlosrogerioinfo/pix-microservices/assets/72615280/6604321e-35bc-4ef9-8758-fd6ee5ae8954)


## Hospedagem gratuita (Asp.Net Core / SQL Server)

Hospedagem gratuita para aplicações asp.net e asp.net core e com banco de dados sql server incluídos gratuitamente.
<a href="https://somee.com/" target="_blank">https://somee.com/</a>

## Links úteis
<a href="https://www.markdownguide.org/basic-syntax/#overview" target="_blank">Sintaxe Básica Markdown</a>

## Contato
E-mail: carlosrogerio.info@gmail.com <br/>

## Arquitetura

![image_a](https://github.com/carlosrogerioinfo/pix-microservices/assets/72615280/bd8c0670-81ea-49ad-8cd7-29652224de2a)

![image_b](https://github.com/carlosrogerioinfo/pix-microservices/assets/72615280/8fb12b56-f037-475f-9add-f51ebc875ef0)
