using Refit;
using System;
using System.Threading.Tasks;

namespace DeckBrew.Mobile.Services
{
    public interface IDeckbrewApi
    {
        [Post("/v1/generate")]
        Task<DeckResponse> GenerateAsync([Body] GenerateDeckCommand request);
    }

    public class GenerateDeckCommand
    {
        public GenerationRequest request { get; set; } = new();
    }

    public class GenerationRequest
    {
        public string format { get; set; } = "Standard";
        public string[] colors { get; set; } = new[] { "U" };
        public string style { get; set; } = "midrange";
        public double? budget { get; set; }
    }

    public class DeckResponse
    {
        public CardSlotDto[] cards { get; set; } = Array.Empty<CardSlotDto>();
        public KeyCardDto[] keyCards { get; set; } = Array.Empty<KeyCardDto>();
        public string[] risks { get; set; } = Array.Empty<string>();
        public string mulligan { get; set; } = "";
    }

    public class CardSlotDto { public string name { get; set; } = ""; public int count { get; set; } }
    public class KeyCardDto { public string name { get; set; } = ""; public string rationale { get; set; } = ""; }
}
