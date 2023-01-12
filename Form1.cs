using System.Text.RegularExpressions;

namespace dz2
{
    public partial class Form1 : Form
    {
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
        private void HandleNewCodeAndDrawObject(object sender, EventArgs e)
        {
            string code = richTextBox1.Text;
            string[] lines = code.Split('\n');
            //provjeri kroz regexe
            string reg_krug = "^Krug\\\\((\\\\d+),(\\\\d+),(\\\\d+)\\\\)$\r\n";
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
            int x1, x2, y1, y2, r;
            //svi su ok napisani, idemo ih sada kroz for petlju sve nacrtati
            for (int i = 0; i < lines.Length; i++)
            {
                string[] parameters = lines[i].Split('(');
                string objectName = parameters[0];
                if (objectName == "Krug")
                {
                    //dohvati 3 broja
                    /*
                    parametri[1] = parametri[1].TrimEnd(' ');
                    parametri[1] = parametri[1].TrimEnd(')');
                    string[] nums = parametri[1].Split(',');
                    x1 = Int32.Parse(nums[0]);
                    y1 = Int32.Parse(nums[1]);
                    r = Int32.Parse(nums[2]);*/

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
                    
                    var g = panel1.CreateGraphics();
                    var p = new Pen(Color.Black, 3);
                    var point1 = new Point(x1, y1);
                    var point2 = new Point(x2, y2);
                    g.DrawLine(p, point1, point2);
                    
                }

            }
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
            }
        }
    }
}