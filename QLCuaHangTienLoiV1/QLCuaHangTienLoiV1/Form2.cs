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
    public partial class Form2 : Form
    {
        private string MyEmail;
        Model1 context = new Model1();
        public Form2()
        {
            InitializeComponent();
        }
        public Form2(string Email)
        {
            InitializeComponent();
            MyEmail = Email;

        }
        private void AccessPermisson()
        {
            int Accessable = 0;
            List<Role> RoleList = context.Role.ToList();
            foreach (var item in RoleList)
            {
                if(item.RoleName == label6.Text)
                {
                    Accessable = item.RoleID;
                }
            }
            if(Accessable == 2)
            {
                button1.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
            }
            if(Accessable == 3)
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button6.Enabled = false;
            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            List<Employee> EmployeeList = context.Employee.ToList();
            foreach (var item in EmployeeList)
            {
                if (item.Email == MyEmail)
                {
                    label4.Text = item.EmployeeName;
                    label6.Text = item.Role.RoleName;
                }
            }
            AccessPermisson();
        }
        //Quản lý nhân viên
        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            this.Hide();
            form3.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            this.Hide();
            form4.ShowDialog();
            this.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5(label4.Text);
            this.Hide();
            form5.ShowDialog(); 
            this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            this.Hide();
            form6.ShowDialog();
            this.Show();
        }
        bool Closing = false;
        private void button7_Click(object sender, EventArgs e)
        {
            Closing = true;
            this.Close();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(Closing == false)
            {
                Application.Exit();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7();
            this.Hide();
            form7.ShowDialog();
            this.Show();
        }
    }
}
