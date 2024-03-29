﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Metadata;
using BLL;

namespace WFPresentationLayer
{
    public partial class FormEntradaProdutosDetalhes : Form
    {
        EntradaProdutosBLL entradaProdutosBLL = new EntradaProdutosBLL();
        FornecedorBLL fornecedorBLL = new FornecedorBLL();
        ProdutoBLL produtoBLL = new ProdutoBLL();
        public EntradaProdutos entradaProdutos { get; set; }

        public FormEntradaProdutosDetalhes(EntradaProdutos entradaProdutos)
        {
            InitializeComponent();
            carregarComboBox();
            this.entradaProdutos = entradaProdutos;
        }

        private void btnAdicionarAoLote_Click_1(object sender, EventArgs e)
        {
            try
            {
                tratarRepeticaoId();
                entradaProdutos.itens.Add(InstanciarEntradaProdutosDetalhes());
                VisualizarItensAdicionados();
                limparTextBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnFinalizar_Click_1(object sender, EventArgs e)
        {
            try
            {
                entradaProdutosBLL.inserir(entradaProdutos);
                tratarQuantidadeEstoque(entradaProdutos.itens);
                MessageBox.Show("Feito com sucesso");
                entradaProdutos.itens.Clear();
                dataGridDia.DataSource = entradaProdutos.itens;
            }
            catch (Exception ex)
            {
                MessageBox.Show("erro na solicitação: " + ex.Message);
            }
        }

        private void tratarQuantidadeEstoque(List<ItensEntrada> itens)
        {
            foreach (ItensEntrada item in itens)
            {
                Produto p = produtoBLL.LerPorID(item.idProduto);
                p.qtdEstoque += item.quantidade;
                produtoBLL.Atualizar(p);
            }
        }

        private ItensEntrada InstanciarEntradaProdutosDetalhes()
        {
            try
            {
                int idProduto = (int)cmbProduto.SelectedValue;
                int idFornecedor = encontrarIdFornecedor();
                int quantidade = int.Parse(txtQuantidadeEntrada.Text);
                double valorUnitario = double.Parse(txtValorEntrada.Text) / int.Parse(txtQuantidadeEntrada.Text);
                return new ItensEntrada(idProduto, idFornecedor, quantidade, valorUnitario);
            }
            catch (Exception)
            {
                throw new Exception("Dados inválidos");
            }
        }

        private int encontrarIdFornecedor()
        {
            return (int)cmbFornecedor.SelectedValue;
        }
        
        private void limparTextBox()
        {
            txtQuantidadeEntrada.Clear();
            txtValorEntrada.Clear();
        }

        private void tratarRepeticaoId()
        {
            for (int i = 0; i < entradaProdutos.itens.Count; i++)
            {
                if ((int)cmbProduto.SelectedValue == entradaProdutos.itens[i].idProduto && (int)cmbFornecedor.SelectedValue == entradaProdutos.itens[i].idFornecedor)
                {
                    entradaProdutos.itens.RemoveAt(i);
                    VisualizarItensAdicionados();
                    throw new Exception("Você não pode inserir duas vezes o mesmo produto com o mesmo fornecedor.\n" +
                        " Preencha novamente!\n Agora, com a quantidade completa de uma só vez.");
                }
            }
        }

        

        private void btnExibirEntradas_Click_1(object sender, EventArgs e)
        {

        }

        

        private void carregarComboBox()
        {
            cmbProduto.DisplayMember = "NOME";
            cmbProduto.ValueMember = "ID";
            cmbProduto.DataSource = produtoBLL.LerTodos();
            cmbFornecedor.DisplayMember = "razaoSocial";
            cmbFornecedor.ValueMember = "ID";
            cmbFornecedor.DataSource = fornecedorBLL.LerTodos();
        }

        private void txtValorEntrada_TextChanged(object sender, EventArgs e)
        {
            CalcularValorUnitario();
        }
        private void txtQuantidadeEntrada_TextChanged(object sender, EventArgs e)
        {
            CalcularValorUnitario();
        }
        private void CalcularValorUnitario()
        {
            if (!String.IsNullOrWhiteSpace(txtQuantidadeEntrada.Text) && !String.IsNullOrWhiteSpace(txtValorEntrada.Text))
                try
                {
                    LblValorUnitarioCompra.Text = (double.Parse(txtValorEntrada.Text) / int.Parse(txtQuantidadeEntrada.Text)).ToString("C2");
                }
                catch (Exception ex)
                {
                    LblValorUnitarioCompra.Text = "R$ 0,00";
                    MessageBox.Show(ex.Message);
                }
            if(String.IsNullOrWhiteSpace(txtQuantidadeEntrada.Text) || String.IsNullOrWhiteSpace(txtValorEntrada.Text))
            {
                LblValorUnitarioCompra.Text = "R$ 0,00";
            }
        }

        private void btnApagarRegistro_Click(object sender, EventArgs e)
        {
            try
            {
                int index = dataGridDia.CurrentRow.Index;
                entradaProdutos.itens.RemoveAt(index);
                VisualizarItensAdicionados();
            }
            catch (Exception)
            {
                MessageBox.Show("Selecione algum produto do lote");
            }
        }

        private void VisualizarItensAdicionados()
        {
            try
            {
                List<ItensAdicionadosEntrada> ProdutosAdicionados = new List<ItensAdicionadosEntrada>();
                int counter = 1;
                foreach (ItensEntrada item in entradaProdutos.itens)
                {
                    ItensAdicionadosEntrada i = new ItensAdicionadosEntrada();
                    Produto p = produtoBLL.LerPorID(item.idProduto);
                    i.Produto = p.nome;
                    i.Compra = counter++;
                    ProdutosAdicionados.Add(i);
                }
                dataGridDia.DataSource = ProdutosAdicionados;
                dataGridDia.Show();
                btnApagarRegistro.Show();
                lblProdutosLote.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("erro na solicitação: "+ex.Message);
            }
            
        }
    }
}
