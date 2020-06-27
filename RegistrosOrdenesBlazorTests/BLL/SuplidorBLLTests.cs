using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegistrosOrdenesBlazor.BLL;
using RegistrosOrdenesBlazor.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegistrosOrdenesBlazor.BLL.Tests
{
    [TestClass()]
    public class SuplidorBLLTests
    {
        [TestMethod()]
        public void GetListTest()
        {
            var lista = new List<Suplidores>();
            lista = SuplidorBLL.GetList(p => true);
            Assert.IsNotNull(lista);
        }
    }
}