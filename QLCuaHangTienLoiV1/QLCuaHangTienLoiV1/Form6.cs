using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLCuaHangTienLoiV1.Models;

namespace QLCuaHangTienLoiV1
{
    public partial class Form6 : Form
    {
        Model1 context = new Model1();
        public Form6()
        {
            InitializeComponent();
        }
        private void FillImportSourceComboBox(List<ImportSource> ImportSourceList)
        {
            this.comboBox1.DataSource = ImportSourceList;
            this.comboBox1.DisplayMember = "ImportSourceName";
            this.comboBox1.ValueMember = "ImportSourceID";
        }
        private void FillProductComboBox(List<Product> ProductList)
        {
            this.comboBox2.DataSource = ProductList;
            this.comboBox2.DisplayMember = "ProductName";
            this.comboBox2.ValueMember = "ProductID";
        }
        private void BindGrid(List<ImportDetail> ImportDetailList)
        {
            dataGridView1.Rows.Clear();
            foreach (var Detail in ImportDetailList)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = Detail.RequestNumber;
                dataGridView1.Rows[index].Cells[1].Value = Detail.Import.ImportSource.ImportSourceName;
                dataGridView1.Rows[index].Cells[2].Value = Detail.Import.Product.ProductName;
                dataGridView1.Rows[index].Cells[3].Value = Detail.Import.ImportPrice;
                dataGridView1.Rows[index].Cells[4].Value = Detail.ImportAmount;
                dataGridView1.Rows[index].Cells[5].Value = (Detail.Import.ImportPrice * Detail.ImportAmount);
                dataGridView1.Rows[index].Cells[6].Value = Detail.ImportDate;
            }
        }
        public void LoadList()
        {
            List<ImportDetail> LoadImportDetailList = context.ImportDetail.ToList();
            BindGrid(LoadImportDetailList);
        }
        private void LoadForm()
        {
            textBox2.Clear();
            comboBox1.SelectedIndex = 0;
        }
        private void Form6_Load(object sender, EventArgs e)
        {
            List<ImportSource> ImportSourceList = context.ImportSource.ToList();
            List<ImportDetail> ImportDetailList = context.ImportDetail.ToList();
            List<Import> ImportList = context.Import.ToList();
            FillImportSourceComboBox(ImportSourceList);
            List<Product> ProductList2 = new List<Product>();
            foreach (var item in ImportSourceList)
            {
                if (item.ImportSourceName == comboBox1.Text)
                {
                    List<Import> ImportList2 = context.Import.Where(a => a.ImportSourceID == item.ImportSourceID).ToList();
                    foreach (var item2 in ImportList2)
                    {
                        List<Product> ProductList = context.Product.Where(b => b.ProductID == item2.ProductID).ToList();
                        ProductList2.AddRange(ProductList);
                    }
                }
            }
            FillProductComboBox(ProductList2);
            BindGrid(ImportDetailList);
            foreach (var item in ImportList)
            {
                if (item.ProductID == (comboBox2.SelectedItem as Product).ProductID)
                {
                    textBox1.Text = item.ImportPrice.ToString();
                }
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<ImportSource> ImportSourceList = context.ImportSource.ToList();
            List<Product> ProductList2 = new List<Product>();
            foreach (var item in ImportSourceList)
            {
                if (item.ImportSourceName == comboBox1.Text)
                {
                    List<Import> ImportList = context.Import.Where(a => a.ImportSourceID == item.ImportSourceID).ToList();
                    foreach (var item2 in ImportList)
                    {
                        List<Product> ProductList = context.Product.Where(b => b.ProductID == item2.ProductID).ToList();
                        ProductList2.AddRange(ProductList);
                    }
                }
            }
            FillProductComboBox(ProductList2);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Import> ImportList = context.Import.ToList();
            foreach (var item in ImportList)
            {
                if (item.ProductID == (comboBox2.SelectedItem as Product).ProductID)
                {
                    textBox1.Text = item.ImportPrice.ToString();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<ImportDetail> ImportDetailList = context.ImportDetail.ToList();
            ImportDetail NewReq = new ImportDetail();
            NewReq.RequestNumber = ImportDetailList.Count + 1;
            NewReq.ImportSourceID = (comboBox1.SelectedItem as ImportSource).ImportSourceID;
            NewReq.ProductID = (comboBox2.SelectedItem as Product).ProductID;
            NewReq.ImportAmount = int.Parse(textBox2.Text);
            NewReq.Total = int.Parse(textBox1.Text) * int.Parse(textBox2.Text);
            NewReq.ImportDate = DateTime.Now;
            context.ImportDetail.Add(NewReq);
            context.SaveChanges();
            LoadList();
            LoadForm();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int SoPhieuYC = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            List<ImportDetail> ImportDetailList = context.ImportDetail.ToList();
            ImportDetail UpdateReq = context.ImportDetail.FirstOrDefault(p => p.RequestNumber == SoPhieuYC);
            UpdateReq.ImportSourceID = (comboBox1.SelectedItem as ImportSource).ImportSourceID;
            UpdateReq.ProductID = (comboBox2.SelectedItem as Product).ProductID;
            UpdateReq.ImportAmount = int.Parse(textBox2.Text);
            UpdateReq.Total = int.Parse(textBox1.Text) * int.Parse(textBox2.Text);
            UpdateReq.ImportDate = DateTime.Now;
            context.SaveChanges();
            LoadList();
            LoadForm();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            comboBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int SoPhieuYC = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            ImportDetail CancelReq = context.ImportDetail.FirstOrDefault(p => p.RequestNumber == SoPhieuYC);
            context.ImportDetail.Remove(CancelReq);
            context.SaveChanges();
            LoadList();
            LoadForm();
        }
        private bool IsNumber(string Value)
        {
            int Result;
            if (int.TryParse(Value, out Result) == false) //Nếu là chữ trả về false
            {
                return false;
            }
            return true; //Nếu là số trả về true
        }
        private void button4_Click(object sender, EventArgs e)
        {
            int A, B;
            if (IsNumber(textBox3.Text) == true) //Nếu trong textbox tìm kiếm là số thì tìm theo số phiếu
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    A = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
                    B = int.Parse(textBox3.Text);
                    if (A != B)
                    {
                        dataGridView1.Rows[i].Visible = false; //Ẩn các dòng không trùng số phiếu
                    }
                    else
                    {
                        dataGridView1.Rows[i].Visible = true; //Hiện thị dòng trùng số phiếu
                    }
                }
            }
            else //Nếu trong textbox tìm kiếm là chữ thì tìm theo tên
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells[1].Value.ToString() != textBox3.Text)
                    {
                        dataGridView1.Rows[i].Visible = false; //Ẩn các dòng không trùng nhà cung cấp
                    }
                    else
                    {
                        dataGridView1.Rows[i].Visible = true; //Hiện thị các dòng trùng nhà cung cấp
                    }
                }
            }
        }
    }
}
