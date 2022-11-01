using System;
using System.Collections.Generic;
using static 消防积分获取.HttpHelp;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using System.Drawing;
using Jint;

namespace 消防积分获取
{


    public partial class Form1 : Form
    {
        [DllImport("kernel32")]// 读配置文件方法的6个参数：所在的分区（section）、 键值、     初始缺省值、   StringBuilder、  参数长度上限 、配置文件路径
        public static extern long GetPrivateProfileString(string section, string key, string defaultValue, StringBuilder retVal, int size, string filePath);
        [DllImport("kernel32")]//写入配置文件方法的4个参数：  所在的分区（section）、  键值、     参数值、       配置文件路径
        private static extern long WritePrivateProfileString(string section, string key, string value, string filePath);
        public Form1()
        {
            InitializeComponent();
        }
        private string Authorization="", alipayMiniMark="";
        private string q;
        private string q1;//用于判断当前执行的人名称
        private int d1=0;     //用于判断是否正在执行

        public delegate void MyInvokeB(string str);//MyInvokeLAB方法创建委托
        public JObject GetList(string url,string dataPost)
        {
            if (alipayMiniMark == "" || Authorization == "")
            {
                JObject newObj2 = new JObject(
                    new JProperty("msg", "没有填写用户信息"),
                    new JProperty("code", 0)
                );
                return newObj2;
            }
            Dictionary<string, string> myDictionary = new Dictionary<string, string>
            {
                { "Authorization", Authorization},
                { "alipayMiniMark",alipayMiniMark}
            };
            //MessageBox.Show(Authorization+"\r\n"+ alipayMiniMark);
            var t = JObject.Parse(HttpGet(url, myDictionary, dataPost));
            if (t["code"].ToString() == "1001")
            {
                return t;
            }
            else
            {
                JObject newObj2 = new JObject(
                    new JProperty("msg", t["msg"].ToString()),
                    new JProperty("code", t["code"].ToString())
                );
                return newObj2;
            }
            
        }

        public JObject PostList(string url, string dataPost)
        {
            if (Authorization == "" || alipayMiniMark == "")
            {
                JObject newObj2 = new JObject(
                    new JProperty("msg", "没有填写用户信息"),
                    new JProperty("code", 0)
                );
                return newObj2;
            }
            Dictionary<string, string> myDictionary = new Dictionary<string, string>
            {
                { "Authorization", Authorization},
                { "alipayMiniMark",alipayMiniMark}
            };
            return JObject.Parse(HttpPost(url, myDictionary, dataPost,ref q));
        }
        public void SetTxt(string str)
        {
            listBox1.Items.Add(str);
            listBox1.TopIndex = listBox1.Items.Count - (int)(listBox1.Height / listBox1.ItemHeight);
        }

        private void WenZzhang()
        {
            MyInvokeB mi = new MyInvokeB(SetTxt);//实例化一个委托，并且指定委托方法
            string url = "https://qmxfxx.119.gov.cn/alipay/mini/api/news/content/list?channel=3&limit=10&offset=1";
            var a = GetList(url, "");
            if ((int)a["code"] == 1001)
            {
                foreach (var item in a["result"]["articleList"])
                {
                    BeginInvoke(mi, q1 + "|提交文章:" + item["title"].ToString());
                    url = "https://qmxfxx.119.gov.cn/alipay/mini/api/home/taskScord/completeTask?parameter1="+ item["articleId"] + "&parameter2=XFZX&taskCode=CTWLREADARTICLE";
                    var d =GetList(url, "");
                    if ((int)a["code"] == 1001)
                    {
                        if (d["result"].ToString() == "您已领取过该任务")
                        {
                            return;
                        }
                    }
                    else
                    {
                        BeginInvoke(mi, q1 + "|提交文章:失败,重试中");
                    }
                        
                }
            }
        }

        /// <summary>
        /// 练习
        /// </summary>
        private void Lianxi()
        {
            MyInvokeB mi = new MyInvokeB(SetTxt);//实例化一个委托，并且指定委托方法
            BeginInvoke(mi, q1 + "|练习题目获取列表");
            string url = "https://qmxfxx.119.gov.cn/alipay/mini/api/course/exam/loadExamPaper/10048/10048";
            var a = PostList(url, "{}");
            BeginInvoke(mi, q1 + "|练习题目获取成功");
            if ((int)a["code"] == 1001)
            {
                //MessageBox.Show(a["result"]["questions"]["single"].ToString());
                var peoples = new JArray();
                foreach (var item in a["result"]["questions"]["single"])
                {
                    JObject newObj2 = new JObject(
                    new JProperty("questionId", item["questionId"]),
                    new JProperty("status", item["status"]),
                    new JProperty("checkOption",item["checkOption"])
                    );
                    peoples.Add(newObj2);
                }
                //MessageBox.Show(peoples.ToString());
                foreach (var item in a["result"]["questions"]["mutil"])
                {
                    JObject newObj2 = new JObject(
                    new JProperty("questionId", item["questionId"]),
                    new JProperty("status", item["status"]),
                    new JProperty("checkOption", item["checkOption"])
                    );
                    peoples.Add(newObj2);
                }
                foreach (var item in a["result"]["questions"]["judge"])
                {
                    JObject newObj2 = new JObject(
                    new JProperty("questionId", item["questionId"]),
                    new JProperty("status", item["status"]),
                    new JProperty("context", item["context"])
                    ); peoples.Add(newObj2);
                }
                BeginInvoke(mi, q1 + "|练习题目解析成功");
                //MessageBox.Show(peoples.ToString());
                url = "https://qmxfxx.119.gov.cn/alipay/mini/api/course/exam/saveOrSubmitPaperQuestion/10048/10048/-1/submit";
                a = PostList(url, peoples.ToString());
                if ((int)a["code"] == 1001)
                {
                    BeginInvoke(mi, q1 + "|练习答题成功,获取积分" + a["result"]["score"].ToString());
                }
                else
                {
                    BeginInvoke(mi, q1 + "|练习答题失败" + a["msg"].ToString());
                }
            }
            else
            {
                BeginInvoke(mi, q1 + "|练习答题失败" + a["msg"].ToString());
            }
        }


        /// <summary>
        /// 考试提交
        /// </summary>
        private void KaoShi()
        {
            MyInvokeB mi = new MyInvokeB(SetTxt);//实例化一个委托，并且指定委托方法
            BeginInvoke(mi, q1 + "|考试题目获取列表");
            string url = "https://qmxfxx.119.gov.cn/alipay/mini/api/course/exam/loadExamPaper/9999/9999";
            var a = PostList(url, "{}");
            BeginInvoke(mi, q1 + "|考试题目获取成功");
            if ((int)a["code"] == 1001)
            {
                var peoples = new JArray();
                foreach (var item in a["result"]["questions"]["single"])
                {
                    JObject newObj2 = new JObject(
                    new JProperty("questionId", item["questionId"]),
                    new JProperty("status", item["status"]),
                    new JProperty("checkOption", item["checkOption"])
                    );
                    peoples.Add(newObj2);
                }
                //MessageBox.Show(peoples.ToString());
                foreach (var item in a["result"]["questions"]["mutil"])
                {
                    JObject newObj2 = new JObject(
                    new JProperty("questionId", item["questionId"]),
                    new JProperty("status", item["status"]),
                    new JProperty("checkOption", item["checkOption"])
                    );
                    peoples.Add(newObj2);
                }
                foreach (var item in a["result"]["questions"]["judge"])
                {
                    JObject newObj2 = new JObject(
                    new JProperty("questionId", item["questionId"]),
                    new JProperty("status", item["status"]),
                    new JProperty("context", item["context"])
                    ); peoples.Add(newObj2);
                }
                BeginInvoke(mi, q1 + "|考试题目解析成功");
                //MessageBox.Show(peoples.ToString());
                url = "https://qmxfxx.119.gov.cn/alipay/mini/api/course/exam/saveOrSubmitPaperQuestion/9999/9999/-1/submit";
                a = PostList(url, peoples.ToString());
                if ((int)a["code"] == 1001)
                {
                    BeginInvoke(mi, q1 + "|考试答题成功,获取积分" + a["result"]["score"].ToString());
                }
                else
                {
                    BeginInvoke(mi, q1 + "|考试答题失败" + a["msg"].ToString());
                }
            }
            else
            {
                BeginInvoke(mi, q1 + "|考试答题失败" + a["msg"].ToString());
            }
        }
        /// <summary>
        /// 视频末尾的2分
        /// </summary>
        private void WeiLiangFen()
        {
            MyInvokeB mi = new MyInvokeB(SetTxt);//实例化一个委托，并且指定委托方法
            BeginInvoke(mi, q1 + "|获取消防视频列表");
            Random r = new Random();
            string url = "https://qmxfxx.119.gov.cn/alipay/mini/api/news/content/list?channel=6&limit=10&offset="+ r.Next(1, 5).ToString();
            var a = GetList(url, "");
            //BeginInvoke(mi, "获取消防视频列表成功");
            if ((int)a["code"] == 1001)
            {
                foreach (var item in a["result"]["videoList"])
                {
                    string ddurl = "https://qmxfxx.119.gov.cn/alipay/mini/api/home/taskScord/completeTask?parameter1=xfysList"+item["videoId"].ToString()+"&parameter2=XFYS&taskCode=CTWLAV";
                    var b = GetList(ddurl, "");
                    if ((int)b["code"] == 1001)
                    {
                        if (b["result"].ToString() == "您已领取过该任务")
                        {
                            return;
                        }
                        BeginInvoke(mi, q1 + "|消防视频提交成功：" + b["result"].ToString()+"分");
                    }
                    else
                    {
                        BeginInvoke(mi, q1 + "|消防视频提交失败,消息：" + b["msg"].ToString());
                    }
                    if (checkBox1.Checked)
                    {
                        Thread.Sleep(3 * 1000);
                        BeginInvoke(mi, q1 + "|当前视频视频:3秒等待中");
                    }
                }
            }
            else
            {
                BeginInvoke(mi, q1 + "|获取消防视频列表失败,消息：" + a["msg"].ToString());
            }
        }

        private void ZhuChenXu()
        {
            MyInvokeB mi = new MyInvokeB(SetTxt);//实例化一个委托，并且指定委托方法
            string url = "https://qmxfxx.119.gov.cn/alipay/mini/api/course/video?courseId=10002";
            string urld = "https://qmxfxx.119.gov.cn/alipay/mini/api/course/video/record";
            var b = GetList(url, "");
            JObject postdata;
            if ((int)b["code"] == 1001)
            {
                foreach (var item in b["result"]["videoDetail"]["videoStructureDetailDtoList"])
                {
                    BeginInvoke(mi, q1+"|提交视频:" + item["videoName"].ToString());
                    url = "https://qmxfxx.119.gov.cn/alipay/mini/api/home/taskScord/completeTask?parameter1=" + item["videoId"].ToString() + "&parameter2=HTWKT&taskCode=CTWLAVTIME";
                    var d = GetList(url, "");
                    if ((int)d["code"] == 1001)
                    {
                        if (d["result"].ToString() == "您已领取过该任务")
                        {
                            WenZzhang();
                            return;
                        }
                    }
                    else
                    {
                        BeginInvoke(mi, q1 + "|提交视频失败");
                    }
                    if (checkBox1.Checked)
                    {
                        Thread.Sleep(((int)item["lastTime"] - 60)*1000);
                        BeginInvoke(mi, q1+"|当前视频视频:" + item["lastTime"].ToString() + "秒,等待中");
                    }
                    postdata = null;
                    postdata = new JObject(
                        new JProperty("courseId", b["result"]["courseInfo"]["courseId"].ToString()),
                        new JProperty("lastTime", item["lastTime"].ToString()),
                        new JProperty("videoProgress", item["videoProgress"].ToString()),
                        new JProperty("videoId", item["videoId"].ToString())
                    );
                    PostList(urld, postdata.ToString());
                    if ((int)d["code"] != 1001)
                    {
                        BeginInvoke(mi, q1 + "|访问失败");
                        //MessageBox.Show("访问失败","错误提醒");
                        //return;
                    }
                    if (checkBox1.Checked)
                    {
                        Thread.Sleep(60*1000);
                        BeginInvoke(mi, "观看视频60秒,等待中");
                    }
                }
            }
            else
            {
                MessageBox.Show(b["code"].ToString());
            }
        }


        private void Zhu(int i)
        {
            var d = new SqliteHelper();
            var doc = new Dictionary<string, string>();
            string starttime = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd HH:mm:ss").ToString();
            Authorization = dataGridView1.Rows[i].Cells[5].Value.ToString();
            alipayMiniMark = dataGridView1.Rows[i].Cells[4].Value.ToString();
            q1 = dataGridView1.Rows[i].Cells[1].Value.ToString();
            GetList("https://qmxfxx.119.gov.cn/alipay/mini/api/home/taskScord/completeTask?taskCode=CTWLLOGIN", "");
            var b = GetList("https://qmxfxx.119.gov.cn/alipay/mini/api/users/activeScore", "");
            if ((int)b["code"] == 1001)
            {
                doc.Clear();
                dataGridView1.Rows[i].Cells[6].Value = b["result"].ToString();
                doc.Add("@jifen", b["result"].ToString());
                doc.Add("@id", dataGridView1.Rows[i].Cells[7].Value.ToString());
                d.ExecuteDataTable("update User set jifen = @jifen where id = @id", doc);
            }
            else
            {
                dataGridView1.Rows[i].Cells[1].Value = b["msg"].ToString();
                dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
            }

            b = GetList("https://qmxfxx.119.gov.cn/alipay/mini/api/users", "");
            if ((int)b["code"] == 1001)
            {
                doc.Clear();
                dataGridView1.Rows[i].Cells[1].Value = b["result"]["nickName"].ToString();
                dataGridView1.Rows[i].Cells[0].Value = b["result"]["userId"].ToString();
                doc.Add("@userId", b["result"]["userId"].ToString());
                doc.Add("@name", b["result"]["nickName"].ToString());
                doc.Add("@id", dataGridView1.Rows[i].Cells[7].Value.ToString());
                d.ExecuteDataTable("update User set name = @name,userId = @userId where id = @id", doc);

                ZhuChenXu();    //主要流程,包含视频和文章
                WeiLiangFen();  //结尾的两分
                Lianxi();       //做联系题目
                KaoShi();       //做考试试题
                                //每个人完成以后统计当日获得的积分
                b = GetList("https://qmxfxx.119.gov.cn/alipay/mini/api/users/activeScore", "");
                if ((int)b["code"] == 1001)
                {
                    doc.Clear();
                    dataGridView1.Rows[i].Cells[8].Value = ((int)b["result"] - int.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString())).ToString();
                    if(int.Parse(dataGridView1.Rows[i].Cells[8].Value.ToString()) > 0)
                    {
                        doc.Add("@name", dataGridView1.Rows[i].Cells[1].Value.ToString());
                        doc.Add("@jifen1", dataGridView1.Rows[i].Cells[6].Value.ToString());
                        doc.Add("@jifen2", b["result"].ToString());
                        doc.Add("@time1", starttime);
                        doc.Add("@time2", DateTime.Now.AddDays(3).ToString("yyyy-MM-dd HH:mm:ss").ToString());
                        doc.Add("@time3", "");
                        doc.Add("@qita1", "");
                        doc.Add("@qita2", "");
                        doc.Add("@userId", dataGridView1.Rows[i].Cells[0].Value.ToString());
                        d.ExecuteDataTable("INSERT INTO Rizhi VALUES((select id from Rizhi order by id desc) + 1, @name, @jifen1, @jifen2,'', '', @time1, @time2, @time3,@qita1, @qita2, @userId)", doc);
                    }
                }
            }
            else
            {
                dataGridView1.Rows[i].Cells[1].Value = b["msg"].ToString();
                dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
            }
        }

        /// <summary>
        /// 主循环
        /// </summary>
        /// <param name="stateInfo"></param>
        private void Nan(object stateInfo)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (Convert.ToDateTime(dataGridView1.Rows[i].Cells[3].Value.ToString()) > DateTime.Now)
                {
                    if (checkBox2.Checked)
                    {
                        if (dataGridView1.Rows[i].Selected)
                        {
                            Zhu(i);
                        }
                    }
                    else
                    {
                        Zhu(i);
                    }
                }
                else
                {
                    dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                }
            }
            if (Authorization == "" || alipayMiniMark == "")
            {
                MessageBox.Show("没有可以运行的用户数据,请检查是否全部过期或者没有增加用户数据？","启动失败");
                d1 = 0;
                return;
            }
            MessageBox.Show("视频任务已完成");
            d1 = 0;
            ((AutoResetEvent)stateInfo).Set();
        }

        static AutoResetEvent autoEvent = new AutoResetEvent(false);

        /// <summary>
        /// 开始程序
        /// </summary>
        private void Start()
        {
            if (d1 < 1)
            {
                d1 = 1;
                ThreadPool.QueueUserWorkItem(new WaitCallback(Nan), autoEvent);
                listBox1.Items.Add("开始执行");
            }
        }
        
        /// <summary>
        /// 开始执行按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Start();
        }

        /// <summary>
        /// 界面初始化
        /// </summary>
        private void ChuShiHua()
        {
            dataGridView1.Rows.Clear();
            var d = new SqliteHelper();
            var b = d.ExecuteDataTable("select id,userId,name,time,time1,Auth,alipay,jifen from User where st = 1", new Dictionary<string, string>());
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.RowHeadersWidth = 60;
            for (int i = 0; i < b.Rows.Count; i++)
            {
                int index = this.dataGridView1.Rows.Add();
                dataGridView1.Rows[index].HeaderCell.Value = (i + 1).ToString();
                dataGridView1.Rows[index].Cells[0].Value = b.Rows[i]["userId"].ToString();
                dataGridView1.Rows[index].Cells[1].Value = b.Rows[i]["name"].ToString();
                dataGridView1.Rows[index].Cells[2].Value = b.Rows[i]["time"].ToString();
                if (Convert.ToDateTime(b.Rows[i]["time1"].ToString()) < DateTime.Now)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                }
                dataGridView1.Rows[index].Cells[3].Value = b.Rows[i]["time1"].ToString();
                dataGridView1.Rows[index].Cells[4].Value = b.Rows[i]["alipay"].ToString();
                dataGridView1.Rows[index].Cells[5].Value = b.Rows[i]["Auth"].ToString();
                dataGridView1.Rows[index].Cells[6].Value = b.Rows[i]["jifen"].ToString();
                dataGridView1.Rows[index].Cells[7].Value = b.Rows[i]["id"].ToString();
            }
            dataGridView1.AllowUserToAddRows = false;
        }

        /// <summary>
        /// 窗口初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            ChuShiHua();
            timer1.Start();
        }

        /*读配置文件*/
        public static string GetValue(string section, string key)
        {
            string strPath = "./config.ini";  //这里是相对路径
            if (File.Exists(strPath))  //检查是否有配置文件，并且配置文件内是否有相关数据。
            {
                int i = 1200;
                StringBuilder sb = new StringBuilder(i);
                GetPrivateProfileString(section, key, "配置文件不存在", sb, i, strPath);

                return sb.ToString();
            }
            else
            {
                return string.Empty;
            }
        }


        /*写配置文件*/
        public static void SetValue(string section, string key, string value)
        {
            // ▼ 获取当前程序启动目录
            // string strPath = Application.StartupPath + @"/config.ini";  这里是绝对路径
            string strPath = "./config.ini";      //这里是相对路径，
            WritePrivateProfileString(section, key, value, strPath);
        }


        /// <summary>
        /// 时间戳反转为时间，有很多中翻转方法，但是，请不要使用过字符串（string）进行操作，大家都知道字符串会很慢！
        /// </summary>
        /// <param name="TimeStamp">时间戳</param>
        /// <param name="AccurateToMilliseconds">是否精确到毫秒</param>
        /// <returns>返回一个日期时间</returns>
        public static DateTime GetTime(long TimeStamp, bool AccurateToMilliseconds = false)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            if (AccurateToMilliseconds)
            {
                return startTime.AddTicks(TimeStamp * 10000);
            }
            else
            {
                return startTime.AddTicks(TimeStamp * 10000000);
            }
        }


        /// <summary>
        /// 增加配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.textBox2.Text.ToString() != "" && this.textBox1.Text.ToString() != "")
            {
                string[] cc = this.textBox1.Text.ToString().Split(".");
                if (cc.Length > 1)
                {
                    var e5 = new Engine()
                        .Execute(System.IO.File.ReadAllText("./js.txt"));
                    var cd = JObject.Parse(e5.Invoke("base64_decode", cc[1]).ToString());
                    //MessageBox.Show(GetTime((long)cd["exp"], false).ToString("yyyy-MM-dd HH:mm:ss"));//过期时间
                    //MessageBox.Show(GetTime((long)cd["nbf"], false).ToString("yyyy-MM-dd HH:mm:ss"));//授权时间
                    //cd["userId"].ToString();//用户ID
                    var d = new SqliteHelper();
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    string sql = "INSERT INTO User VALUES ((select id from User order by id desc)+1,'',null,'{0}','{1}','{2}','{3}','','','',null,null,'{4}',1)";
                    sql = string.Format(sql, this.textBox2.Text.ToString(), this.textBox1.Text.ToString(), GetTime((long)cd["nbf"], false).ToString("yyyy-MM-dd HH:mm:ss"), GetTime((long)cd["exp"], false).ToString("yyyy-MM-dd HH:mm:ss"), cd["userId"].ToString());
                    d.ExecuteNonQuery(sql, dic);
                    MessageBox.Show("添加参数成功!", "添加成功");
                    textBox2.Text = ""; textBox1.Text = "";
                    ChuShiHua();
                }
                else
                {
                    MessageBox.Show("参数有误", "添加失败");
                }
            }
            else
            {
                MessageBox.Show("确定两个参数都不能为空!", "添加失败");
            }

        }

        /// <summary>
        /// 定时执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            var MM = DateTime.Now.ToString("HH");
            var SS = DateTime.Now.ToString("mm");
             if (d1 < 1)
            {
                label4.Text = MM + SS;
                if (MM == "06" && SS == "00")
                {
                    ChuShiHua();
                    ThreadPool.QueueUserWorkItem(new WaitCallback(Nan), autoEvent);
                    listBox1.Items.Add("开始执行");
                }
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var b = new Form2();
            b.st = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            b.ShowDialog();
        }

        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否确认删除这"+dataGridView1.SelectedRows.Count.ToString() + "项？", "警告",
                             MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            //判断是否取消事件
            if (result == DialogResult.No)
            {
                return;
            }
            string ii = "";
            var d = new SqliteHelper();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Selected)
                {
                    if (ii == "")
                    {
                        ii = dataGridView1.Rows[i].Cells[7].Value.ToString();
                    }
                    else
                    {
                        ii = ii + "," + dataGridView1.Rows[i].Cells[7].Value.ToString();
                    }
                }
            }
            string sql = "update User set st = 2 where id in ({0})";
            sql = string.Format(sql,ii);
            d.ExecuteNonQuery(sql, dic);
            MessageBox.Show("删除成功!", "添加成功");
            ChuShiHua();

        }

    }
}
