using AngouriMath.Extensions;
using System.Text.RegularExpressions;

namespace Pitago.Calculation
{
    public class CalculationProcessor
    {
        public string Process(string math, bool formatResult = true)
        {
            string result = string.Empty;

            // Check if the text is empty or not
            if (string.IsNullOrEmpty(math))
                return string.Empty;

            // Get the letter from  math text
            var varriables = Regex.Matches(math, @"[a-zA-Z]");

            if (varriables.Count == 0) // No varriable found then calculate the string like numerical math
            {
                if(math.Contains('='))
                    result = math.Simplify().Stringize();
                else
                    result = math.EvalNumerical().Stringize();
            }
            else // Varriable found, validate the varriable and calculate, if calculation failed then try simplify the math
            {
                result = math.Simplify().Stringize();
            }

            if (string.IsNullOrEmpty(result)) // Calculation failed, return nothing
                return string.Empty;

            if(formatResult) 
                return FormatResult(math, result);
            else 
                return result;
        }

        private string FormatResult(string math, string result)
        {
            return string.Concat(
                math.EndsWith(' ') ? "" : " ",
                math.Contains('=') ? "=>" : "=",
                " ",
                result
            );
        }
    }
}
