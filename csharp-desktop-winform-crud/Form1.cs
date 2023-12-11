using System;
using System.Windows.Forms;

namespace csharp_desktop_winform_crud
{
    public partial class Form1 : Form
    {

        private string dataPath = "./data.csv";
        private DataService dataService;

        public Form1()
        {
            InitializeComponent();
            dataService = new DataService();
            dataService.path = dataPath;
            dataService.createFile();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addPost add_post = new addPost();
            add_post.ShowDialog();
        }

        /// <summary>
        /// Load data from file and view it in dataGridView
        /// </summary>
        private void loadData()
        {
            try
            {
                var posts = dataService.getPosts();
                foreach (var post in posts)
                {
                    string[] rowData = { post.id.ToString(), post.title, post.description };
                    dataGridView1.Rows.Add(rowData);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                DialogResult dr = MessageBox.Show("Are you sure to delete this?", "Delete Action", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (dr == DialogResult.Yes)
                {
                    if (dataGridView1.SelectedRows.Count > 0)
                    {
                        int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                        string id = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();

                        dataService.deletePost(int.Parse(id));
                    }
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if a valid cell is clicked (not a header or empty space)
            if (e.RowIndex >= 0)
            {
                // Clear the previous selection
                dataGridView1.ClearSelection();

                // Select the entire row of the clicked cell
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
        }
    }
}
