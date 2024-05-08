using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    /// <summary>
    /// This is the recognizer class for the Valley pattern, which uses the recognizer abstract class as a template
    /// </summary>
    internal class Recognizer_Valley : Recognizer
    {
        // This invokes the constructor from the recognizer abstract class, initializing it with the pattern name and length
        public Recognizer_Valley() : base("Valley", 3)
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

            SmartCandlestick prevPrevC = lscs[currIndex - 2];                               // Create a smartcandlestick with the index 2 before current
            SmartCandlestick prevC = lscs[currIndex - 1];                                   // Create a smartcandlestick with the index before current
            SmartCandlestick currC = lscs[currIndex];                                       // Create a smartcandlestick with the current index

            if (currC.CandleProperties.TryGetValue(patternName, out bool value))            // If this has been evaluated before then:
            {
                return value;                                                               // Return the value from the dictionary
            }
            else
            {
                bool isPrevCLowest = prevC.low < prevPrevC.low && prevC.low < currC.low;    // Evaluate the pattern
                currC.CandleProperties.Add(patternName, isPrevCLowest);                     // Add the result to the dictionary
                return isPrevCLowest;                                                       // Return result
            }


        }
    }
}
