using AngouriMath.Extensions;
using System.Text.RegularExpressions;

namespace Pitago.Calculation
{
    public class CalculationProcessor
    {
        private const string ArrowSign = "=>";
        private const string EqualSign = "=";

        public string Process(string math, bool formatResult = true)
        {
            try
            {
                string result = string.Empty;

                // Check if the text is empty or not
                if (string.IsNullOrEmpty(math))
                    return string.Empty;

                // Get the letter from  math text
                var varriables = Regex.Matches(math, @"[a-zA-Z]");

                if (varriables.Count == 0) // No varriable found then calculate the string like numerical math
                {
                    if (math.Contains('='))
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

                if (formatResult)
                    return FormatResult(math, result);
                else
                    return result;
            }
            catch
            {
                return String.Empty;
            }
        }

        private string FormatResult(string math, string result)
        {
            return string.Concat(
                math.EndsWith(' ') ? "" : " ",
                math.Contains(EqualSign) ? ArrowSign : EqualSign,
                " ",
                result
            );
        }

        public bool IsLineCalculated(string line)
        {
            return line.Contains(ArrowSign) ||
                line.Contains(EqualSign) && line.EvalBoolean() == AngouriMath.Entity.Boolean.True;
        }

        private string GetOriginalMath(string text)
        {
            if (text.Contains(ArrowSign))
                return text.Substring(0, text.IndexOf(ArrowSign)).Trim();
            else if (text.Contains(EqualSign))
                return text.Substring(0, text.IndexOf(EqualSign)).Trim();
            else
                return text.Trim();
        }

        public string CalculateLine(string line, bool recalculate = true)
        {
            if (IsLineCalculated(line))
            {
                if(recalculate)
                    return Process(GetOriginalMath(line));
                return line;
            }
            else
                return Process(line);
        }
    }

}
