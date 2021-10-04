﻿using System.Text.RegularExpressions;

namespace Exkyn.Core.Helpers
{
    public class ValidateHelpers
    {
        public static bool CPF(string cpf)
        {
            cpf = VariableHelpers.NoFormatting(cpf);

            if (cpf == null || cpf.Length > 11)
                return false;

            while (cpf.Length != 11)
            {
                cpf = '0' + cpf;
            }

            bool igual = true;

            for (int i = 1; i < 11 && igual; i++)
            {
                if (cpf[i] != cpf[0])
                    igual = false;
            }

            if (igual || cpf == "12345678909")
                return false;

            int[] numeros = new int[11];

            for (int i = 0; i < 11; i++)
            {
                numeros[i] = int.Parse(cpf[i].ToString());
            }

            int soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma += (10 - i) * numeros[i];
            }

            int resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;

            for (int i = 0; i < 10; i++)
            {
                soma += (11 - i) * numeros[i];

                resultado = soma % 11;
            }

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else
            {
                if (numeros[10] != 11 - resultado)
                    return false;
            }

            return true;
        }

        public static bool CNPJ(string cnpj)
        {
            cnpj = VariableHelpers.NoFormatting(cnpj);

            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;

            if (cnpj.Length != 14)
                return false;

            tempCnpj = cnpj.Substring(0, 12);

            soma = 0;

            for (int i = 0; i < 12; i++)
            {
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            }

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;

            for (int i = 0; i < 13; i++)
            {
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            }

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);
        }

        public static bool PIS(string pis)
        {
            pis = VariableHelpers.NoFormatting(pis);

            int[] multiplier = new int[10] { 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int sum = 0;
            int rest;

            if (pis.Length != 11)
                return false;

            pis = pis.PadLeft(11, '0');

            for (int i = 0; i < 10; i++)
            {
                sum += int.Parse(pis[i].ToString()) * multiplier[i];
            }

            rest = sum % 11;

            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            return pis.EndsWith(rest.ToString());
        }

        public static bool Email(string email)
        {
            var regex = new Regex("^[A-Za-z0-9](([_.-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([.-]?[a-zA-Z0-9]+)*)([.][A-Za-z]{2,4})$");

            var match = regex.Match(email);

            return match.Success;
        }
    }
}