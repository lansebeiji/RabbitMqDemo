using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace GD.CoursePreparationSystem.Domain
{
    public class DBConfig
    {
        private static string connString = null;

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string ConnString
        {
            get
            {
                if (string.IsNullOrWhiteSpace(connString))
                {
                    connString = ConfigurationManager.ConnectionStrings["ERPMessage"].ConnectionString;
                }

                return connString;
            }
        }
    }
}
