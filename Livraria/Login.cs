using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Livraria
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        public static string usuario;
        public static string codUsuario;

        //conectando com o banco de dados
        SqlConnection cn = new SqlConnection(@"Data source=TERCIUS\SQLEXPRESS;integrated security=SSPI;initial Catalog=db_livraria");
        SqlCommand cm = new SqlCommand();
        //SqlDataReader dt;
        

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSenha_MouseDown(object sender, MouseEventArgs e)
        {
            txtSenha.UseSystemPasswordChar = false;
        }

        private void btnSenha_MouseUp(object sender, MouseEventArgs e)
        {
            txtSenha.UseSystemPasswordChar = true;
        }

        private void btnAcessar_Click(object sender, EventArgs e)
        {
            if (txtLogin.Text == "" || txtSenha.Text == "")
            {
                MessageBox.Show("Obrigatorio preencher os campos Login e Senha!", "Atenção !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else 
            {
                try
                {
                    cn.Open();
                    cm.CommandText = "select * from tbl_atendente where ds_Login =('" + txtLogin.Text + "') and ds_Senha =('" + txtSenha.Text + "')";
                    cm.Connection = cn;
                    SqlDataAdapter da = new SqlDataAdapter(cm);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        usuario = dt.Rows[0]["ds_Login"].ToString();
                        codUsuario = dt.Rows[0]["cd_atendente"].ToString();
                        frmMenu menu = new frmMenu();
                        menu.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Usuário ou Senha Invalidos", "Ocorreu um erro !!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtLogin.Clear();
                        txtSenha.Clear();
                        txtLogin.Focus();
                    }
                }
                catch (Exception erro)
                {
                    MessageBox.Show(erro.Message);
                    cn.Close();
                }
                finally 
                {
                    cn.Close();
                }
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
