using APIBoletim.Context;
using APIBoletim.Domains;
using APIBoletim.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace APIBoletim.Repositories
{
    public class AlunoRepository : IAluno
    {

        // 1 - Chamamos nossa classe de conexao
        BoletimContext conexao = new BoletimContext();

        // 2 - Chamamos nosso objeto que dará os comandos SQL
        SqlCommand cmd = new SqlCommand();

        public List<Aluno> LerTodos()
        {
            // 3 -  Conecto com o banco
            cmd.Connection = conexao.Conectar();

            // 4 - Preparo minha Query 
            cmd.CommandText = "SELECT * FROM Aluno";

            // 5 - Executo o comando para ler
            SqlDataReader dados = cmd.ExecuteReader();

            // 6 - Crio uma lista para exibir meus cadastros
            List<Aluno> alunos = new List<Aluno>();

            // 7 - Jogamos os dados lidos no banco na lista
            while (dados.Read())
            {
                alunos.Add(
                    new Aluno()
                    {
                        IdAluno = Convert.ToInt32(dados.GetValue(0)),
                        Nome = dados.GetValue(1).ToString(),
                        RA = dados.GetValue(2).ToString(),
                        Idade = Int32.Parse(dados.GetValue(3).ToString())
                    }
                );
            }

            // 8 - Desconectar
            conexao.Desconectar();

            return alunos;
        }

        public Aluno Alterar(Aluno a)
        {
            throw new NotImplementedException();
        }

        public Aluno BuscarPorID(int id)
        {
            // 3 -  Conecto com o banco
            cmd.Connection = conexao.Conectar();

            // 4 - Preparo minha Query 
            cmd.CommandText = "SELECT * FROM Aluno WHERE IdAluno = @id";
            cmd.Parameters.AddWithValue("@id", id);

            // 5 - Executo o comando para ler
            SqlDataReader dados = cmd.ExecuteReader();

            // 6 - Crio uma lista para exibir meus cadastros
            Aluno aluno = new Aluno();

            while(dados.Read())
            {
                // 7 - Jogamos os dados lidos no banco no objeto Aluno
                aluno.IdAluno = Convert.ToInt32(dados.GetValue(0));
                aluno.Nome = dados.GetValue(1).ToString();
                aluno.RA = dados.GetValue(2).ToString();
                aluno.Idade = Int32.Parse(dados.GetValue(3).ToString());            
            }

            // 8 - Desconectar
            conexao.Desconectar();

            return aluno;
        }

        public Aluno Cadastrar(Aluno a)
        {
            cmd.Connection = conexao.Conectar();

            cmd.CommandText = 
                "INSERT INTO Aluno (Nome, RA, Idade) " +
                "VALUES" +
                "(@nome, @ra, @idade)";
            cmd.Parameters.AddWithValue("@nome", a.Nome);
            cmd.Parameters.AddWithValue("@ra", a.RA);
            cmd.Parameters.AddWithValue("@idade", a.Idade);

            // Será este comando o responsável por injetar os dados no banco efetivamente
            cmd.ExecuteNonQuery();

            return a;
        }

        public Aluno Excluir(Aluno a)
        {
            throw new NotImplementedException();
        }

    }
}
