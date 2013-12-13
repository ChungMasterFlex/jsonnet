using System;
using OAuth;
using System.Net;
using System.Data;
using System.IO;
using System.Web;

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
				string data;
				using (var reader = new StreamReader(response.GetResponseStream()))
				{
					data = reader.ReadToEnd();
				}
				return data;
			}
				/*

var request = WebRequest.Create(newUrl) as HttpWebRequest;
				//			var request2 = HttpWebRequest.Create(newUrl);
				request.ContentType = "application/json";
				// do you need this?
				request.Method = "GET";
				//I don't think we need this b/c it's duplicated but I'll put in anyway just in case we need this for some reason on our end.

				using (HttpWebResponse response = request.GetResponse () as HttpWebResponse) {
					if (response.StatusCode != HttpStatusCode.OK)
						Console.Out.WriteLine ("Error fetching data. Server returned status code: {0}", response.StatusCode);
					using (StreamReader reader = new StreamReader (response.GetResponseStream ())) {
						var content = reader.ReadToEnd ();
						if (string.IsNullOrWhiteSpace (content)) {
							Console.Out.WriteLine ("Response contained empty body...");
						} else {
							Console.Out.WriteLine ("Response Body: \r\n {0}", content);
						}
					}
				} // end using



				 */





			public static void Main(string[] args)
			{
				string searchTerm;
				string location;
				//		int numLocation;

				searchTerm = args [0];
				location = args [1];
				//		numLocation = args [2];



				Console.Write(Search(searchTerm, location));
				Console.ReadKey();
			}

			#endregion
		}


















	}
}

