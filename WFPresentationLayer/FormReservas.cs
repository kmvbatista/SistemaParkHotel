﻿using BLL;
using Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFPresentationLayer
{
    public partial class FormReservas : Form
    {
        public FormReservas()
        {
            InitializeComponent();
        }

        ReservaBLL reservaBLL = new ReservaBLL();

        private void btncadastrarReservas_Click(object sender, EventArgs e)
        {
            try
            {
                Reserva reserva = InstanciarReserva();
                MessageBox.Show(reservaBLL.inserir(reserva));
                VisualizarDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
            
        }

        private void VisualizarDataGrid()
        {
            try
            {
                dataGridViewReservas.DataSource = reservaBLL.LerTodos();
                this.dataGridViewReservas.Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Erro no acesso ao banco: "+ex.Message);
            }
            
        }

        private void btnConsultarReservas_Click(object sender, EventArgs e)
        {
            try
            {
                VisualizarDataGrid();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Erro na solicitação: "+ex.Message);
            }
        }

        private void dataGridViewReservas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private Reserva InstanciarReserva()
        {
            int idCliente = int.Parse(txtnumClienteReserva.Text);
            int idUsuario = Usuario.UsuarioLogado.id;
            DateTime dataPrevistaChegada = DateTimeChegadaReserva.Value;
            DateTime dataPrevistaSaida = dateTimeSaidaReserva.Value;
            int idQuarto = int.Parse(txtnumQuartoReserva.Text);
            Reserva reserva = new Reserva(idCliente, idUsuario, dataPrevistaChegada, dataPrevistaSaida, idQuarto);
            return reserva;
        }

 
    }
}
