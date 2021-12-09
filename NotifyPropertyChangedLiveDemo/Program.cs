using System;
using System.ComponentModel;

namespace CatastrofizerLiveDemo
{
  static class Program
  {
    static void Main()
    {
      var carModel = new CarModel ( "MyCar", 4, 200, "bobby" );

           // var carmodelRequest = new CatastrofizerGenerator.CarModelRequest();
      Console.Write("Got ");
      PrintCarDetails();

      Console.WriteLine();
      Console.WriteLine("Updating to racing model...");
      carModel.Model = "Racing " + carModel.Model;
      carModel.NumberOfDoors = 2;
      

      Console.WriteLine();
      Console.WriteLine("You got speeding ticket");
      Console.WriteLine("GAME OVER");

      return;



      void PrintCarDetails()
        => Console.WriteLine($"Car {{ Model: {carModel.Model},"
                             + $" Doors: {carModel.NumberOfDoors},"
                             + $" Speed: {carModel.SpeedKmPerHour:N0} km/h,  }}");
    }
  }
}