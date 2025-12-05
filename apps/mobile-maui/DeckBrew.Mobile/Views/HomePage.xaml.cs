using Microsoft.Maui.Controls;
using DeckBrew.Contracts; // Usar el contrato compartido
using DeckBrew.Contracts.DTOs;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace DeckBrew.Mobile.Views
{
    public partial class HomePage : ContentPage
    {
        private readonly IDeckBrewApi _api;
        public ObservableCollection<CardSlotDto> Cards { get; } = new();

        public HomePage(IDeckBrewApi api)
        {
            InitializeComponent();
            _api = api ?? throw new InvalidOperationException("API client not resolved");
            CardsList.ItemsSource = Cards;
        }

        private async void OnGenerateClicked(object sender, EventArgs e)
        {
            var request = new GenerateDeckRequest
            {
                Request = new GenerationRequestDto
                {
                    Format = FormatPicker.SelectedItem?.ToString() ?? "Standard",
                    Colors = (ColorsEntry.Text ?? "U").Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries),
                    Style = StylePicker.SelectedItem?.ToString() ?? "midrange",
                    Budget = double.TryParse(BudgetEntry.Text, out var b) ? b : null
                }
            };

            var response = await _api.GenerateDeckAsync(request);
            Cards.Clear();
            foreach (var c in response.Cards) 
                Cards.Add(c);
        }
    }
}
