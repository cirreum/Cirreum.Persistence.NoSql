namespace Cirreum.Persistence;

using System;

/// <summary>
/// The composite index attribute exposes the ability to declaratively
/// specify a property that participates in a composite index.
/// Properties are grouped into composite indexes by <see cref="GroupName"/>
/// and ordered by <see cref="Position"/> within each group.
/// For more information, see https://learn.microsoft.com/azure/cosmos-db/index-policy#composite-indexes.
/// </summary>
/// <remarks>
/// Constructor accepting the <paramref name="groupName"/> to associate this property
/// with a named composite index group.
/// </remarks>
/// <param name="groupName">The name that groups properties into a single composite index.</param>
/// <param name="order">The sort order for this path in the composite index.</param>
/// <param name="position">The ordinal position of this path within the composite index group.</param>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public sealed class CompositeIndexAttribute(
	string groupName,
	CompositePathSortOrder order = CompositePathSortOrder.Ascending,
	int position = 0) : Attribute {

	/// <summary>
	/// Gets the group name that identifies the composite index this property belongs to.
	/// </summary>
	public string GroupName { get; } = groupName ?? throw new ArgumentNullException(nameof(groupName), "A group name is required.");

	/// <summary>
	/// Gets the sort order for this path in the composite index.
	/// </summary>
	public CompositePathSortOrder Order { get; } = order;

	/// <summary>
	/// Gets the ordinal position of this path within the composite index group.
	/// </summary>
	public int Position { get; } = position;

	/// <summary>
	/// Gets or sets an explicit path override.
	/// </summary>
	/// <remarks>
	/// When null, the path is auto-derived as <c>/{propertyName}</c>,
	/// using the <c>JsonPropertyNameAttribute</c> value if present,
	/// or the camelCase property name otherwise.
	/// </remarks>
	public string? Path { get; set; }
}
