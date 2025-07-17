## API LOL Team Sorter

### Features

- [x] Documentação da API com Scalar
- [x] Autenticação e Autorização com JWT com HTTP-only cookies
- [x] Refresh tokens com 1 semana de expiração e job com hangfire para limpar tokens revogados/expirados 
- [x] Login com email, senha ou Discord
- [x] Sorteio de times por Estrelas, Elo, lane ou aleatório
- [x] Cadastro de jogadores buscando informações de ranking da API da Riot
- [x] Atualizar ranking de jogadores cadastrados com a API de ranking da Riot
- [x] Visualizar informações de maestria de campeões e últimas partidas ranqueadas do jogador 
- [x] Histórico de sorteios filtrado por intervalo de datas
- [x] Selecionar vencedor da partida e contagem de vitórias
- [x] Ranking de jogadores com mais vitórias
- [x] Gerenciamento de usuários, grupos e permissões


### 🛠 Tecnologias

As seguintes ferramentas foram usadas na construção do projeto:
- [.NET](https://dotnet.microsoft.com/en-us/)
- [Postgres](https://www.postgresql.org/)
- Carter
- Hangfire
- Refit

### 🛠 Padrões Utilizados

As seguintes padrões foram usados na construção do projeto:
- DDD (Domain-Driven Design)
- CQRS (Command Query Responsibility Segregation)
- SOLID
- Strategy
- UnitOfWork
- Repository

### Pré-requisitos

Antes de começar, você vai precisar ter instalado em sua máquina as seguintes ferramentas:
[Git](https://git-scm.com), [.NET](https://dotnet.microsoft.com/en-us/).
[Postgres](https://www.postgresql.org/) ou subir container utilizando o [Docker](https://www.docker.com/).
Também é preciso configurar connectionString, apiKey riot, informações oauth do Discord, secret JTW no arquivo `lol-team-sorter/src/LoLTeamSorter.API/appsettings.Development.json`.
Além disto é bom ter um editor para trabalhar com o código como [Visual Studio](https://visualstudio.microsoft.com/pt-br/downloads/).


### 🎲 Rodando o Back End (servidor)

#### Rodando LoLTeamSorter.API

```bash
# Clone este repositório
$ git clone <https://github.com/henriquesan14/lol-team-sorter.git>

# Acesse a pasta do projeto no terminal/cmd
$ cd lol-team-sorter

# Vá para a pasta da LoLTeamSorter.API
$ cd src/LoLTeamSorter.API

# Execute a aplicação com o comando do dotnet
$ dotnet run

# A API iniciará na porta:5000 com HTTP
```

### Autor
---

<a href="https://www.linkedin.com/in/henrique-san/">
 <img style="border-radius: 50%;" src="https://avatars.githubusercontent.com/u/33522361?v=4" width="100px;" alt=""/>
 <br />
 <sub><b>Henrique Santos</b></sub></a> <a href="https://www.linkedin.com/in/henrique-san/">🚀</a>


Feito com ❤️ por Henrique Santos 👋🏽 Entre em contato!

[![Linkedin Badge](https://img.shields.io/badge/-Henrique-blue?style=flat-square&logo=Linkedin&logoColor=white&link=https://www.linkedin.com/in/henrique-san/)](https://www.linkedin.com/in/henrique-san/) 
