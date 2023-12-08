using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        bool provjera = true;
        public Form1()
        {
            InitializeComponent();
            listView1.View = View.Details;
            listView1.Columns.Add("Ime datoteke", 150);
            listView1.Columns.Add("Veličina", 100);
            listView1.Columns.Add("Datum/Vrijeme", 150);
            listView1.ColumnClick += listView1_ColumnClick;
            listView1.ListViewItemSorter = null;
        }
        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            var items = listView1.Items.Cast<ListViewItem>().ToList();
            if (e.Column == 0)
            {
                items = items.OrderBy(item => item.SubItems[e.Column].Text).ToList();
            }
            else if(e.Column == 1)
            {
                items = items.OrderBy(item => int.Parse(item.SubItems[e.Column].Text)).ToList();
            }
            else
            {
                items = items.OrderBy(item => DateTime.Parse(item.SubItems[e.Column].Text)).ToList();
            }
            listView1.Items.Clear();
            listView1.Items.AddRange(items.ToArray());
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void InformacijeDatoteka()
        {
            {

                try
                {
                    string uneseniPut1 = textBox1.Text;
                    string uneseniPut = uneseniPut1.TrimStart('"').TrimEnd('"');
                    
                    if (checkBox1.Checked)
                    {
                        string[] datoteke = Directory.GetFiles(uneseniPut, "*.*");
                        for (int i = 0; i < datoteke.Length; i++)
                        {
                            FileInfo fileInfo = new FileInfo(datoteke[i]);
                            string nazivDatoteke = Path.GetFileName(datoteke[i]);
                            long velicinaDatoteke = fileInfo.Length;
                            DateTime vrijemeZadnjeIzmjene = fileInfo.LastWriteTime;
                            ListViewItem item = new ListViewItem(nazivDatoteke);
                            item.SubItems.Add(velicinaDatoteke.ToString());
                            item.SubItems.Add(vrijemeZadnjeIzmjene.ToString());
                            listView1.Items.Add(item);
                        }
                        
                    }
                    else
                    {
                        string[] datoteke = Directory.GetFiles(uneseniPut, "*.*");
                        for (int i = 0; i < datoteke.Length; i++)
                        {
                            FileAttributes atributi = File.GetAttributes(datoteke[i]);
                            if ((atributi & FileAttributes.Hidden) == 0 )
                            {
                                FileInfo fileInfo = new FileInfo(datoteke[i]);
                                string nazivDatoteke = Path.GetFileName(datoteke[i]);
                                long velicinaDatoteke = fileInfo.Length;
                                DateTime vrijemeZadnjeIzmjene = fileInfo.LastWriteTime;
                                ListViewItem item = new ListViewItem(nazivDatoteke);
                                item.SubItems.Add(velicinaDatoteke.ToString());
                                item.SubItems.Add(vrijemeZadnjeIzmjene.ToString());
                                listView1.Items.Add(item);
                            }
                        }
                     }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Došlo je do greške: " + ex.Message);

                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && provjera)
            {
                provjera = false;
                InformacijeDatoteka();
            }
            else if(textBox1.Text != "" && provjera == false)
            {
                MessageBox.Show("Već ste pretražili folder");
            }     
            else
            {
                MessageBox.Show("Niste unjeli putanju");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}