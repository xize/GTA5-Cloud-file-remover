using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTAV_cloud_remover
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = Path.GetTempPath();
            IEnumerable<string> files;
            try
            {
                files = Directory.EnumerateFiles(path, "cloud_*.dat", SearchOption.TopDirectoryOnly);
            } catch(Exception)
            {
                MessageBox.Show("no files found", "error: no files!");
                return;
            }
            int allfiles = files.ToArray().Length;

            if(allfiles <= 0)
            {
                MessageBox.Show("no files found", "error: no files!");
                return;
            }

            int index = 0;
            long fileSize = 0;
            while (files.GetEnumerator().MoveNext())
            {
                string file = files.First();
                Console.WriteLine("removing file: "+file);
                fileSize += new FileInfo(file).Length;
                File.Delete(file);
                try
                {
                    progressBar1.Value = (int)((100 * index++) / allfiles - 1);
                } catch(Exception)
                {
                    Console.WriteLine("value: "+allfiles+","+(allfiles-1)+","+(100*index++));
                }
                 Console.WriteLine("index: "+index);
            }

            MessageBox.Show("in total "+allfiles+" files have been removed\n\nthat is in total: "+fileSize / 1048+"kb", "complete!");

        }
    }
}
