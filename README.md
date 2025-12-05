# DeckBrew AI

Asistente de **deckbuilding** para *Magic: The Gathering* que genera mazos **legales** y **explicados** a partir de un brief (formato, colores, estilo, presupuesto, meta local). Cliente **.NET MAUI** (Android/iOS/Windows) + Backend **ASP.NET Core** con arquitectura **Clean/Hexagonal**.

> **Nota**: El proyecto ha sido refactorizado siguiendo el **Layout B (Clean Architecture)** como se describe a continuación.
> Para información sobre la migración desde Layout A, consulta [MIGRATION.md](MIGRATION.md).

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
- [Despliegue en Docker](#despliegue-en-docker)
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
Arquitectura **Clean/Hexagonal** con **separación de capas**:
- **Domain**: entidades y puertos del dominio; sin dependencias.
- **Application**: casos de uso (Commands/Handlers) y orquestación.
- **Infrastructure**: adaptadores (datos, LLM, vector DB, motor de reglas).
- **Api**: **Minimal APIs** con OpenAPI/Swagger.
- **Mobile**: **.NET MAUI** consumiendo API REST.

### Backend
- ASP.NET Core 10 con Minimal API + OpenAPI.
- Inyección de dependencias nativa para orquestar casos de uso.
- Reglas de formato/curva/presupuesto/colores.
- Embeddings para sinergia (preparado para FAISS/Weaviate/Pinecone).
- Observabilidad; UI OpenAPI sólo en Development.

### Frontend
- .NET MAUI (Android/iOS/Windows), MVVM.
- Cliente **Refit** para consumir API REST.
- Persistencia local (SQLite/Preferences) y configuración `DECKBREW_API_URL`.

## Estructura del repositorio

### Layout B (Clean Architecture) - **ACTUAL**
```
DeckBrew-AI/
├─ src/
│  ├─ DeckBrew.Domain/          # Entidades, puertos (sin dependencias)
│  ├─ DeckBrew.Application/     # Casos de uso, handlers, validadores
│  ├─ DeckBrew.Infrastructure/  # Adaptadores (repos, rules, synergy)
│  ├─ DeckBrew.Api/            # Minimal APIs, DI, OpenAPI
│  └─ README.md
├─ apps/
│  └─ mobile-maui/
│     └─ DeckBrew.Mobile/
├─ Dockerfile
├─ docker-compose.yml
├─ MIGRATION.md                 # Guía de migración Layout A → B
└─ README.md (este archivo)
```

## Requisitos
- .NET SDK 10.0+
- Docker (opcional, para contenedores)
- Visual Studio 2022 o VS Code
- Emulador Android / iOS / Windows para MAUI

## Ejecución en local

### API (Backend)
```bash
cd src/DeckBrew.Api
dotnet restore
dotnet run

# API disponible en: http://localhost:8100
# Swagger UI: http://localhost:8100/swagger
```

### Cliente MAUI
```bash
cd apps/mobile-maui/DeckBrew.Mobile
dotnet restore
dotnet build

# Configurar URL del API (opcional, default: http://localhost:8100)
export DECKBREW_API_URL=http://localhost:8100

# Ejecutar en emulador o dispositivo
dotnet run --framework net10.0-android   # Android
dotnet run --framework net10.0-windows   # Windows
```

## Despliegue en Docker

### Build y ejecución
```bash
# Build imagen
docker-compose build

# Ejecutar contenedor
docker-compose up -d

# API disponible en: http://localhost:8100
```

### Portainer
1. Portainer → **Stacks** → *Add stack*.
2. Pega contenido de `docker-compose.yml`.
3. Deploy → API en `http://<host>:8100`.
4. Configura `DECKBREW_API_URL` en la app móvil.

## Configuración (variables de entorno)

**API**
- `ASPNETCORE_ENVIRONMENT`: `Development|Production`
- `ASPNETCORE_URLS`: URL de escucha (default: `http://+:8100`)
- `DB_CONNECTION_STRING`: PostgreSQL/SQL Server (futuro)
- `LLM_PROVIDER`: `OpenAI|AzureOpenAI|Disabled` (futuro)
- `LLM_API_KEY`: clave del LLM (futuro)
- `VECTOR_DB_PROVIDER`: `FAISS|Weaviate|Pinecone` (futuro)

**Cliente MAUI**
- `DECKBREW_API_URL`: URL base del backend (default: `http://localhost:8100`)

## API (contratos y endpoints)

**Base**: `/v1`

### POST /v1/generate
Genera un nuevo mazo basado en los parámetros.

**Request:**
```json
{
  "request": {
    "format": "Standard",
    "colors": ["U", "R"],
    "style": "control",
    "budget": 100.0,
    "localMeta": null
  }
}
```

**Response:**
```json
{
  "cards": [
    { "name": "Island", "count": 24 },
    { "name": "Counterspell", "count": 4 }
  ],
  "keyCards": [
    { "name": "Counterspell", "rationale": "Key card for the strategy" }
  ],
  "risks": ["Mana base might be too light"],
  "mulligan": "Keep hands with 3-4 lands and at least one removal spell."
}
```

### GET /v1/decks/{id}
Obtiene un mazo guardado por ID.

### DELETE /v1/decks/{id}
Elimina un mazo por ID.

### GET /v1/health
Health check del servicio.

## Datos e ingesta
- **Fuente**: Scryfall/MTG JSON.
- **Worker** (futuro): descarga/normaliza/versiona; reindexa embeddings.
- **DB** (futuro): PostgreSQL o SQL Server.
- **Actual**: Repositorios in-memory para MVP.

## Calidad, observabilidad y CI

### Testing (planificado)
- Tests unitarios de Domain y Application
- Tests de integración de API
- Tests E2E de app móvil

### Observabilidad (planificado)
- OpenTelemetry (traces, metrics, logs)
- Health checks avanzados
- Swagger UI solo en Development

### CI/CD (sugerido)
- GitHub Actions: build/test en cada PR
- Docker image build automático
- Deploy a staging/prod

## Roadmap

### Fase 1: MVP (Completado) ✅
- [x] Arquitectura Clean/Hexagonal
- [x] Minimal APIs con OpenAPI
- [x] Motor de reglas básico
- [x] Repositorios in-memory
- [x] Cliente MAUI conectado

### Fase 2: Persistencia
- [ ] Implementar repositorios SQL (PostgreSQL/SQL Server)
- [ ] Entity Framework Core o Dapper
- [ ] Migraciones de base de datos

### Fase 3: Datos reales
- [ ] Worker de ingesta Scryfall/MTG JSON
- [ ] Normalización y persistencia de cartas
- [ ] Actualización automática de datos

### Fase 4: Sinergias
- [ ] Implementar embeddings (OpenAI/Azure)
- [ ] Vector DB (FAISS/Weaviate/Pinecone)
- [ ] API de sugerencias de swap

### Fase 5: Features avanzadas
- [ ] Exportadores (Archidekt/TappedOut)
- [ ] Análisis de meta local
- [ ] Optimización de presupuesto

### Fase 6: Testing y QA
- [ ] Tests unitarios completos
- [ ] Tests de integración
- [ ] Tests E2E automatizados

### Fase 7: Observabilidad
- [ ] OpenTelemetry integrado
- [ ] Dashboards de monitoring
- [ ] Alertas y logging estructurado

## Licencia y avisos
- Respetar términos de datasets/APIs; no redistribuir arte con copyright.
- La IA **asiste**; no garantiza rendimiento competitivo.
- Magic: The Gathering es marca registrada de Wizards of the Coast.

---

## Contribuir

Para contribuir al proyecto:
1. Fork el repositorio
2. Crea una rama con tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## Soporte

Para preguntas, issues o sugerencias:
- Abre un **Issue** en GitHub
- Consulta [MIGRATION.md](MIGRATION.md) para detalles de arquitectura
- Revisa [src/README.md](src/README.md) para documentación del backend
