using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolarZadanie;

namespace SolarZadanieTests
{
    [TestClass]
    public class CalculatorUnitTests
    {
        [TestMethod, TestCategory("Constructor")]
        public void Constructor_Default()
        {
            Calculator c = new Calculator();

            Assert.AreEqual(c.InputFileName, "input.json");
            Assert.AreEqual(c.ResultFileName, "output.txt");
        }

        [TestMethod, TestCategory("Constructor")]
        public void Constructor_DifferentInputFileName()
        {
            Calculator c = new Calculator(InputFileName: "newInput.json");

            Assert.AreEqual(c.InputFileName, "newInput.json");
            Assert.AreEqual(c.ResultFileName, "output.txt");
        }

        [TestMethod, TestCategory("Constructor")]
        public void Constructor_DifferentResultsFileName()
        {
            Calculator c = new Calculator(ResultFileName: "newOutput.txt");

            Assert.AreEqual(c.InputFileName, "input.json");
            Assert.AreEqual(c.ResultFileName, "newOutput.txt");
        }

        [DataTestMethod, TestCategory("Calculate")]
        [DataRow("../../../TestCases/inputNums.json")]
        public void Calculate_InputNums(string inputFileName)
        {
            Calculator c = new Calculator(InputFileName: inputFileName);

            Assert.IsTrue(c.Calculate());
        }

        [DataTestMethod, TestCategory("Calculate")]
        [DataRow("../../../TestCases/inputNumsAndStrings.json")]
        public void Calculate_InputNumsAndStrings(string inputFileName)
        {
            Calculator c = new Calculator(InputFileName: inputFileName);

            Assert.IsTrue(c.Calculate());
        }

        [DataTestMethod, TestCategory("Calculate")]
        [DataRow("../../../TestCases/inputStrings.json")]
        public void Calculate_InputStrings(string inputFileName)
        {
            Calculator c = new Calculator(InputFileName: inputFileName);

            Assert.IsTrue(c.Calculate());
        }

        [DataTestMethod, TestCategory("Calculate")]
        [DataRow("../../../TestCases/inputWrongInput.json")]
        public void Calculate_WrongInput(string inputFileName)
        {
            Calculator c = new Calculator(InputFileName: inputFileName);

            Assert.IsFalse(c.Calculate());
        }
    }
}
