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
    public partial class Form3 : Form
    {
        Model1 context = new Model1();
        public Form3()
        {
            InitializeComponent();
        }
        private void FillRoleComboBox(List<Role> RoleList)
        {
            this.comboBox1.DataSource = RoleList;
            this.comboBox1.DisplayMember = "RoleName";
            this.comboBox1.ValueMember = "RoleID";
        }
        private void BindGrid(List<Employee> EmployeeList)
        {
            dataGridView1.Rows.Clear();
            foreach (var Employee in EmployeeList)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = "NV" + Employee.EmployeeID.ToString("D3");
                dataGridView1.Rows[index].Cells[1].Value = Employee.EmployeeName;
                dataGridView1.Rows[index].Cells[2].Value = Employee.Email;
                dataGridView1.Rows[index].Cells[3].Value = Employee.Address;
                dataGridView1.Rows[index].Cells[4].Value = Employee.PhoneNumber;
                dataGridView1.Rows[index].Cells[5].Value = Employee.Role.RoleName;
                textBox1.Text = "NV" + (Employee.EmployeeID + 1).ToString("D3");
            }
        }
        public void LoadList()
        {
            List<Employee> LoadEmployeeList = context.Employee.ToList();
            BindGrid(LoadEmployeeList);
        }
        private void LoadForm()
        {
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            comboBox1.SelectedIndex = 0;
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            textBox1.Text = "NV001";
            List<Employee> EmployeeList = context.Employee.ToList();
            List<Role> RoleList = context.Role.ToList();
            FillRoleComboBox(RoleList);
            BindGrid(EmployeeList);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            List<Employee> EmployeeList = context.Employee.ToList();
            Employee NewEmployee = new Employee();
            NewEmployee.EmployeeID = int.Parse(textBox1.Text.Substring(2));
            NewEmployee.EmployeeName = textBox2.Text;
            NewEmployee.Email = textBox3.Text;
            NewEmployee.Address = textBox4.Text;
            NewEmployee.PhoneNumber = textBox5.Text;
            NewEmployee.RoleID = (comboBox1.SelectedItem as Role).RoleID;
            context.Employee.Add(NewEmployee);
            context.SaveChanges();
            LoadList();
            LoadForm();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int MaNV = int.Parse(textBox1.Text.Substring(2));
            Employee UpdateEmployee = context.Employee.FirstOrDefault(p => p.EmployeeID == MaNV);
            UpdateEmployee.EmployeeName = textBox2.Text;
            UpdateEmployee.Email = textBox3.Text;
            UpdateEmployee.Address = textBox4.Text;
            UpdateEmployee.PhoneNumber = textBox5.Text;
            UpdateEmployee.RoleID = (comboBox1.SelectedItem as Role).RoleID;
            context.SaveChanges();
            LoadList();
            LoadForm();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            int A, B;
            if (IsNumber(textBox6.Text) == true) //Nếu trong textbox tìm kiếm là số thì tìm theo mã
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    A = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString().Substring(2));
                    B = int.Parse(textBox6.Text);
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
                    if (dataGridView1.Rows[i].Cells[1].Value.ToString() != textBox6.Text)
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
        private void button4_Click(object sender, EventArgs e)
        {
            LoadList();
            LoadForm();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
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
    }
}
