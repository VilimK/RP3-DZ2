using System.Text.RegularExpressions;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;

namespace dz2
{
    public partial class Form1 : Form
    {
        public static class Information
        {
            public static bool crta = true; //true = crta, false = krug
            public static bool first_click = true; //true = prvi klik, false = drugi klik
            public static int FstClickCoordinateX
            {
                get;
                set; 
            }
            public static int FstClickCoordinateY;
            public static int SndClickCoordinateX;
            public static int SndClickCoordinateY;
            public static List<string> lines = new List<string>(); 
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
            Form1.Information.crta = false; 
        }
        private void Crta_Click(object sender, EventArgs e)
        {
            Form1.Information.crta = true;
        }
        void DeleteAllDrawings()
        {
            var g = panel1.CreateGraphics();
            g.Clear(panel1.BackColor);
        }
        void DrawCircle(int r,int x,int y)
        {
            var g = panel1.CreateGraphics();
            var p = new Pen(Color.Black, 2);
            g.DrawEllipse(p, x - r, y - r, r + r, r + r);
        }
        void DrawAllCodes(List <string> lines)
        {
            //prvo obrisi sve na drawing panelu
            string code = richTextBox1.Text;
            string reg_krug = "^Krug\\((\\d+),(\\d+),(\\d+)\\)$";
            string reg_crta = "^Crta\\((\\d+),(\\d+),(\\d+),(\\d+)\\)$";
            for (int i = 0; i < lines.Count; i++)
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
            Form1.Information.lines = lines; 
            DeleteAllDrawings();
            int x1, x2, y1, y2, r;
            for (int i = 0; i < lines.Count; i++)
            {
                string[] parameters = lines[i].Split('(');
                string objectName = parameters[0];
                if (objectName == "Krug")
                {
                    //dohvati 3 broja

                    parameters[1] = parameters[1].TrimEnd(' ');
                    parameters[1] = parameters[1].TrimEnd(')');
                    string[] nums = parameters[1].Split(',');
                    nums[2] = nums[2].Trim(')');
                    //raspakiraj

                    x1 = Int32.Parse(nums[0]);
                    y1 = Int32.Parse(nums[1]);
                    r = Int32.Parse(nums[2]);
                    //crtaj krug
                    DrawCircle(r, x1, y1);
                }
                else if (objectName == "Crta")
                {
                    //dohvati 4 broja
                    parameters[1] = parameters[1].TrimEnd(' ');
                    parameters[1] = parameters[1].TrimEnd(')');
                    string[] nums = parameters[1].Split(',');
                    //nums[3] = nums[3].Remove();
                    nums[3] = nums[3].TrimEnd('\n');
                    nums[3] = nums[3].TrimEnd(')');
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
            List<string> lines = new List<string>(code.Split('\n'));
            //provjeri kroz regexe
            string reg_krug = "^Krug\\((\\d+),(\\d+),(\\d+)\\)$";
            string reg_crta = "^Crta\\((\\d+),(\\d+),(\\d+),(\\d+)\\)$";
            for (int i = 0; i < lines.Count; i++)
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
            Form1.Information.lines = lines;
            DrawAllCodes(Form1.Information.lines); 

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
                List<string> lines = new List<string>(code.Split('\n'));
                //provjeri kroz regexe
                string reg_krug = "^Krug\\((\\d+),(\\d+),(\\d+)\\)$";
                string reg_crta = "^Crta\\((\\d+),(\\d+),(\\d+),(\\d+)\\)$";
                for (int i = 0; i < lines.Count; i++)
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
                Form1.Information.lines = lines; 
                DrawAllCodes(Form1.Information.lines);
            }
            else if (odabir == "Kod")
            {
                splitContainer1.Panel2Collapsed = true;
                splitContainer1.Panel2.Hide();
                splitContainer1.Panel1Collapsed = false;
                splitContainer1.Panel1.Show();
            }
            else if (odabir == "Vizualno ure?ivanje")
            {
                splitContainer1.Panel1Collapsed = true;
                splitContainer1.Panel1.Hide();
                splitContainer1.Panel2Collapsed = false;
                splitContainer1.Panel2.Show();
                string code = richTextBox1.Text;
                List<string> lines = new List<string>(code.Split('\n'));
                //provjeri kroz regexe
                string reg_krug = "^Krug\\((\\d+),(\\d+),(\\d+)\\)$";
                string reg_crta = "^Crta\\((\\d+),(\\d+),(\\d+),(\\d+)\\)$";
                for (int i = 0; i < lines.Count; i++)
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
                Form1.Information.lines = lines; 
                DrawAllCodes(Form1.Information.lines);
            }
        }
        string UnpackLinesInString(List<string> lines)
        {
            string line = ""; 
            for(int i = 0; i < lines.Count; i++)
            {
                //radi sigurnosti, da se ne pojave dvije linije u jednoj ili nepotrebne prazne linije 
                lines[i] = lines[i].TrimEnd('\n');
                lines[i] += '\n';
                line += lines[i]; 
            }
            return line; 
        }
        void UpdateCodeAfterVisualDrawCrta(int x1, int y1, int x2, int y2)
        {
            //slozi string
            string new_line = "Crta(";
            new_line += x1.ToString();
            new_line += ",";
            new_line += y1.ToString();
            new_line += ",";
            new_line += x2.ToString();
            new_line += ",";
            new_line += y2.ToString();
            new_line += ")";
            Form1.Information.lines.Add(new_line); 
            string one_line = UnpackLinesInString(Form1.Information.lines);
            one_line = one_line.Replace("\n\n", "\n");
            richTextBox1.Text = one_line;
        }

        void UpdateCodeAfterVisualDrawKrug(int r, int x,int y)
        {
            string new_line = "Krug(";
            new_line += x.ToString();
            new_line += ",";
            new_line += y.ToString();
            new_line += ",";
            new_line += r.ToString();
            new_line += ")";
            Form1.Information.lines.Add(new_line);
            string one_line = UnpackLinesInString(Form1.Information.lines);
            one_line= one_line.Replace("\n\n", "\n");
            richTextBox1.Text = one_line;
        }

        private void ClickForVisualDraw(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("U funckiji clickforvisualdraw!");
            if (Form1.Information.first_click)
            {
                //crta slu?aj
                Point point = panel1.PointToClient(Cursor.Position);
                Form1.Information.FstClickCoordinateX = point.X;
                Form1.Information.FstClickCoordinateY = point.Y;
            }
            else 
            {
                //krug slu?aj, u ovoj grani obavljamo crtanje
                //spremi informacije
                Point point = panel1.PointToClient(Cursor.Position);
                Form1.Information.SndClickCoordinateX= point.X;
                Form1.Information.SndClickCoordinateY = point.Y;
                int x1, y1, x2, y2;
                x1 = Form1.Information.FstClickCoordinateX;
                y1 = Form1.Information.FstClickCoordinateY;
                x2 = Form1.Information.SndClickCoordinateX;
                y2 = Form1.Information.SndClickCoordinateY;
                //pogledaj je li krug ili crta
                if (Form1.Information.crta)
                {
                    DrawLine(x1, y1, x2, y2);
                    UpdateCodeAfterVisualDrawCrta(x1, y1, x2, y2);
                }
                else
                {
                    //izracunaj radius kao udaljenost izmedu dvije tocke, a srediste je x1,y1 => euklidska metrika
                    double r_double = (x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1);
                    r_double = Math.Sqrt(r_double);
                    int r = Convert.ToInt32(r_double);
                    DrawCircle(r, x1, y1);
                    UpdateCodeAfterVisualDrawKrug(r,x1,y1); 
                }
            }
            Form1.Information.first_click = !(Form1.Information.first_click);
        }


        private void Repaint(object sender, PaintEventArgs e)
        {
            DrawAllCodes(Form1.Information.lines);
        }
    }
}