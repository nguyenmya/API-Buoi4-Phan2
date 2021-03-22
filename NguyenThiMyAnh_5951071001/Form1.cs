using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NguyenThiMyAnh_5951071001
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-G86DS4D\SQLEXPRESS;Initial Catalog=DemoCRUD;Integrated Security=True");
        private void Form1_Load(object sender, EventArgs e)
        {
            GetStudentsRecord();
        }
        private void GetStudentsRecord()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM StudentsTb", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dgvStudents.DataSource = dt;
        }

        private bool IsValidData()
        {
            if (HoSinhVien.Text == string.Empty
                || TenSinhVien.Text == string.Empty
                || DiaChi.Text == string.Empty
                || string.IsNullOrEmpty(SoDienThoai.Text)
                || string.IsNullOrEmpty(SoBaoDanh.Text))
            {
                MessageBox.Show("Có lỗi chưa nhập dữ liệu!!!", "Lỗi dữ liệu ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO StudentsTb VALUES(@Name, @FatherName, @RollNumber, @Address, @Mobile)", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Name", HoSinhVien.Text);
            cmd.Parameters.AddWithValue("@FatherName", TenSinhVien.Text);
            cmd.Parameters.AddWithValue("@RollNumber", SoBaoDanh.Text);
            cmd.Parameters.AddWithValue("@Address", DiaChi.Text);
            cmd.Parameters.AddWithValue("@Mobile", SoDienThoai.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            GetStudentsRecord();
        }
        public int StudentID;
        private void dgvStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            StudentID = Convert.ToInt32(dgvStudents.SelectedRows[0].Cells[5].Value);
            HoSinhVien.Text = dgvStudents.SelectedRows[0].Cells[0].Value.ToString();
            TenSinhVien.Text = dgvStudents.SelectedRows[0].Cells[1].Value.ToString();
            SoBaoDanh.Text = dgvStudents.SelectedRows[0].Cells[2].Value.ToString();
            DiaChi.Text = dgvStudents.SelectedRows[0].Cells[3].Value.ToString();
            SoDienThoai.Text = dgvStudents.SelectedRows[0].Cells[4].Value.ToString();
        }
        private void ResetData()
        {
            StudentID = 0;
            HoSinhVien.Text = "";
            TenSinhVien.Text = "";
            SoBaoDanh.Text = "";
            DiaChi.Text = "";
            SoDienThoai.Text = "";
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (StudentID > 0)
            {
                SqlCommand cmd = new SqlCommand("UPDATE StudentsTb SET Name = @Name, FatherName = @FatherName, RollNumber = @RollNumber, Address = @Address, Mobile = @Mobile where StudentID = @ID", con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", HoSinhVien.Text);
                cmd.Parameters.AddWithValue("@FatherName", TenSinhVien.Text);
                cmd.Parameters.AddWithValue("@RollNumber", SoBaoDanh.Text);
                cmd.Parameters.AddWithValue("@Address", DiaChi.Text);
                cmd.Parameters.AddWithValue("@Mobile", SoDienThoai.Text);
                cmd.Parameters.AddWithValue("@ID", StudentID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                GetStudentsRecord();
                ResetData();
            }
            else
            {
                MessageBox.Show("Cập nhật bị lỗi !!!", "Lỗi !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (StudentID > 0)
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM StudentsTb WHERE StudentID = @ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", StudentID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentsRecord();
                ResetData();
            }
            else
            {
                MessageBox.Show("Xoá bị lỗi !!!", "Lỗi !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnXacLap_Click(object sender, EventArgs e)
        {

        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
