namespace Coinbase.AdvancedTradeApiClient.Utilities;

internal interface IModelMapper<TModel> where TModel : class
{
    TModel ToModel();
}
