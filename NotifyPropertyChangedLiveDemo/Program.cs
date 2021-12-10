using System;
using CatastrofizerLiveDemo.Models;

namespace CatastrofizerLiveDemo
{
  static class Program
  {
    static void Main()
    {
            var productRequest = new ProductRequest { CreatedOn = DateTime.Now, Description = "it's great!", Name = "bob", Price = 100m};
            var d = productRequest.ToDomain();
            Console.WriteLine("Good Times");
    }
  }
}