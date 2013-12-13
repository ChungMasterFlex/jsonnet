using System;
using OAuth;
using System.Net;
using System.Data;
using System.IO;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Collections;
using RestSharp;


namespace AaronProject4
{
	class MainClass
	{

		/*				
		public static void Main (string[] args)
		{
			Console.WriteLine ("Hello World!");
		}
	*/
		// http://www.linkedin.com/groupItem?view=&gid=40949&type=member&item=222993741&commentID=125168962&qid=fe702176-999c-431c-a067-170f5e2986df&goback=.gmp_40949
		// yelpsharp

		//internal class Program
		public class Program
		{
			#region Class Methods

			public static string Search(string term, string location)
			{
				string url = String.Format("http://api.yelp.com/v2/search?term={0}&location={1}&limit=10&category_filter=food", Uri.EscapeDataString(term), Uri.EscapeDataString(location));
				return LoadData(new Uri(url));
			}

			//	private static string LoadData(Uri uri)
			public static string LoadData(Uri uri)
			{
				string url, parameters;
				var oAuth = new OAuthBase();
				string nonce = oAuth.GenerateNonce();
				string timeStamp = oAuth.GenerateTimeStamp();
				string signature = oAuth.GenerateSignature(uri,
					"-bxN4K-9DwqvkBGuvnat6Q", //change
					"QwLAfJ-Jduq1uWUgtQzc2gAf-q8", //change
					"tebMxjbj5At7nZ43Olz-MXI9egDnTHkJ", //change
					"_vKhx2te-KEXmNp-ShHvbp6pbmI", //change
					"GET",
					timeStamp,
					nonce,
					OAuthBase.SignatureTypes.HMACSHA1, out url, out parameters);
				string newUrl = string.Format("{0}?{1}&oauth_signature={2}", url, parameters, HttpUtility.UrlEncode(signature));
	
				var request = WebRequest.Create(newUrl) as HttpWebRequest;
				WebResponse response = request.GetResponse();

				//var theJsonObject = JsonConvert.DeserializeObject<RootObject> (response);

				//string data;
				//this does not work				JObject okay1;

				/*
				YelpSearchResults theJsonObject;

				using (var reader = new StreamReader(response.GetResponseStream()))
				{
					data = reader.ReadToEnd();
					// okay1 = JObject.Parse(data); // This didn't necessary return any errors but it is considered a string object, not sure if that's an issue. 
					theJsonObject = JsonConvert.DeserializeObject<YelpSearchResults> (data);

				}
				*/
			
				//var theJsonObject = JsonConvert.DeserializeObject<RootObject> (data);
				//string testingTheJsonObject;
				//testingTheJsonObject = theJsonObject.total;
				//return testingTheJsonObject;


				}


//OBJECT that will capture the JSON
			public class Span
			{
				public double latitude_delta { get; set; }
				public double longitude_delta { get; set; }
			}

			public class Center
			{
				public double latitude { get; set; }
				public double longitude { get; set; }
			}

			public class Region
			{
				public Span span { get; set; }
				public Center center { get; set; }
			}

			public class Location
			{
				public string cross_streets { get; set; }
				public string city { get; set; }
				public List<string> display_address { get; set; }
				public List<string> neighborhoods { get; set; }
				public string postal_code { get; set; }
				public string country_code { get; set; }
				public List<string> address { get; set; }
				public string state_code { get; set; }
			}

			public class Business
			{
				public bool is_claimed { get; set; }
				public double rating { get; set; }
				public string mobile_url { get; set; }
				public string rating_img_url { get; set; }
				public int review_count { get; set; }
				public string name { get; set; }
				public string snippet_image_url { get; set; }
				public string rating_img_url_small { get; set; }
				public string url { get; set; }
				public string phone { get; set; }
				public string snippet_text { get; set; }
				public string image_url { get; set; }
				public List<List<string>> categories { get; set; }
				public string display_phone { get; set; }
				public string rating_img_url_large { get; set; }
				public string id { get; set; }
				public bool is_closed { get; set; }
				public Location location { get; set; }
				public int? menu_date_updated { get; set; }
				public string menu_provider { get; set; }
			}

			public class RootObject
			{
				public Region region { get; set; }
				public int total { get; set; }
				public List<Business> businesses { get; set; }
			}

//MAINSTRING
			public static void Main(string[] args)
			{
				string searchTerm;
				string location;
				//		int numLocation;

				searchTerm = args [0];
				location = args [1];
				//		numLocation = args [2];

				//		Console.Write(Search(searchTerm, location));
				Console.Write(Search(searchTerm, location));
				// no you dont get this remember -- Console.Write (cool);
				Console.ReadKey();
			}



			#endregion
		} //end Program

	}  //end mainclasss
} //end Program
