using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Livraria
{
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
            principal1.BringToFront();
        }

        private void btnFunc_Click(object sender, EventArgs e)
        {
            frmFuncionario1.BringToFront();
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {
            lblusuarioLogado.Text = Login.usuario;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Login telaLogin = new Login();
            telaLogin.Show();
            this.Hide();
        }

        private void pneMenu_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
