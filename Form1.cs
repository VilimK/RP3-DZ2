using System.Text.RegularExpressions;
using System.Drawing;

namespace dz2
{
    public partial class Form1 : Form
    {
        public class Settings 
        {
            public bool crta = true;
            public bool first_click = true;
            public Tuple<int, int> FstClickCoordinates;
            public Tuple<int, int> SndClickCoordinates;
            public string[] lines; 
        };
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }

        void DrawCircle(int x,int y,int r)
        {
            var g = panel1.CreateGraphics();
            var p = new Pen(Color.Black, 2);
            g.DrawEllipse(p, x - r, y - r, r + r, r + r);
        }
        void DrawAllCodes(string[] lines)
        {
            int x1, x2, y1, y2, r;
            for (int i = 0; i < lines.Length; i++)
            {
                string[] parameters = lines[i].Split('(');
                string objectName = parameters[0];
                if (objectName == "Krug")
                {
                    //dohvati 3 broja

                    parameters[1] = parameters[1].TrimEnd(' ');
                    parameters[1] = parameters[1].TrimEnd(')');
                    string[] nums = parameters[1].Split(',');
                    //raspakiraj
                    r = Int32.Parse(nums[0]);
                    x1 = Int32.Parse(nums[1]);
                    y1 = Int32.Parse(nums[2]);
                    //crtaj krug
                    DrawCircle(x1, y1, r);
                }
                else if (objectName == "Crta")
                {
                    //dohvati 4 broja
                    parameters[1] = parameters[1].TrimEnd(' ');
                    parameters[1] = parameters[1].TrimEnd(')');
                    string[] nums = parameters[1].Split(',');
                    x1 = Int32.Parse(nums[0]);
                    y1 = Int32.Parse(nums[1]);
                    x2 = Int32.Parse(nums[2]);
                    y2 = Int32.Parse(nums[3]);
                    //crtanje
                    DrawLine(x1, y1, x2, y2);
                }
            }
        }
        void DrawLine(int x1,int y1,int x2,int y2)
        {
            var g = panel1.CreateGraphics();
            var p = new Pen(Color.Black, 2);
            var point1 = new Point(x1, y1);
            var point2 = new Point(x2, y2);
            g.DrawLine(p, point1, point2);
        }
        private void HandleNewCodeAndDrawObject(object sender, EventArgs e)
        {
            string code = richTextBox1.Text;
            string[] lines = code.Split('\n');
            //provjeri kroz regexe
            string reg_krug = "^Krug\\((\\d+),(\\d+),(\\d+)\\)$";
            string reg_crta = "^Crta\\((\\d+),(\\d+),(\\d+),(\\d+)\\)$";
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == "\n" || String.IsNullOrEmpty(lines[i]))
                {
                    continue;
                }
                if (!Regex.IsMatch(lines[i], reg_krug) && !Regex.IsMatch(lines[i], reg_crta))
                {
                    //ovaj regex nije dobar, dakle nista ne radimo
                    return;
                }
            }
            //svi su ok napisani, idemo ih sada kroz for petlju sve nacrtati
            DrawAllCodes(lines); 

        }
        private void Change_View(object sender, EventArgs e)
        {
            string odabir = toolStripComboBox1.SelectedItem.ToString();
            if (odabir == "Oboje")
            {
                splitContainer1.Panel1Collapsed = false;
                splitContainer1.Panel1.Show();
                splitContainer1.Panel2Collapsed = false;
                splitContainer1.Panel2.Show();
                string code = richTextBox1.Text;
                string[] lines = code.Split('\n');
                //provjeri kroz regexe
                string reg_krug = "^Krug\\((\\d+),(\\d+),(\\d+)\\)$";
                string reg_crta = "^Crta\\((\\d+),(\\d+),(\\d+),(\\d+)\\)$";
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i] == "\n" || String.IsNullOrEmpty(lines[i]))
                    {
                        continue;
                    }
                    if (!Regex.IsMatch(lines[i], reg_krug) && !Regex.IsMatch(lines[i], reg_crta))
                    {
                        //ovaj regex nije dobar, dakle nista ne radimo
                        return;
                    }
                }
                DrawAllCodes(lines);
            }
            else if (odabir == "Kod")
            {
                splitContainer1.Panel2Collapsed = true;
                splitContainer1.Panel2.Hide();
                splitContainer1.Panel1Collapsed = false;
                splitContainer1.Panel1.Show();
            }
            else if (odabir == "Vizualno ureðivanje")
            {
                splitContainer1.Panel1Collapsed = true;
                splitContainer1.Panel1.Hide();
                splitContainer1.Panel2Collapsed = false;
                splitContainer1.Panel2.Show();
                string code = richTextBox1.Text;
                string[] lines = code.Split('\n');
                //provjeri kroz regexe
                string reg_krug = "^Krug\\((\\d+),(\\d+),(\\d+)\\)$";
                string reg_crta = "^Crta\\((\\d+),(\\d+),(\\d+),(\\d+)\\)$";
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i] == "\n" || String.IsNullOrEmpty(lines[i]))
                    {
                        continue;
                    }
                    if (!Regex.IsMatch(lines[i], reg_krug) && !Regex.IsMatch(lines[i], reg_crta))
                    {
                        //ovaj regex nije dobar, dakle nista ne radimo
                        return;
                    }
                }
                DrawAllCodes(lines);
            }
        }

        private void ClickForVisualDraw(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("U funckiji clickforvisualdraw!");
            if (true)
            {
                Point point = panel1.PointToClient(Cursor.Position);
                MessageBox.Show(point.ToString());
            }
        }
    }
}