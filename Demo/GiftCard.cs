using CatastrofizerGenerator;

namespace CatastrofizerLiveDemo
{
    public partial class GiftCard : ICatastrofizable
    {
        public string ClaimCode { get; set; }
        public string Sku { get; set;}
        public decimal Amount { get; set;}
    }
}
