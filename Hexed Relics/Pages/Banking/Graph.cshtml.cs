using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Serialization;
using System.Text.Json;
using System.Web;
using Newtonsoft.Json;
using Plotly.NET.Interactive;
using Plotly.NET;
using Plotly.NET.ConfigObjects;
using Microsoft.FSharp.Core.CompilerServices;
using Microsoft.FSharp.Core;

namespace Hexed_Relics.Pages.Banking
{
    [BindProperties]
    public class GraphModel : PageModel
    {
        public string[] X = Array.Empty<string>();
        public float[] Y = Array.Empty<float>();
        public void OnGet(string Code)
        {
            if (Code == null) Code = "AAPL";
            string QUERY_URL = "https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol=" + Code + "&interval=5min&apikey=KF8QQ8WZA2861K9L";
            Uri queryUri = new Uri(QUERY_URL);

            using (WebClient client = new WebClient())
            {
                // if using .NET Core (System.Text.Json)
                // using .NET Core libraries to parse JSON is more complicated. For an informative blog post

                dynamic json_data = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(client.DownloadString(queryUri));
                List<string> XValues = new List<string>();
                List<float> YValues = new List<float>();
                foreach (var key in json_data["Time Series (5min)"])
                {
                    string X = key.ToString();  
                    X = X.Substring(12, 8);
                    XValues.Add(X);
                    foreach (var Key in key.Children()["1. open"])
                    {
                        YValues.Add(float.Parse(Key.ToString()));
                    }
                }
                XValues.Reverse();
                YValues.Reverse();
                X = XValues.ToArray();
                Y = YValues.ToArray();
                Chart2D.Chart.Line<string, float, string>(XValues.ToArray(), YValues.ToArray(), true, Code, false, 1.0, null, null, null).Show();

            }
        }
    }
}
