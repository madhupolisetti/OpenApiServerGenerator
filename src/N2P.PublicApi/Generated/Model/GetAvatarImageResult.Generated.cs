using System;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace N2P.PublicApi.Generated.Model
{
    public class GetAvatarImageResult : IResponse
    {
        private readonly Func<ControllerBase, ActionResult> _actionResultFactory;

        private GetAvatarImageResult(Func<ControllerBase, ActionResult> actionResultFactory)
        {
            _actionResultFactory = actionResultFactory;
        }

        ActionResult IResponse.MapToActionResult(ControllerBase controllerBase)
        {
            return _actionResultFactory(controllerBase);
        }

        public static GetAvatarImageResult Success(string location)
        {
            return new(c => c.RedirectPreserveMethod(location));
        }
    }
}
