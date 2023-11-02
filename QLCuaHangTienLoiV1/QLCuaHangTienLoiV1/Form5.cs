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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace QLCuaHangTienLoiV1
{
    public partial class Form5 : Form
    {
        private string UserName;
        Model1 context = new Model1();
        public Form5()
        {
            InitializeComponent();
        }
        public Form5(string Name)
        {
            InitializeComponent();
            UserName = Name;
        }
        private void FillProductComboBox(List<Product> ProductList)
        {
            this.comboBox1.DataSource = ProductList;
            this.comboBox1.DisplayMember = "ProductName";
            this.comboBox1.ValueMember = "ProductID";
        }
        private void FillEmployeeComboBox(List<Employee> EmployeeList)
        {
            this.comboBox2.DataSource = EmployeeList;
            this.comboBox2.DisplayMember = "EmployeeName";
            this.comboBox2.ValueMember = "EmployeeID";
        }
        private void BindGrid(List<InvoiceDetail> InvoiceDetailList)
        {
            textBox1.Text = "HD001";
            textBox3.Text = "1";
            dataGridView1.Rows.Clear();
            foreach (var Detail in InvoiceDetailList)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = "HD" + Detail.InvoiceID.ToString("D3");
                dataGridView1.Rows[index].Cells[1].Value = Detail.Product.ProductName;
                dataGridView1.Rows[index].Cells[2].Value = Detail.Product.SellPrice;
                dataGridView1.Rows[index].Cells[3].Value = Detail.Amount;
                dataGridView1.Rows[index].Cells[4].Value = (Detail.Product.SellPrice) * Detail.Amount;
                dataGridView1.Rows[index].Cells[5].Value = Detail.Invoice.InvoiceDate;
                dataGridView1.Rows[index].Cells[6].Value = Detail.Invoice.Employee.EmployeeName;
                textBox1.Text = "HD" + (Detail.InvoiceID + 1).ToString("D3");
            }
        }
        public void LoadList()
        {
            List<InvoiceDetail> LoadInvoiceDetailList = context.InvoiceDetail.ToList();
            BindGrid(LoadInvoiceDetailList);
        }
        private void LoadForm()
        {
            textBox3.Text = "1";
            comboBox2.Text = UserName;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button6.Enabled = true;
            button7.Enabled = false;
        }
        private void Form5_Load(object sender, EventArgs e)
        {
            List<Product> ProductList = context.Product.ToList();
            List<Employee> EmployeeList = context.Employee.ToList();
            List<InvoiceDetail> InvoiceDetailList = context.InvoiceDetail.ToList();
            List<Invoice> InvoiceList = context.Invoice.ToList();
            FillProductComboBox(ProductList);
            FillEmployeeComboBox(EmployeeList);
            comboBox2.Text = UserName;
            BindGrid(InvoiceDetailList);
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button7.Enabled = false;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Product> ProductList = context.Product.ToList();
            foreach (var item in ProductList)
            {
                if (item.ProductName == comboBox1.Text)
                {
                    textBox2.Text = item.SellPrice.ToString();
                }
            }
        }
        private void ThemThongTinHoaDon(List<InvoiceDetail> InvoiceDetailList)
        {
            InvoiceDetail NewInvoiceDetail = new InvoiceDetail();
            NewInvoiceDetail.InvoiceID = int.Parse(textBox1.Text.Substring(2));
            NewInvoiceDetail.ProductID = (comboBox1.SelectedItem as Product).ProductID;
            NewInvoiceDetail.Amount = int.Parse(textBox3.Text);
            NewInvoiceDetail.Total = int.Parse(textBox2.Text) * int.Parse(textBox3.Text);
            context.InvoiceDetail.Add(NewInvoiceDetail);
            context.SaveChanges();
        }
        //Thêm Hóa Đơn
        private void button6_Click(object sender, EventArgs e)
        {
            List<Invoice> InvoiceList = context.Invoice.ToList();
            List<InvoiceDetail> InvoiceDetailList = context.InvoiceDetail.ToList();
            Invoice NewInvoice = new Invoice();
            NewInvoice.InvoiceID = int.Parse(textBox1.Text.Substring(2));
            NewInvoice.EmployeeID = (comboBox2.SelectedItem as Employee).EmployeeID;
            NewInvoice.InvoiceDate = DateTime.Now;
            context.Invoice.Add(NewInvoice);
            context.SaveChanges();
            ThemThongTinHoaDon(InvoiceDetailList);
            LoadList();
            LoadForm();
        }
        //Thêm dữ liệu vào HĐ
        private void button1_Click(object sender, EventArgs e)
        {
            List<InvoiceDetail> InvoiceDetailList = context.InvoiceDetail.ToList();
            ThemThongTinHoaDon(InvoiceDetailList);
            LoadList();
            LoadForm();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button6.Enabled = false;
            button7.Enabled = true;
        }
        private void XoaHoaDon(List<Invoice> InvoiceList)
        {
            int MaHD = int.Parse(textBox1.Text.Substring(2));
            int MaSP = (comboBox1.SelectedItem as Product).ProductID;
            Invoice DeleteInvoice = context.Invoice.FirstOrDefault(p => p.InvoiceID == MaHD);
            context.Invoice.Remove(DeleteInvoice);
            context.SaveChanges();
        }
        private void DataCheck(int MaHD)
        {
            List<InvoiceDetail> InvoiceDetailList = context.InvoiceDetail.Where(p => p.InvoiceID == MaHD).ToList();
            if(InvoiceDetailList.Count == 0)
            {
                List<Invoice> InvoiceList = context.Invoice.ToList();
                XoaHoaDon(InvoiceList);
            }
        }
        //Xóa dữ liệu trong HĐ
        private void button3_Click(object sender, EventArgs e)
        {
            List<InvoiceDetail> InvoiceDetailList = context.InvoiceDetail.ToList();
            int MaHD = int.Parse(textBox1.Text.Substring(2));
            int MaSP = (comboBox1.SelectedItem as Product).ProductID;
            InvoiceDetail DeleteInvoiceData = context.InvoiceDetail.FirstOrDefault(p => p.InvoiceID == MaHD && p.ProductID == MaSP);
            context.InvoiceDetail.Remove(DeleteInvoiceData);
            context.SaveChanges();
            DataCheck(MaHD);
            LoadList();
            LoadForm();
        }
        //Xóa Hóa Đơn
        private void button7_Click(object sender, EventArgs e)
        {
            List<Invoice> InvoiceList = context.Invoice.ToList();
            List<InvoiceDetail> InvoiceDetailList = context.InvoiceDetail.ToList();
            int MaHD = int.Parse(textBox1.Text.Substring(2));
            InvoiceDetail DeleteInvoiceData = context.InvoiceDetail.FirstOrDefault(p => p.InvoiceID == MaHD);
            context.InvoiceDetail.Remove(DeleteInvoiceData);
            context.SaveChanges();
            XoaHoaDon(InvoiceList);
            LoadList();
            LoadForm();
        }
        //Sửa Hóa Đơn
        private void button2_Click(object sender, EventArgs e)
        {
            List<InvoiceDetail> InvoiceDetailList = context.InvoiceDetail.ToList();
            int MaHD = int.Parse(textBox1.Text.Substring(2));
            int MaSP = (comboBox1.SelectedItem as Product).ProductID;
            InvoiceDetail UpdateInvoiceDetail = context.InvoiceDetail.FirstOrDefault(p => p.InvoiceID == MaHD && p.ProductID == MaSP);
            UpdateInvoiceDetail.ProductID = (comboBox1.SelectedItem as Product).ProductID;
            UpdateInvoiceDetail.Amount = int.Parse(textBox3.Text);
            UpdateInvoiceDetail.Total = int.Parse(textBox2.Text) * int.Parse(textBox3.Text);
            context.SaveChanges();
            LoadList();
            LoadForm();
        }
        //Reload
        private void button5_Click(object sender, EventArgs e)
        {
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
        //Tìm kiếm Mã Hóa đơn
        private void button4_Click(object sender, EventArgs e)
        {
            int A, B;
            if (IsNumber(textBox4.Text) == true)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {

                    A = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
                    B = int.Parse(textBox4.Text);
                    if (A != B)
                    {
                        dataGridView1.Rows[i].Visible = false;
                    }
                    else
                    {
                        dataGridView1.Rows[i].Visible = true;
                    }
                }
            }
            else
            {
                MessageBox.Show(" Vui lòng nhập số ");
            }
        }
    }
}
