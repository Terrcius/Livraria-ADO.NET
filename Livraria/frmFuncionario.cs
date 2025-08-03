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
    public partial class frmFuncionario : UserControl
    {
        public frmFuncionario()
        {
            InitializeComponent();
        }

        //conectando com o banco de dados
        SqlConnection cn = new SqlConnection(@"Data source=TERCIUS\SQLEXPRESS;integrated security=SSPI;initial Catalog=db_livraria");

        private void desabilitaCampos()
        {
            lbl_Cod2.Visible = false;
            lbl_Cod.Visible = false;
            txtNome.Enabled = false;
            btnNovo.Enabled = true;
            txtLogin.Enabled = false;
            txtSenha.Enabled = false;
            btnGravar.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnCancelar.Enabled = false;
        }

        private void habilitaCampos()
        {
            btnNovo.Enabled = false;
            btnGravar.Enabled = true;
            txtBusca.Enabled = true;
            txtBusca.Text = "";
            txtLogin.Enabled = true;
            txtSenha.Enabled = true;
            btnCancelar.Enabled = true;
            txtNome.Enabled = true;
            txtNome.Focus();
            dgvFunc.DataSource = null;
        }

        private void limparCampos()
        {
            txtNome.Text = string.Empty;
            txtLogin.Text = string.Empty;
            txtSenha.Text = string.Empty;
            txtNome.Focus();
            txtBusca.Clear();
            dgvFunc.DataSource = null;
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text == "") 
            {
                MessageBox.Show("Obrigatório o preenchimento do campo Nome", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNome.Focus();
            }
            else if(txtLogin.Text == "")
            {
                MessageBox.Show("Obrigatório o preenchimento do campo Login", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLogin.Focus();
            }
            else if(txtSenha.Text == "")
            {
                MessageBox.Show("Obrigatório o preenchimento do campo Senha", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSenha.Focus();
            }
            else if (txtSenha.Text.Length < 8)
            {
                MessageBox.Show("O campo Senha deve ter no minimo 8 digitos", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSenha.Focus();
            }
            else
            {
                using (SqlCommand cm = new SqlCommand("sp_inserir_atendente", cn))
                {
                    try
                    {
                        cn.Open();
                       
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.Parameters.AddWithValue("@login", txtLogin.Text);
                        cm.Parameters.AddWithValue("@senha", txtSenha.Text);
                        cm.Parameters.AddWithValue("@nome", txtNome.Text);

                        cm.ExecuteNonQuery();
                        MessageBox.Show("Atendente cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        limparCampos();
                        
                    }
                    catch (SqlException ex) when (ex.Number == 50001) // Erro de login duplicado
                    {
                        MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtLogin.Focus();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao cadastrar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        cn.Close();
                    }
                }
            }
        }

        private void frmFuncionario_Load(object sender, EventArgs e)
        {
            desabilitaCampos();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            habilitaCampos();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text == "")
            {
                MessageBox.Show("Obrigatório o preenchimento do campo Nome", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNome.Focus();
            }
            else if (txtLogin.Text == "")
            {
                MessageBox.Show("Obrigatório o preenchimento do campo Login", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLogin.Focus();
            }
            else if (txtSenha.Text == "")
            {
                MessageBox.Show("Obrigatório o preenchimento do campo Senha", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSenha.Focus();
            }
            else if (txtSenha.Text.Length < 8)
            {
                MessageBox.Show("O campo Senha deve ter no minimo 8 digitos", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSenha.Focus();
            }
            else
            {
                using (SqlCommand cm = new SqlCommand("sp_atualizar_atendente", cn))
                {
                    try
                    {
                        cn.Open();
                        cm.CommandText = "sp_atualizar_atendente";
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.Parameters.AddWithValue("@id", lbl_Cod.Text);
                        cm.Parameters.AddWithValue("@login", txtLogin.Text);
                        cm.Parameters.AddWithValue("@senha", txtSenha.Text);
                        cm.Parameters.AddWithValue("@nome", txtNome.Text);
                        cm.ExecuteNonQuery();
                        MessageBox.Show("Dados atualizados!");
                        limparCampos();


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro: " + ex.Message);
                    }
                    finally
                    {
                        cn.Close();
                    }
                }
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            // Cria uma nova conexão e comando para esta operação
            using (SqlConnection cn = new SqlConnection(@"Data source=TERCIUS\SQLEXPRESS;integrated security=SSPI;initial Catalog=db_livraria"))
            using (SqlCommand cm = new SqlCommand("sp_excluir_atendente", cn))
            {
                try
                {
                    cn.Open();
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@id", lbl_Cod.Text);

                    cm.ExecuteNonQuery();
                    MessageBox.Show("Atendente excluído!");
                    limparCampos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                }
                // O using fecha automaticamente a conexão, mesmo se ocorrer erro
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            desabilitaCampos();
            limparCampos();
        }

        private void manipularDados() 
        {
            lbl_Cod2.Visible = true;
            lbl_Cod.Visible = true;
            btnAlterar.Enabled = true;
            btnCancelar.Enabled = true;
            btnExcluir.Enabled = true;
            btnNovo.Enabled = false;
            btnGravar.Enabled = false;
            txtNome.Enabled = true;
            txtBusca.Enabled = true;
            txtLogin.Enabled = true;
            txtSenha.Enabled = true;
        }

        private void txtBusca_TextChanged(object sender, EventArgs e)
        {
            if (txtBusca.Text != "")
            {
                using (var cn = new SqlConnection(@"Data source=TERCIUS\SQLEXPRESS;integrated security=SSPI;initial Catalog=db_livraria"))
                {
                    try
                    {
                        cn.Open();

                        // Criação do comando dentro do using
                        using (var cmd = new SqlCommand("sp_buscar_atendente", cn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@nome", txtBusca.Text);

                            // DataAdapter e DataTable
                            using (var da = new SqlDataAdapter(cmd))
                            {
                                var dt = new DataTable();
                                da.Fill(dt);
                                dgvFunc.DataSource = dt;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro na busca: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    // A conexão será fechada automaticamente ao sair do bloco using
                }
            }
            else
            {
                dgvFunc.DataSource = null;
            }
        }


        private void carregaAtendente()
        {
            lbl_Cod.Text = dgvFunc.SelectedRows[0].Cells[0].Value.ToString();
            txtLogin.Text = dgvFunc.SelectedRows[0].Cells[1].Value.ToString();
            txtSenha.Text = dgvFunc.SelectedRows[0].Cells[2].Value.ToString();
            txtNome.Text = dgvFunc.SelectedRows[0].Cells[3].Value.ToString();
            manipularDados();
        }
        private void dgvFunc_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            manipularDados();
            carregaAtendente();
        }

        private void btnHistoricoExclusoes_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(@"Data source=TERCIUS\SQLEXPRESS;integrated security=SSPI;initial Catalog=db_livraria"))
            {
                try
                {
                    cn.Open();

                    string query = @"
                SELECT 
                    A.nm_atendente AS 'Atendente',
                    L.dt_exclusao AS 'Data Exclusão',
                    L.nm_usuario AS 'Excluído por'
                    FROM tbl_log_exclusoes L
                    INNER JOIN tbl_atendente A ON L.id_atendente = A.cd_atendente
                    ORDER BY L.dt_exclusao DESC";

                    SqlDataAdapter da = new SqlDataAdapter(query, cn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Cria um novo formulário para exibir os dados
                    Form formHistorico = new Form();
                    formHistorico.Text = "Histórico de Exclusões";
                    formHistorico.Size = new Size(500, 400);

                    DataGridView dgv = new DataGridView();
                    dgv.Dock = DockStyle.Fill;
                    dgv.DataSource = dt;

                    formHistorico.Controls.Add(dgv);
                    formHistorico.ShowDialog();

                    DateTimePicker dtpInicio = new DateTimePicker();
                    DateTimePicker dtpFim = new DateTimePicker();
                    Button btnFiltrar = new Button() { Text = "Filtrar" };

                    btnFiltrar.Click += (s, ev) => {
                        // Atualiza a query com WHERE dt_exclusao BETWEEN @inicio AND @fim
                    };
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar histórico: " + ex.Message,
                                  "Erro",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Error);
                }
            }
        }

        private void btnGerarJson_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(@"Data source=TERCIUS\SQLEXPRESS;integrated security=SSPI;initial Catalog=db_livraria"))
                {
                    cn.Open();

                    // Chamando a procedure que já existe no banco
                    using (SqlCommand cmd = new SqlCommand("sp_buscar_atendente_json", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Executa e obtém o JSON
                        string json = (string)cmd.ExecuteScalar();

                        // Cria um novo formulário para exibir o JSON
                        Form formJson = new Form();
                        formJson.Text = "JSON de Atendentes";
                        formJson.Size = new Size(600, 500);
                        formJson.StartPosition = FormStartPosition.CenterScreen;

                        // TextBox para mostrar o JSON com scroll
                        TextBox txtJson = new TextBox();
                        txtJson.Multiline = true;
                        txtJson.ScrollBars = ScrollBars.Both;
                        txtJson.Dock = DockStyle.Fill;
                        txtJson.Text = json;
                        txtJson.Font = new Font("Consolas", 10); // Fonte monoespaçada para melhor visualização

                        // Botão para copiar
                        Button btnCopiar = new Button();
                        btnCopiar.Text = "Copiar JSON";
                        btnCopiar.Dock = DockStyle.Bottom;
                        btnCopiar.Click += (s, ev) => {
                            Clipboard.SetText(json);
                            MessageBox.Show("JSON copiado para a área de transferência!");
                        };

                        // Adiciona os controles ao formulário
                        formJson.Controls.Add(txtJson);
                        formJson.Controls.Add(btnCopiar);

                        formJson.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao gerar JSON: {ex.Message}", "Erro",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
