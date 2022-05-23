using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using SolarZadanie.Models;

namespace SolarZadanie
{
    public class Calculator
    {
        public string InputFileName { get; private set; }
        public string ResultFileName { get; private set; }
        private Dictionary<string, CalculateData> objDict;

        //Constructor with default files for input and result names
        public Calculator(string InputFileName = "input.json", string ResultFileName = "output.txt")
        {
            this.InputFileName = InputFileName;
            this.ResultFileName = ResultFileName;
        }

        //Method to check if there is any input and output file
        private bool FilesExists()
        {
            //Check if output.txt exists, if not create it
            if (!File.Exists(ResultFileName))
                File.Create(ResultFileName).Close();

            //Check if input.json exists, if not return
            if (!File.Exists(InputFileName))
            {
                //Show message in console
                Console.WriteLine($"{InputFileName} is missing! You need to put {InputFileName} in same folder as program .exe!");

                //StreamWriter to save message in output.txt
                StreamWriter streamWriter = new StreamWriter(ResultFileName);
                streamWriter.WriteLine($"{InputFileName} is missing! You need to put {InputFileName} in same folder as program .exe!");
                streamWriter.Close();

                //File is not in program folder
                return false;
            }

            return true;
        }

        //Method to deserialize data from input
        private bool IsDataDeserialized()
        {
            try
            {
                //Set JsonSerializerOptions to handle number as string case
                JsonSerializerOptions options = new()
                {
                    NumberHandling =
                    JsonNumberHandling.AllowReadingFromString |
                    JsonNumberHandling.WriteAsString,
                    WriteIndented = true
                };

                //Load data from input.json and save it to Dictionary
                string jsonString = File.ReadAllText(InputFileName);
                objDict = JsonSerializer.Deserialize<Dictionary<string, CalculateData>>(jsonString, options);

                //Everything is fine, data deserialized
                return true;
            }
            catch (JsonException ex)
            {
                //Show error message in console
                Console.WriteLine(ex.Message);

                //StreamWriter to save error message in output.txt
                StreamWriter streamWriter = new StreamWriter(ResultFileName);
                streamWriter.WriteLine(ex.Message);
                streamWriter.Close();

                //Something went wrong, data is not deserialized correctly
                return false;
            }
            catch (InvalidOperationException ex)
            {
                //Show error message in console
                Console.WriteLine(ex.Message);

                //StreamWriter to save error message in output.txt
                StreamWriter streamWriter = new StreamWriter(ResultFileName);
                streamWriter.WriteLine(ex.Message);
                streamWriter.Close();

                //Something went wrong, data is not deserialized correctly
                return false;
            }
            catch (ArgumentException ex)
            {
                //Show error message in console
                Console.WriteLine(ex.Message);

                //StreamWriter to save error message in output.txt
                StreamWriter streamWriter = new StreamWriter(ResultFileName);
                streamWriter.WriteLine(ex.Message);
                streamWriter.Close();

                //Something went wrong, data is not deserialized correctly
                return false;
            }
            catch (FormatException ex)
            {
                //Show error message in console
                Console.WriteLine(ex.Message);

                //StreamWriter to save error message in output.txt
                StreamWriter streamWriter = new StreamWriter(ResultFileName);
                streamWriter.WriteLine(ex.Message);
                streamWriter.Close();

                //Something went wrong, data is not deserialized correctly
                return false;
            }
            catch (Exception ex)
            {
                //Show error message in console
                Console.WriteLine(ex.Message);

                //StreamWriter to save error message in output.txt
                StreamWriter streamWriter = new StreamWriter(ResultFileName);
                streamWriter.WriteLine(ex.Message);
                streamWriter.Close();

                //Something went wrong, data is not deserialized correctly
                return false;
            }
        }

        //Sort results
        private IOrderedEnumerable<KeyValuePair<string, CalculateData>> SortResults() => objDict.OrderBy(item => item.Value.result);

        //Save results
        private void SaveResults(IOrderedEnumerable<KeyValuePair<string, CalculateData>> sortedDict)
        {
            //StreamWriter to save results output.txt
            StreamWriter streamWriter = new StreamWriter(ResultFileName);

            foreach (var item in sortedDict)
            {
                streamWriter.WriteLine($"{item.Key}: {item.Value.result}");
            }
            streamWriter.Close();
        }

        //Method to calculate input variables
        public bool Calculate()
        {
            //Check if files exists if not return false it means Calculating failed
            if(!FilesExists()) return false;

            //Deserialize data from input if not return false it means Calculating failed
            if (!IsDataDeserialized()) return false;

            //Calculate
            foreach (var item in objDict)
            {
                switch (item.Value.@operator)
                {
                    case "add":
                        item.Value.result = item.Value.value1 + item.Value.value2;
                        break;
                    case "sub":
                        item.Value.result = item.Value.value1 - item.Value.value2;
                        break;
                    case "mul":
                        item.Value.result = item.Value.value1 * item.Value.value2;
                        break;
                    case "sqrt":
                        item.Value.result = Math.Sqrt(item.Value.value1);
                        break;
                    default:
                        objDict.Remove(item.Key);
                        break;
                }
            }

            //Sort results
            IOrderedEnumerable<KeyValuePair<string, CalculateData>> sortedDict = SortResults();

            //Save results
            SaveResults(sortedDict);

            //Calculated and saved successfully return true
            return true;
        }
    }
}
