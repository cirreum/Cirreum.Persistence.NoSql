namespace Cirreum.Persistence;

using System;

/// <summary>
/// The excluded path attribute exposes the ability to declaratively
/// specify a path that should be excluded from the indexing policy.
/// For more information, see https://learn.microsoft.com/azure/cosmos-db/index-policy.
/// </summary>
/// <remarks>
/// Constructor accepting the <paramref name="path"/> to exclude.
/// </remarks>
/// <param name="path">The path to exclude from indexing (e.g., "/description/*" or "/*").</param>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public sealed class ExcludedPathAttribute(string path) : Attribute {

	/// <summary>
	/// Gets the path to exclude from the indexing policy.
	/// </summary>
	public string Path { get; } = path ?? throw new ArgumentNullException(nameof(path), "A path is required.");
}
