using JuTCo.Core.Domain;
using JuTCo.Web.Review.Contracts;
using Riok.Mapperly.Abstractions;

namespace JuTCo.Web.Mapping;

[Mapper]
public static partial class ReviewMapper
{
    public static partial ReviewResultModel ToModel(this ReviewResult reviewResult);
}