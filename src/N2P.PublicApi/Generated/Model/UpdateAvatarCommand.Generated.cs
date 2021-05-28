using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace N2P.PublicApi.Generated.Model
{
    public class UpdateAvatarCommand : IRequest<UpdateAvatarCommandResult>
    {
        [FromForm]
        public IFormFile AvatarImageFile { get; set; }
    }
}
