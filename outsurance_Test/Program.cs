using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace outsurance_Test
{
    class Program
    {
        static void Main(string[] args)
        {

            //Assumptions:
            //1.All fields are mandatory
            //2.Address fields format (separator is space)
            //a.Street number
            //b.Street name
            //Note:
            //The solution is intended to solve the test.This is not a production ready code as it may fail if the input file is larger.
            //If the input file is larger then I may propose using MemoryMappedFile or StreamReader and upload data in to sql and use LINQ to SQL for other process.
            //There are CSV thridparty libs available like CSV helper etc which can be utilized for large files as well
            
            try
                {
                var processInstance = new ProcessData();
                var dataItems = processInstance.LoadData("data.csv");
                processInstance.WriteTxtScenarioOne(dataItems);
                processInstance.WriteTxtScenarioTwo(dataItems);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error -- " + ex.Message);
            }

            Console.ReadLine();

        }
    }
}
