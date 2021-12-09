
using CatastrofizerGenerator;

namespace NotifyPropertyChangedLiveDemo
{
  public partial class CarModel : ICatastrofizable
  {
    public double SpeedKmPerHour{ get; set; }
    public int NumberOfDoors{ get; set; }
    public string Model{ get; set; }

  }
}