using System;
using System.Windows.Forms;

namespace csharp_desktop_winform_crud
{
    public partial class addPost : Form
    {
        private string dataPath = "./data.csv";
        private DataService dataService;

        public addPost()
        {
            InitializeComponent();
            dataService = new DataService();
            dataService.path = dataPath;
            dataService.createFile();
        }

        private void addPost_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Title is required.");
            }else if (textBox2.Text.Trim() == "")
            {
                MessageBox.Show("Description is required.");
            }
            else
            {
                dataService.insertData(textBox1.Text, textBox2.Text);
                MessageBox.Show("Post has been added.");
            }
        }
    }
}
