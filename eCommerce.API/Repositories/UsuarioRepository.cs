﻿using eCommerce.API.Models;
using eCommerce.API.Repository;
using System.Data;
using System.Data.SqlClient;

/*
 * Connection => Estabelecer conexão com banco.
 * Command => Insert, Update, Delete -> Comandos sem retorno ou com poucos retornos
 * DataReader => Arq. Conectada. Se mantém conectado no banco, não guarda informações na memória, impedindo outras consultas de serem executadas na mesma tabela.
 * DataAdapter => Arq. Desconectada. Baixa as informações para a memória e se desconecta do banco, permitindo outras consultas na mesma tabela.
 * **/

namespace eCommerce.API.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private IDbConnection _connection;

        public UsuarioRepository()
        {
            _connection = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=eCommerce;Trusted_Connection=True;");
        }

        public List<Usuario> Get()
        {
            List<Usuario> usuarios = new List<Usuario>();

            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Usuarios";
                command.Connection = (SqlConnection)_connection;

                _connection.Open();

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.Id = dataReader.GetInt32("Id");
                    usuario.Nome = dataReader.GetString("Nome");
                    usuario.Email = dataReader.GetString("Email");
                    usuario.Sexo = dataReader.GetString("Sexo");
                    usuario.RG = dataReader.GetString("RG");
                    usuario.CPF = dataReader.GetString("CPF");
                    usuario.NomeMae = dataReader.GetString("NomeMae");
                    usuario.SituacaoCadastro = dataReader.GetString("SituacaoCadastro");
                    usuario.DataCadastro = dataReader.GetDateTimeOffset(8);

                    usuarios.Add(usuario);
                }
            }
            finally { _connection.Close(); }


            return usuarios;
        }

        public Usuario Get(int id)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = $"SELECT * FROM Usuarios WHERE Id = @id";
                command.Parameters.AddWithValue("@id", id);
                command.Connection = (SqlConnection)_connection;

                _connection.Open();

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.Id = dataReader.GetInt32("Id");
                    usuario.Nome = dataReader.GetString("Nome");
                    usuario.Email = dataReader.GetString("Email");
                    usuario.Sexo = dataReader.GetString("Sexo");
                    usuario.RG = dataReader.GetString("RG");
                    usuario.CPF = dataReader.GetString("CPF");
                    usuario.NomeMae = dataReader.GetString("NomeMae");
                    usuario.SituacaoCadastro = dataReader.GetString("SituacaoCadastro");
                    usuario.DataCadastro = dataReader.GetDateTimeOffset(8);

                    return usuario;
                }
            }
            finally
            {
                _connection.Close();
            }

            return null;
        }

        public void Insert(Usuario usuario)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "INSERT INTO Usuarios(Nome, Email, Sexo, RG, CPF, NomeMae, SituacaoCadastro, DataCadastro) VALUES (@Nome, @Email, @Sexo, @RG, @CPF, @NomeMae, @SituacaoCadastro, @DataCadastro); SELECT CAST(scope_identity() AS int)";
                command.Connection = (SqlConnection)_connection;

                command.Parameters.AddWithValue("@Nome", usuario.Nome);
                command.Parameters.AddWithValue("@Email", usuario.Email);
                command.Parameters.AddWithValue("@Sexo", usuario.Sexo);
                command.Parameters.AddWithValue("@RG", usuario.RG);
                command.Parameters.AddWithValue("@CPF", usuario.CPF);
                command.Parameters.AddWithValue("@NomeMae", usuario.NomeMae);
                command.Parameters.AddWithValue("@SituacaoCadastro", usuario.SituacaoCadastro);
                command.Parameters.AddWithValue("@DataCadastro", usuario.DataCadastro);

                _connection.Open();

                usuario.Id = (int)command.ExecuteScalar();
            }
            finally
            {
                _connection.Close();
            }
        }

        public void Update(Usuario usuario)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "UPDATE Usuarios SET Nome = @Nome, Email = @Email, Sexo = @Sexo, RG = @RG, CPF = @CPF, NomeMae = @NomeMae, SituacaoCadastro = @SituacaoCadastro, DataCadastro = @DataCadastro WHERE Id = @id;";
                command.Connection = (SqlConnection)_connection;

                command.Parameters.AddWithValue("@Id", usuario.Id);
                command.Parameters.AddWithValue("@Nome", usuario.Nome);
                command.Parameters.AddWithValue("@Email", usuario.Email);
                command.Parameters.AddWithValue("@Sexo", usuario.Sexo);
                command.Parameters.AddWithValue("@RG", usuario.RG);
                command.Parameters.AddWithValue("@CPF", usuario.CPF);
                command.Parameters.AddWithValue("@NomeMae", usuario.NomeMae);
                command.Parameters.AddWithValue("@SituacaoCadastro", usuario.SituacaoCadastro);
                command.Parameters.AddWithValue("@DataCadastro", usuario.DataCadastro);

                _connection.Open();

                command.ExecuteNonQuery();
            } 
            finally
            {
                _connection.Close();
            }
        }

        public void Delete(int id)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "DELETE FROM Usuarios WHERE Id = @id";
                command.Connection = (SqlConnection)_connection;

                command.Parameters.AddWithValue("@id", id);

                _connection.Open();

                command.ExecuteNonQuery();
            }
            finally
            {
                _connection.Close();
            }
        }
    }
}
