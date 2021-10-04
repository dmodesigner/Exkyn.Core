﻿using System.Collections.Generic;
using System.Net.Http;

namespace Exkyn.Core.Helpers
{
    public class ApiHelpers<TSaida> where TSaida : class
    {
        private static HttpClient client = new HttpClient();

        public static TSaida Get(string url)
        {
            var response = client.GetAsync(url);

            if (response.Result.IsSuccessStatusCode)
                return response.Result.Content.ReadAsAsync<TSaida>().Result;

            return null;
        }

        public static List<TSaida> GetList(string url)
        {
            var response = client.GetAsync(url);

            if (response.Result.IsSuccessStatusCode)
                return response.Result.Content.ReadAsAsync<List<TSaida>>().Result;

            return null;
        }
    }

    public class ApiHelpers<TEntrada, TSaida> where TSaida : class
    {
        private static HttpClient client = new HttpClient();

        public static TSaida Post(string url, TEntrada obj)
        {
            var response = client.PostAsJsonAsync(url, obj);

            if (response.Result.IsSuccessStatusCode)
                return response.Result.Content.ReadAsAsync<TSaida>().Result;

            return null;
        }

        public static List<TSaida> PostList(string url, TEntrada obj)
        {
            var response = client.PostAsJsonAsync(url, obj);

            if (response.Result.IsSuccessStatusCode)
                return response.Result.Content.ReadAsAsync<List<TSaida>>().Result;

            return null;
        }
    }
}
