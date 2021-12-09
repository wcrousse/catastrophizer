
using CatastrofizerGenerator;

namespace CatastrofizerLiveDemo
{
  public partial class CarModel : ICatastrofizable
  {
    public double SpeedKmPerHour{ get; set; }
    public int NumberOfDoors{ get; set; }
    public string Model{ get; set; }
    public string Bobby{ get; set; }

        public CarModel(string model, int numberOfDoors, double speedKmPerHour, string bobby)
        {
            Model = model;
            NumberOfDoors = numberOfDoors;
            SpeedKmPerHour = speedKmPerHour;
            Bobby = bobby;
        }
    }
}