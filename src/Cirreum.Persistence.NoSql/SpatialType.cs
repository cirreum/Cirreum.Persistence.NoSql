namespace Cirreum.Persistence;

/// <summary>
/// Specifies the supported spatial types for spatial indexing.
/// </summary>
public enum SpatialType {

	/// <summary>
	/// Represents a point geometry.
	/// </summary>
	Point = 0,

	/// <summary>
	/// Represents a line string geometry.
	/// </summary>
	LineString = 1,

	/// <summary>
	/// Represents a polygon geometry.
	/// </summary>
	Polygon = 2,

	/// <summary>
	/// Represents a multi-polygon geometry.
	/// </summary>
	MultiPolygon = 3
}
