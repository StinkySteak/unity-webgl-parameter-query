using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace StinkySteak.Web.ParameterQuery
{
    public static class WebGLParameterInfo
    {
        [DllImport("__Internal")]
        private static extern System.IntPtr GetQueryParams();

        [DllImport("__Internal")]
        private static extern void FreeQueryParams(System.IntPtr ptr);

        private const string EMPTY_JSON = "{}";

        [Obsolete("Please use GetParameterUnity instead.")]
        public static string GetParameters()
        {
            if (Application.platform != RuntimePlatform.WebGLPlayer)
            {
                Debug.LogWarning("Query parameters are only available in WebGL builds.");
                return EMPTY_JSON;
            }

            System.IntPtr ptr = GetQueryParams();

            try
            {
                string jsonString = Marshal.PtrToStringUTF8(ptr);
                return jsonString ?? EMPTY_JSON;
            }
            finally
            {
                FreeQueryParams(ptr);
            }
        }

        public static string GetParametersUnity()
        {
            string url = Application.absoluteURL;
            string json = ParseQueryUrlToJson(url);

            return json;
        }

        private static string ParseQueryUrlToJson(string url)
        {
            string json = EMPTY_JSON;
            string query = new Uri(url).Query;

            Debug.Log($"[{nameof(WebGLParameterInfo)}]: Url: {url} query: {query}");

            if (string.IsNullOrEmpty(query))
            {
                return json;
            }
            try
            {
                string[] parameters = query[1..].Split('&');
                json = "{";
                for (int i = 0; i < parameters.Length; i++)
                {
                    string[] keyValue = parameters[i].Split('=');
                    bool islast = i == parameters.Length - 1;
                    json += $"\"{keyValue[0]}\": \"{keyValue[1]}\"{(islast ? "" : ",")}";
                }
                json += "}";
            }
            catch (Exception)
            {
                throw new Exception($"Invalide query format");
            }

            return json;
        }
    }
}