using Microsoft.AspNetCore.Mvc;

namespace N2P.PublicApi.Generated.Model
{
    public interface IResponse
    {
        ActionResult MapToActionResult(ControllerBase controllerBase);
    }
}
