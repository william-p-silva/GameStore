# 🎮 GameStore

GameStore é uma aplicação completa de e-commerce voltada para venda de jogos, consoles e acessórios, construída com foco em aprendizado prático de arquitetura moderna, boas práticas de desenvolvimento e simulação de um sistema real de marketplace.

O projeto é dividido em duas camadas principais: **frontend** e **backend**, organizadas de forma independente para facilitar escalabilidade, manutenção e evolução.

---

## Objetivo do Projeto

O GameStore foi desenvolvido com o propósito de simular um sistema real de e-commerce, abordando desafios comuns do mercado como:

* Gerenciamento de produtos e categorias
* Autenticação e autorização de usuários
* Manipulação de carrinho de compras
* Validação de estoque em tempo real
* Estruturação de APIs escaláveis
* Separação clara entre frontend e backend

Além disso, o projeto serve como base prática para estudos em desenvolvimento web full-stack, com foco em aplicações reais.

---

## Problema que o projeto resolve

O sistema busca resolver a necessidade de:

> Criar uma plataforma organizada e segura onde usuários possam navegar por produtos, adicionar itens ao carrinho e gerenciar suas compras de forma eficiente.

Na prática, ele simula o funcionamento de plataformas como marketplaces, lidando com:

* Consistência de dados (ex: estoque)
* Segurança (JWT, autenticação)
* Experiência do usuário (fluxo de compra)
* Organização de código (camadas, DTOs, services)

---

## Arquitetura do Projeto

O repositório está organizado da seguinte forma:

```
GameStore/
│
├── frontend/   → Interface do usuário (UI)
├── backend/    → API, regras de negócio e banco de dados
└── README.md   → Documentação geral do projeto
```

Essa separação permite:

* Desenvolvimento independente entre front e back
* Melhor organização do código
* Facilidade para deploy separado
* Escalabilidade futura

---

## Tecnologias Utilizadas

### Backend

* .NET / ASP.NET Core
* Entity Framework Core
* MySQL
* JWT (JSON Web Token) para autenticação
* LINQ para consultas dinâmicas

### Frontend

*(detalhado no README do frontend)*

---

## Principais Funcionalidades

* Cadastro e autenticação de usuários
* CRUD de produtos e categorias
* Sistema de carrinho de compras persistente por usuário
* Validação de estoque ao adicionar/atualizar itens
* Paginação e filtros em listagens
* Estrutura baseada em DTOs e Services

---

## Funcionalidades em evolução

O projeto está em constante evolução e pretende incluir:

* Sistema de pedidos (checkout)
* Histórico de compras
* Melhorias na experiência do usuário
* Dashboard administrativo

---

## Propósito educacional

Além de funcional, o projeto tem forte foco em aprendizado, incluindo:

* Aplicação de boas práticas (Clean Code)
* Organização em camadas (Controller → Service → Data)
* Uso de DTOs para desacoplamento
* Construção de APIs RESTful
* Simulação de cenários reais de mercado

---

## Repositório

Acesse o projeto completo:
  https://github.com/william-p-silva/GameStore

---

## Observação

Cada camada do projeto possui seu próprio README com instruções específicas de execução, configuração e detalhes técnicos:

* `frontend/README.md`
* `backend/README.md`

---

## Autor

Projeto desenvolvido por William José Pereira da Silva, com foco em evolução prática na área de desenvolvimento full-stack e construção de sistemas reais.

---
