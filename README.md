# DeckBrew AI

Asistente de **deckbuilding** para *Magic: The Gathering* que genera mazos **legales** y **explicados** a partir de un brief (formato, colores, estilo, presupuesto, meta local). Cliente **.NET MAUI** (Android/iOS/Windows) + Backend **ASP.NET Core** con arquitectura **Clean/Hexagonal**.

> **Nota**: Este README está preparado para dos posibles estructuras que hemos utilizado en el proyecto:
>
> - **Layout A (monorepo apps/services/infra)**: `apps/mobile-maui/DeckBrew.Mobile`, `services/deckbrew.api`, `infra/`.
> - **Layout B (Clean Architecture)**: `src/DeckBrew.*` (Domain, Application, Infrastructure, Api, Workers), `clients/DeckBrew.Mobile`, `infra/`.
>
> Ajusta los comandos a la ruta que corresponda en tu repo. Si tu estructura difiere, abre un issue o comparte el árbol (`tree -L 3`) y actualizo este README.

---

## Tabla de contenido
- [Objetivo y propuesta de valor](#objetivo-y-propuesta-de-valor)
- [Funcionalidades](#funcionalidades)
- [Pantallas (app .NET MAUI)](#pantallas-app-net-maui)
- [Arquitectura](#arquitectura)
  - [Vista global](#vista-global)
  - [Backend](#backend)
  - [Frontend](#frontend)
- [Estructura del repositorio](#estructura-del-repositorio)
- [Requisitos](#requisitos)
- [Ejecución en local](#ejecución-en-local)
  - [Layout A](#layout-a-monorepo-appsservicesinfra)
  - [Layout B](#layout-b-clean-architecture-src)
- [Despliegue en Portainer](#despliegue-en-portainer)
- [Configuración (variables de entorno)](#configuración-variables-de-entorno)
- [API (contratos y endpoints)](#api-contratos-y-endpoints)
- [Datos e ingesta](#datos-e-ingesta)
- [Calidad, observabilidad y CI](#calidad-observabilidad-y-ci)
- [Roadmap](#roadmap)
- [Licencia y avisos](#licencia-y-avisos)

---

## Objetivo y propuesta de valor
- **Objetivo:** Generar mazos **legales** y **coherentes** con explicaciones tácticas, trazabilidad y edición guiada por sinergias.
- **Propuesta:** Ahorra tiempo al crear listas; **enseña** decisiones; **adapta** al **meta local** y **presupuesto**.

## Funcionalidades
**MVP**
- Brief guiado (formato/colores/estilo/presupuesto/meta).
- Generación de lista legal (60/100 cartas según formato) con **reglas duras**.
- Explicaciones (5 key cards), **riesgos** y **mulligan**.
- Edición con **sugerencias por sinergia** (embeddings).
- Guardado local y **exportación** (CSV/decklist).
- Telemetría básica.

**Futuras**
- Exportadores (Archidekt/TappedOut).
- Workers de ingesta Scryfall/MTG JSON y reindexado de embeddings.
- Monetización Freemium/Pro.

## Pantallas (app .NET MAUI)
1. Inicio / Generar mazo.
2. Resultados (lista, curva, colores, key cards, riesgos, mulligan).
3. Editor (swap + sugerencias; validación en tiempo real).
4. Guardados.
5. Exportación.
6. Ajustes.

## Arquitectura
### Vista global
Arquitectura **Clean/Hexagonal** con **CQRS** ligero:
- **Domain**: entidades y puertos del dominio; sin dependencias.
- **Application**: casos de uso (Commands/Queries) y orquestación.
- **Infrastructure**: adaptadores (datos, LLM, vector DB, motor de reglas).
- **Delivery**: **API** (Minimal APIs/OpenAPI) y **Workers** (ingesta/reindexado).
- **Client**: **.NET MAUI** consumiendo **OpenAPI**.

### Backend
- ASP.NET Core Minimal API + OpenAPI.
- MediatR (o DI nativa) para orquestar casos de uso.
- Reglas de formato/curva/presupuesto/colores.
- Embeddings para sinergia (FAISS/Weaviate/Pinecone).
- Observabilidad; UI OpenAPI sólo en Development.

### Frontend
- .NET MAUI (Android/iOS/Windows), MVVM.
- Cliente **OpenAPI** generado (NSwag/Kiota).
- Persistencia local (SQLite/Preferences) y configuración `DECKBREW_API_URL`.

## Estructura del repositorio

### Layout A (apps/services/infra)
```
DeckBrew-AI/
├─ apps/
│  └─ mobile-maui/
│     └─ DeckBrew.Mobile/
├─ services/
│  ├─ deckbrew.api/
│  ├─ deckbrew.data/
│  └─ deckbrew.generator/
└─ infra/
   ├─ Dockerfile.api
   └─ docker-compose.yaml
```

### Layout B (Clean Architecture)
```
DeckBrew-AI/
├─ src/
│  ├─ DeckBrew.Domain/
│  ├─ DeckBrew.Application/
│  ├─ DeckBrew.Infrastructure/
│  ├─ DeckBrew.Api/
│  └─ DeckBrew.Workers.Ingest/
├─ clients/
│  └─ DeckBrew.Mobile/
└─ infra/
   ├─ Dockerfile.api
   └─ docker-compose.yaml
```

## Requisitos
- .NET SDK 9 (o 8 si el proyecto lo fija).
- Docker/Portainer (despliegue API).
- Emulador Android / Windows para MAUI.

## Ejecución en local

### Layout A (monorepo apps/services/infra)
**API**
```bash
cd services/deckbrew.api
dotnet restore
ASPNETCORE_URLS=http://localhost:8080 dotnet run
# OpenAPI UI (dev): http://localhost:8080/swagger
```
**MAUI**
```bash
cd apps/mobile-maui/DeckBrew.Mobile
dotnet restore && dotnet build
export DECKBREW_API_URL=http://localhost:8080
# Ejecuta en emulador/dispositivo
```

### Layout B (Clean Architecture src)
**API**
```bash
cd src/DeckBrew.Api
dotnet restore
ASPNETCORE_URLS=http://localhost:8080 dotnet run
```
**MAUI**
```bash
cd clients/DeckBrew.Mobile
dotnet restore && dotnet build
export DECKBREW_API_URL=http://localhost:8080
```

## Despliegue en Portainer
1. Portainer → **Stacks** → *Add stack*.
2. Pega `infra/docker-compose.yaml`.
3. Deploy → API en `http://<host>:8080`.
4. Configura `DECKBREW_API_URL` en la app.

## Configuración (variables de entorno)
**API**
- `ASPNETCORE_ENVIRONMENT`: `Development|Production`
- `DB_CONNECTION_STRING`: PostgreSQL/SQL Server
- `LLM_PROVIDER`: `OpenAI|AzureOpenAI|Disabled`
- `LLM_API_KEY`: clave del LLM
- `VECTOR_DB_PROVIDER`: `FAISS|Weaviate|Pinecone`
- `ALLOW_OPENAPI_UI`: `true` sólo en dev

**Cliente MAUI**
- `DECKBREW_API_URL`: URL base del backend

## API (contratos y endpoints)
**Base**: `/v1`
- `POST /generate` → body: `{ format, colors[], style, budget?, localMeta? }`
  → `200`: `{ cards[], keyCards[], risks[], mulligan }`
- `POST /swap` (fase 2)
- `GET /decks/{id}` / `DELETE /decks/{id}` (si se persiste)

## Datos e ingesta
- Fuente: Scryfall/MTG JSON.
- Worker: descarga/normaliza/versiona; reindexa embeddings.
- DB: PostgreSQL o SQL Server.

## Calidad, observabilidad y CI
- Tests de **Domain**/**Application**; integración de API.
- OpenTelemetry/logs; OpenAPI UI sólo en dev.
- GitHub Actions (sugerido): build/test y contenedor del API.

## Roadmap
- [ ] Motor de reglas (legalidad/curva/presupuesto/colores).
- [ ] Ingesta Scryfall/MTG JSON + persistencia.
- [ ] Embeddings MVP y sugerencias de swap.
- [ ] OpenAPI v1 estable + cliente MAUI generado.
- [ ] Exportadores.

## Licencia y avisos
- Respetar términos de datasets/APIs; no redistribuir arte con copyright.
- La IA **asiste**; no garantiza rendimiento competitivo.
