using Coinbase.AdvancedTradeApiClient.Enums;
using System;

namespace Coinbase.AdvancedTradeApiClient.Models.Queries;

/// <summary>
/// Représente un ensemble de critères pour filtrer la recherche d'ordres
/// via l'API Advanced Trade de Coinbase.
/// </summary>
/// <remarks>
/// - Une propriété à null signifie « pas de filtre » pour ce critère.
/// - Les dates sont supposées en UTC.
/// - Si <see cref="StartDate"/> et <see cref="EndDate"/> sont définies, <see cref="StartDate"/> doit être inférieure ou égale à <see cref="EndDate"/>.
/// </remarks>
public class OrderQueryFilter
{
    /// <summary>
    /// Identifiant du produit (par ex. "BTC-USD"). Null pour inclure tous les produits.
    /// </summary>
    public string? ProductId { get; set; } = null;

    /// <summary>
    /// Statuts d'ordres à inclure. Null pour ne pas filtrer par statut.
    /// </summary>
    public OrderStatus[]? OrderStatus { get; set; } = null;

    /// <summary>
    /// Début (UTC) de la fenêtre temporelle de recherche. Null pour aucune borne basse.
    /// </summary>
    public DateTime? StartDate { get; set; } = null;

    /// <summary>
    /// Fin (UTC) de la fenêtre temporelle de recherche. Null pour aucune borne haute.
    /// </summary>
    public DateTime? EndDate { get; set; } = null;

    /// <summary>
    /// Type d'ordre à inclure (Market, Limit, Stop, StopLimit). Null pour tous.
    /// </summary>
    public OrderType? OrderType { get; set; } = null;

    /// <summary>
    /// Côté de l'ordre (Buy ou Sell). Null pour inclure les deux côtés.
    /// </summary>
    public OrderSide? OrderSide { get; set; } = null;

    /// <summary>
    /// Logique de tri des résultats. Null pour l'ordre par défaut
    /// </summary>
    public OrderSortingType? SortingType { get; set; } = null;

    public int? Limit { get; set; } = null;
}
