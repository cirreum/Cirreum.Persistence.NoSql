namespace Cirreum.Persistence;

using System;

/// <summary>
/// The partition key path attribute exposes the ability to declaratively
/// specify a partition key path. This attribute should be used in
/// conjunction with a <see cref="System.Text.Json.Serialization.JsonPropertyNameAttribute"/> on the property
/// whose value will act as the partition key. Partition key paths should start with "/",
/// for example "/partition". For more information,
/// see https://docs.microsoft.com/azure/cosmos-db/partitioning-overview.
/// </summary>
/// <remarks>
/// By default, "/id" is used.
/// </remarks>
/// <remarks>
/// Constructor accepting the <paramref name="path"/> of the partition key.
/// </remarks>
/// <param name="path"></param>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public sealed class PartitionKeyPathAttribute(string path) : Attribute {

	/// <summary>
	/// Gets the path of the parition key.
	/// </summary>
	public string Path { get; } = path ?? throw new ArgumentNullException(nameof(path), "A path is required.");
}