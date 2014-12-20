using System;
using ElasticsearchCRUD.ContextAddDeleteUpdate.CoreTypeAttributes;
using ElasticsearchCRUD.ContextAddDeleteUpdate.IndexModel.SettingsModel;
using ElasticsearchCRUD.Model;

namespace ElasticsearchGermanAnalyzer
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var elasticsearchGermanSearchProvider = new ElasticsearchGermanSearchProvider();

			// Create the index defintion
			//var indexDefinition = elasticsearchGermanSearchProvider.CreateNewIndexDefinitionForGermanSearch();

			// create a new index and type mapping in elasticseach
			elasticsearchGermanSearchProvider.CreateIndex( new IndexDefinition());
			Console.ReadLine();

			elasticsearchGermanSearchProvider.CreateSomeMembers();
			Console.ReadLine();

			//{
			//  "query": {
			//		"match": {"info": "munich"}
			//	 }
			//  }
			//}

			//{
			//  "query": {
			//	"match": {
			//	  "info": {
			//		"query": "munich",
			//		"fuzziness": 2,
			//		"prefix_length": 1
			//	  }
			//	}
			//  }
			//}

			//{
			//		"filter" : {
			//			"query" : {
			//				"query_string" : {
			//					"query" : "münich",
			//					"analyzer" : "german"
			//				}
			//			}
			//		}
			//}

			// we expect 4 results for each of the following searches
			var resultSean = elasticsearchGermanSearchProvider.Search("munich");
			Console.WriteLine("Found for sean Total: {0}", resultSean.Hits.Total);
			Console.ReadLine();

			var resultJohny = elasticsearchGermanSearchProvider.Search("Muenich");
			Console.WriteLine("Found for Johny Total: {0}", resultJohny.Hits.Total);
			Console.ReadLine();

			var resultSeanWithFada = elasticsearchGermanSearchProvider.Search("Münich");
			Console.WriteLine("Found for Séan Total: {0}", resultSeanWithFada.Hits.Total);

			// http://localhost:9200/germandatas/_analyze?&analyzer=german&text=Muenich%20munich%20m%C3%BCnich%20M%C3%BCnich

			Console.ReadLine();
		}
	}

	public class GermanData
	{
		public long Id { get; set; }

		public string Name { get; set; }

		public string FamilyName { get; set; }

		[ElasticsearchString(Fields = typeof(FieldDataDefinition), Analyzer=LanguageAnalyzers.German)]
		public string Info { get; set; }
	}

	
	public class FieldDataDefinition
	{
		[ElasticsearchString(Index=StringIndex.not_analyzed)]
		public string Raw { get; set; }	
	}
}
