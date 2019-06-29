﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metadata;
using DAL;

namespace BLL
{
    public class EntradaProdutosDetalhesBLL
    {
        List<string> erros = new List<string>();
        ProdutoDAL produtoDAL = new ProdutoDAL();
        FornecedorDAL fornecedorDAL = new FornecedorDAL();
        EntradaProdutosDAL entradaProdutosDAL = new EntradaProdutosDAL();
        EntradaProdutosDetalhesDAL entradaDal = new EntradaProdutosDetalhesDAL();

        public string inserir(EntradaProdutosDetalhes Entrada)
        {
            if (this.Validar(Entrada))
            {
                return entradaDal.Inserir(Entrada);
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < erros.Count(); i++)
            {
                sb.Append(erros[i]);
            }
            return sb.ToString();
        }

        
        public bool Validar(EntradaProdutosDetalhes Entrada)
        {
            if (Entrada.valorUnitario < 0)
                erros.Add("o valor tem não pode ser negativo");

            if (Entrada.quantidade < 0)
            {
                erros.Add("a  quantidade não pode ser menor que 0");
            }
            TratarDependencias(Entrada);

            if (erros.Count() > 0)
            {
                return false;
            }
            return true;
        }
        public EntradaProdutosDetalhes LerPorID(int id)
        {

            return entradaDal.LerPorID(id);

        }
        public List<EntradaProdutosDetalhes> LerTodos()
        {
            return entradaDal.LerTodos();
        }

        private void TratarDependencias(EntradaProdutosDetalhes entrada)
        {
            if (entrada.idProduto < 0)
            {
                erros.Add("id do produto deve ser informado");
            }
            else
            {
                Produto produto = produtoDAL.LerPorID(entrada.idProduto);
                if (produto == null)
                {
                    erros.Add("produto não encontrado no banco");
                }
            }
            if (entrada.idFornecedor < 0)
            {
                erros.Add("id do fornecedor deve ser informado");
            }
            else
            {
                Fornecedor fornecedor = fornecedorDAL.LerPorID(entrada.idFornecedor);
                if (fornecedor == null)
                {
                    erros.Add("cliente não encontrado no banco");
                }
            }
            EntradaProdutos entradaProdutos = new EntradaProdutos();
            entradaProdutos = entradaProdutosDAL.LerPorID(entrada.idEntradaProduto);
            erros.Add("erro de integridade referencial em relação à entrada de produtos.");
        }
    }
}
