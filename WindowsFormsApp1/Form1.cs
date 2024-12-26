using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
                
        }
        public List<BenhNhan> GetAll()
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:44395/api/")
            };
            var response = client.GetAsync("BenhNhan").Result;
            return response.IsSuccessStatusCode ? response.Content.ReadAsAsync<List<BenhNhan>>().Result : null;
        }
        public void Display()
        {
            dgvBenhNhan.DataSource = GetAll();
        }
        public BenhNhan GetBenhNhan()
        {
            return new BenhNhan() { MaBN = txtMa.Text, HoTen = txtTen.Text, GioiTinh = txtGioiTinh.Text, NoiSinh = txtNoiSinh.Text, VienPhi=float.Parse(txtVienPhi.Text) };
        }
        public bool Edit(BenhNhan x)
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:44395/api/")
            };
            var response = client.PutAsJsonAsync("BenhNhan", x).Result;
            return response.IsSuccessStatusCode;

        }
        public bool Delete(string id)
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:44395/api/")
            };
            var response = client.DeleteAsync("BenhNhan?id=" + id).Result;
            return response.IsSuccessStatusCode;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Display();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!Edit(GetBenhNhan()))
            {
                MessageBox.Show("Không sửa được bn");
                return;
            }
            Display();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }
            if (!Delete(txtMa.Text))
            {
                MessageBox.Show("Không xóa được bn");
                return;
            }
            Display();
        }
    }
}
