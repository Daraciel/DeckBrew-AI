using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using DeckBrew.Contracts;
using DeckBrew.Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace DeckBrew.Mobile.Views
{
    public partial class HomePage : ContentPage
    {
        private readonly IDeckBrewApi _api;
        public ObservableCollection<CardSlotDto> Cards { get; } = new();
        private readonly HashSet<string> _selectedColors = new();

        // Mapeo de botones a códigos de color MTG
        private readonly Dictionary<string, string> _colorMapping = new()
        {
            { "WhiteButton", "W" },
            { "BlueButton", "U" },
            { "BlackButton", "B" },
            { "RedButton", "R" },
            { "GreenButton", "G" },
            { "ColorlessButton", "C" }
        };

        // Mapeo de botones a sus frames
        private readonly Dictionary<string, View> _frameMapping = new();

        public HomePage(IDeckBrewApi api)
        {
            InitializeComponent();
            _api = api ?? throw new InvalidOperationException("API client not resolved");
            CardsList.ItemsSource = Cards;

            // Inicializar mapeo de frames
            _frameMapping["WhiteButton"] = WhiteFrame;
            _frameMapping["BlueButton"] = BlueFrame;
            _frameMapping["BlackButton"] = BlackFrame;
            _frameMapping["RedButton"] = RedFrame;
            _frameMapping["GreenButton"] = GreenFrame;
            _frameMapping["ColorlessButton"] = ColorlessFrame;
        }

        private async void OnColorButtonClicked(object sender, EventArgs e)
        {
            if (sender is not Button button) return;

            var styleId = button.StyleId;
            if (string.IsNullOrEmpty(styleId)) return;

            var colorCode = _colorMapping[styleId];
            var frame = _frameMapping[styleId];
            
            // Toggle color selection
            if (_selectedColors.Contains(colorCode))
            {
                _selectedColors.Remove(colorCode);
                // Restaurar apariencia original (no presionado)
                await AnimateUnpress(frame, button);
            }
            else
            {
                _selectedColors.Add(colorCode);
                // Marcar como seleccionado (presionado)
                await AnimatePress(frame, button);
            }
        }

        private async Task AnimatePress(View frame, Button button)
        {
            // Efecto de botón presionado: escala reducida, sin sombra, más opaco
            await Task.WhenAll(
                frame.ScaleToAsync(0.9, 100, Easing.CubicOut),
                button.ScaleToAsync(0.9, 100, Easing.CubicOut)
            );

            //if (frame is Frame f)
            //    f.HasShadow = false;
            button.Opacity = 0.7;
        }

        private async Task AnimateUnpress(View frame, Button button)
        {
            // Efecto de botón no presionado: escala normal, con sombra, opacidad normal
            await Task.WhenAll(
                frame.ScaleToAsync(1.0, 100, Easing.CubicOut),
                button.ScaleToAsync(1.0, 100, Easing.CubicOut)
            );

            //if (frame is Frame f)
            //    f.HasShadow = true;
            button.Opacity = 1.0;
        }

        private async void OnGenerateClicked(object sender, EventArgs e)
        {
            try
            {
                // Validar que se haya seleccionado al menos un color
                if (_selectedColors.Count == 0)
                {
                    await DisplayAlertAsync("Error", "Debes seleccionar al menos un color", "OK");
                    return;
                }

                var request = new GenerateDeckRequest
                {
                    Request = new GenerationRequestDto
                    {
                        Format = FormatPicker.SelectedItem?.ToString() ?? "Standard",
                        Colors = _selectedColors.ToArray(),
                        Style = StylePicker.SelectedItem?.ToString() ?? "midrange",
                        Budget = double.TryParse(BudgetEntry.Text, out var b) ? b : null
                    }
                };

                var response = await _api.GenerateDeckAsync(request);
                
                Cards.Clear();
                
                if (response?.Cards != null)
                {
                    foreach (var c in response.Cards) 
                    {
                        Cards.Add(c);
                    }
                }
                
                await DisplayAlertAsync("Éxito", $"Mazo generado con {Cards.Count} cartas", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync("Error", $"No se pudo generar el mazo: {ex.Message}", "OK");
            }
        }
    }
}
