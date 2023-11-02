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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QLCuaHangTienLoiV1
{
    public partial class Form4 : Form
    {
        Model1 context = new Model1();
        public Form4()
        {
            InitializeComponent();
        }
        private void FillRoleComboBox(List<ProductType> ProductTypeList)
        {
            this.comboBox1.DataSource = ProductTypeList;
            this.comboBox1.DisplayMember = "ProductTypeName";
            this.comboBox1.ValueMember = "ProductTypeID";
        }
        private void BindGrid(List<Product> ProductList)
        {
            dataGridView1.Rows.Clear();
            foreach (var Product in ProductList)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = "SP" + Product.ProductID.ToString("D3");
                dataGridView1.Rows[index].Cells[1].Value = Product.ProductName;
                dataGridView1.Rows[index].Cells[2].Value = Product.ProductType.ProductTypeName;
                dataGridView1.Rows[index].Cells[3].Value = Product.SellPrice;
                textBox1.Text = "SP" + (Product.ProductID + 1).ToString("D3");
            }
        }
        public void LoadList()
        {
            List<Product> LoadProductList = context.Product.ToList();
            BindGrid(LoadProductList);
        }
        private void LoadForm()
        {
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            comboBox1.SelectedIndex = 0;
        }
        private void Form4_Load(object sender, EventArgs e)
        {
            textBox1.Text = "SP001";
            List<Product> ProductList = context.Product.ToList();
            List<ProductType> ProductTypeList = context.ProductType.ToList();
            FillRoleComboBox(ProductTypeList);
            BindGrid(ProductList);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            List<Product> ProductList = context.Product.ToList();
            Product NewProduct = new Product();
            NewProduct.ProductID = int.Parse(textBox1.Text.Substring(2));
            NewProduct.ProductName = textBox2.Text;
            NewProduct.ProductTypeID = (comboBox1.SelectedItem as ProductType).ProductTypeID;
            NewProduct.SellPrice = int.Parse(textBox3.Text);
            context.Product.Add(NewProduct);
            context.SaveChanges();
            LoadList();
            LoadForm();
        }
        //Chỉnh sửa thông tin sản phẩm
        private void button2_Click(object sender, EventArgs e)
        {
            int MaSP = int.Parse(textBox1.Text.Substring(2));
            Product UpdateProduct = context.Product.FirstOrDefault(p => p.ProductID == MaSP);
            UpdateProduct.ProductName = textBox2.Text;
            UpdateProduct.ProductTypeID = (comboBox1.SelectedItem as ProductType).ProductTypeID;
            UpdateProduct.SellPrice = int.Parse(textBox3.Text);
            context.SaveChanges();
            LoadList();
            LoadForm();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }
        //Kiểm tra trên textbox tìm kiếm là chữ hay số
        private bool IsNumber(string Value)
        {
            int Result;
            if (int.TryParse(Value, out Result) == false) //Nếu là chữ trả về false
            {
                return false;
            }
            return true; //Nếu là số trả về true
        }
        //Reload lại Form
        private void button4_Click(object sender, EventArgs e)
        {
            LoadList();
            LoadForm();
        }
        //Tìm kiếm theo mã hoặc tên
        private void button3_Click(object sender, EventArgs e)
        {
            int A, B;
            if (IsNumber(textBox4.Text) == true) //Nếu trong textbox tìm kiếm là số thì tìm theo mã
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    A = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString().Substring(2));
                    B = int.Parse(textBox4.Text);
                    if (A != B)
                    {
                        dataGridView1.Rows[i].Visible = false; //Ẩn các dòng không trùng mã
                    }
                    else
                    {
                        dataGridView1.Rows[i].Visible = true; //Hiện thị dòng trùng mã
                    }
                }
            }
            else //Nếu trong textbox tìm kiếm là chữ thì tìm theo tên
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells[1].Value.ToString() != textBox4.Text)
                    {
                        dataGridView1.Rows[i].Visible = false; //Ẩn các dòng không trùng tên
                    }
                    else
                    {
                        dataGridView1.Rows[i].Visible = true; //Hiện thị các dòng trùng tên
                    }
                }
            }
        }
        //Thoát
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
