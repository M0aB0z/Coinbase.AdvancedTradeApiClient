namespace Coinbase.AdvancedTrade.Utilities;

internal interface IModelMapper<TModel> where TModel : class
{
    TModel ToModel();
}
