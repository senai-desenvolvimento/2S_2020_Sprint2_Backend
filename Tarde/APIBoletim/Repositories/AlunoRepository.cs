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

        // Chamamos a classe de conexao do banco
        BoletimContext conexao = new BoletimContext();

        // Chamamos o objeto que poderá receber e executar os comandos do banco
        SqlCommand cmd = new SqlCommand();

        public Aluno Alterar(Aluno a)
        {
            throw new NotImplementedException();
        }

        public Aluno BuscarPorId(int id)
        {
            cmd.Connection = conexao.Conectar();

            cmd.CommandText = "SELECT * FROM Aluno WHERE IdAluno = @id ";

            // Atribuímos as variáveis que vem como argumento
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader dados = cmd.ExecuteReader();

            Aluno a = new Aluno();

            while(dados.Read())
            {
                a.IdAluno   = Convert.ToInt32(dados.GetValue(0));
                a.Nome      = dados.GetValue(1).ToString();
                a.RA        = dados.GetValue(2).ToString();
                a.Idade     = Convert.ToInt32(dados.GetValue(3));
            }

            conexao.Desconectar();

            return a;
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

            // DML -> ExecuteNonQuery

            return a;
        }

        public Aluno Excluir(Aluno a)
        {
            throw new NotImplementedException();
        }

        public List<Aluno> LerTodos()
        {
            // Abrir conexao
            cmd.Connection = conexao.Conectar();

            // Preparar a query (consulta)
            cmd.CommandText = "SELECT * FROM Aluno";

            SqlDataReader dados = cmd.ExecuteReader();

            // Criamos a lista pra guardar os alunos
            List<Aluno> alunos = new List<Aluno>();

            while(dados.Read())
            {
                alunos.Add(
                    new Aluno()
                    {
                        IdAluno     = Convert.ToInt32(dados.GetValue(0)),
                        Nome        = dados.GetValue(1).ToString(),
                        RA          = dados.GetValue(2).ToString(),
                        Idade       = Convert.ToInt32(dados.GetValue(3))
                    }
                ); 
            }

            // Fechar conexao
            conexao.Desconectar();

            return alunos;
        }
    }
}
