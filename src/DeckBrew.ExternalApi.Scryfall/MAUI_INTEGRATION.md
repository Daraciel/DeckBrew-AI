# Integración con DeckBrew.Mobile

## ?? Agregar Referencia al Proyecto

En `DeckBrew.Mobile.csproj`, agrega la referencia:

```xml
<ItemGroup>
  <ProjectReference Include="..\..\..\src\DeckBrew.ExternalApi.Scryfall\DeckBrew.ExternalApi.Scryfall.csproj" />
</ItemGroup>
```

## ?? Configuración en MauiProgram.cs

```csharp
using DeckBrew.ExternalApi.Scryfall;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        // Registrar servicios de Scryfall
        builder.Services.AddScryfallServices();

        // Registrar tus ViewModels que usen Scryfall
        builder.Services.AddTransient<SearchViewModel>();
        builder.Services.AddTransient<CommanderSelectorViewModel>();

        return builder.Build();
    }
}
```

## ?? Ejemplo de ViewModel

```csharp
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DeckBrew.ExternalApi.Scryfall;
using DeckBrew.ExternalApi.Scryfall.Helpers;
using DeckBrew.ExternalApi.Scryfall.Models;
using System.Collections.ObjectModel;

namespace DeckBrew.Mobile.ViewModels;

public partial class SearchViewModel : ObservableObject
{
    private readonly IScryfallService _scryfallService;

    [ObservableProperty]
    private string _searchText = string.Empty;

    [ObservableProperty]
    private bool _isSearching;

    [ObservableProperty]
    private ObservableCollection<ScryfallCard> _searchResults = new();

    [ObservableProperty]
    private ObservableCollection<string> _suggestions = new();

    public SearchViewModel(IScryfallService scryfallService)
    {
        _scryfallService = scryfallService;
    }

    [RelayCommand]
    private async Task SearchCardsAsync()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
            return;

        IsSearching = true;

        try
        {
            var results = await _scryfallService.SearchCardsAsync(SearchText);
            SearchResults = new ObservableCollection<ScryfallCard>(results);
        }
        catch (Exception ex)
        {
            // Manejar error
            await Shell.Current.DisplayAlert("Error", $"Error buscando cartas: {ex.Message}", "OK");
        }
        finally
        {
            IsSearching = false;
        }
    }

    [RelayCommand]
    private async Task GetAutocompleteSuggestionsAsync()
    {
        if (string.IsNullOrWhiteSpace(SearchText) || SearchText.Length < 2)
        {
            Suggestions.Clear();
            return;
        }

        try
        {
            var suggestions = await _scryfallService.AutocompleteCardNameAsync(SearchText);
            Suggestions = new ObservableCollection<string>(suggestions);
        }
        catch
        {
            // Silenciosamente manejar errores de autocompletado
        }
    }

    partial void OnSearchTextChanged(string value)
    {
        GetAutocompleteSuggestionsCommand.Execute(null);
    }
}
```

## ?? Ejemplo de Vista XAML

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:vm="clr-namespace:DeckBrew.Mobile.ViewModels"
             x:Class="DeckBrew.Mobile.Views.SearchPage"
             Title="Buscar Cartas">

    <ContentPage.BindingContext>
        <vm:SearchViewModel />
    </ContentPage.BindingContext>

    <Grid RowDefinitions="Auto,*" Padding="20">
        
        <!-- Search Bar -->
        <VerticalStackLayout Grid.Row="0" Spacing="10">
            <Entry Text="{Binding SearchText}"
                   Placeholder="Buscar cartas..."
                   ReturnCommand="{Binding SearchCardsCommand}" />
            
            <!-- Suggestions -->
            <CollectionView ItemsSource="{Binding Suggestions}"
                           IsVisible="{Binding Suggestions.Count}"
                           MaximumHeightRequest="200">
                <CollectionView.ItemTemplate>
                    <DataTemplate>
                        <Label Text="{Binding .}"
                               Padding="10,5"
                               FontSize="14">
                            <Label.GestureRecognizers>
                                <TapGestureRecognizer 
                                    Command="{Binding Source={RelativeSource AncestorType={x:Type vm:SearchViewModel}}, Path=SelectSuggestionCommand}"
                                    CommandParameter="{Binding .}" />
                            </Label.GestureRecognizers>
                        </Label>
                    </DataTemplate>
                </CollectionView.ItemTemplate>
            </CollectionView>

            <Button Text="Buscar"
                    Command="{Binding SearchCardsCommand}"
                    IsEnabled="{Binding IsSearching, Converter={StaticResource InvertedBoolConverter}}" />
        </VerticalStackLayout>

        <!-- Results -->
        <CollectionView Grid.Row="1"
                       ItemsSource="{Binding SearchResults}"
                       Margin="0,20,0,0">
            <CollectionView.ItemTemplate>
                <DataTemplate>
                    <Grid ColumnDefinitions="80,*" Padding="0,10">
                        <Image Grid.Column="0"
                               Source="{Binding ImageUris.Small}"
                               Aspect="AspectFit"
                               HeightRequest="60" />
                        
                        <VerticalStackLayout Grid.Column="1" Padding="10,0">
                            <Label Text="{Binding Name}"
                                   FontAttributes="Bold"
                                   FontSize="16" />
                            <Label Text="{Binding ManaCost}"
                                   FontSize="14"
                                   TextColor="Gray" />
                            <Label Text="{Binding TypeLine}"
                                   FontSize="12"
                                   TextColor="Gray" />
                        </VerticalStackLayout>
                    </Grid>
                </DataTemplate>
            </CollectionView.ItemTemplate>
        </CollectionView>

        <!-- Loading Indicator -->
        <ActivityIndicator Grid.Row="1"
                          IsRunning="{Binding IsSearching}"
                          IsVisible="{Binding IsSearching}"
                          HorizontalOptions="Center"
                          VerticalOptions="Center" />
    </Grid>
</ContentPage>
```

## ?? Ejemplo de Búsqueda de Comandantes

```csharp
public partial class CommanderSelectorViewModel : ObservableObject
{
    private readonly IScryfallService _scryfallService;

    [ObservableProperty]
    private ObservableCollection<ScryfallCard> _commanders = new();

    [ObservableProperty]
    private string _selectedColorIdentity = string.Empty;

    public CommanderSelectorViewModel(IScryfallService scryfallService)
    {
        _scryfallService = scryfallService;
    }

    [RelayCommand]
    private async Task LoadCommandersAsync(string colorIdentity)
    {
        try
        {
            var commanders = await _scryfallService.SearchCommandersAsync(colorIdentity);
            Commanders = new ObservableCollection<ScryfallCard>(commanders);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Error cargando comandantes: {ex.Message}", "OK");
        }
    }

    [RelayCommand]
    private async Task SelectGrixisCommandersAsync()
    {
        await LoadCommandersAsync(ScryfallConstants.ColorIdentities.Grixis);
    }

    [RelayCommand]
    private async Task SelectBorosCommandersAsync()
    {
        await LoadCommandersAsync(ScryfallConstants.ColorIdentities.Boros);
    }
}
```

## ??? Cache de Imágenes

Para optimizar el rendimiento, considera implementar un sistema de cache:

```csharp
public class ImageCacheService
{
    private readonly IScryfallService _scryfallService;
    private readonly string _cacheFolder;
    private readonly Dictionary<string, string> _cachedImages = new();

    public ImageCacheService(IScryfallService scryfallService)
    {
        _scryfallService = scryfallService;
        _cacheFolder = Path.Combine(FileSystem.CacheDirectory, "card_images");
        Directory.CreateDirectory(_cacheFolder);
    }

    public async Task<string> GetCachedImagePathAsync(string imageUri, Guid cardId)
    {
        var cacheKey = $"{cardId}.jpg";
        var cachePath = Path.Combine(_cacheFolder, cacheKey);

        if (File.Exists(cachePath))
            return cachePath;

        var imageStream = await _scryfallService.GetCardImageAsync(imageUri);
        
        using var fileStream = File.Create(cachePath);
        await imageStream.CopyToAsync(fileStream);

        return cachePath;
    }
}
```

## ?? Tips y Mejores Prácticas

1. **Rate Limiting**: El servicio ya maneja el rate limiting automáticamente
2. **Cancelación**: Usa `CancellationToken` para cancelar búsquedas largas
3. **Cache**: Implementa cache local para imágenes y búsquedas frecuentes
4. **Offline**: Considera guardar resultados comunes offline
5. **Error Handling**: Maneja apropiadamente errores de red

## ?? Query Builder en UI

```csharp
// Construir queries dinámicamente basado en selección del usuario
var query = ScryfallQueryBuilder.Create();

if (IsCommanderFormat)
    query.WithFormat(ScryfallConstants.Formats.Commander);

if (SelectedColors.Any())
    query.WithCommanderIdentity(SelectedColors.ToArray());

if (MinCmc.HasValue)
    query.WithCmc(MinCmc.Value, ">=");

if (MaxCmc.HasValue)
    query.WithCmc(MaxCmc.Value, "<=");

var results = await _scryfallService.SearchCardsAsync(query.Build());
```
