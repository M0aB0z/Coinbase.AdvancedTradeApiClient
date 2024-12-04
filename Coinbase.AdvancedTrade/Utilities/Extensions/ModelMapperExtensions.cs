using System.Collections.Generic;
using System.Linq;

namespace Coinbase.AdvancedTrade.Utilities.Extensions;

internal static class ModelMapperExtensions
{
    public static IReadOnlyList<TModel> ToModel<TModel>(this IEnumerable<IModelMapper<TModel>> apiModels)
        where TModel : class
        => apiModels.Select(x => x.ToModel()).ToArray();
}
