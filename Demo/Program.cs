using System;

namespace CatastrofizerLiveDemo
{
    static class Program
  {
    static void Main()
    {
            var giftCardRequest = new Models.GiftCardRequest { Amount = 55m, ClaimCode = "imaclaimcode", Sku = "imasku" };
            var giftCard = giftCardRequest.ToDomain();
        Console.WriteLine("Good Times");
    }
  }
}