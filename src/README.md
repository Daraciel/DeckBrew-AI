# DeckBrew Backend - Clean Architecture

Esta es la implementación del backend de DeckBrew siguiendo arquitectura Clean/Hexagonal (Layout B).

## Estructura

```
src/
??? DeckBrew.Domain/           # Capa de dominio (sin dependencias)
?   ??? Entities/              # Entidades del dominio
?   ??? ValueObjects/          # Objetos de valor
?   ??? Ports/                 # Interfaces (puertos)
?
??? DeckBrew.Application/      # Capa de aplicación (casos de uso)
?   ??? Commands/              # Comandos
?   ??? Handlers/              # Manejadores de comandos
?   ??? Validators/            # Validadores FluentValidation
?
??? DeckBrew.Infrastructure/   # Capa de infraestructura (adaptadores)
?   ??? Repositories/          # Implementación de repositorios
?   ??? Rules/                 # Motor de reglas MTG
?   ??? Synergy/               # Calculadora de sinergias
?
??? DeckBrew.Api/              # Capa de entrega (API)
    ??? Program.cs             # Minimal APIs + configuración DI
```

## Dependencias entre capas

```
Domain (sin dependencias)
   ?
Application (depende de Domain)
   ?
Infrastructure (depende de Domain)
   ?
Api (depende de Application + Infrastructure)
```

## Puertos y Adaptadores

### Puertos (Domain/Ports)
- `ICardRepository`: Acceso a datos de cartas
- `IDeckRepository`: Persistencia de mazos
- `IRulesEngine`: Motor de reglas de formato
- `ISynergyCalculator`: Cálculo de sinergias (embeddings)

### Adaptadores (Infrastructure)
- `InMemoryCardRepository`: Implementación en memoria (MVP)
- `InMemoryDeckRepository`: Persistencia en memoria (MVP)
- `MtgRulesEngine`: Reglas de legalidad MTG
- `StubSynergyCalculator`: Stub para sinergias (MVP)

## Ejecución

```bash
cd src/DeckBrew.Api
dotnet run
```

La API estará disponible en `http://localhost:8100`
Swagger UI en `http://localhost:8100/swagger`

## Endpoints

### POST /v1/generate
Genera un nuevo mazo basado en los parámetros.

**Request:**
```json
{
  "request": {
    "format": "Standard",
    "colors": ["U", "R"],
    "style": "control",
    "budget": 100.0
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
Obtiene un mazo por ID.

### DELETE /v1/decks/{id}
Elimina un mazo por ID.

### GET /v1/health
Health check del servicio.

## Próximos pasos

1. Reemplazar repositorios in-memory con PostgreSQL/SQL Server
2. Implementar `ISynergyCalculator` con embeddings (FAISS/Weaviate)
3. Añadir Workers para ingesta de datos Scryfall
4. Agregar tests unitarios e integración
5. Implementar observabilidad (OpenTelemetry)
