using System;
using System.IO;
using System.Net;

namespace SCIA.Common
{
  
    public static class WebRequestHelper
  {
    #region Constants

    public const int Hour = 60 * Minute;
    public const int Minute = 60 * Second;
    public const int Second = 1000;

    #endregion

    #region Public methods

    public static string DownloadString(string url, int? timeout = null, int? readWriteTimeout = null)
    {
      using (var response = RequestAndGetResponse(url, timeout, readWriteTimeout))
      {
        var stream = response.GetResponseStream();
        using (var streamReader = new StreamReader(stream))
        {
          return streamReader.ReadToEnd();
        }
      }
    }


    public static HttpWebResponse RequestAndGetResponse(string url, int? timeout = null, int? readWriteTimeout = null, string cookies = null)
    {
      return RequestAndGetResponse(new Uri(url), timeout, readWriteTimeout, cookies);
    }

    public static HttpWebResponse RequestAndGetResponse(Uri uri, int? timeout = null, int? readWriteTimeout = null, string cookies = null)
    {
      var webRequest = CreateRequest(uri, timeout, readWriteTimeout, cookies);

      return webRequest.GetResponse() as HttpWebResponse;
    }

    #endregion

    #region Private methods

    private static HttpWebRequest CreateRequest(string url, int? timeout = null, int? readWriteTimeout = null, string cookies = null)
    {
      return CreateRequest(new Uri(url), timeout, readWriteTimeout, cookies);
    }

    private static HttpWebRequest CreateRequest(Uri url, int? timeout = null, int? readWriteTimeout = null, string cookies = null)
    {
      var webRequest = (HttpWebRequest)WebRequest.Create(url);
      if (cookies != null)
      {
        webRequest.Headers.Add(HttpRequestHeader.Cookie, cookies);
      }

      return webRequest;
    }

    #endregion

   
  }
}