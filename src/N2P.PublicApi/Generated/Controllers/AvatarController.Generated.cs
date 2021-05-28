using System.CodeDom.Compiler;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using N2P.PublicApi.Generated.Model;

namespace N2P.PublicApi.Generated.Controllers
{
    [GeneratedCode("N2PApiServerGenerator", "1.0.0")]
    [ApiController]
    public class AvatarController: ControllerBase
    {
        private readonly IMediator _mediator;

        public AvatarController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPut, Route("users/me/avatar", Name = "UpdateMyAvatar")]
        public async Task<ActionResult> UpdateMyAvatar([FromForm]UpdateAvatarCommand command)
        {
            var result = await _mediator.Send(command);
            return ((IResponse) result).MapToActionResult(this);
        }
        [HttpGet, Route("users/{userId:int}/avatar-{avatarImageSize}.jpg", Name = "GetAvatarImage")]
        public async Task<ActionResult> GetAvatarImage([FromRoute]int userId, [FromRoute] string avatarImageSize)
        {
            var query = new GetAvatarImageQuery();
            query.UserId = userId;
            query.AvatarImageSize = avatarImageSize;
            var result = await _mediator.Send(query);
            return ((IResponse) result).MapToActionResult(this);
        }
    }
}
