using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace outsurance_Test
{
    public class ProcessData
    {
        public IEnumerable<Customer> LoadData(string file)
        {
            if (!File.Exists(file))
            {
                throw new Exception("File not found: File Name:" + file);
            }
            var data = File.ReadAllText(file);
            if (!string.IsNullOrEmpty(data))
            {
                var bulkData = data.Replace("\r", "").Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                if (bulkData.Count > 0)
                {
                    bulkData.RemoveAt(0);
                }
                if (bulkData.Count > 0)
                {
                    var dataItems = bulkData.Select((str) =>
                    {
                        var itemArray = str.Split(new char[] { ',' });
                        var streetArray = itemArray.Length >= 3 ? itemArray[2].Split(new char[] { ' ' }).ToList() : null;
                        var streetNum = string.Empty;
                        var streetName = string.Empty;
                        if (streetArray != null)
                        {
                            streetNum = streetArray.Count >= 1 ? streetArray[0] : string.Empty;
                            if (streetArray.Count >= 1)
                            {
                                streetArray.RemoveAt(0);
                            }
                            streetName = streetArray.Count >= 1 ? string.Join(" ", streetArray) : string.Empty;
                        }
                        var cust = new Customer()
                        {
                            FirstName = itemArray.Length >= 1 ? itemArray[0] : string.Empty,
                            LastName = itemArray.Length >= 2 ? itemArray[1] : string.Empty,
                            StreetNumber = streetNum,
                            StreetName = streetName,
                            PhoneNumber = itemArray.Length >= 4 ? Convert.ToInt32(itemArray[3]) : 0
                        };
                        return cust;
                    });
                    return dataItems.ToList();
                }
                else
                {
                    throw new Exception("Only header row found in the file");
                }
            }
            else
            {
                throw new Exception("No Data found");
            }
        }
        public void WriteTxtScenarioOne(IEnumerable<Customer> customers)
        {
            const string fileName = @"ScenarioOne.txt";
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            var names = (from x in customers
                         select x.FirstName).ToList();
            names.AddRange((from x in customers
                            select x.LastName).ToList());
            var groupOfNames = names.GroupBy(x => x)
                        .Select((group) =>
                        {
                            return new { group.Key, group.ToList().Count };
                        }
                      ).OrderBy(x => x.Key).OrderByDescending(x => x.Count);
            var data = string.Join(Environment.NewLine, groupOfNames.Select(x => x.Key + ", " + x.Count));
            File.WriteAllText(fileName, data);
        }
        public void WriteTxtScenarioTwo(IEnumerable<Customer> customers)
        {
            const string fileName = @"ScenarioTwo.txt";
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            var streetNames = customers.OrderBy(x => x.StreetName).Select(s => s.StreetNumber + " " + s.StreetName).ToList();
            var data = string.Join(Environment.NewLine, streetNames.Select(x => x));
            File.WriteAllText(fileName, data);
        }
    }
}