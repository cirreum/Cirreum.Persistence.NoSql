namespace Cirreum.Persistence;

using System;

/// <summary>
/// The indexing policy attribute exposes the ability to declaratively
/// specify the indexing mode and behavior for a container.
/// For more information, see https://learn.microsoft.com/azure/cosmos-db/index-policy.
/// </summary>
/// <remarks>
/// Constructor accepting the <paramref name="mode"/> for the indexing policy.
/// </remarks>
/// <param name="mode">The indexing mode to use for the container.</param>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public sealed class IndexingPolicyAttribute(IndexingMode mode) : Attribute {

	/// <summary>
	/// Gets the indexing mode.
	/// </summary>
	public IndexingMode Mode { get; } = mode;

	/// <summary>
	/// Gets or sets whether indexing is automatic.
	/// </summary>
	/// <remarks>Defaults to <see langword="true"/>.</remarks>
	public bool Automatic { get; set; } = true;
}
