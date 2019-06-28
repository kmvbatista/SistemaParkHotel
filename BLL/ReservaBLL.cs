﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metadata;
using DAL;

namespace BLL
{
    public class ReservaBLL
    {
        List<string> erros = new List<string>();

        ReservaDAL reservaDal = new ReservaDAL();

        public string inserir(Reserva reserva)
        {
            if (this.Validar(reserva))
            {
                return reservaDal.Inserir(reserva);
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < erros.Count(); i++)
            {
                sb.Append(erros[i]);
            }
            return sb.ToString();
        }

        public string Atualizar(Reserva reserva)
        {
            if (this.Validar(reserva))
            {
                return reservaDal.Atualizar(reserva);
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < erros.Count(); i++)
            {
                sb.Append(erros[i]);
            }
            return sb.ToString();
        }

        public bool Validar(Reserva reserva)
        {
            
            
            if (reserva.dataPrevisaoSaida < reserva.dataPrevisaoChegada)
            {
                erros.Add("datas inválidas");
            }

            if (erros.Count > 0)
            {
                return false;
            }
            return true;

        }
        public Reserva LerPorID(int id)
        {
            return reservaDal.LerPorID(id);
        }
        public List<Reserva> LerTodos()
        {
            return reservaDal.LerTodos();
        }
    }
}
