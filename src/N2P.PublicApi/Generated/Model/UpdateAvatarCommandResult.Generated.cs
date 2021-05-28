using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace N2P.PublicApi.Generated.Model
{
    public class UpdateAvatarCommandResult : IResponse
    {
        private readonly Func<ControllerBase, ActionResult> _actionResultFactory;

        private UpdateAvatarCommandResult(Func<ControllerBase, ActionResult> actionResultFactory)
        {
            _actionResultFactory = actionResultFactory;
        }

        ActionResult IResponse.MapToActionResult(ControllerBase controllerBase)
        {
            return _actionResultFactory(controllerBase);
        }

        public static UpdateAvatarCommandResult Success(AvatarModel avatarModel)
        {
            return new(c => c.Ok(avatarModel));
        }

        public static UpdateAvatarCommandResult Error(ValidationError error)
        {
            return new(c => c.UnprocessableEntity(error));
        }
    }
}
