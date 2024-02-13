﻿using eCommerce.API.Models;
using eCommerce.API.Repository;
using System.Data;
using System.Data.SqlClient;

namespace eCommerce.API.Repositories
{
    public class UsuarioProcedureRepository : IUsuarioRepository
    {
        private IDbConnection _connection;

        public UsuarioProcedureRepository()
        {
            _connection = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=eCommerce;Trusted_Connection=True;");
        }

        public List<Usuario> Get()
        {
            List<Usuario> usuarios = new List<Usuario>();

            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = (SqlConnection)_connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SelecionarUsuarios";

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
                command.Connection = (SqlConnection)_connection;

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SelecionarUsuario";
                command.Parameters.AddWithValue("@id", id);

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
            _connection.Open();

            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = (SqlConnection)_connection;

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "CadastrarUsuario";

                command.Parameters.AddWithValue("@nome", usuario.Nome);
                command.Parameters.AddWithValue("@email", usuario.Email);
                command.Parameters.AddWithValue("@sexo", usuario.Sexo);
                command.Parameters.AddWithValue("@rg", usuario.RG);
                command.Parameters.AddWithValue("@cpf", usuario.CPF);
                command.Parameters.AddWithValue("@nomeMae", usuario.NomeMae);
                command.Parameters.AddWithValue("@situacaoCadastro", usuario.SituacaoCadastro);
                command.Parameters.AddWithValue("@dataCadastro", usuario.DataCadastro);
                usuario.Id = (int)command.ExecuteScalar();
            }
            finally
            {
                _connection.Close();
            }
        }

        public void Update(Usuario usuario)
        {
            _connection.Open();

            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = (SqlConnection)_connection;

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "AtualizarUsuario";

                command.Parameters.AddWithValue("@id", usuario.Id);
                command.Parameters.AddWithValue("@nome", usuario.Nome);
                command.Parameters.AddWithValue("@email", usuario.Email);
                command.Parameters.AddWithValue("@sexo", usuario.Sexo);
                command.Parameters.AddWithValue("@rg", usuario.RG);
                command.Parameters.AddWithValue("@cpf", usuario.CPF);
                command.Parameters.AddWithValue("@nomeMae", usuario.NomeMae);
                command.Parameters.AddWithValue("@situacaoCadastro", usuario.SituacaoCadastro);
                command.Parameters.AddWithValue("@dataCadastro", usuario.DataCadastro);

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
                command.Connection = (SqlConnection)_connection;

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "DeletarUsuario";

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
