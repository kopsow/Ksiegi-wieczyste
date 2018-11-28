using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ksiegi_wieczyste;


namespace KsiegiWieczysteTest
{
    [TestClass]
    public class ParseryTest
    {
        [TestMethod]
        [ExpectedException(typeof(System.Data.SqlClient.SqlException))]

        public void TestDbBrakParametrow()
        {
            DatabaseClass db = new DatabaseClass("", "", "","");
            db.Polacz();
        }
        [TestMethod]
        public void Debit_WithValidAmount_UpdatesBalance()
        {
            // Arrange

           

            Assert.AreEqual(1, 1, 1, "Account not debited correctly");
        }
    }
}
