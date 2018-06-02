
using System.Web;
using System.Net;
using System.IO;
using System.Text;

public class Handler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Write("Hello World");
        
        Stream ms = MyWebRequest.Get("https://www.cnblogs.com/xingzc/p/5986464.html");
        StreamReader sr = new StreamReader(ms, Encoding.UTF8);
        string re = sr.ReadToEnd();

        string path = context.Server.MapPath("~/Data");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        //FileStream fs = File.Open(path + "/5986464.html", FileMode.Append);
        StreamWriter sw = new StreamWriter(File.Open(path + "/" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".html", FileMode.Append));
        sw.Write(re);
        sw.Flush();
        sw.Close();
        sw.Dispose();




















    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}