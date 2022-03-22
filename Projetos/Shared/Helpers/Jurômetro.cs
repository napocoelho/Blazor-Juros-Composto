using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geral.Shared.Helpers
{
    public record Juros(int Periodo, decimal Capital, decimal JurosPeriodo, decimal JurosAcumulado);

    public static class Jurômetro
    {
        public static decimal Round(this decimal value, int decimals)
        {
            return decimal.Round(value, decimals);
        }

        public static List<Juros> CalcularJurosComposto(int periodoCalculado, decimal capitalInicial, decimal jurosAplicado)
        {
            List<Juros> composto = new();

            for (int idx = 0; idx <= periodoCalculado; idx++)
            {
                Juros? jurosAnterior = composto.LastOrDefault();

                if (jurosAnterior is not null)
                {
                    decimal jurosPeriodo = jurosAnterior.Capital / 100 * jurosAplicado;
                    decimal capital = jurosAnterior.Capital + jurosPeriodo;
                    decimal jurosAcumulado = jurosPeriodo + composto.Sum(x => x.JurosPeriodo);

                    Juros juros = new Juros(idx, capital.Round(2), jurosPeriodo.Round(2), jurosAcumulado.Round(2));
                    composto.Add(juros);
                }
                else    /* Primeiro período (dia, mês, ano, etc...) */
                {
                    Juros juros = new Juros(0, capitalInicial, 0, 0);
                    composto.Add(juros);
                }
            }

            return composto;
        }
    }
}
