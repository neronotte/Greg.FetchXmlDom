using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml;

namespace Greg.FetchXmlDom.Model
{
	/// <summary>
	/// Object oriented representation of a FetchXml query.
	/// </summary>
    public class FetchXmlExpression : EntityBaseExpression, IValidatableObject
	{
		/// <summary>
		/// Creates a simple FetchXml expression
		/// </summary>
		public FetchXmlExpression()
        {
		}

		/// <summary>
		/// Creates a simple FetchXml expression
		/// </summary>
		/// <param name="tableName">The name of the main table</param>
		/// <exception cref="ArgumentNullException">If the name of the main table is not provided</exception>
		public FetchXmlExpression(string tableName)
		{
			if (string.IsNullOrWhiteSpace(tableName))
			{
				throw new ArgumentNullException(nameof(tableName), "Table name cannot be null or whitespace");
			}

			this.TableName = tableName;
		}


		/// <summary>
		/// Creates a FetchXml expression with paging management
		/// </summary>
		/// <param name="tableName">The name of the main table</param>
		/// <param name="page">The index of the page to fetch. Must be >= 0</param>
		/// <param name="pagingCookie">The paging cookie returned by the previous invocation of the query.</param>
		/// <param name="count">Optional. The number of records in each page.</param>
		/// <param name="returnTotalRecordCount">Optional. Indicates whether the total number of records matching the query criteria should be returned or not</param>
		/// <exception cref="ArgumentNullException">If the name of the main table is not provided</exception>
		public FetchXmlExpression(string tableName, int page, string pagingCookie, int? count = null, bool? returnTotalRecordCount = null)
			: this(tableName)
		{
			this.Page = page;
			this.PagingCookie = pagingCookie;
			this.Count = count;
		}


		/// <summary>
		/// Creates a FetchXml expression with a limit on the number of records to fetch
		/// </summary>
		/// <param name="tableName">The name of the main table</param>
		/// <param name="top">The number of records to fetch (must be between 1 and 5000)</param>
		/// <exception cref="ArgumentNullException">If the name of the main table is not provided</exception>
		/// <exception cref="ArgumentOutOfRangeException">If the top value is less than 1 or greater than 5000</exception>
		public FetchXmlExpression(string tableName, int top) 
			: this(tableName)
		{
			if (top < 1 || top > 5000)
				throw new ArgumentOutOfRangeException(nameof(top), "Top value must be between 1 and 5000.");

            this.Top = top;
		}

		/// <summary>
		/// Creates a FetchXml expression to be used for aggregate queries
		/// </summary>
		/// <param name="tableName">The name of the main table</param>
		/// <param name="aggregate"><c>True</c> if you want an aggregate query, <c>false</c> if you only need distinct values</param>
		/// <param name="aggregateLimit">Set a limit below the standard 50,000 record aggregate limit.</param>
		/// <param name="distinct">Boolean value to specify that duplicate rows not be included in the results.</param>
		/// <exception cref="ArgumentNullException">If the name of the main table is not provided</exception>
		/// <exception cref="ArgumentOutOfRangeException">If the aggregate limit is specified and is less than 1 or greater than 50000</exception>
		public FetchXmlExpression(string tableName, bool aggregate, int? aggregateLimit = null, bool? distinct = null)
			: this(tableName)
		{
			if (aggregateLimit.HasValue && (aggregateLimit.Value < 1 || aggregateLimit.Value > 50000))
			{
				throw new ArgumentOutOfRangeException(nameof(aggregateLimit), "AggregateLimit value cannot be less than 1 or greater than 50000");
			}

			this.Aggregate = aggregate;
			this.AggregateLimit = aggregateLimit;
			this.Distinct = distinct;
		}


		/// <summary>
		/// Boolean value to specify that the query returns aggregate values.
		/// See <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/aggregate-data"/>
		/// </summary>
		public bool? Aggregate { get; set; }

		/// <summary>
		/// Set a limit below the standard 50,000 record aggregate limit.
		/// See <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/aggregate-data#limitations"/>
		/// </summary>
		public int? AggregateLimit { get; set; }

		/// <summary>
		/// When using Dataverse long term data retention, set datasource to 'retained' to indicate the query is for retained rows only.
		/// See <see cref="https://learn.microsoft.com/en-us/power-apps/maker/data-platform/data-retention-overview"/>
		/// </summary>
		public bool? ForRetainedRowsOnly { get; set; }

		/// <summary>
		/// Boolean value to specify that duplicate rows not be included in the results.
		/// See <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/overview#return-distinct-results"/>
		/// </summary>
		public bool? Distinct { get; set; }

		/// <summary>
		/// Boolean value to direct the query to be broken up into smaller parts and reassemble the results before returning them. 
		/// Using latematerialize might improve performance for some long-running queries.
		/// See <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/optimize-performance#late-materialize-query"/>
		/// </summary>
		public bool? LateMaterialize { get; set; }

		/// <summary>
		/// A string value to apply one or more SQL optimizations. 
		/// See <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/reference/fetch#options"/>
		/// </summary>
		public HashSet<FetchOption> Options { get; } = new HashSet<FetchOption>();

		/// <summary>
		/// Positive integer value to specify the page number to return.
		/// See <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/page-results"/>
		/// </summary>
		public int? Page { get; set; }

		/// <summary>
		/// String value from a previous page of data to make retrieving the next page of data more efficient.
		/// See <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/page-results"/>
		/// </summary>ì
		public string PagingCookie { get; set; }

		/// <summary>
		/// Positive integer value to specify the number of records to return in a page.
		/// See <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/page-results"/>
		/// </summary>
		public int? Count { get; set; }

		/// <summary>
		/// Boolean value to specify whether the total number of records matching the criteria is returned.
		/// See <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/count-rows"/>
		/// </summary>
		public bool? ReturnTotalRecordCount { get; set; }

		/// <summary>
		/// Positive integer value to specify the number of records to return.
		/// This value can't exceed 5,000.
		/// Don't use top together with the page, count, or returntotalrecordcount attributes.
		/// See <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/overview#limit-the-number-of-rows"/>
		/// </summary>
		public int? Top { get; set; }


		/// <summary>
		/// Boolean value to specify that choice column data sorting should Use Raw Order By mode. 
		/// This sorts the choice options by the integer value. 
		/// Without this, the default is to sort choice columns using the choice label values.
		/// </summary>
		public bool? UseRawOrderBy { get; set; }




		/// <summary>
		/// The logical name of the table.
		/// </summary>
		[Required(ErrorMessage = "Table name is required")]
		public string TableName { get; set; }




		public bool TryValidate(out IEnumerable<ValidationResult> validationResults)
		{
			var validationResultList = new List<ValidationResult>();
			var result = Validator.TryValidateObject(this, new ValidationContext(this), validationResultList, true);
			validationResults = validationResultList;
			return result;
		}

		public IEnumerable<ValidationResult> Validate()
		{
			TryValidate(out var validationResults);
			return validationResults;
		}




		public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (this.Aggregate.GetValueOrDefault())
			{
				validationContext.Items.Add("aggregate-query", true);
			}

			if (AggregateLimit.HasValue && AggregateLimit.Value < 1)
			{
				yield return new ValidationResult("AggregateLimit value cannot be less than 1", new[] { nameof(AggregateLimit) });
			}

			if (AggregateLimit.HasValue && AggregateLimit.Value > 50000)
			{
				yield return new ValidationResult("AggregateLimit value cannot exceed 50000", new[] { nameof(AggregateLimit) });
			}

			if (Top.HasValue && Top.Value < 1)
			{
				yield return new ValidationResult("Top value cannot be less than 1", new[] { nameof(Top) });
			}

			if (Top.HasValue && Top.Value > 5000)
			{
				yield return new ValidationResult("Top value cannot exceed 5000", new[] { nameof(Top) });
			}

			if (Top.HasValue && (Page.HasValue || Count.HasValue || ReturnTotalRecordCount.HasValue))
			{
				yield return new ValidationResult("Don't use top together with the page, count, or returntotalrecordcount attributes", new[] { nameof(Top) });
			}

			if (Page.HasValue && Page.Value < 0)
			{
				yield return new ValidationResult("Page value cannot be negative", new[] { nameof(Page) });
			}

			var baseResult = base.Validate(validationContext);
			foreach (var validationResult in baseResult)
			{
				yield return validationResult;
			}
		}


		/// <summary>
		/// Converts the current object to its XML representation, optinally validating the object structure.
		/// </summary>
		/// <param name="validate">Indicates whether the message should be validated before conversion or not</param>
		/// <returns>The XML representation of the current FetchExpression</returns>
		/// <exception cref="ValidationException">Thrown when the validation fails.</exception>
		public virtual string ToString(bool validate)
		{
			if (validate && !TryValidate(out IEnumerable<ValidationResult> validationResults))
			{
				throw new ValidationException("FetchExpression is not valid", null, validationResults);
			}

			return ToString();
		}

		/// <summary>
		/// Converts the current object to its XML representation. On this override, validation is not executed.
		/// If you want to validate the object before conversion, use the <see cref="ToString(bool)"/> method.
		/// </summary>
		/// <returns>The XML representation of the current FetchExpression</returns>
		public override string ToString()
		{
			var sb = new StringBuilder();
			using (var writer = XmlWriter.Create(sb, new XmlWriterSettings
			{
				Indent = true,
				OmitXmlDeclaration = true,
				NewLineOnAttributes = false
			}))
			{
				writer.WriteStartElement("fetch");
				if (Aggregate.HasValue)
				{
					writer.WriteAttributeString("aggregate", Aggregate.Value.ToString().ToLower());
				}
				if (AggregateLimit.HasValue)
				{
					writer.WriteAttributeString("aggregatelimit", AggregateLimit.Value.ToString());
				}
				if (Distinct.HasValue)
				{
					writer.WriteAttributeString("distinct", Distinct.Value.ToString().ToLower());
				}
				if (Top.HasValue)
				{
					writer.WriteAttributeString("top", Top.Value.ToString());
				}
				if (Page.HasValue)
				{
					writer.WriteAttributeString("page", Page.Value.ToString());
				}
				if (Count.HasValue)
				{
					writer.WriteAttributeString("count", Count.Value.ToString());
				}
				if (!string.IsNullOrEmpty(PagingCookie))
				{
					writer.WriteAttributeString("paging-cookie", PagingCookie);
				}
				if (ReturnTotalRecordCount.HasValue)
				{
					writer.WriteAttributeString("returntotalrecordcount", ReturnTotalRecordCount.Value.ToString().ToLower());
				}
				if (LateMaterialize.HasValue)
				{
					writer.WriteAttributeString("latematerialize", LateMaterialize.Value.ToString().ToLower());
				}
				if (Options.Count > 0)
				{
					writer.WriteAttributeString("options", string.Join(",", Options));
				}
				if (UseRawOrderBy.HasValue)
				{
					writer.WriteAttributeString("useraworderby", UseRawOrderBy.Value.ToString().ToLower());
				}
				if (ForRetainedRowsOnly.HasValue && ForRetainedRowsOnly.Value)
				{
					writer.WriteAttributeString("datasource", "retained");
				}

				if (!string.IsNullOrWhiteSpace(TableName))
				{
					writer.WriteStartElement("entity");
					writer.WriteAttributeString("name", TableName);
					base.WriteXmlElements(writer);
					writer.WriteEndElement();
				}

				writer.WriteEndElement();
			}
			return sb.ToString();
		}




		/// <summary>
		///	Defines an implicit cast operator that converts a FetchExpression to a string.
		/// </summary>
		public static implicit operator string(FetchXmlExpression fetch)
		{
			return fetch.ToString();
		}

	}
}
