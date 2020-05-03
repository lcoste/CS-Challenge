using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;

namespace JokeGenerator
{
	/// <summary>
	/// Class handling the API calls
	/// </summary>
    public class JsonFeed
    {
		static public readonly string CHUCK_NORRIS_API = "https://api.chucknorris.io/jokes/";
		static public readonly string NAME_API = "https://names.privserv.com/api/";
		
		readonly HttpClient client = new HttpClient();

		/// <summary>
		/// Executes a GET of the URL passed in.
		/// </summary>
		/// <param name="url">URL to hit.</param>
		/// <returns>The response from the URL as a string.</returns>
		public string Get(string url)
		{
			string result;
			result = client.GetStringAsync(url).Result;

			return result;
		}

		/// <summary>
		/// Executes a GET of the URL passed in. Only returns the 
		/// </summary>
		/// <param name="url">URL to hit.</param>
		/// <param name="fields">Fields in the response that should be returned.</param>
		/// <returns>Dictionary of the fields requested.</returns>
		public Dictionary<string, dynamic> Get(string url, string[] fields)
		{
			dynamic response = JsonConvert.DeserializeObject<dynamic>(client.GetStringAsync(url).Result);
			Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();

			foreach (string field in fields)
			{
				result.Add(field, response[field]);
			}

			return result;
		}

		/// <summary>
		/// Executes a GET of the URL passed in. 
		/// </summary>
		/// <param name="host">Host to hit.</param>
		/// <param name="endpoint">Endpoint to hit.</param>
		/// <returns>The response from the URL as a string.</returns>
		public string Get(string host, string endpoint)
		{
			return Get(GenerateUrl(host, endpoint, null));
		}

		/// <summary>
		/// Executes a GET of the URL passed in. 
		/// </summary>
		/// <param name="host">Host to hit.</param>
		/// <param name="endpoint">Endpoint to hit.</param>
		/// <param name="parameters">parameters to pass into the URL.</param>
		/// <param name="fields">Fields in the response that should be returned.</param>
		/// <returns>Dictionary of the fields requested.</returns>
		public Dictionary<string, dynamic> Get(string host, string endpoint, Dictionary<string, string> parameters, string[] fields)
		{
			return Get(GenerateUrl(host, endpoint, parameters), fields);
		}

		/// <summary>
		/// Generates a properly formatted URL.
		/// </summary>
		/// <param name="host">Host of the URL to generate.</param>
		/// <param name="endpoint">Endpoint of the URL.</param>
		/// <param name="parameters">Parameters to pass into the URL.</param>
		/// <returns>Properly formatted and encoded URL.</returns>
		static public string GenerateUrl(string host, string endpoint, Dictionary<string, string> parameters)
		{
			if (host.EndsWith('/'))
			{
				host = host.Substring(0, host.Length - 1);
			}
			if (endpoint.StartsWith('/'))
			{
				endpoint = endpoint.Substring(1);
			}

			string resultUrl = host;
			resultUrl += '/';
			resultUrl += endpoint;

			if (parameters != null)
			{
				for (int i = 0; i < parameters.Count; i++)
				{
					if (i == 0) { resultUrl += '?'; }
					else { resultUrl += '&'; }

					KeyValuePair<String, String> parameter = parameters.ElementAt(i);

					resultUrl += Uri.EscapeDataString(parameter.Key);
					resultUrl += '=';
					resultUrl += Uri.EscapeDataString(parameter.Value);
				}
			}

			return resultUrl;
		}
    }
}
