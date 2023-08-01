using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace 消防积分获取
{
    public class HttpHelp
    {
        //HttpClient,异步Post
        public bool PostAsync(string url, Dictionary<string, string> myDictionary, string postParaJsonStr, ref string outResStr)
        {
            bool res = false;
            var handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip,
            };
            try
            {
                using (var http = new HttpClient(handler))
                {
                    string postPara = postParaJsonStr;
                    using (HttpContent content = new StringContent(postPara))
                    {
                        content.Headers.ContentType = new MediaTypeHeaderValue("application/json;charset=UTF-8");
                        foreach (var item in myDictionary)
                        {
                            content.Headers.Add(item.Key, item.Value);
                        }
                        var response = http.PostAsync(url, content).Result;
                        outResStr = response.Content.ReadAsStringAsync().Result;
                        res = response.IsSuccessStatusCode;
                    }
                };
            }
            catch (Exception ex)
            {
                outResStr = ex.Message;
                res = false;
            }
            return res;
        }

        //HttpClient,异步Get
        public bool GetAsync(string url, Dictionary<string, string> myDictionary, string postParaStr, ref string outResStr)
        {
            bool res = false;
            var handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip
            };
            try
            {
                using (var http = new HttpClient(handler))
                {
                    using (HttpContent content = new StringContent(postParaStr))
                    {
                        content.Headers.ContentType = new MediaTypeHeaderValue("application/json;charset=UTF-8");
                        foreach (var item in myDictionary)
                        {
                            content.Headers.Add(item.Key, item.Value);
                        }
                        var response = http.GetAsync(url + "?" + postParaStr).Result; // postParaStr 注意拼接好
                        outResStr = response.Content.ReadAsStringAsync().Result;
                        res = response.IsSuccessStatusCode;
                    }
                };
            }
            catch (Exception ex)
            {
                outResStr = ex.Message;
                res = false;
            }
            return res;
        }

        //WebRequest同步Post
        public static string HttpPost(string Url, Dictionary<string, string> myDictionary, string postDataStr, ref string outResStr)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "POST";
                request.ContentType = "application/json;charset=UTF-8";
                foreach (var item in myDictionary)
                {
                    request.Headers.Add(item.Key, item.Value);
                }
                Encoding encoding = Encoding.UTF8;
                byte[] postData = encoding.GetBytes(postDataStr);
                request.ContentLength = postData.Length;
                Stream myRequestStream = request.GetRequestStream();
                myRequestStream.Write(postData, 0, postData.Length);
                myRequestStream.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, encoding);
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();

                return retString;
            }
            catch (Exception)
            {
                JObject newObj2 = new JObject(
                    new JProperty("msg", "访问错误"),
                    new JProperty("code", 0)
                );
                return newObj2.ToString();
            }
            
        }

        //WebRequest同步Get
        public static string HttpGet(string Url, Dictionary<string, string> myDictionary, string postDataStr)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
                request.Method = "GET";
                request.ContentType = "text/html;charset=UTF-8";
                foreach (var item in myDictionary)
                {
                    request.Headers.Add(item.Key, item.Value);
                }
                //Random r = new Random();
                //int dd = r.Next(1, 4);
                //if (dd== 1)
                //{
                //    request.UserAgent = "Mozilla/5.0 (Linux; Android 10; EVR-AL00 Build/HUAWEIEVR-AL00; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/74.0.3729.186 Mobile Safari/537.36 baiduboxapp/11.0.5.12 (Baidu; P1 10)";
                //}else if (dd == 2)
                //{
                //    request.UserAgent = "Mozilla/5.0 (Linux; Android 10; VCE-AL00 Build/HUAWEIVCE-AL00; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/74.0.3729.186 Mobile Safari/537.36 baiduboxapp/11.0.5.12 (Baidu; P1 10)";
                //}
                //else if (dd == 3)
                //{
                //    request.UserAgent = "Mozilla/5.0 (Linux; U; Android 9; zh-cn; Redmi 7 Build/PKQ1.181021.001) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/71.0.3578.141 Mobile Safari/537.36 XiaoMi/MiuiBrowser/11.8.14";
                //}
                //else
                //{
                //    request.UserAgent = "Mozilla/5.0 (Linux; U; Android 4.0.3; zh-CN; SM901 Build/NZH54D) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/69.0.3497.100 UWS/3.22.2.43 Mobile Safari/537.36 UCBS/3.22.2.43_220223200704 NebulaSDK/1.8.100112 Nebula AlipayDefined(nt:3G,ws:360|0|3.0) AliApp(AP/10.3.0.8000) AlipayClient/10.3.0.8000 Language/zh-Hans useStatusBar/true isConcaveScreen/false Region/CNAriver/1.0.0";
                //}

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
                return retString;
            }
            catch (Exception e)
            {
                JObject newObj2 = new JObject(
                    new JProperty("msg", "访问错误"),
                    new JProperty("code", 0)
                );
                Console.WriteLine(e.Message);
                return newObj2.ToString();
            }

        }
    }
}