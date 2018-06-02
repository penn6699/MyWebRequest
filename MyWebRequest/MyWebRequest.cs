using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

/// <summary>
/// 我的网络请求
/// </summary>
public class MyWebRequest
{
    

    /// <summary>
    /// Get请求
    /// </summary>
    /// <param name="url">url地址。可带参数</param>
    /// <param name="param">参数</param>
    /// <param name="Cookies">Cookies参数</param>
    /// <returns></returns>
    public static HttpWebResponse _Get(string url, Dictionary<string, object> param = null, Dictionary<string, string> Cookies = null)
    {
        Uri uri = new Uri(url);

        param = param == null ? new Dictionary<string, object>() : param;
        string pStr = string.Empty;
        foreach (KeyValuePair<string, object> kvp1 in param)
        {
            pStr += string.Format("{0}{1}={2}", string.IsNullOrEmpty(pStr) ? "" : "&", kvp1.Key, kvp1.Value);
        }
        //增加参数
        if (!string.IsNullOrEmpty(pStr))
        {
            url = url + (string.IsNullOrEmpty(uri.Query) ? "?" : "&") + pStr;
        }


        HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
        request.Method = "GET";

        //如果没有下面这行代码 将获取不到相应的Cookie （response.Cookies）    
        request.CookieContainer = new CookieContainer();

        CookieCollection cc = new CookieCollection();
        Cookies = Cookies == null ? new Dictionary<string, string>() : Cookies;
        foreach (KeyValuePair<string, string> kvp in Cookies)
        {
            cc.Add(new Cookie(kvp.Key, kvp.Value, "/", uri.Host));
        }
        request.CookieContainer.Add(cc);

        return (HttpWebResponse)request.GetResponse();


    }

    /// <summary>
    /// Post请求
    /// </summary>
    /// <param name="url">url地址</param>
    /// <param name="param">参数</param>
    /// <param name="Cookies">Cookies参数</param>
    /// <returns></returns>
    public static HttpWebResponse _Post(string url, Dictionary<string, object> param = null, Dictionary<string, string> Cookies = null)
    {

        HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
        request.Method = "POST";
        request.ContentType = "application/x-www-form-urlencoded";
        //如果没有 下面这行代码 将获取不到相应的Cookie （response.Cookies）  
        request.CookieContainer = new CookieContainer();

        CookieCollection cc = new CookieCollection();
        Uri uri = new Uri(url);
        Cookies = Cookies == null ? new Dictionary<string, string>() : Cookies;
        foreach (KeyValuePair<string, string> kvp in Cookies)
        {
            cc.Add(new Cookie(kvp.Key, kvp.Value, "/", uri.Host));
        }
        request.CookieContainer.Add(cc);

        //写入参数
        if (param != null)
        {
            string strPostdata = "";
            foreach (KeyValuePair<string, object> kvp in param)
            {
                if (!string.IsNullOrEmpty(kvp.Key))
                {
                    strPostdata += (strPostdata == "" ? "" : "&") + string.Format("{0}={1}", kvp.Key, kvp.Value.ToString());
                }
            }
            if (strPostdata != "")
            {
                //往服务器写入数据
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(strPostdata);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
            }

        }

        //获取服务端返回
        return (HttpWebResponse)request.GetResponse();

       
    }


    /// <summary>
    /// Get请求
    /// </summary>
    /// <param name="url">url地址。可带参数</param>
    /// <param name="param">参数</param>
    /// <param name="Cookies">Cookies参数</param>
    /// <returns></returns>
    public static Stream Get(string url, Dictionary<string, object> param = null, Dictionary<string, string> Cookies = null) {
        return _Get(url, param, Cookies).GetResponseStream(); 
    }

    /// <summary>
    /// Post请求
    /// </summary>
    /// <param name="url">url地址</param>
    /// <param name="param">参数</param>
    /// <param name="Cookies">Cookies参数</param>
    /// <returns></returns>
    public static Stream Post(string url, Dictionary<string, object> param = null, Dictionary<string, string> Cookies = null)
    {
        return _Post(url, param, Cookies).GetResponseStream();
    }
    
}
