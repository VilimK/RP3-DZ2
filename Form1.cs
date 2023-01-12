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