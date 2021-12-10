using CatastrofizerGenerator;

namespace CatastrofizerDemo.Models
{
    public partial class Person : ICatastrofizable
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime Birthdaydate { get; set; }


        public bool EquivalentTo(PersonRequest request)
        {
            return request.Birthdaydate == Birthdaydate && request.Name == Name;  
        }
    }

    public partial class Dog : ICatastrofizable 
    {
        public string Name { get; set; }
        public bool IsGoodBoy { get; set; }
        public int Age { get; set; }
    }

}
