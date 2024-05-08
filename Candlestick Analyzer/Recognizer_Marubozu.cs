using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    /// <summary>
    /// This is the recognizer class for the Marubozu pattern, which uses the recognizer abstract class as a template
    /// </summary>
    internal class Recognizer_Marubozu : Recognizer
    {
        // This invokes the constructor from the recognizer abstract class, initializing it with the pattern name and length
        public Recognizer_Marubozu() : base("Marubozu", 1)
        {
        }
        /// <summary>
        /// This function is responsible for evaluating the bool value for the pattern that determines if the pattern
        /// is present or not.
        /// </summary>
        /// <param name="lscs"></param>         This represents the list of candlesticks that is passed
        /// <param name="currIndex"></param>    This represents the current index to be used for evaluating the pattern
        /// <returns></returns>                 This function returns a bool value for whether the pattern is present or not
        public override bool Recognize(List<SmartCandlestick> lscs, int currIndex)
        {
            if (currIndex < patternLength - 1) { return false; }                            // If this is an invalid index return false

            SmartCandlestick cs = lscs[currIndex];                                          // Create a smartcandlestick with the current index

            if (cs.CandleProperties.TryGetValue(patternName, out bool value))               // If this has been evaluated before then:
            {
                return value;                                                               // Return the value from the dictionary
            }
            else
            {
                bool cv = (cs.BodyRange >= (decimal)0.94 * cs.Range);                       // Else, calculate the pattern
                cs.CandleProperties.Add(patternName, cv);                                   // Add the result to the dictionary
                return cv;                                                                  // Return the result
            }
        }
    }
}
