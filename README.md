# Livraria

# 📚 Livraria ADO.NET - Projeto Final de Banco de Dados  

**Um sistema de gerenciamento de livraria usando C# e ADO.NET para conexão com SQL Server.**  

## 🎯 Objetivo  
Projeto desenvolvido como **trabalho final da disciplina Banco de Dados**, com o intuito de aplicar conceitos de:  
✔ **CRUD** (Create, Read, Update, Delete)  
✔ **Acesso a dados com ADO.NET**  
✔ **Modelagem e consultas SQL**  
✔ **Interface gráfica (Windows Forms)**  

## 🛠 Tecnologias Utilizadas  
- **C#** (.NET Framework)  
- **ADO.NET** (SqlConnection, SqlCommand, SqlDataReader)  
- **SQL Server** (Banco de dados relacional)  
- **Windows Forms** (Interface de usuário)  

## 📋 Funcionalidades  
- **Cadastro de Livros** (Título, Autor, Gênero, Preço)  
- **Cadastro de Gêneros**  
- **Consulta e Filtro de Livros**  
- **Edição e Exclusão de Registros**  

## 🚀 Como Executar o Projeto  
1. **Pré-requisitos**:  
   - Visual Studio (2019 ou superior)  
   - SQL Server (LocalDB ou Express)  

2. **Configurar o Banco de Dados**:  
   - Execute o script SQL disponível em `Scripts/CriacaoBanco.sql` para criar as tabelas.  
   - Atualize a **connection string** no `App.config`:  
     ```xml
     <connectionStrings>
         <add name="LivrariaConnection" connectionString="Server=.;Database=LivrariaDB;Integrated Security=True;" />
     </connectionStrings>
     ```

3. **Executar o Projeto**:  
   - Abra a solução no Visual Studio (`Livraria.sln`).  
   - Compile e execute o projeto (F5).  

## 📝 Observações  
- Este projeto foi desenvolvido para fins **educacionais**, como parte da avaliação da disciplina **Banco de Dados**.  
- Melhorias futuras podem incluir: autenticação de usuários, relatórios e migração para Entity Framework.  

## 👨‍💻 Autor  
[João Antônio Pereira de Araújo Tercius] - [GitHub](https://github.com/Terrcius)  

---  
**📌 Nota do Autor**:  
*Agradeço ao professor [Eduardo Chagas] e à turma pelo aprendizado durante o semestre!*  
