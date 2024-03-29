﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DAL;
using DAO.ViewModels;
using Metadata;



namespace BLL
{
    public class CheckinBLL
    {
        List<string> erros = new List<string>();
        CheckinDAL checkinDAL = new CheckinDAL();
        QuartoBLL quartoBll = new QuartoBLL();
        ClienteBLL clienteBll = new ClienteBLL();
        ReservaBLL reservaBll = new ReservaBLL();


        public void inserir(Checkin checkin)
        {
            Validar(checkin);
            if (erros.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < erros.Count(); i++)
                {
                    sb.Append(erros[i]);
                }
                erros.Clear();
                throw new Exception(sb.ToString());
            }

            //Se chegou aqui, bora inserir o checkin e ocupar o quarto
            //TransactionScope Isolation Level (Serializable, COmmitedRead)
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    checkinDAL.Inserir(checkin);
                    quartoBll.Ocupar(checkin.quartoId);
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal void AtualizarPendenciaCheckout(int id)
        {
            checkinDAL.AtualizarPendenciaCheckout(id);
        }

        public Checkin LerPorID(int id)
        {
            try
            {
                return checkinDAL.LerPorID(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Checkin> LerTodos()
        {
            return checkinDAL.LerTodos();
        }
        public List<CheckinViewModel> LerCheckinViewModels()
        {
            try
            {
                return checkinDAL.LerCheckinViewModel();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Validar(Checkin checkin)
        {
            double diferenca = checkin.dataPrevistaSaida.Subtract(checkin.dataEntrada).Minutes;

            if (checkin.dataPrevistaSaida.Subtract(checkin.dataEntrada).Minutes<0)
            {
                erros.Add("Data saída não pode ser menor que a data de entrada");
            }
            TratarIntegridadeReferencial(checkin);

        }

        private void TratarIntegridadeReferencial(Checkin checkin)
        {
            if (checkin.quartoId <= 0)
            {
                erros.Add("QUarto deve ser informado.");
            }
            else
            {
                Quarto quartoASerOcupado = quartoBll.LerPorID(checkin.quartoId);
                if (quartoASerOcupado == null)
                {
                    erros.Add("QUarto deve ser informado.");
                }
                else if (quartoASerOcupado.estaOcupado&&!(checkin.idReserva>0))
                {
                    erros.Add("O quarto já esta em uso.");
                }
            }
            if (checkin.clienteId <= 0)
            {
                erros.Add("Cliente deve ser informado.");
            }
            else
            {
                Cliente clienteAOcuparOQuarto = clienteBll.LerPorID(checkin.clienteId);
                if (clienteAOcuparOQuarto == null)
                {
                    erros.Add("Cliente deve ser informado.");
                }
            }
        }
    }
}

