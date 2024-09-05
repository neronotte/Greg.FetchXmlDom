using Greg.FetchXmlDom.Model;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Threading.Tasks;

namespace Microsoft.Xrm.Sdk
{
    /// <summary>
    /// Extension methods to provide useful entry points in IOrganizationService
    /// </summary>
    public static class OrganizationServiceExtensions
	{

		/// <summary>
		/// Executes a FetchXml query and returns the result as an EntityCollection
		/// </summary>
		/// <param name="crm">The organization service that will perform the operation</param>
		/// <param name="fetchXml">The query to execute</param>
		/// <returns>
		/// The collection of entities that match the query
		/// </returns>
		/// <exception cref="System.ComponentModel.DataAnnotations.ValidationException">If validate=true and the validation fails</exception>
		public static EntityCollection RetrieveMultiple(this IOrganizationService crm, FetchXmlExpression fetchXml)
		{
			if (crm == null) throw new ArgumentNullException(nameof(crm));
			if (fetchXml == null) throw new ArgumentNullException(nameof(fetchXml));


			var request = new RetrieveMultipleRequest
			{
				Query = new FetchExpression(fetchXml)
			};

			var response = (RetrieveMultipleResponse)crm.Execute(request);
			return response.EntityCollection;
		}


		/// <summary>
		/// Executes a FetchXml query asynchronously and returns the result as an EntityCollection
		/// </summary>
		/// <param name="crm">The organization service that will perform the operation</param>
		/// <param name="fetchXml">The query to execute</param>
		/// <returns>
		/// The collection of entities that match the query
		/// </returns>
		/// <exception cref="System.ComponentModel.DataAnnotations.ValidationException">If validate=true and the validation fails</exception>
		public static async Task<EntityCollection> RetrieveMultipleAsync(this IOrganizationServiceAsync2 crm, FetchXmlExpression fetchXml)
		{
			var request = new RetrieveMultipleRequest
			{
				Query = new FetchExpression(fetchXml)
			};

			var response = (RetrieveMultipleResponse)(await crm.ExecuteAsync(request));
			return response.EntityCollection;
		}
	}
}
