using System;
using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace N2P.PublicApi.Generated.Model
{
    public class GetAvatarImageQuery : IRequest<GetAvatarImageResult>
    {
        [JsonIgnore]
        public int UserId { get; set; }

        [JsonIgnore]
        public string AvatarImageSize { get; set; }
    }
}
