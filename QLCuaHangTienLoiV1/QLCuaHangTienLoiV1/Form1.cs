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
    public partial class Form1 : Form
    {
        Model1 context = new Model1();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int KQ = 0; // Kiểm tra
            List<Account> AccountList = context.Account.ToList();
            foreach(var item in AccountList)
            {
                if(item.Email == textBox1.Text && item.Password == textBox2.Text)
                {
                    KQ = 1;
                }
            }
            if(KQ == 0)
            {
                MessageBox.Show(" Email hoặc Password chưa chính xác ");
            }
            if(KQ == 1)
            {
                MessageBox.Show(" Đăng nhập thành công ");
                Form2 form2 = new Form2(textBox1.Text);
                this.Hide();
                form2.ShowDialog();
                this.Show();
            }
        }
    }
}
