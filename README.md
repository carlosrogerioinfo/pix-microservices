# Pix Microservices

Sistema de pagamentos Pix implementado com arquitetura de microsservicos em .NET 7.

## Stack Tecnologica

| Categoria | Tecnologia |
|-----------|-----------|
| Framework | ASP.NET Core 7.0 |
| ORM | Entity Framework Core 7.0 |
| Banco de Dados | SQL Server 2022 / PostgreSQL |
| Autenticacao | JWT Bearer |
| Logging | Serilog (Console + File) |
| Resiliencia | Polly (Retry + Circuit Breaker) |
| Documentacao | Swagger / OpenAPI |
| Mapeamento | AutoMapper 12 |
| Senhas | BCrypt.Net-Next (work factor 12) |
| Mensageria | MediatR 12 (behaviors logging + performance) |
| Versionamento | Asp.Versioning.Mvc 7.1.1 |
| Testes | xUnit + Moq + FluentAssertions |
| Containers | Docker + Docker Compose |

## Arquitetura

```
pix-microservices/
└── src/
    ├── building blocks/
    │   ├── Pix.Microservices.Core        # JWT, Serilog, Middleware, BCrypt, MediatR behaviors, Versioning, HealthChecks
    │   ├── Pix.Microservices.Domain      # Entidades, Repositorios (interfaces), DTOs, Paginacao
    │   └── Pix.Microservices.Infrastructure  # EF Core, Repositorios (impl), DbContext, IDesignTimeDbContextFactory
    ├── services/
    │   ├── Pix.Banks.Api       :7221     # Bancos, Contas, Transacoes, Historico
    │   ├── Pix.Companies.Api   :7222     # Empresas e vinculos
    │   ├── Pix.Devices.Api     :7223     # Dispositivos
    │   ├── Pix.Users.Api       :7224     # Usuarios e autenticacao
    │   └── Pix.Gateway.Api     :7176     # API Gateway (agrega todos os servicos)
    └── tests/
        ├── Pix.Microservices.UnitTests
        └── Pix.Microservices.IntegrationTests
```

## Rotas da API (v1)

### Banks API (porta 7221)
| Metodo | Rota | Descricao |
|--------|------|-----------|
| GET | /bank | Listar bancos |
| GET | /bank/{id} | Buscar banco por ID |
| POST | /bank | Criar banco |
| PUT | /bank | Atualizar banco |
| DELETE | /bank/{id} | Remover banco |
| GET | /bank-account | Listar contas bancarias |
| GET | /bank-account/{id} | Buscar conta por ID |
| POST | /bank-account | Criar conta |
| PUT | /bank-account | Atualizar conta |
| DELETE | /bank-account/{id} | Remover conta |
| GET | /bank-transactions/{companyid} | Listar transacoes por empresa |
| GET | /bank-transaction-history | Listar historico de transacoes |

### Users API (porta 7224)
| Metodo | Rota | Descricao |
|--------|------|-----------|
| GET | /user | Listar usuarios |
| GET | /user/{id} | Buscar usuario por ID |
| POST | /user | Criar usuario |
| PUT | /user | Atualizar usuario |
| DELETE | /user/{id} | Remover usuario |

### Health Checks (todos os servicos)
| Endpoint | Descricao |
|----------|-----------|
| GET /health | Status completo com todos os checks |
| GET /health/ready | Prontidao (verifica banco de dados) |
| GET /health/live | Liveness (sempre 200 se o processo responde) |

## Features Senior

### Logging estruturado (Serilog)
- Bootstrap logger com fallback para console
- Request logging com nivel dinamico baseado em status code e duracao
- Sink Console + File com rolling diario
- Enrichment com nome do servico e maquina

### Seguranca (BCrypt)
- Senhas hasheadas com work factor 12
- Verificacao em memoria (nunca comparar hash no banco)
- Fallback para usuarios legados sem senha

### Resiliencia (Polly)
- Retry com backoff exponencial (3 tentativas: 2s, 4s, 8s) no Gateway
- Circuit Breaker (abre apos 5 falhas, fecha apos 30s)

### Global Exception Handler
- Middleware centralizado que captura todas as excecoes
- Resposta padronizada em application/problem+json
- Mapeamento por tipo: KeyNotFoundException 404, UnauthorizedAccessException 401, ArgumentException 400

### MediatR Behaviors
- LoggingBehavior: loga entrada, saida e duracao de cada request
- PerformanceBehavior: alerta para requests maiores que 500ms

### API Versioning
- Versao default 1.0 assumida automaticamente
- Suporte a URL segment, header X-Api-Version e query string api-version

### Secrets via variaveis de ambiente
- DATABASE_CONNECTION sobrepos ConnectionStrings:DatabaseConnection
- JWT_SECRET_KEY sobrepos JwtSettings:SecretKey
- UseSqlServer suporta SQL Server ou PostgreSQL

## Como Executar

### Com Docker (recomendado)

```bash
docker-compose up -d
```

Acesse o Gateway em: http://localhost:5000/swagger

Os servicos ficam disponiveis em:
- Gateway: http://localhost:5000
- Banks: http://localhost:5001
- Companies: http://localhost:5002
- Devices: http://localhost:5003
- Users: http://localhost:5004

### Sem Docker (local)

#### Pre-requisitos
- .NET 7 SDK
- PostgreSQL ou SQL Server

#### Variaveis de ambiente
```powershell
$env:DATABASE_CONNECTION = "Host=localhost;Port=5432;Database=pix_db;User Id=postgres;Password=postgres;"
$env:JWT_SECRET_KEY = "sua-chave-secreta-aqui-minimo-32-chars"
```

#### Executar servicos (terminais separados)
```bash
dotnet run --project "src/services/Pix.Banks.Api"
dotnet run --project "src/services/Pix.Companies.Api"
dotnet run --project "src/services/Pix.Devices.Api"
dotnet run --project "src/services/Pix.Users.Api"
dotnet run --project "src/services/Pix.Gateway.Api"
```

## Banco de Dados

Ver [MIGRATIONS.md](MIGRATIONS.md) para comandos de migration.

## Testes

```bash
# Unit Tests (11 testes)
dotnet test "src/tests/Pix.Microservices.UnitTests"

# Integration Tests
dotnet test "src/tests/Pix.Microservices.IntegrationTests"

# Todos os testes
dotnet test Pix.Microservices.Api.sln
```
