using System;
using System.Collections.Generic;
using System.Text;

namespace Kimera.Domain.Common
{
    public static class GuardClauses
    {
        public static void IsnotNull(object ArgumentValue, string ArgumentName)
        {
            if (ArgumentValue is null)
                throw new ArgumentNullException(ArgumentName);
        }
        public static void IsNOtNullOrEmpty(string valorArgumento, string nomeArgumento)
        {
            if (string.IsNullOrEmpty(valorArgumento))
                throw new ArgumentNullException(nomeArgumento);

        }
        public static void IsSmallerOrEqualThanZero(int valorArgumento, string nomeArgumento)
        {
            if (valorArgumento <= 0)
                throw new ArgumentOutOfRangeException($"Argumento '{nomeArgumento}' não pode ser menor ou igual a zero");
        }
        public static void IsInvalidRange(decimal valorArgumento, string nomeArgumento, decimal minValue, decimal maxValue)
        {
            if (valorArgumento < minValue || valorArgumento > maxValue)
                throw new ArgumentOutOfRangeException(nomeArgumento, $"Argumento '{nomeArgumento}' deve estar entre {minValue} e {maxValue}");
        }
        public static void IsSmallerThanZero(int valorArgumento, string nomeArgumento)
        {
            if (valorArgumento < 0)
                throw new ArgumentOutOfRangeException($"Argumento '{nomeArgumento}' não pode ser menor que zero");
        }



    }
}
