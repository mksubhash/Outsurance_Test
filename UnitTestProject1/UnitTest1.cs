using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using outsurance_Test;
using System.Collections.Generic;
using System.IO;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void DataLoad_Test()
        {
            const string testFile = "test.csv";            
            GenerateTestCsv(testFile);

            var processInstance = new ProcessData();
            var dataExpected = GetTestCustomer() as List<Customer>;
            var data = processInstance.LoadData(testFile) as List<Customer>;

            Assert.AreEqual(dataExpected.Count, data.Count);
            Assert.AreEqual(dataExpected[0].FirstName, data[0].FirstName);

        }

        [TestMethod]
        public void WriteTxtScenarioOne_Test()
        {
            
            const string testFile = "test.csv";
            GenerateTestCsv(testFile);
            var processInstance = new ProcessData();

            var customers = processInstance.LoadData(testFile);

            processInstance.WriteTxtScenarioOne(customers);
                    
            var expected = ScenarioOneExpected;
            var actual = ScenarioOneActual();
            Assert.IsTrue(string.Equals(expected,actual));
            
        }

        [TestMethod]
        public void WriteTxtScenarioTwo_Test()
        {

            const string testFile = "test.csv";
            GenerateTestCsv(testFile);
            var processInstance = new ProcessData();

            var customers = processInstance.LoadData(testFile);

            processInstance.WriteTxtScenarioTwo(customers);

            var expected = ScenarioTwoExpected;
            var actual = ScenarioTwoActual();
            Assert.IsTrue(string.Equals(expected, actual));

            

        }
        public void GenerateTestCsv(string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            var csvData = @"FirstName, LastName, Address, PhoneNumber" + Environment.NewLine + @"Jimmy,Smith,102 Long Lane,29384857" + Environment.NewLine + @"Clive,Smith,65 Ambling Way,31214788" + Environment.NewLine + @"James,Brown,82 Stewart St,32114566";

            File.WriteAllText(fileName, csvData);

        }
        public IEnumerable<Customer> GetTestCustomer()
        {
            var customers = new List<Customer>();
            var customer = new Customer()
            {
                FirstName = "Jimmy",
                LastName = "Smith",
                PhoneNumber = 29384857,
                StreetName = "Long Lane",
                StreetNumber = "102"

            };
            customers.Add(customer);
            customer = new Customer()
            {
                FirstName = "Clive",
                LastName = "Smith",
                PhoneNumber = 31214788,
                StreetName = "Ambling Way",
                StreetNumber = "65"

            };
            customers.Add(customer);
            customer = new Customer()
            {
                FirstName = "James",
                LastName = "Brown",
                PhoneNumber = 32114566,
                StreetName = "Stewart St",
                StreetNumber = "82"

            };
            customers.Add(customer);
            return customers;
        }


        public string ScenarioOneActual()
        {
            string actual = File.ReadAllText(@"ScenarioOne.txt");
            return actual;

        }
        public string ScenarioOneExpected
        {
            get { return "Smith, 2"+Environment.NewLine+ "Brown, 1" + Environment.NewLine + "Clive, 1" + Environment.NewLine + "James, 1" + Environment.NewLine + "Jimmy, 1"; }
            
        }
        public string ScenarioTwoActual()
        {
            string actual = File.ReadAllText(@"ScenarioTwo.txt");
            return actual;

        }
        public string ScenarioTwoExpected
        {
            get { return "65 Ambling Way" + Environment.NewLine + "102 Long Lane" + Environment.NewLine + "82 Stewart St"; }
            
        }
    }

  
}
