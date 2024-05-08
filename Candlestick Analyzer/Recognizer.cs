using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    /// <summary>
    /// This is the abstract class that will be used as the template for all recognizers of all 14 patterns.
    /// From this class, the recognizers will have a base from which to build upon to calculate thier patterns.
    /// </summary>
    public abstract class Recognizer
    {
        public string patternName { get; set; }     // Declare the member to hold the pattern name with get and set methods
        public int patternLength { get; set; }      // Declare the memebr to hold the pattern length with get and set methods

        /// <summary>
        /// This is the constructor for the recognizer classes, which sets them up with the pattern name and length
        /// </summary>
        /// <param name="pn"></param>               // This represents the pattern name that will be passed
        /// <param name="pl"></param>               // This represents the pattern length that will be passed
        public Recognizer(string pn, int pl)
        {
            patternName = pn;                       // Set the value for the pattern name using the parameter passed
            patternLength = pl;                     // Set the value for the pattern length using the parameter passed
        }

        /// <summary>
        /// This is the recognizer function that takes the list of candlesticks and an index to recognize
        /// whether the pattern is present or not using a bool value. Since each recognizer will evaluate a
        /// different pattern, their Recognize function will not be the same, which is the reason why this
        /// function is abstract and will be defined by the Recognizer_XXX
        /// </summary>
        /// <param name="lscs"></param>             This represents a list of smart candlesticks passed
        /// <param name="index"></param>            This represents the index of the smart candlestick to be evaluated
        /// <returns></returns>                     This function returns a bool value which is True if present and False if not
        public abstract bool Recognize(List<SmartCandlestick> lscs, int index);

        /// <summary>
        /// This function is responsible for going through a list of candlesticks and calling recognize for
        /// for all candlesticks so that they we can build the pattern dictionary for that pattern with only
        /// one call of this function. Since this function is the same for every recognizer, it is not abstract
        /// so it is only defined here.
        /// </summary>
        /// <param name="lscs"></param>
        public void RecognizeAll(List<SmartCandlestick> lscs)
        {
            for(int i = 0; i < lscs.Count(); i++)               // This for-loop goes through all the candlesticks in the list passed
            {
                Recognize(lscs, i);                             // Call Recognize with the list of candlesticks and the current index
            }
        }
    }
}
