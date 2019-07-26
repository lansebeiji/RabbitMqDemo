using GD.CoursePreparationSystem.Common;
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

namespace MessageReceive
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var iocManager = IocManager.Instance;
            Assembly[] assemblies = new Assembly[]
             {
               Assembly.Load("GD.CoursePreparationSystem.Common"),
              Assembly.Load("GD.CoursePreparationSystem.Domain"),
               Assembly.Load("GD.CoursePreparationSystem.RabbitMQ"),
               Assembly.Load("GD.CoursePreparationSystem.Message"),
              Assembly.Load("MessageReceive")
             };
            iocManager.RegisterAutofacModules(assemblies);
            var container = iocManager.Build();
            AutoMapperConfiguration.Initialize(assemblies);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var handler = new SubscribeHandler();
            MessageBox.Show(handler.Listening());
        }
    }
}
