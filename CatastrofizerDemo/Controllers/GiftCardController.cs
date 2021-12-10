using Microsoft.AspNetCore.Mvc;
using CatastrofizerDemo.Models;

namespace CatastrofizerDemo.Controllers
{
    public partial class GiftCardController
    {
        public Task<IActionResult> Update(GiftCardRequest request)
        {
            request.ToDomain();
            throw new NotImplementedException();
        }
    }
}
