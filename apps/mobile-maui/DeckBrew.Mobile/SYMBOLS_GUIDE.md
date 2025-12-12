# Actualización a Símbolos de Mana con Imágenes

## Estado Actual ?

Los botones ahora son **circulares** con las letras W, U, B, R, G, C en los colores correspondientes de MTG.

## Para Usar Imágenes de Símbolos Reales

### Paso 1: Descargar las imágenes de símbolos

Puedes obtener los símbolos oficiales de estas fuentes:

1. **Keyrune** - Fuente de iconos MTG como PNG
   - https://github.com/andrewgioia/keyrune
   
2. **Mana Font** - Generador de símbolos
   - https://mana.andrewgioia.com/
   
3. **Scryfall API** - Símbolos oficiales
   ```
   https://svgs.scryfall.io/card-symbols/W.svg
   https://svgs.scryfall.io/card-symbols/U.svg
   https://svgs.scryfall.io/card-symbols/B.svg
   https://svgs.scryfall.io/card-symbols/R.svg
   https://svgs.scryfall.io/card-symbols/G.svg
   https://svgs.scryfall.io/card-symbols/C.svg
   ```

### Paso 2: Convertir SVG a PNG (si es necesario)

Si descargas SVG, convierte a PNG:
- **Tamaño recomendado**: 200x200 px
- **Fondo**: Transparente
- **Formato**: PNG

Herramientas:
- https://cloudconvert.com/svg-to-png
- GIMP, Inkscape, Photoshop

### Paso 3: Agregar al proyecto

1. Coloca los archivos en: `apps/mobile-maui/DeckBrew.Mobile/Resources/Images/`
   ```
   mana_w.png
   mana_u.png
   mana_b.png
   mana_r.png
   mana_g.png
   mana_c.png
   ```

2. En Visual Studio:
   - Click derecho en cada imagen
   - Properties ? Build Action ? **MauiImage**

### Paso 4: Actualizar HomePage.xaml

Reemplaza los `<Button>` por `<ImageButton>`:

```xml
<!-- Ejemplo para Blanco -->
<Border StrokeThickness="0" 
        x:Name="WhiteBorder"
        Padding="3"
        Margin="5">
  <Border.StrokeShape>
    <RoundRectangle CornerRadius="40"/>
  </Border.StrokeShape>
  <ImageButton x:Name="WhiteButton" 
               StyleId="WhiteButton"
               Source="mana_w.png"
               BackgroundColor="#F8F6D8"
               WidthRequest="70"
               HeightRequest="70"
               CornerRadius="35"
               Clicked="OnColorButtonClicked"
               Aspect="AspectFit"
               Padding="10"/>
</Border>
```

Repite para todos los colores cambiando:
- `x:Name` y `StyleId`
- `Source="mana_X.png"`
- `BackgroundColor`

### Paso 5: Actualizar HomePage.xaml.cs

En el método `OnColorButtonClicked`, cambia:
```csharp
if (sender is not Button button) return;
```

Por:
```csharp
if (sender is not ImageButton button) return;
```

## Alternativa: Usar Fuentes de Iconos

Si prefieres usar fuentes en lugar de imágenes:

1. Descarga **Mana Font** de https://mana.andrewgioia.com/
2. Agrégala al proyecto en `Resources/Fonts/`
3. Usa `FontImageSource` en lugar de archivos PNG

Ejemplo:
```xml
<ImageButton.Source>
  <FontImageSource 
    FontFamily="Mana"
    Glyph="&#xe600;"
    Color="White"
    Size="40"/>
</ImageButton.Source>
```

## Diseño Actual vs Con Imágenes

### Actual (Letras)
```
? W    ?? U    ? B
?? R    ?? G    ? C
```

### Con Imágenes
```
[??]   [??]   [??]
[??]   [??]   [?]
```
Donde cada símbolo es la imagen real del mana de MTG.

## Notas Importantes

- Las imágenes deben tener **fondo transparente**
- El `CornerRadius="35"` hace los botones perfectamente circulares (70/2)
- El `Border` con `StrokeThickness` crea el efecto de selección dorado
- El `Padding="10"` en ImageButton da espacio al símbolo dentro del círculo
