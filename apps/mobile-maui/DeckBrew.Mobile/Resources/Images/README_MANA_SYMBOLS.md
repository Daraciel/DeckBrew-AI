# Símbolos de Mana para DeckBrew

Esta carpeta debe contener los símbolos de mana de Magic: The Gathering.

## Archivos necesarios

Coloca las siguientes imágenes en esta carpeta (`Resources/Images/`):

- `mana_w.png` - Símbolo de maná blanco (W)
- `mana_u.png` - Símbolo de maná azul (U)
- `mana_b.png` - Símbolo de maná negro (B)
- `mana_r.png` - Símbolo de maná rojo (R)
- `mana_g.png` - Símbolo de maná verde (G)
- `mana_c.png` - Símbolo de maná incoloro (C)

## Recomendaciones

- **Tamaño**: 200x200 px (se escalará automáticamente)
- **Formato**: PNG con transparencia
- **Fondo**: Transparente

## Fuentes para obtener los símbolos

1. **Mana Font** - Fuente de iconos de MTG: https://mana.andrewgioia.com/
2. **Scryfall** - API con símbolos oficiales: https://scryfall.com/docs/api/card-symbols
3. **MTG Wiki** - Recursos de la comunidad

## Configuración en el proyecto

Las imágenes ya están referenciadas en `HomePage.xaml` como:
```xml
<ImageButton Source="mana_w.png" ... />
```

Solo necesitas agregar los archivos PNG a la carpeta `Resources/Images/` del proyecto MAUI.

## Alternativa temporal

Si no tienes acceso a las imágenes oficiales, puedes usar:
- Círculos de colores sólidos
- Letras grandes (W, U, B, R, G, C)
- Emojis como ?????????

## Build Action

Asegúrate de que las imágenes tengan el Build Action:
- **MauiImage** en Visual Studio

O agrégalas al `.csproj`:
```xml
<MauiImage Include="Resources\Images\mana_w.png" />
```
