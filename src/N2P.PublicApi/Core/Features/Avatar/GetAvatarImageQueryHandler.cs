using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using N2P.PublicApi.Generated.Model;

namespace N2P.PublicApi.Core.Features.Avatar
{
    public class GetAvatarImageQueryHandler : IRequestHandler<GetAvatarImageQuery, GetAvatarImageResult>
    {
        public Task<GetAvatarImageResult>  Handle(GetAvatarImageQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
