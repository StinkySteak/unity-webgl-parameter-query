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
    }
}