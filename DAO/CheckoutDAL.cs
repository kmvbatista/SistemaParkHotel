﻿using BLL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metadata
{
    public class CheckoutDAL : IEntityCRUD<Checkout>
    {
        StringConexao stc = new StringConexao();


        public string Atualizar(Checkout item)
        {
            throw new NotImplementedException();
        }

        public string Excluir(Checkout item)
        {
            throw new NotImplementedException();
        }

        public string Inserir(Checkout checkout)
        {
            string stringConexao = stc.getStringConexao();
            SqlConnection connection = new SqlConnection(stringConexao);
            SqlCommand command = new SqlCommand();

            command.CommandText = "INSERT INTO USUARIOS (CHECK_IN_ID, VALOR_TOTAL, DATA_SAIDA) VALUES  (@CHECK_IN_ID, @VALOR_TOTAL, @DATA_SAIDA)";
            command.Parameters.AddWithValue("@VALOR_TOTAL", checkout.valorTotal);
            command.Parameters.AddWithValue("@DATA_SAIDA", checkout.dataSaida);
            command.Parameters.AddWithValue("@CHECK_IN_ID", checkout.idCheckin);


            return "";
        }

        public Checkout LerPorID(int id)
        {
            throw new NotImplementedException();
        }

        public List<Checkout> LerTodos()
        {
            throw new NotImplementedException();
        }
    }
}
