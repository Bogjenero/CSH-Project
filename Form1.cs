using System.Diagnostics.Eventing.Reader;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        bool sortOrder = true;

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
                if (sortOrder)
                    items = items.OrderBy(item => item.SubItems[e.Column].Text).ToList();
                else
                    items = items.OrderByDescending(item => item.SubItems[e.Column].Text).ToList();
            }

            else if (e.Column == 1)
            {
                if (sortOrder)
                    items = items.OrderBy(item => int.Parse(item.SubItems[e.Column].Text)).ToList();
                else
                    items = items.OrderByDescending(item => int.Parse(item.SubItems[e.Column].Text)).ToList();
            }
            else
            {
                if (sortOrder)
                    items = items.OrderBy(item => DateTime.Parse(item.SubItems[e.Column].Text)).ToList();
                else
                    items = items.OrderByDescending(item => DateTime.Parse(item.SubItems[e.Column].Text)).ToList();
            }
            sortOrder = !sortOrder;
            listView1.Items.Clear();
            listView1.Items.AddRange(items.ToArray());
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void InformacijeDatoteka()
        {
            try
            {
                string[] datoteke;
                string uneseniPut1 = textBox1.Text;
                string uneseniPut = uneseniPut1.TrimStart('"').TrimEnd('"');
                EnumerationOptions options = new EnumerationOptions();
                listView1.Items.Clear();
                if (checkBox1.Checked)
                {
                    datoteke = Directory.GetFiles(uneseniPut, "*.*");
                }
                else
                {
                    options.AttributesToSkip = FileAttributes.Hidden;
                    datoteke = Directory.GetFiles(uneseniPut, "*.*", options);
                }
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
                };

            }
            catch (Exception ex)
            {
                Console.WriteLine("Došlo je do greške: " + ex.Message);

            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog.SelectedPath;
                InformacijeDatoteka();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            InformacijeDatoteka();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(textBox1.Text))
            {
                InformacijeDatoteka();
            }
            else
            {
                listView1.Items.Clear();
            }
        }
    }
}


