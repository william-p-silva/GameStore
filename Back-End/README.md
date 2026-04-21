# 🎮 GameStore - Backend

Este é o backend da aplicação **GameStore**, responsável por toda a lógica de negócio, autenticação, persistência de dados e exposição da API REST consumida pelo frontend.

A aplicação foi desenvolvida utilizando **ASP.NET Core** seguindo boas práticas de arquitetura em camadas, com foco em escalabilidade, segurança e organização de código.

---

## Tecnologias Utilizadas

* ASP.NET Core (Web API)
* Entity Framework Core (ORM)
* MySQL (Banco de dados)
* JWT (JSON Web Token) para autenticação
* LINQ (consultas dinâmicas)
* BCrypt (hash de senha)

---

## Arquitetura

O backend segue uma arquitetura em camadas:

```
Controllers → Services → Data (DbContext) → Banco de Dados
```

### Responsabilidades

* **Controllers**

  * Recebem requisições HTTP
  * Validam entrada básica
  * Extraem dados do JWT (usuário logado)
  * Chamam os services

* **Services**

  * Contêm a lógica de negócio
  * Validam regras (estoque, existência, etc.)
  * Manipulam entidades
  * Retornam DTOs

* **DTOs**

  * Controlam o que entra e sai da API
  * Evitam exposição direta das entidades

* **DbContext (EF Core)**

  * Gerencia conexão com o banco
  * Mapeia entidades e relacionamentos

---

## Autenticação com JWT

O sistema utiliza autenticação baseada em **JWT**.

### Fluxo:

1. Usuário faz login
2. API valida email e senha
3. Um token JWT é gerado
4. O token deve ser enviado nas próximas requisições:

```
Authorization: Bearer {token}
```

### Informações no token:

* Id do usuário
* Email
* Role (perfil)

---

## Principais Entidades

* **Usuario**
* **Produto**
* **Categoria**
* **Carrinho**
* **CarrinhoItem**

### Relacionamentos importantes

* 1 Usuário → 1 Carrinho
* 1 Carrinho → N Itens
* 1 Produto → 1 Categoria

---

## Endpoints Principais

### Usuário

| Método | Rota          | Descrição        |
| ------ | ------------- | ---------------- |
| POST   | `/user`       | Criar usuário    |
| POST   | `/user/login` | Login (gera JWT) |
| GET    | `/user`       | Listar usuários  |
| GET    | `/user/{id}`  | Buscar por ID    |
| PUT    | `/user/{id}`  | Atualizar        |
| DELETE | `/user/{id}`  | Remover          |

---

### Produto

| Método | Rota            | Descrição                       |
| ------ | --------------- | ------------------------------- |
| POST   | `/produto`      | Criar produto                   |
| GET    | `/produto`      | Listar (com filtro e paginação) |
| GET    | `/produto/{id}` | Buscar por ID                   |
| PUT    | `/produto/{id}` | Atualizar                       |
| DELETE | `/produto/{id}` | Remover                         |

 Filtros disponíveis:

* Nome
* Categoria
* Paginação

---

### Categoria

| Método | Rota              | Descrição                    |
| ------ | ----------------- | ---------------------------- |
| POST   | `/categoria`      | Criar categoria              |
| GET    | `/categoria`      | Listar (com ou sem produtos) |
| GET    | `/categoria/{id}` | Buscar por ID                |
| PUT    | `/categoria/{id}` | Atualizar                    |
| DELETE | `/categoria/{id}` | Remover                      |

---

### Carrinho (Protegido com JWT)

> Todos os endpoints exigem usuário autenticado

| Método | Rota                          | Descrição                 |
| ------ | ----------------------------- | ------------------------- |
| GET    | `/api/carrinho/carrinho`      | Obter carrinho do usuário |
| POST   | `/api/carrinho/adicionarItem` | Adicionar produto         |
| DELETE | `/api/carrinho/removerItem`   | Remover produto           |
| PUT    | `/api/carrinho/atualizarItem` | Atualizar quantidade      |

---

## Regras de Negócio Importantes

* O carrinho é único por usuário
* Produtos não podem ser adicionados sem estoque
* Quantidade é validada ao adicionar e atualizar
* Itens duplicados no carrinho são somados (não recriados)
* Remoção ocorre diretamente no banco

---

## Paginação

Listagens utilizam um padrão de resposta paginada:

```json
{
  "data": [],
  "total": 100,
  "page": 1,
  "pageSize": 10
}
```

---

## Configuração do Projeto

### 1. Banco de Dados

Configure a connection string no `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;database=gamer_store;user=root;password=senha"
}
```

---

### 2. JWT

```json
"Jwt": {
  "Key": "SUA_CHAVE_SECRETA",
  "Issuer": "GameStore",
  "Audience": "GameStoreUsers"
}
```

---

### 3. Migrations

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

## Como Executar

```bash
dotnet run
```

A API estará disponível em:

```
https://localhost:xxxx/swagger
```

---

## Testes

A API pode ser testada via:

* Swagger (já incluso)
* Postman / Insomnia

---

## Boas Práticas Aplicadas

* Separação de responsabilidades
* Uso de DTOs
* Validação de regras no service
* Segurança com JWT
* Queries otimizadas com LINQ
* Paginação padronizada
* Uso de async/await

---

## 🚀 Próximos Passos

* Implementação de pedidos (checkout)
* Histórico de compras
* Logs e monitoramento

---

## Observação

Este backend foi desenvolvido com foco educacional, simulando cenários reais de mercado e servindo como base para evolução contínua.

---

## Autor

Projeto desenvolvido por William José Pereira da Silva, com foco em evolução prática na área de desenvolvimento full-stack e construção de sistemas reais.

---
