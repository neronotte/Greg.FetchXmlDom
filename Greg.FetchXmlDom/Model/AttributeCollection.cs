using System.Collections;
using System.Collections.Generic;

namespace Greg.FetchXmlDom.Model
{
	/// <summary>
	/// Represents a collection of attributes to be returned by the query.
	/// </summary>
	public class AttributeCollection : IReadOnlyList<AttributeExpression>
	{
		private readonly List<AttributeExpression> _attributes = new List<AttributeExpression>();
		private bool _allAttributes = false;

		/// <summary>
		/// Gets the attribute at the specified index.
		/// </summary>
		/// <param name="index">The index of the attribute to return.</param>
		/// <returns>The attribute at the specified index</returns>
		public AttributeExpression this[int index] => _attributes[index];


		/// <summary>
		/// Indicates that all non-null column values for each row are returned. 
		/// It is the same as not adding any attribute elements. 
		/// We don't recommend using this element for most cases.
		/// </summary>
		public bool All 
		{
			get => _allAttributes;
			set 
			{
				if (value)
				{
					_attributes.Clear();
				}

				_allAttributes = value;
			}
		}

		/// <summary>
		/// Gets the total number of attributes in the collection.
		/// </summary>
		public int Count => _attributes.Count;

		/// <summary>
		/// Enumerates the attributes in the collection.
		/// </summary>
		/// <returns>An enumerator</returns>
		public IEnumerator<AttributeExpression> GetEnumerator()
		{
			return _attributes.GetEnumerator();
		}

		/// <summary>
		/// Enumerates the attributes in the collection.
		/// </summary>
		/// <returns>An enumerator</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <summary>
		/// Adds a new attribute in the collection.
		/// </summary>
		/// <param name="attribute">The expression to add to the collection</param>
		public AttributeCollection Add(AttributeExpression attribute)
		{
			_attributes.Add(attribute);
			_allAttributes = false;
			return this;
		}

		/// <summary>
		/// Adds a set of attributes in the collection.
		/// </summary>
		/// <param name="attributeNames">The list of names of the attributes to add to the collection</param>
		/// <returns>
		/// The current collection, to allow for fluent syntax combining multiple calls.
		/// </returns>
		public AttributeCollection Add(params string[] attributeNames)
		{
			foreach (var attributeName in attributeNames)
			{
				Add(new AttributeExpression(attributeName));
			}
			return this;
		}
	}
}
