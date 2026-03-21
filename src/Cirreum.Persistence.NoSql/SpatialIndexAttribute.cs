namespace Cirreum.Persistence;

using System;

/// <summary>
/// The spatial index attribute exposes the ability to declaratively
/// specify a property that should have a spatial index.
/// Multiple spatial types can be specified on the same property
/// by applying this attribute multiple times.
/// For more information, see https://learn.microsoft.com/azure/cosmos-db/index-policy#spatial-indexes.
/// </summary>
/// <remarks>
/// Constructor accepting the <paramref name="spatialType"/> to index.
/// </remarks>
/// <param name="spatialType">The spatial type to index on this property.</param>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public sealed class SpatialIndexAttribute(SpatialType spatialType) : Attribute {

	/// <summary>
	/// Gets the spatial type to index.
	/// </summary>
	public SpatialType SpatialType { get; } = spatialType;

	/// <summary>
	/// Gets or sets an explicit path override.
	/// </summary>
	/// <remarks>
	/// When null, the path is auto-derived as <c>/{propertyName}/*</c>,
	/// using the <c>JsonPropertyNameAttribute</c> value if present,
	/// or the camelCase property name otherwise.
	/// </remarks>
	public string? Path { get; set; }
}
