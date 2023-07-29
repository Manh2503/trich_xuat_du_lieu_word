using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Interface;
namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string strfilename = openFileDialog1.FileName;
                Document doc = new Document();
                doc.LoadFromFile(strfilename, FileFormat.Doc);

                StringBuilder sb = new StringBuilder();
                foreach (Section section in doc.Sections)
                {
                    int index = 1;
                    foreach (Paragraph paragraph in section.Paragraphs)
                    {
                        sb.AppendLine(paragraph.Text);

                        foreach (DocumentObject docobject in paragraph.ChildObjects)
                        {
                            //nếu kiểu của docobject là kiểu ảnh thì thực hiện xuất ảnh
                            if (docobject.DocumentObjectType == DocumentObjectType.Picture)
                            {
                                DocPicture picture = docobject as DocPicture;

                                //đặt tên choa nhr
                                String imagename = String.Format(@"images\img_{0}.png", index);

                                //lưu ảnh
                                picture.Image.Save(imagename, System.Drawing.Imaging.ImageFormat.Png);
                                index++;
                            }
                        }
                    }

                }

                File.WriteAllText(@"result.txt", sb.ToString());
                //hiển thị file txt
                System.Diagnostics.Process.Start(@"result.txt");
            }
        }
    }
}
