using CatastrofizerGenerator;

namespace CatastrofizerDemo.Models
{
    public partial class GiftCard: ICatastrofizable
    {
        public string ClaimCode { get; set; }
        public string Pin { get; set; }
        public decimal Amount { get; set; }
    }
}
