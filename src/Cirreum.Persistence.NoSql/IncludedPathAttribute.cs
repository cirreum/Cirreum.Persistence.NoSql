namespace Cirreum.Persistence;

using System;

/// <summary>
/// The included path attribute exposes the ability to declaratively
/// specify a property that should be included in the indexing policy.
/// For more information, see https://learn.microsoft.com/azure/cosmos-db/index-policy.
/// </summary>
/// <remarks>
/// Constructor accepting an optional explicit <paramref name="path"/>.
/// If the path is null, the resolver will derive it from the property name.
/// </remarks>
/// <param name="path">The explicit path to include, or null to auto-derive from the property name.</param>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class IncludedPathAttribute(string? path = null) : Attribute {

	/// <summary>
	/// Gets the explicit path to include in the indexing policy.
	/// </summary>
	/// <remarks>
	/// When null, the path is auto-derived as <c>/{propertyName}/?</c>,
	/// using the <c>JsonPropertyNameAttribute</c> value if present,
	/// or the camelCase property name otherwise.
	/// </remarks>
	public string? Path { get; } = path;
}
