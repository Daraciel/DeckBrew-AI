# DeckBrew.ExternalApi.Scryfall

Wrapper para la API de Scryfall, proporcionando acceso simplificado a datos de Magic: The Gathering.

## ?? Instalación

Agrega la referencia al proyecto en tu `.csproj`:

```xml
<ItemGroup>
  <ProjectReference Include="..\..\src\DeckBrew.ExternalApi.Scryfall\DeckBrew.ExternalApi.Scryfall.csproj" />
</ItemGroup>
```

## ?? Configuración

### Registro en DI Container

```csharp
// En MauiProgram.cs o Startup.cs
builder.Services.AddScryfallServices();
```

## ?? Uso

### Inyección de Dependencias

```csharp
public class MyViewModel
{
    private readonly IScryfallService _scryfallService;

    public MyViewModel(IScryfallService scryfallService)
    {
        _scryfallService = scryfallService;
    }
}
```

### Búsqueda de Cartas

```csharp
// Búsqueda básica
var cards = await _scryfallService.SearchCardsAsync("lightning bolt");

// Búsqueda por nombre exacto
var card = await _scryfallService.GetCardByNameAsync("Black Lotus");

// Autocompletado
var suggestions = await _scryfallService.AutocompleteCardNameAsync("tef");

// Carta aleatoria
var randomCard = await _scryfallService.GetRandomCardAsync();
```

### Búsqueda de Comandantes

```csharp
// Todos los comandantes
var commanders = await _scryfallService.SearchCommandersAsync();

// Comandantes de identidad específica
var grixisCommanders = await _scryfallService.SearchCommandersAsync("ubr");

// Cartas legales en Commander
var commanderCards = await _scryfallService.SearchCommanderLegalCardsAsync("t:creature");
```

### Consultas Avanzadas

```csharp
// Usar sintaxis de búsqueda de Scryfall
var redLegendaries = await _scryfallService.SearchCardsAsync("c:red t:legendary");

// Filtrar por formato
var modernCards = await _scryfallService.SearchCardsAsync("f:modern cmc<=3");

// Comandantes Boros
var borosCommanders = await _scryfallService.SearchCardsAsync("is:commander commander:rw");
```

### Símbolos de Maná

```csharp
// Obtener todos los símbolos
var symbols = await _scryfallService.GetAllSymbolsAsync();

// Parsear coste de maná
var cost = await _scryfallService.ParseManaCostAsync("{2}{U}{U}");
Console.WriteLine($"CMC: {cost.Cmc}, Colors: {string.Join(",", cost.Colors)}");
```

### Imágenes

```csharp
// Descargar imagen de carta
var card = await _scryfallService.GetCardByNameAsync("Sol Ring");
if (card?.ImageUris?.Large != null)
{
    var imageStream = await _scryfallService.GetCardImageAsync(card.ImageUris.Large);
    // Usar el stream para mostrar la imagen
}
```

### Colecciones (Sets)

```csharp
// Obtener todos los sets
var sets = await _scryfallService.GetAllSetsAsync();

// Obtener set específico
var innistrad = await _scryfallService.GetSetByCodeAsync("mid");
```

### Catálogos

```csharp
// Todos los nombres de cartas
var cardNames = await _scryfallService.GetAllCardNamesAsync();

// Tipos de criatura
var creatureTypes = await _scryfallService.GetCreatureTypesAsync();

// Habilidades
var keywords = await _scryfallService.GetKeywordAbilitiesAsync();
```

## ?? Sintaxis de Búsqueda Avanzada

La API soporta la sintaxis completa de Scryfall:

| Filtro | Descripción | Ejemplo |
|--------|-------------|---------|
| `c:color` | Color de la carta | `c:red` |
| `t:type` | Tipo de carta | `t:creature` |
| `cmc=X` | Coste de maná convertido | `cmc=3` |
| `commander:colors` | Identidad de comandante | `commander:boros` |
| `f:format` | Legal en formato | `f:commander` |
| `set:code` | Colección específica | `set:mid` |
| `is:commander` | Puede ser comandante | `is:commander` |
| `o:"text"` | Texto del oráculo | `o:"draw a card"` |
| `pow>X` | Fuerza mayor que X | `pow>4` |
| `tou<X` | Resistencia menor que X | `tou<3` |

### Operadores

- `AND` (implícito con espacio)
- `OR`
- `NOT` o `-`
- Paréntesis para agrupar

### Ejemplos Avanzados

```csharp
// Criaturas rojas legendarias de CMC <= 4
await _scryfallService.SearchCardsAsync("c:red t:legendary t:creature cmc<=4");

// Comandantes Grixis (Azul/Negro/Rojo)
await _scryfallService.SearchCardsAsync("is:commander commander:ubr");

// Cartas con "draw" en el texto, legales en Commander
await _scryfallService.SearchCardsAsync("o:draw f:commander");
```

## ? Rate Limiting

El servicio implementa rate limiting automático respetando el límite de 10 requests/segundo de Scryfall.

## ?? Modelos Principales

### ScryfallCard
Representa una carta completa con todos sus atributos:
- Información básica (nombre, coste, tipo)
- Imágenes en múltiples tamaños
- Legalidades por formato
- Precios
- Texto del oráculo
- Estadísticas (poder, resistencia, lealtad)

### ScryfallSet
Información de colecciones:
- Código y nombre
- Fecha de lanzamiento
- Cantidad de cartas
- Tipo de set

### ScryfallSymbol
Símbolos de maná con:
- Representación textual
- URI de SVG
- Información de colores
- Valor de maná

## ?? Casos de Uso para DeckBrew

1. **Búsqueda de Cartas**: Autocompletado y búsqueda en tiempo real
2. **Construcción de Mazos**: Validar legalidad y verificar cartas
3. **Sugerencias de Comandantes**: Filtrar por identidad de color
4. **Visualización**: Mostrar imágenes de alta calidad
5. **Información de Precios**: Consultar valores actualizados
6. **Símbolos de Maná**: Renderizar correctamente en la UI

## ?? Referencias

- [Documentación oficial de Scryfall API](https://scryfall.com/docs/api)
- [Sintaxis de búsqueda](https://scryfall.com/docs/syntax)
