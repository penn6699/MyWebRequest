
using System.Web;
using System.Net;
using System.IO;
using System.Text;

public class Handler : IHttpHandler
{
    public bool IsReusable { get { return false; } }
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Write("测试 MyWebRequest 抓取文件...");

        string path = context.Server.MapPath("~/Data");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        using (Stream ms = MyWebRequest.Get("https://www.cnblogs.com/xingzc/p/5986464.html"))
        {
            using (StreamReader sr = new StreamReader(ms, Encoding.UTF8))
            {
                string re = sr.ReadToEnd();
                StreamWriter sw = new StreamWriter(File.Open(path + "/https-" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".html", FileMode.Append));
                sw.Write(re);
                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
            ms.Close();
        }

        using (Stream ms = MyWebRequest.Get("http://www.jyubbs.com/bbs/forum.php?a=123"))
        {
            using (StreamReader sr = new StreamReader(ms, Encoding.UTF8))
            {
                string re = sr.ReadToEnd();
                
                StreamWriter sw = new StreamWriter(File.Open(path + "/http-" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".html", FileMode.Append));
                sw.Write(re);
                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
            ms.Close();
        }
        


        context.Response.Write("\n\n抓取完成");









    }


}