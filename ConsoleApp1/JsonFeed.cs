using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class JsonFeed
    {
		//declare respective clients
        private static HttpClient _jokeClient = new HttpClient();
        private static HttpClient _nameClient = new HttpClient();
        private static HttpClient _categoryClient = new HttpClient();
        static string _url = "";
        static Tuple<string, string> names;


        public JsonFeed() { }

        public JsonFeed(string endpoint)
        {
            _url = endpoint;
        }

        public string[] GetRandomJokes(string category, int count)
        {
            if (_jokeClient.BaseAddress == null)
            {
                _jokeClient.BaseAddress = new Uri(_url);
            }
            string[] jokes = new String[count];

			//loop through generating the requested number of jokes
            for (int i = 0; i < count; i++)
            {
                string sol = null;
                string url = "random";
                if (category != null)
                {
                    url = url + "?" + "category=" + category;
                }

                var responseTask = _jokeClient.GetAsync(url);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    sol = JsonConvert.DeserializeObject<dynamic>(readTask.Result).value;
                }
                else sol = result.StatusCode.ToString() + "~ There was an issue reaching the API, might be an illegal Category";

                //Replace Chuck Norris with a random name
                if (names != null)
                {
                    sol = NameChange(sol, names.Item1, names.Item2);
                }

                jokes[i] = sol;

            }
            names = null;
            return jokes;
        }
        public static string NameChange(string joke, string firstN, string lastN)
        {
            return joke.Replace("Chuck", firstN).Replace("Norris", lastN);
        }

        //Helper method used to fetch random names through a consumed API call
        public void Getnames()
        {
            dynamic sol;
            if (_nameClient.BaseAddress == null)
            {
                _nameClient.BaseAddress = new Uri("https://names.privserv.com/api/");
            }
            var responseTask = _nameClient.GetAsync("");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                sol = JsonConvert.DeserializeObject<dynamic>(readTask.Result);
                names = Tuple.Create(sol.name.ToString(), sol.surname.ToString());
            }
            else sol = result.StatusCode.ToString();
        }


        public String GetCategories()
        {
            string sol = null;
            if (_categoryClient.BaseAddress == null)
            {
                _categoryClient.BaseAddress = new Uri(_url);
            }

            var responseTask = _categoryClient.GetAsync("categories");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                sol = readTask.Result;
            }
            else sol = result.StatusCode.ToString();
            return sol;
        }
    }
}
