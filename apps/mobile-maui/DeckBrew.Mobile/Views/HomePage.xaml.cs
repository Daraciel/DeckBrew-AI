using Microsoft.Maui.Controls;
using DeckBrew.Mobile.Services;
using System;
using System.Collections.ObjectModel;

namespace DeckBrew.Mobile.Views
{
    public partial class HomePage : ContentPage
    {
        private readonly IDeckbrewApi _api;
        public ObservableCollection<CardSlotDto> Cards { get; } = new();

        public HomePage()
        {
            InitializeComponent();
            _api = Handler?.MauiContext?.Services.GetService(typeof(IDeckbrewApi)) as IDeckbrewApi
                   ?? throw new InvalidOperationException("API client not resolved");
            CardsList.ItemsSource = Cards;
        }

        private async void OnGenerateClicked(object sender, EventArgs e)
        {
            var req = new GenerationRequest
            {
                format = FormatPicker.SelectedItem?.ToString() ?? "Standard",
                colors = (ColorsEntry.Text ?? "U").Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries),
                style = StylePicker.SelectedItem?.ToString() ?? "midrange",
                budget = double.TryParse(BudgetEntry.Text, out var b) ? b : null
            };

            var deck = await _api.GenerateAsync(req);
            Cards.Clear();
            foreach (var c in deck.cards) Cards.Add(c);
        }
    }
}
