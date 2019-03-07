using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Rolodex.Messages.Commands;
using Rolodex.Messages.Commands.Responses;
using Rolodex.Web.ViewModels;

namespace Rolodex.Web.Controllers
{
    [Route("api/entries")]
    [ApiController]
    public class EntriesApiController : ControllerBase
    {
        readonly IBusControl            _busControl;
        readonly List<RolodexViewModel> _rolodexEntries;

        public EntriesApiController(List<RolodexViewModel> rolodexEntries, IBusControl busControl)
        {
            _rolodexEntries = rolodexEntries;
            _busControl     = busControl;
        }

        [HttpPost]
        public async Task<IActionResult> AddNew(NewEntryRequest entryRequest)
        {
            var response = await _busControl.Request<NewEntryRequest, INewEntryResponse>(entryRequest);

            var responseHandler = new ResponseHandler();
            response.Message.ApplyTo(responseHandler);

            return responseHandler.Result;
        }

        [HttpGet]
        public IEnumerable<RolodexViewModel> AllEntries()
            => _rolodexEntries;

        class ResponseHandler : INewEntryResponseHandler
        {
            public IActionResult Result { get; private set; }

            public void Fail(string message)
                => Result = new BadRequestObjectResult(message);

            public void Success()
                => Result = new OkResult();
        }
    }
}