# ?? DeckBrew.ExternalApi.Scryfall - Proyecto Wrapper de la API de Scryfall

## ? Proyecto Creado Exitosamente

Se ha creado un wrapper completo y funcional para la API de Scryfall, optimizado para su uso en DeckBrew.

## ?? Estructura del Proyecto

```
src/DeckBrew.ExternalApi.Scryfall/
??? DeckBrew.ExternalApi.Scryfall.csproj
??? GlobalUsings.cs
??? README.md
??? MAUI_INTEGRATION.md
??? USEFUL_QUERIES.md
?
??? Models/
?   ??? ScryfallCard.cs          # Modelo principal de carta
?   ??? ScryfallList.cs          # Lista paginada de resultados
?   ??? ScryfallSet.cs           # Modelo de colección
?   ??? ScryfallSymbol.cs        # Símbolos de maná
?   ??? ScryfallCatalog.cs       # Catálogos (tipos, nombres, etc.)
?   ??? ManaCost.cs              # Coste de maná parseado
?
??? Helpers/
?   ??? ScryfallQueryBuilder.cs  # Constructor fluido de queries
?   ??? ScryfallConstants.cs     # Constantes útiles (colores, formatos, etc.)
?
??? Examples/
?   ??? ScryfallUsageExamples.cs # Ejemplos de uso completos
?
??? IScryfallApi.cs              # Interface Refit para la API
??? IScryfallService.cs          # Interface del servicio de alto nivel
??? ScryfallService.cs           # Implementación del servicio
??? ServiceCollectionExtensions.cs # Extensiones para DI
```

## ?? Características Implementadas

### ? Funcionalidades Principales

1. **Búsqueda de Cartas**
   - Búsqueda por nombre exacto o fuzzy
   - Búsqueda avanzada con sintaxis Scryfall
   - Autocompletado de nombres
   - Búsqueda paginada
   - Cartas aleatorias

2. **Soporte de Commander**
   - Búsqueda específica de comandantes
   - Filtrado por identidad de color
   - Verificación de legalidad en formato

3. **Colecciones (Sets)**
   - Listar todas las colecciones
   - Buscar por código de colección
   - Información detallada de sets

4. **Símbolos de Maná**
   - Obtener todos los símbolos
   - Parsear costes de maná
   - Información de colores y valores

5. **Catálogos**
   - Nombres de cartas
   - Tipos de criatura
   - Habilidades (keywords)
   - Tipos de planeswalker, tierras, etc.

6. **Imágenes**
   - Descarga de imágenes en múltiples tamaños
   - URLs para small, normal, large, PNG, art_crop, border_crop

### ? Características Técnicas

- **Rate Limiting Automático**: Respeta el límite de 10 requests/segundo
- **Manejo de Errores**: Try-catch con fallbacks apropiados
- **Inyección de Dependencias**: Totalmente compatible con DI de .NET
- **Async/Await**: Operaciones completamente asíncronas
- **Cancellation Tokens**: Soporte para cancelación de operaciones
- **Paginación**: Manejo automático de resultados paginados
- **Type-Safe**: Modelos fuertemente tipados
- **Refit**: Usa Refit para simplificar llamadas HTTP

## ?? Dependencias

- **Refit 7.2.22**: Cliente HTTP declarativo
- **Refit.HttpClientFactory 7.2.22**: Integración con HttpClientFactory
- **Microsoft.Extensions.DependencyInjection.Abstractions 10.0.0**
- **Microsoft.Extensions.Http 10.0.0**

## ?? Próximos Pasos

### 1. Agregar Referencia en DeckBrew.Mobile

```xml
<ItemGroup>
  <ProjectReference Include="..\..\..\src\DeckBrew.ExternalApi.Scryfall\DeckBrew.ExternalApi.Scryfall.csproj" />
</ItemGroup>
```

### 2. Registrar Servicios en MauiProgram.cs

```csharp
using DeckBrew.ExternalApi.Scryfall;

builder.Services.AddScryfallServices();
```

### 3. Usar en ViewModels

```csharp
public class MyViewModel
{
    private readonly IScryfallService _scryfallService;

    public MyViewModel(IScryfallService scryfallService)
    {
        _scryfallService = scryfallService;
    }

    public async Task SearchCards()
    {
        var cards = await _scryfallService.SearchCardsAsync("is:commander id:ubr");
        // Usar las cartas...
    }
}
```

## ?? Documentación Incluida

1. **README.md**: Documentación general del proyecto
2. **MAUI_INTEGRATION.md**: Guía completa de integración con .NET MAUI
3. **USEFUL_QUERIES.md**: Biblioteca de queries útiles para diferentes casos de uso
4. **ScryfallUsageExamples.cs**: Ejemplos de código comentados

## ?? Helpers Incluidos

### ScryfallQueryBuilder

Constructor fluido para crear queries complejas:

```csharp
var query = ScryfallQueryBuilder.Create()
    .IsCommander()
    .WithCommanderIdentity("ubr")
    .WithCmc(6, "<=")
    .WithFormat(ScryfallConstants.Formats.Commander)
    .Build();
```

### ScryfallConstants

Constantes tipadas para evitar strings mágicos:

```csharp
ScryfallConstants.Colors.Blue
ScryfallConstants.ColorIdentities.Grixis
ScryfallConstants.Formats.Commander
ScryfallConstants.Types.Creature
```

## ?? Casos de Uso Principales para DeckBrew

1. ? **Búsqueda de Comandantes** por identidad de color
2. ? **Autocompletado** en campos de búsqueda
3. ? **Construcción de Mazos** con verificación de legalidad
4. ? **Visualización de Cartas** con imágenes de alta calidad
5. ? **Sugerencias de Cartas** basadas en estrategias
6. ? **Información de Precios** para presupuesto de mazos
7. ? **Símbolos de Maná** correctamente renderizados

## ?? Listo para Usar

El proyecto está completamente funcional y listo para integrarse en tu aplicación DeckBrew.Mobile. 

- ? Compila sin errores
- ? Respeta las mejores prácticas de .NET
- ? Documentación completa
- ? Ejemplos de uso
- ? Optimizado para .NET 10
- ? Compatible con .NET MAUI

## ?? Próximas Mejoras Opcionales

1. **Cache Local**: Implementar cache de SQLite para búsquedas frecuentes
2. **Modo Offline**: Guardar cartas favoritas para acceso sin conexión
3. **Unit Tests**: Agregar tests unitarios para los servicios
4. **Image Cache**: Sistema de cache de imágenes en FileSystem
5. **Bulk Downloads**: Métodos para descargar múltiples cartas en batch
6. **Advanced Filters UI**: Componentes de UI para construcción visual de queries

---

**¡El proyecto está listo para usar! ??**

Revisa los archivos de documentación para ver ejemplos detallados de cómo usar cada funcionalidad.
