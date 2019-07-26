using GD.CoursePreparationSystem.Common;
using GD.CoursePreparationSystem.Domain.Model;
using GD.CoursePreparationSystem.Message;
using GD.CoursePreparationSystem.Message.CourseModule;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RabbitMqDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var iocManager = IocManager.Instance;
            Assembly[] assemblies = new Assembly[]
            {
                    Assembly.Load("GD.CoursePreparationSystem.Domain"),
                    Assembly.Load("GD.CoursePreparationSystem.RabbitMQ"),
                   Assembly.Load("GD.CoursePreparationSystem.Message")
            };
            iocManager.RegisterAutofacModules(assemblies);
            var container = iocManager.Build();
            AutoMapperConfiguration.Initialize(assemblies);
           
        }

        private void Button1_Click(object sender, EventArgs e)
        {
           bool b= MessageSender.SendTestMessage(new CourseDto
            {
                Id = 11,
                Name = "test"
            });
            if(b)
            {
                MessageBox.Show("消息发送success");
            }
            else
            {
                MessageBox.Show("消息发送失败");
            }
        }
    }
}
