using System;
using NUnit.Framework;
using Moq;

namespace SAT.Tests
{
    [TestFixture]
    public class RFCTest
    {
        private Mock<RFC> _rfcMock;

        [SetUp]
        public void SetUp()
        {
            _rfcMock = new Mock<RFC>();
        }

        [Test]
        public void GenerarRFC_EntradaValida_RegresaFormatoCorrecto()
        {
            // Arrange
            string nombre = "Juan";
            string apellidoPaterno = "Perez";
            string apellidoMaterno = "Gomez";
            DateTime fechaNacimiento = new DateTime(1990, 5, 24);

            // Act
            var rfc = _rfcMock.Object.GenerarRFC(nombre, apellidoPaterno, apellidoMaterno, fechaNacimiento);

            // Assert
            StringAssert.IsMatch(@"^[A-Z]{4}\d{6}[A-Z0-9]{3}$", rfc); // Verifica el formato: 4 letras, 6 números, 3 caracteres
        }

        [Test]
        public void GenerarRFC_NombresVacios_LanzaArgumentException()
        {
            // Arrange
            string nombre = "";
            string apellidoPaterno = "";
            string apellidoMaterno = "";
            DateTime fechaNacimiento = new DateTime(1990, 5, 24);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _rfcMock.Object.GenerarRFC(nombre, apellidoPaterno, apellidoMaterno, fechaNacimiento));
        }

        [Test]
        public void GenerarRFC_FechaEnFuturo_LanzaArgumentException()
        {
            // Arrange
            string nombre = "Juan";
            string apellidoPaterno = "Perez";
            string apellidoMaterno = "Gomez";
            DateTime fechaNacimiento = DateTime.Today.AddYears(1); // Fecha en el futuro

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _rfcMock.Object.GenerarRFC(nombre, apellidoPaterno, apellidoMaterno, fechaNacimiento));
        }

        [Test]
        public void GenerarRFC_FechaAntesDe1900_LanzaArgumentException()
        {
            // Arrange
            string nombre = "Juan";
            string apellidoPaterno = "Perez";
            string apellidoMaterno = "Gomez";
            DateTime fechaNacimiento = new DateTime(1899, 12, 31); // Fecha antes de 1900

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _rfcMock.Object.GenerarRFC(nombre, apellidoPaterno, apellidoMaterno, fechaNacimiento));
        }

        [Test]
        public void GenerarHomoclave_RegresaLongitudYFormatoCorrecto()
        {
            // Arrange
            var rfcGenerator = new RFC();
            string homoclave = rfcGenerator.GenerarHomoclave();

            // Act & Assert
            Assert.AreEqual(3, homoclave.Length); // Debe tener 3 caracteres
            StringAssert.IsMatch(@"^[A-Z0-9]{3}$", homoclave); // Solo letras y números
        }
    }
}
