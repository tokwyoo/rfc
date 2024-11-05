using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT
{
    public class RFC
    {
        public string GenerarRFC(string nombre, string apellidoPaterno, string apellidoMaterno, DateTime fechaNacimiento)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellidoPaterno) || string.IsNullOrWhiteSpace(apellidoMaterno))
                throw new ArgumentException("Los nombres y apellidos no pueden estar vacíos.");

            if (fechaNacimiento > DateTime.Today)
                throw new ArgumentException("La fecha de nacimiento no puede ser en el futuro.");

            if (fechaNacimiento.Year < 1900)
                throw new ArgumentException("La fecha de nacimiento no puede ser menor al año 1900.");

            StringBuilder rfc = new StringBuilder();

            // Extraer iniciales para la primera parte del RFC
            rfc.Append(ObtenerIniciales(apellidoPaterno, apellidoMaterno, nombre));

            // Agregar fecha de nacimiento en formato AA/MM/DD
            rfc.Append(fechaNacimiento.ToString("yyMMdd"));

            // Agregar homoclave
            rfc.Append(GenerarHomoclave());

            return rfc.ToString().ToUpper();
        }

        public string ObtenerIniciales(string apellidoPaterno, string apellidoMaterno, string nombre)
        {
            StringBuilder iniciales = new StringBuilder();

            // Primera letra y primer vocal interna del apellido paterno
            iniciales.Append(apellidoPaterno[0]);
            iniciales.Append(ObtenerPrimeraVocal(apellidoPaterno.Substring(1)));

            // Primera letra del apellido materno
            iniciales.Append(apellidoMaterno[0]);

            // Primera letra del primer nombre
            iniciales.Append(nombre[0]);

            return iniciales.ToString();
        }

        public char ObtenerPrimeraVocal(string palabra)
        {
            foreach (char c in palabra)
            {
                if ("AEIOUaeiou".Contains(c))
                    return c;
            }
            return 'X';
        }

        public string GenerarHomoclave()
        {
            Random random = new Random();
            string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            // Generar tres caracteres aleatorios que pueden ser letras o números
            char caracter1 = caracteres[random.Next(caracteres.Length)];
            char caracter2 = caracteres[random.Next(caracteres.Length)];
            char caracter3 = caracteres[random.Next(caracteres.Length)];

            return $"{caracter1}{caracter2}{caracter3}";
        }
    }
}
