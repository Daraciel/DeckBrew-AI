# Guía de Migración: Layout A ? Layout B (Clean Architecture)

## Resumen de cambios

Se ha refactorizado el backend de DeckBrew de una arquitectura monolítica (Layout A) a una arquitectura Clean/Hexagonal (Layout B) con separación clara de responsabilidades.

## Estructura anterior (Layout A)
```
services/deckbrew.api/
??? Program.cs (todo junto)
```

## Nueva estructura (Layout B)
```
src/
??? DeckBrew.Domain/           # Entidades, Value Objects, Puertos
??? DeckBrew.Application/      # Casos de uso, Comandos, Validadores
??? DeckBrew.Infrastructure/   # Adaptadores, Repositorios, Rules Engine
??? DeckBrew.Api/              # Minimal APIs, Configuración DI
```

## Beneficios

### ? Desacoplamiento
- El dominio no tiene dependencias externas
- Los puertos permiten intercambiar implementaciones fácilmente
- Testeable sin dependencias de infraestructura

### ? Mantenibilidad
- Código organizado por responsabilidades
- Fácil localizar funcionalidad
- Cambios localizados sin afectar otras capas

### ? Escalabilidad
- Fácil agregar nuevos casos de uso
- Fácil cambiar implementaciones (ej: de in-memory a PostgreSQL)
- Preparado para agregar Workers, eventos, etc.

### ? Testabilidad
- Domain y Application completamente testeables sin infraestructura
- Mocking sencillo de puertos
- Tests de integración solo en la capa API

## Cambios en contratos API

### Antes (Layout A)
```http
POST /generate
Body: { format, colors, style, budget }
```

### Ahora (Layout B)
```http
POST /v1/generate
Body: { 
  request: { format, colors, style, budget } 
}
```

**Cambio en la app móvil:** Se actualizó el cliente Refit para envolver el request en un comando.

## Migración paso a paso

1. ? **Domain Layer**: Entidades, Value Objects y Puertos creados
2. ? **Application Layer**: Commands, Handlers y Validators implementados
3. ? **Infrastructure Layer**: Adaptadores in-memory para MVP
4. ? **API Layer**: Minimal APIs con OpenAPI/Swagger
5. ? **Mobile App**: Cliente actualizado al nuevo contrato
6. ? **Docker**: Dockerfile y docker-compose.yml actualizados

## Próximos pasos

### Fase 1: Persistencia real
- [ ] Implementar `SqlCardRepository` (PostgreSQL/SQL Server)
- [ ] Implementar `SqlDeckRepository`
- [ ] Configurar Entity Framework Core o Dapper

### Fase 2: Sinergias con embeddings
- [ ] Implementar `VectorSynergyCalculator` con FAISS/Weaviate
- [ ] Generar embeddings de cartas
- [ ] API para búsqueda de sinergias

### Fase 3: Workers
- [ ] `DeckBrew.Workers.Ingest`: Descarga Scryfall/MTG JSON
- [ ] Normalización y persistencia de datos
- [ ] Reindexado automático de embeddings

### Fase 4: Tests
- [ ] Tests unitarios Domain
- [ ] Tests unitarios Application (Handlers)
- [ ] Tests de integración API
- [ ] Tests E2E app móvil

### Fase 5: Observabilidad
- [ ] OpenTelemetry (traces, metrics, logs)
- [ ] Health checks avanzados
- [ ] Monitoring dashboard

## Comandos útiles

### Ejecutar nueva API
```bash
cd src/DeckBrew.Api
dotnet run
# API en http://localhost:8100
# Swagger en http://localhost:8100/swagger
```

### Ejecutar app móvil
```bash
cd apps/mobile-maui/DeckBrew.Mobile
export DECKBREW_API_URL=http://localhost:8100
dotnet build
# Ejecutar en emulador/dispositivo
```

### Build con Docker
```bash
docker-compose up --build
```

### Tests (cuando estén implementados)
```bash
dotnet test
```

## Comparación de código

### Antes: Monolito en Program.cs
```csharp
app.MapPost("/generate", (GenerationRequest req, IValidator validator) =>
{
    // Validación
    // Lógica de negocio
    // Acceso a datos
    // Reglas
    // Todo mezclado
});
```

### Ahora: Arquitectura limpia
```csharp
// Program.cs - Solo configuración y routing
app.MapPost("/v1/generate", async (command, handler, validator) =>
{
    var result = await handler.Handle(command);
    return Results.Ok(result);
});

// Handler - Orquestación
public async Task<Result> Handle(Command command)
{
    // Usa puertos para orquestar
    var cards = await _cardRepository.GetLegal(...);
    var isValid = _rulesEngine.Validate(...);
    return new Result(...);
}
```

## Dependencias de paquetes NuGet

- **DeckBrew.Domain**: Sin dependencias
- **DeckBrew.Application**: FluentValidation
- **DeckBrew.Infrastructure**: (preparado para EF Core, Dapper, etc.)
- **DeckBrew.Api**: Swashbuckle, FluentValidation.AspNetCore

## Estructura de archivos completa

```
DeckBrew-AI/
??? src/
?   ??? DeckBrew.Domain/
?   ?   ??? Entities/
?   ?   ?   ??? Card.cs
?   ?   ?   ??? Deck.cs
?   ?   ??? ValueObjects/
?   ?   ?   ??? GenerationRequest.cs
?   ?   ??? Ports/
?   ?       ??? ICardRepository.cs
?   ?       ??? IDeckRepository.cs
?   ?       ??? IRulesEngine.cs
?   ?       ??? ISynergyCalculator.cs
?   ??? DeckBrew.Application/
?   ?   ??? Commands/
?   ?   ?   ??? GenerateDeckCommand.cs
?   ?   ??? Handlers/
?   ?   ?   ??? GenerateDeckHandler.cs
?   ?   ??? Validators/
?   ?       ??? GenerationRequestValidator.cs
?   ??? DeckBrew.Infrastructure/
?   ?   ??? Repositories/
?   ?   ?   ??? InMemoryCardRepository.cs
?   ?   ?   ??? InMemoryDeckRepository.cs
?   ?   ??? Rules/
?   ?   ?   ??? MtgRulesEngine.cs
?   ?   ??? Synergy/
?   ?       ??? StubSynergyCalculator.cs
?   ??? DeckBrew.Api/
?   ?   ??? Program.cs
?   ?   ??? appsettings.json
?   ?   ??? Properties/
?   ?       ??? launchSettings.json
?   ??? README.md
??? apps/
?   ??? mobile-maui/
?       ??? DeckBrew.Mobile/
??? Dockerfile
??? docker-compose.yml
??? README.md
??? MIGRATION.md (este archivo)
```

## Preguntas frecuentes

**¿Por qué no usar MediatR?**
Para este MVP, la inyección de dependencias nativa de .NET es suficiente. MediatR se puede agregar fácilmente si se necesita más adelante.

**¿Por qué in-memory para el MVP?**
Permite iterar rápido sin depender de infraestructura externa. Los puertos permiten cambiar a SQL fácilmente.

**¿Qué pasó con el código antiguo?**
Se mantiene en `services/deckbrew.api/` pero ya no se usa. Se puede eliminar cuando se confirme que todo funciona.

**¿Cómo agregar un nuevo caso de uso?**
1. Crear Command en Application/Commands
2. Crear Handler en Application/Handlers
3. Registrar en Program.cs
4. Mapear endpoint en API

**¿Cómo cambiar a PostgreSQL?**
1. Crear SqlCardRepository implementando ICardRepository
2. Configurar EF Core / Dapper
3. Registrar en Program.cs en lugar de InMemoryCardRepository

## Contacto y soporte

Para dudas sobre la migración, abrir un issue en GitHub o consultar el README principal.
