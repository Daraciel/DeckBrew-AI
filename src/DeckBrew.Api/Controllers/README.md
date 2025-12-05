# Controllers

Esta carpeta contiene los controladores (endpoints) de la API organizados por dominio funcional.

## Estructura

### DeckController
Endpoints relacionados con la generación y gestión de mazos.

**Endpoints:**
- `POST /v1/generate` - Genera un nuevo mazo
- `GET /v1/decks/{id}` - Obtiene un mazo por ID
- `DELETE /v1/decks/{id}` - Elimina un mazo por ID

### SystemController
Endpoints del sistema para monitoreo y diagnóstico.

**Endpoints:**
- `GET /v1/health` - Health check del servicio

## Patrón utilizado

Los controladores utilizan **métodos de extensión** sobre `IEndpointRouteBuilder` siguiendo el patrón de **Minimal APIs** de ASP.NET Core.

### Ventajas

1. **Organización**: Endpoints agrupados por funcionalidad
2. **Mantenibilidad**: Fácil localizar y modificar endpoints
3. **Reutilización**: Los métodos de extensión se pueden usar en múltiples lugares
4. **Testabilidad**: Lógica separada en métodos privados
5. **Clean Code**: `Program.cs` queda limpio y legible

## Cómo agregar un nuevo controlador

1. Crear archivo `NuevoController.cs` en esta carpeta
2. Crear clase estática con método de extensión:

```csharp
public static class NuevoController
{
    public static IEndpointRouteBuilder MapNuevoEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/v1/nuevo")
            .WithTags("Nuevo");

        group.MapGet("/", GetAsync)
            .WithName("GetNuevo")
            .Produces(200);

        return endpoints;
    }

    private static IResult GetAsync()
    {
        return Results.Ok(new { message = "Hello" });
    }
}
```

3. Registrar en `Program.cs`:

```csharp
app.MapNuevoEndpoints();
```

## Convenciones

- **Nombres de métodos**: Verbos + Async (ej: `GetDeckAsync`, `GenerateDeckAsync`)
- **Rutas**: Usar `/v1/` como prefijo para versionado
- **Tags**: Agrupar por dominio funcional
- **Nombres**: Nombres descriptivos para OpenAPI (`.WithName()`)
- **Produces**: Especificar códigos de respuesta HTTP

## Dependency Injection

Los parámetros de los endpoints son inyectados automáticamente por el framework:
- **Handlers**: Lógica de negocio
- **Validators**: Validación de requests
- **Repositories**: Acceso a datos
- **CancellationToken**: Para operaciones cancelables

Ejemplo:
```csharp
private static async Task<IResult> MiEndpoint(
    MiCommand command,        // Del body
    MiHandler handler,        // DI
    IValidator validator,     // DI
    CancellationToken ct)     // DI
{
    // Lógica del endpoint
}
```
