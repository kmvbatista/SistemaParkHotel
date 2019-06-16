﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metadata;
using DAL;

namespace BLL
{
    class ClienteBLL
    {
        private bool ValidarCPF(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }


        public string Cadastrar(Cliente cli)
        {
            List<string> erros = new List<string>();

            #region Nome
            if (string.IsNullOrWhiteSpace(cli.Nome))
            {
                erros.Add("Nome deve ser informado.");
            }
            else
            {
                cli.Nome = cli.Nome.Trim();
                if (cli.Nome.Length < 3 || cli.Nome.Length > 60)
                {
                    erros.Add("Nome deve conter entre 3 e 60 caracteres.");
                }
                else
                {
                    for (int i = 0; i < cli.Nome.Length; i++)
                    {
                        if (!char.IsLetter(cli.Nome[i]) && cli.Nome[i] != ' ')
                        {
                            erros.Add("Nome inválido");
                            break;
                        }
                    }
                }
            }
            #endregion

            #region CPF
            if (string.IsNullOrWhiteSpace(cli.CPF))
            {
                erros.Add("CPF deve ser informado.");
            }
            else
            {
              
                cli.CPF = cli.CPF.Trim();
                cli.CPF = cli.CPF.Replace(".", "").Replace("-", "");
                if (!this.ValidarCPF(cli.CPF))
                {
                    erros.Add("CPF inválido");
                }
            }
            #endregion

            #region RG
            if (string.IsNullOrWhiteSpace(cli.RG))
            {
                erros.Add("RG deve ser informado.");
            }
            else
            {
                cli.RG = cli.RG.Trim();
                cli.RG = cli.RG.Replace(".", "").Replace("/", "").Replace("-", "");
                if (cli.RG.Length < 5 || cli.RG.Length > 9)
                {
                    erros.Add("RG deve conter entre 5 e 9 caracteres.");
                }
            }
            #endregion


            #region Telefone1
            if (string.IsNullOrWhiteSpace(cli.Telefone1))
            {
                erros.Add("Primeiro número de telefone deve ser informado.");
            }
            else
            {
                cli.Telefone1 =
                    cli.Telefone1.Replace(" ", "")
                                .Replace("(", "")
                                .Replace(")", "")
                                .Replace("-", "");

                if (cli.Telefone1.Length < 8 || cli.Telefone1.Length > 15)
                {
                    erros.Add("Telefone deve conter entre 8 e 15 caracteres.");
                }
            }
            #endregion



            #region Telefone2
            if (string.IsNullOrWhiteSpace(cli.Telefone2))
            {
                erros.Add("Segundo número de telefone deve ser informado.");
            }
            else
            {
                cli.Telefone2 =
                    cli.Telefone1.Replace(" ", "")
                                .Replace("(", "")
                                .Replace(")", "")
                                .Replace("-", "");

                if (cli.Telefone2.Length < 8 || cli.Telefone2.Length > 15)
                {
                    erros.Add("Telefone deve conter entre 8 e 15 caracteres.");
                }
            }
            #endregion



            #region Email

            bool isEmail = Regex.IsMatch(cli.Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            if (!isEmail)
            {
                erros.Add("Email deve ser informado.");
            }

            EnderecoBLL eBLL = new EnderecoBLL();
            string errosEndereco = eBLL.Validar(cli.EnderecoCliente);

            if (!string.IsNullOrWhiteSpace(errosEndereco))
            {
                erros.Add(errosEndereco);
            }

            StringBuilder builder = new StringBuilder();
            if (erros.Count != 0)
            {
                for (int i = 0; i < erros.Count; i++)
                {
                    builder.AppendLine(erros[i]);
                }
                return builder.ToString();
            }
            new ClienteDAL().Inserir(cli);
            return "Cliente cadastrado com sucesso";

            #endregion

        }
    }
}

