﻿using System;
using System.IO;
using System.Net;
using System.Text;

namespace Web.Utils
{
    public class HttpHelper
    {
        #region HTTP POST请求

        public string HttpPost(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);

            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";
            request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);

            Stream myRequestStream = request.GetRequestStream();
            byte[] postBytes = Encoding.UTF8.GetBytes(postDataStr);

            myRequestStream.Write(postBytes, 0, postBytes.Length);
            myRequestStream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();

            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }

        #endregion

        #region HTTP GET请求

        public string HttpGet(string url, string paramStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + (paramStr == "" ? "" : "?") + paramStr);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));

            string result = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return result;
        }

        #endregion

        #region 判断图片是否存在

        public static bool IsImageExists(string url)
        {
            WebResponse response = null;

            bool result = false;

            try
            {
                Uri uri = new Uri(url);

                WebRequest req = WebRequest.Create(uri);
                response = req.GetResponse();

                result = response == null ? false : true;
            }
            catch
            {
                result = false;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }

            return result;
        }

        #endregion
    }
}