using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    /// <summary>
    /// This is the recognizer class for the Bullish Harami pattern, which uses the recognizer abstract class as a template
    /// </summary>
    internal class Recognizer_BullishHarami : Recognizer
    {
        // This invokes the constructor from the recognizer abstract class, initializing it with the pattern name and length
        public Recognizer_BullishHarami() : base("BullishHarami", 2)
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
            if (currIndex < patternLength - 1) { return false; }                                    // If this is an invalid index return false

            SmartCandlestick prevC = lscs[currIndex - 1];                                           // Create a smartcandlestick with the index before current
            SmartCandlestick currC = lscs[currIndex];                                               // Create a smartcandlestick with the current index

            if (currC.CandleProperties.TryGetValue(patternName, out bool value))                    // If this has been evaluated before then:
            {
                return value;                                                                       // Return the value from the dictionary
            }
            else
            {
                bool isPrevBearish = prevC.close < prevC.open;                                       // Check if previous candlestick is bearish
                bool isCurrBullish = currC.close > currC.open;                                       // Check if current candlestick is bullish
                bool isCurrBodyContained = (currC.close < prevC.open) && (currC.open > prevC.close); // Check if current candlestick is within the previous

                currC.CandleProperties.Add(patternName, isPrevBearish && isCurrBullish && isCurrBodyContained);     // Add the result to the dictionary
                // Check if the current candlestick is bullish and the current candlestick is bearish with previous being the engulfing body
                return isPrevBearish && isCurrBullish && isCurrBodyContained;
            }

        }
    }
}
