using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageReceive
{
    public class Loger
    {
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="content">日志内容</param>
        /// <param name="title">日志标题</param>
        /// <param name="folderName">文件夹名称</param>
        /// <param name="filePrefixName">文件前缀名</param>
        public static void Write(string content, string title = "", string folderName = "Log", string filePrefixName = "Log")
        {
            try
            {
                lock (typeof(Loger))
                {
                    DateTime dateTimeNow = DateTime.Now;
                    string logDirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log", folderName);
                    if (!Directory.Exists(logDirPath))
                    {
                        Directory.CreateDirectory(logDirPath);
                    }

                    string logFilePath = string.Format("{0}/{1}-{2}.txt", logDirPath, filePrefixName, dateTimeNow.ToString("yyyy-MM-dd"));
                    using (StreamWriter writer = new StreamWriter(logFilePath, true, Encoding.UTF8))
                    {
                        try
                        {
                            writer.WriteLine("------------------------------------------------------------------------------------------");
                            writer.WriteLine(title);
                            writer.WriteLine("日志时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                            writer.WriteLine(content);
                            writer.WriteLine("------------------------------------------------------------------------------------------");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Loger.cs Line45" + ex.Message);
                        }

                        writer.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Loger.cs Line54" + ex);
                //throw new Exception("无法将日志写入文件,请查看安装目录是否有权限!");
            }
        }

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="format">符合格式字符串</param>
        /// <param name="args">一个对象数组，其中包含零个或多个要设置格式的对象</param>
        public static void WriteFormat(string format, params object[] args)
        {
            string content = string.Format(format, args);
            Write(content, "", "Log", "Log");
        }

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="ex">Exception对象</param>
        /// <param name="title">日志标题</param>
        /// <param name="folderName">文件夹名称</param>
        /// <param name="filePrefixName">文件前缀名</param>
        public static void Write(Exception ex, string title = "", string folderName = "Exception", string filePrefixName = "Exception")
        {
            string content = string.Format("错误信息：{1}{0}错误来源：{2}{0}堆栈信息：{0}{3}", Environment.NewLine, ex.Message, ex.Source, ex.StackTrace);
            Write(content, title, folderName, filePrefixName);
        }
    }
}
