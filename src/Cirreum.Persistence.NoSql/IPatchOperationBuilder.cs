namespace Cirreum.Persistence;

using System;
using System.Linq.Expressions;

/// <summary>
/// Allows a collection of PatchOperation's to be built./>
/// </summary>
public interface IPatchOperationBuilder<TEntity> where TEntity : IEntity {

	// ByExpression

	/// <summary>
	/// Add a property with the specified value.
	/// </summary>
	/// <typeparam name="TValue">The type of the property that is being replaced.</typeparam>
	/// <param name="expression">The expression to define which property to operate on.</param>
	/// <param name="value">The value of the property being added.</param>
	/// <returns>The source instance of <see cref="IPatchOperationBuilder{TEntity}"/></returns>
	/// <remarks>
	/// Add performs one of the following, depending on the target path:
	/// <list type="bullet">
	///     <item>
	///         <description>If the target path specifies an element that doesn't exist, it's added.</description>
	///     </item>
	///     <item>
	///         <description>If the target path specifies an element that already exists, its value is replaced.</description>
	///     </item>
	///     <item>
	///         <description>If the target path is a valid array index, a new element is inserted into the array at the specified index. This shifts existing elements after the new element.</description>
	///     </item>
	///     <item>
	///         <description>If the index specified is equal to the length of the array, it appends an element to the array. Instead of specifying an index, you can also use the - character. It also results in the element being appended to the array.</description>
	///     </item>
	/// </list>
	/// <para>
	/// Note: Specifying an index greater than the array length results in an error.
	/// </para>
	/// </remarks>
	IPatchOperationBuilder<TEntity> Add<TValue>(Expression<Func<TEntity, TValue>> expression, TValue value);

	/// <summary>
	/// Set a property with the specified value.
	/// </summary>
	/// <typeparam name="TValue">The type of the property that is being set.</typeparam>
	/// <param name="expression">The expression to define which property to operate on.</param>
	/// <param name="value">The value to set the property defined with.</param>
	/// <returns>The source instance of <see cref="IPatchOperationBuilder{TEntity}"/></returns>
	/// <remarks>
	/// <para>
	/// Set operation is similar to Add except with the Array data type. If the target path is a
	/// valid array index, the existing element at that index is updated
	/// </para>
	/// </remarks>
	IPatchOperationBuilder<TEntity> Set<TValue>(Expression<Func<TEntity, TValue>> expression, TValue value);

	/// <summary>
	/// Replace a property with the specified value.
	/// </summary>
	/// <typeparam name="TValue">The type of the property that is being replaced.</typeparam>
	/// <param name="expression">The expression to define which property to operate on.</param>
	/// <param name="value">The value to replace the property defined with.</param>
	/// <returns>The source instance of <see cref="IPatchOperationBuilder{TEntity}"/></returns>
	/// <remarks>
	/// <para>
	/// Replace operation is similar to Set except it follows strict replace only semantics. In case the
	/// target path specifies an element or an array that doesn't exist, it results in an error.
	/// </para>
	/// <para>
	/// This currently only supports operations on properties on the root level of a JSON document,
	/// replacing properties on a nested object for example are currently not supported.
	/// </para>
	/// </remarks>
	IPatchOperationBuilder<TEntity> Replace<TValue>(Expression<Func<TEntity, TValue>> expression, TValue value);

	/// <summary>
	/// Remove performs one of the following, depending on the target path:
	/// </summary>
	/// <typeparam name="TValue">The type of the property that is been replaced.</typeparam>
	/// <param name="expression">The expression to define which property to operate on.</param>
	/// <returns>The source instance of <see cref="IPatchOperationBuilder{TEntity}"/></returns>
	/// <remarks>
	/// <list type="bullet">
	///     <item>
	///         <description>If the target path specifies an element that doesn't exist, it results in an error.</description>
	///     </item>
	///     <item>
	///         <description>If the target path specifies an element that already exists, it's removed.</description>
	///     </item>
	///     <item>
	///         <description>If the target path is an array index, it's deleted and any elements above the specified index are shifted back one position.</description>
	///     </item>
	/// </list>
	/// <para>
	/// Note: Specifying an index equal to or greater than the array length would result in an error.
	/// </para>
	/// </remarks>
	IPatchOperationBuilder<TEntity> Remove<TValue>(Expression<Func<TEntity, TValue>> expression);

	/// <summary>
	/// Increment a <see cref="long"/> value.
	/// </summary>
	/// <typeparam name="TValue">The type of the property that is being incremented.</typeparam>
	/// <param name="expression">The property access expression.</param>
	/// <param name="value">The value to increment by.</param>
	/// <returns>The source instance of <see cref="IPatchOperationBuilder{TEntity}"/></returns>
	/// <remarks>
	/// <para>
	/// This operator increments a field by the specified value. It can accept both positive and negative
	/// values. If the field doesn't exist, it creates the field and sets it to the specified value.
	/// </para>
	/// </remarks>
	IPatchOperationBuilder<TEntity> Increment<TValue>(Expression<Func<TEntity, TValue>> expression, long value);

	/// <summary>
	/// Increment a <see cref="double"/> value.
	/// </summary>
	/// <typeparam name="TValue">The type of the property that is being incremented.</typeparam>
	/// <param name="expression">The property access expression.</param>
	/// <param name="value">The value to increment by.</param>
	/// <returns>The source instance of <see cref="IPatchOperationBuilder{TEntity}"/></returns>
	/// <remarks>
	/// <para>
	/// This operator increments a field by the specified value. It can accept both positive and negative
	/// values. If the field doesn't exist, it creates the field and sets it to the specified value.
	/// </para>
	/// </remarks>
	IPatchOperationBuilder<TEntity> Increment<TValue>(Expression<Func<TEntity, TValue>> expression, double value);


	//ByPath

	/// <summary>
	/// Add a property with the specified value using a string path.
	/// </summary>
	/// <typeparam name="TValue">The type of the property that is being added.</typeparam>
	/// <param name="propertyPath">The string path to the property (e.g. "propertyName" or "/propertyName").</param>
	/// <param name="value">The value of the property being added.</param>
	/// <returns>The source instance of <see cref="IPatchOperationBuilder{TEntity}"/></returns>
	/// <remarks>
	/// Add performs one of the following, depending on the target path:
	/// <list type="bullet">
	///     <item>
	///         <description>If the target path specifies an element that doesn't exist, it's added.</description>
	///     </item>
	///     <item>
	///         <description>If the target path specifies an element that already exists, its value is replaced.</description>
	///     </item>
	///     <item>
	///         <description>If the target path is a valid array index, a new element is inserted into the array at the specified index. This shifts existing elements after the new element.</description>
	///     </item>
	///     <item>
	///         <description>If the index specified is equal to the length of the array, it appends an element to the array. Instead of specifying an index, you can also use the - character. It also results in the element being appended to the array.</description>
	///     </item>
	/// </list>
	/// <para>
	/// Note: Specifying an index greater than the array length results in an error.
	/// </para>
	/// </remarks>
	IPatchOperationBuilder<TEntity> AddByPath<TValue>(string propertyPath, TValue value);

	/// <summary>
	/// Set a property with the specified value using a string path.
	/// </summary>
	/// <typeparam name="TValue">The type of the property that is being set.</typeparam>
	/// <param name="propertyPath">The string path to the property (e.g. "propertyName" or "/propertyName").</param>
	/// <param name="value">The value to set the property defined with.</param>
	/// <returns>The source instance of <see cref="IPatchOperationBuilder{TEntity}"/></returns>
	/// <remarks>
	/// <para>
	/// Set operation is similar to Add except with the Array data type. If the target path is a
	/// valid array index, the existing element at that index is updated.
	/// </para>
	/// <para>
	/// Use this method when you need to set properties on entities that may not be directly expressible
	/// through the expression-based methods, such as when working with interface properties.
	/// </para>
	/// </remarks>
	IPatchOperationBuilder<TEntity> SetByPath<TValue>(string propertyPath, TValue value);

	/// <summary>
	/// Replace a property with the specified value using a string path.
	/// </summary>
	/// <typeparam name="TValue">The type of the property that is being replaced.</typeparam>
	/// <param name="propertyPath">The string path to the property (e.g. "propertyName" or "/propertyName").</param>
	/// <param name="value">The value to replace the property defined with.</param>
	/// <returns>The source instance of <see cref="IPatchOperationBuilder{TEntity}"/></returns>
	/// <remarks>
	/// <para>
	/// Replace operation is similar to Set except it follows strict replace only semantics. In case the
	/// target path specifies an element or an array that doesn't exist, it results in an error.
	/// </para>
	/// <para>
	/// This method allows you to specify the property path directly as a string, which is useful when
	/// working with interface properties or when the property path cannot be expressed as a lambda expression.
	/// </para>
	/// </remarks>
	IPatchOperationBuilder<TEntity> ReplaceByPath<TValue>(string propertyPath, TValue value);

	/// <summary>
	/// Remove a property using a string path.
	/// </summary>
	/// <param name="propertyPath">The string path to the property (e.g. "propertyName" or "/propertyName").</param>
	/// <returns>The source instance of <see cref="IPatchOperationBuilder{TEntity}"/></returns>
	/// <remarks>
	/// Remove performs one of the following, depending on the target path:
	/// <list type="bullet">
	///     <item>
	///         <description>If the target path specifies an element that doesn't exist, it results in an error.</description>
	///     </item>
	///     <item>
	///         <description>If the target path specifies an element that already exists, it's removed.</description>
	///     </item>
	///     <item>
	///         <description>If the target path is an array index, it's deleted and any elements above the specified index are shifted back one position.</description>
	///     </item>
	/// </list>
	/// <para>
	/// Note: Specifying an index equal to or greater than the array length would result in an error.
	/// </para>
	/// </remarks>
	IPatchOperationBuilder<TEntity> RemoveByPath(string propertyPath);

	/// <summary>
	/// Increment a numeric value using a string path.
	/// </summary>
	/// <param name="propertyPath">The string path to the property (e.g. "propertyName" or "/propertyName").</param>
	/// <param name="value">The value to increment by.</param>
	/// <returns>The source instance of <see cref="IPatchOperationBuilder{TEntity}"/></returns>
	/// <remarks>
	/// <para>
	/// This operator increments a field by the specified value. It can accept both positive and negative
	/// values. If the field doesn't exist, it creates the field and sets it to the specified value.
	/// </para>
	/// <para>
	/// This method is particularly useful when incrementing numeric properties that may be defined in interfaces
	/// or base classes and cannot be directly accessed through a lambda expression due to conversion issues.
	/// </para>
	/// </remarks>
	IPatchOperationBuilder<TEntity> IncrementByPath(string propertyPath, long value);

	/// <summary>
	/// Increment a floating-point value using a string path.
	/// </summary>
	/// <param name="propertyPath">The string path to the property (e.g. "propertyName" or "/propertyName").</param>
	/// <param name="value">The value to increment by.</param>
	/// <returns>The source instance of <see cref="IPatchOperationBuilder{TEntity}"/></returns>
	/// <remarks>
	/// <para>
	/// This operator increments a field by the specified value. It can accept both positive and negative
	/// values. If the field doesn't exist, it creates the field and sets it to the specified value.
	/// </para>
	/// <para>
	/// This method uses a string path rather than an expression, making it suitable for cases where
	/// the property is defined in an interface or base class and cannot be directly accessed through
	/// a lambda expression.
	/// </para>
	/// </remarks>
	IPatchOperationBuilder<TEntity> IncrementByPath(string propertyPath, double value);

}