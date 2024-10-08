﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml;

namespace Greg.FetchXmlDom.Model
{
	/// <summary>
	/// Object oriented representation of a FetchXml query.
	/// </summary>
    public class FetchXmlExpression : EntityBaseExpression
	{
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
			this.ReturnTotalRecordCount = returnTotalRecordCount;
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
			this.Aggregate = aggregate;
			this.AggregateLimit = aggregateLimit;
			this.Distinct = distinct;
		}

		/// <summary>
		/// The logical name of the table.
		/// </summary>
		public string TableName { get; }

		/// <summary>
		/// Boolean value to specify that the query returns aggregate values.
		/// See <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/aggregate-data"/>
		/// </summary>
		public bool? Aggregate { get; set; }

		/// <summary>
		/// Set a limit below the standard 50,000 record aggregate limit.
		/// See <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/aggregate-data#limitations"/>
		/// </summary>
		private int? _aggregateLimit;
		public int? AggregateLimit 
		{ 
			get => _aggregateLimit; 
			set
			{
				if (value.HasValue && (value.Value < 1 || value.Value > 50000))
				{
					throw new ArgumentOutOfRangeException(nameof(AggregateLimit), "AggregateLimit value cannot be less than 1 or greater than 50000");
				}
				_aggregateLimit = value;
			}
		}

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
		private int? _page;
		public int? Page
		{
			get => _page;
			set
			{
				if (value.HasValue && value.Value < 0)
				{
					throw new ArgumentOutOfRangeException(nameof(Page), "Page value cannot be less than 0");
				}
				if (value.HasValue && Top.HasValue)
				{
					throw new ArgumentException("Don't use page together with top attribute", nameof(Page));
				}
				_page = value;
			}
		}

		/// <summary>
		/// String value from a previous page of data to make retrieving the next page of data more efficient.
		/// See <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/page-results"/>
		/// </summary>
		private string _pagingCookie;
		public string PagingCookie
		{
			get => _pagingCookie;
			set
			{
				if (string.IsNullOrWhiteSpace(value)) value = null;
				if (value != null && Top.HasValue)
				{
					throw new ArgumentException("Don't use pagingCookie together with top attribute", nameof(PagingCookie));
				}
				_pagingCookie = value;
			}
		}

		/// <summary>
		/// Positive integer value to specify the number of records to return in a page.
		/// See <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/page-results"/>
		/// </summary>
		private int? _count;
		public int? Count
		{
			get => _count;
			set
			{
				if (value.HasValue && value.Value < 1)
				{
					throw new ArgumentOutOfRangeException(nameof(Count), "Count value cannot be less than 1");
				}
				if (value.HasValue && Top.HasValue)
				{
					throw new ArgumentException("Don't use count together with top attribute", nameof(Count));
				}
				_count = value;
			}
		}

		/// <summary>
		/// Boolean value to specify whether the total number of records matching the criteria is returned.
		/// See <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/count-rows"/>
		/// </summary>
		private bool? _returnTotalRecordCount;
		public bool? ReturnTotalRecordCount
		{
			get => _returnTotalRecordCount;
			set
			{
				if (value.HasValue && Top.HasValue)
				{
					throw new ArgumentException("Don't use count together with top attribute", nameof(ReturnTotalRecordCount));
				}
				_returnTotalRecordCount = value;
			}
		}

		/// <summary>
		/// Positive integer value to specify the number of records to return.
		/// This value can't exceed 5,000.
		/// Don't use top together with the page, count, or returntotalrecordcount attributes.
		/// See <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/overview#limit-the-number-of-rows"/>
		/// </summary>
		/// 
		private int? _top;
		public int? Top
		{
			get => _top;
			set
			{
				if (value.HasValue && (value.Value < 1 || value.Value > 5000))
				{
					throw new ArgumentOutOfRangeException(nameof(Top), "Top value cannot be less than 1 or greater than 5000");
				}
				if (value.HasValue && (Page.HasValue || !string.IsNullOrWhiteSpace(PagingCookie) || Count.HasValue || ReturnTotalRecordCount.HasValue))
				{
					throw new ArgumentException("Don't use top together with the page, pagingCookie, count, or returntotalrecordcount attributes", nameof(Top));
				}
				_top = value;
			}
		}


		/// <summary>
		/// Boolean value to specify that choice column data sorting should Use Raw Order By mode. 
		/// This sorts the choice options by the integer value. 
		/// Without this, the default is to sort choice columns using the choice label values.
		/// </summary>
		public bool? UseRawOrderBy { get; set; }





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
