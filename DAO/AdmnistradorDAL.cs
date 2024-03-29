﻿using Metadata;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DAL;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AdmnistradorDAL : IEntityCRUD<Administrador>
    {
        AdmnistradorDAL admDAL = new AdmnistradorDAL();

        public string Atualizar(Administrador admnistrador)
        {
            string stringConexao = StringConexao.GetStringConexao();
            SqlConnection connection = new SqlConnection(stringConexao);
            SqlCommand command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "UPDATE ADMINISTRADOR SET NOME= @NOME, RG= @RG, CPF= @CPF,ENDERECO= @ENDERECO, TELEFONE= @TELEFONE,EMAIL= @EMAIL,SENHA= @SENHA";
            command.Parameters.AddWithValue("@NOME", admnistrador.nome);
            command.Parameters.AddWithValue("@RG", admnistrador.rg);
            command.Parameters.AddWithValue("@CPF", admnistrador.cpf);
            command.Parameters.AddWithValue("@ENDERECO", admnistrador.endereco);
            command.Parameters.AddWithValue("@TELEFONE", admnistrador.telefone);
            command.Parameters.AddWithValue("@EMAIL", admnistrador.email);
            command.Parameters.AddWithValue("@SENHA", admnistrador.senha);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                return "erro de conexão com o banco";
            }
            finally
            {
                connection.Close();
            }
            return "dados atualizados com sucesso";
        }

        public string Excluir(Administrador admnistrador)
        {
            string stringConexao = StringConexao.GetStringConexao();
            SqlConnection connection = new SqlConnection(stringConexao);
            SqlCommand command = new SqlCommand();
            command.CommandText = "DELETE FROM USUARIOS WHERE ID= @ID";
            command.Parameters.AddWithValue("@ID", admnistrador.id);
            command.Connection = connection;

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                return "erro de conexão com o banco";
            }
            finally
            {
                connection.Close();
            }

            return "excluído com sucesso";
            
        }

        public string Inserir(Administrador admnistrador)
        {
            string stringConexao = StringConexao.GetStringConexao();
            SqlConnection connection = new SqlConnection(stringConexao);
            SqlCommand command = new SqlCommand();
            command.CommandText = "INSERT INTO USUARIOS (ID, NOME, RG, CPF, ENDERECO, TELEFONE, EMAIL, SENHA) VALUES (@ID, @NOME, @RG, @CPF, @ENDERECO, @TELEFONE, @EMAIL, @SENHA)";
            command.Parameters.AddWithValue("@NOME", admnistrador.nome);
            command.Parameters.AddWithValue("@RG", admnistrador.rg);
            command.Parameters.AddWithValue("@CPF", admnistrador.cpf);
            command.Parameters.AddWithValue("@ENDERECO", admnistrador.endereco);
            command.Parameters.AddWithValue("@TELEFONE", admnistrador.telefone);
            command.Parameters.AddWithValue("@EMAIL", admnistrador.email);

            command.Parameters.AddWithValue("@SENHA", admnistrador.senha);
            command.Connection = connection;

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                return "erro de conexão com o banco";
            }
            finally
            {
                connection.Close();
            }

            return "";
        }

        public Administrador LerPorID(int id)
        {
            string stringConexao = StringConexao.GetStringConexao();
            SqlConnection connection = new SqlConnection(stringConexao);
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM USUARIOS WHERE ID= @ID";
            command.Parameters.AddWithValue("@ID", id);
            command.Connection = connection;

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    
                    return instanciarAdmin(reader);

                }
                return null;

            }
            catch (SqlException e)
            {
            }
            finally
            {
                connection.Close();
            }
            return null;

        }

        private Administrador instanciarAdmin(SqlDataReader reader)
        {
            Administrador admin = new Administrador();
            admin.id = Convert.ToInt32(reader["ID"]);
            admin.nome = Convert.ToString(reader["NOME"]);
            admin.cpf = Convert.ToString(reader["CPF"]);
            admin.rg = Convert.ToString(reader["RG"]);
            admin.endereco = Convert.ToString(reader["ENDERECO"]);
            admin.telefone = Convert.ToString(reader["TELEFONE"]);
            admin.email = Convert.ToString(reader["EMAIL"]);
            admin.senha = Convert.ToString(reader["senha"]);
         

            return admin;
        }

        public List<Administrador> LerTodos()
        {
            string stringConexao = StringConexao.GetStringConexao();
            SqlConnection connection = new SqlConnection(stringConexao);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            List<Administrador> list = new List<Administrador>();
            command.CommandText = "select * from USUARIOS";
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (Convert.ToBoolean(reader["IS_ADMIN"]))
                    {
                        list.Add(instanciarAdmin(reader));
                    }
                }
                return list;


            }
            catch (SqlException e)
            {
            }
            finally
            {
                connection.Close();
                
            }
            return list;
        }
    }
}
