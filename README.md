🎬 ProjetoFilmes — .NET 8 + OMDb API

Aplicação construída em .NET 8 (Minimal API) consumindo a OMDb API e exibindo os resultados em uma interface web responsiva (HTML, CSS e JavaScript), servida pelo próprio backend.

🚀 Tecnologias

.NET 8 / Minimal API

HttpClient para consumo de API externa

User Secrets (proteção da API key)

Swagger / OpenAPI

HTML + CSS + JS (frontend simples e responsivo)

🎯 Funcionalidades

Busca de filmes, séries e jogos por título

Filtros por ano, tipo e página

Lista com cards (poster + informações)

Modal com detalhes completos (atores, diretor, nota, sinopse…)

Integração direta com a OMDb API

🧩 Estrutura

Minimal API (C#)
 ↓
OMDb API via HttpClient
 ↓
Frontend HTML/JS consumindo a API local

🔧 Como rodar

git clone https://github.com/SEU_USUARIO/ProjetoFilmes.git
cd ProjetoFilmes
dotnet user-secrets set "Omdb:ApiKey" "SUA_KEY"
dotnet run

Acesso:

App: http://localhost:5046
Swagger: http://localhost:5046/swagger






