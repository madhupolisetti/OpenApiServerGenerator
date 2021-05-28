using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using N2P.PublicApi.Generated.Model;

namespace N2P.PublicApi.Core.Features.Avatar
{
    public class UpdateAvatarCommandHandler : IRequestHandler<UpdateAvatarCommand, UpdateAvatarCommandResult>
    {
        public Task<UpdateAvatarCommandResult> Handle(UpdateAvatarCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
