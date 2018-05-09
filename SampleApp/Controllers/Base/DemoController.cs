using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace SampleApp.Controllers.Base
{
    [EnableCors("CORS")]
    public class DemoController : Controller
    {
        protected const string API_PREFIX = "api/v1";
        protected const int PageSize = 10;
    }
}
