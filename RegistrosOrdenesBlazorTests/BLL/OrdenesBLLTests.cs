using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegistrosOrdenesBlazor.BLL;
using RegistrosOrdenesBlazor.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegistrosOrdenesBlazor.BLL.Tests
{
    [TestClass()]
    public class OrdenesBLLTests
    {
        [TestMethod()]
        public void GuardarTest()
        {
            Ordenes ordenes = new Ordenes();
            ordenes.OrdenId = 0;
            ordenes.SuplidorId = 1;
            ordenes.Fecha = DateTime.Now;
            ordenes.Monto = 100;
            ordenes.OrdenDetalles.Add(new OrdenesDetalle
            {
                OrdenDetalleId = 0,
                OrdenId = 0,
                ProductoId = 1,
                Costo = 1000,
                Cantidad = 1
            });
        }

        [TestMethod()]
        public void EliminarTest()
        {
            bool paso = OrdenesBLL.Eliminar(1);
            Assert.AreEqual(paso, true);
        }

        [TestMethod()]
        public void BuscarTest()
        {
            Ordenes ordenes = new Ordenes();
            ordenes = OrdenesBLL.Buscar(1);
            Assert.IsNotNull(ordenes);
        }

        [TestMethod()]
        public void GetListTest()
        {
            var lista = new List<Ordenes>();
            lista = OrdenesBLL.GetList(p => true);
            Assert.IsNotNull(lista);
        }

        [TestMethod()]
        public void ExisteTest()
        {
            bool paso = OrdenesBLL.Existe(1);
            Assert.AreEqual(paso, true);
        }
    }
}