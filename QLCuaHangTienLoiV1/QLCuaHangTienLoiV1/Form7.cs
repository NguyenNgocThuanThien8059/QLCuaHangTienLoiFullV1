using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLCuaHangTienLoiV1.Models;

namespace QLCuaHangTienLoiV1
{
    public partial class Form7 : Form
    {
        Model1 context = new Model1();
        public Form7()
        {
            InitializeComponent();
        }
        private void FillProductComboBox(List<Product> ProductList)
        {
            this.comboBox1.DataSource = ProductList;
            this.comboBox1.DisplayMember = "ProductName";
            this.comboBox1.ValueMember = "ProductID";
        }
        private void BindGrid(List<Storage> StorageList)
        {
            dataGridView1.Rows.Clear();
            foreach (var Storage in StorageList)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = Storage.Area;
                if (Storage.ProductID != null)
                {
                    dataGridView1.Rows[index].Cells[1].Value = Storage.Product.ProductName;
                }
                else
                {
                    dataGridView1.Rows[index].Cells[1].Value = "";
                }
                dataGridView1.Rows[index].Cells[2].Value = Storage.StockAmount;
                dataGridView1.Rows[index].Cells[3].Value = Storage.MaxStock;
            }
        }
        private void Form7_Load(object sender, EventArgs e)
        {
            List<Storage> StorageList = context.Storage.ToList();
            List<Product> ProductList = context.Product.ToList();
            FillProductComboBox(ProductList);
            BindGrid(StorageList);
        }
        public void LoadList()
        {
            List<Storage> LoadStorageList = context.Storage.ToList();
            BindGrid(LoadStorageList);
            textBox1.Clear();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            LoadList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[1].Value.ToString() != textBox1.Text)
                {
                    dataGridView1.Rows[i].Visible = false;
                }
                else
                {
                    dataGridView1.Rows[i].Visible = true;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox2.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }
    }
}
