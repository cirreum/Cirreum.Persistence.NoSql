namespace Cirreum.Persistence;

using System;

/// <summary>
/// The container attribute exposes the ability to declaratively
/// specify a persistence container name.
/// </summary>
/// <remarks>
/// Constructor accepting the <paramref name="name"/> of the container.
/// </remarks>
/// <param name="name"></param>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public sealed class ContainerAttribute(string name) : Attribute {

	/// <summary>
	/// Gets the path of the parition key.
	/// </summary>
	public string Name { get; } = name ?? throw new ArgumentNullException(nameof(name), "A name is required.");
}