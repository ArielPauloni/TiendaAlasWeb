using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GUI
{
    /// <summary>
    /// Descripción breve de DownloadFile
    /// </summary>
    public class DownloadFile : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string tmpPath = context.Server.MapPath("~/");
            System.Web.HttpRequest request = System.Web.HttpContext.Current.Request;
            string fileName = request.QueryString["fileName"];

            System.IO.FileInfo file = new System.IO.FileInfo(tmpPath + @"\" + fileName);

            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "text/plain";
            response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ";");
            response.TransmitFile(context.Server.MapPath(fileName));
            response.Flush();

            if (file.Exists) { file.Delete(); }

            response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}