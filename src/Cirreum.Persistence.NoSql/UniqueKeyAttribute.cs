namespace Cirreum.Persistence;

using System;

/// <summary>
/// The unique key attribute exposes the ability to declaratively
/// specify a property that can contribute to a unique key constraint.
/// For more information, see https://docs.microsoft.com/azure/cosmos-db/unique-keys.
/// </summary>
/// <remarks>
/// Constructor accepting the <paramref name="keyName"/> for a given property.
/// </remarks>
/// <param name="keyName"></param>
/// <param name="propertyPath">The property path to match for the constraint</param>
/// <remarks>If the propertyPath is null the name of the property this is defined will be used.</remarks>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public sealed class UniqueKeyAttribute(string keyName, string propertyPath) : Attribute {

	/// <summary>
	/// Gets the key name that represents the unique key.
	/// </summary>
	/// <remarks>This is the unique name to match a set of paths on</remarks>
	public string KeyName { get; } = keyName ?? "onlyUniqueKey";

	/// <summary>
	/// The property path to use for the key
	/// </summary>
	public string PropertyPath { get; set; } = propertyPath;
}