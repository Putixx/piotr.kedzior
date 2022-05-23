
namespace SolarZadanie
{
    class Program
    {
        //Files names
        private const string inputFileName = "input.json";
        private const string resultName = "output.txt";

        static void Main(string[] args)
        {
            //Create new Calculator object with default input = input.json and default output = output.txt
            Calculator calc1 = new Calculator();

            //Create new Calculator object with defined by us variables for input and output
            Calculator calc2 = new Calculator(inputFileName, resultName);

            //Calculate
            calc1.Calculate();
        }
    }
}
