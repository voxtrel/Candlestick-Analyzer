using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Yaniel Gonzalez Velez
namespace Project1
{
    /// <summary>
    /// This class is responsible for defining the extra features of a SmartCandlestick.
    /// It defines the Range, TopPrice, BottomPrice, BodyRange, TopTail, BottomTail, and CandleProperties
    /// which is the dictionary for the patterns.
    /// </summary>
    public class SmartCandlestick : Candlestick
    {

        public decimal Range { get; set; }              // Declare the member for the Range and its get and set methods
        public decimal TopPrice { get; set; }           // Declare the member for the TopPrice and its get and set methods
        public decimal BottomPrice { get; set; }        // Declare the member for the BottomPrice and its get and set methods
        public decimal BodyRange { get; set; }          // Declare the member for the BodyRange and its get and set methods
        public decimal TopTail { get; set; }            // Declare the member for the TopTail and its get and set methods
        public decimal BottomTail { get; set; }         // Declare the member for the BottomTail and its get and set methods
        public Dictionary<string, bool> CandleProperties { get; set; } // Declare the member for the Pattern Dictionary and its get and set methods

        //Defalut Constructor
        public SmartCandlestick() { }

        //Copy Constructor
        public SmartCandlestick(Candlestick cs)
        {
            this.open = cs.open;            // Copy the open from candlestick passed
            this.close = cs.close;          // Copy the close from candlestick passed
            this.high = cs.high;            // Copy the high from candlestick passed 
            this.low = cs.low;              // Copy the low from candlestick passed
            this.volume = cs.volume;        // Copy the volume from candlestick passed
            this.date = cs.date;            // Copy the date from candlestick passed
            ComputeProperties();            // Function call to the function that computes the newly added SmartCandlestcik properties
            ComputePatternProperties();     // Function call to the function that builds the SmartCandlesticks's pattern dictionary
        }

        /// <summary>
        /// This is the SmartCandlestick Constructor which invokes the Candlestick constructor.
        /// This constructor sets all the properties for the SmartCandlestick
        /// </summary>
        /// <param name="rowOfData"></param>        This represents the string that is used by the Candlestick constructor to get the Candlestick properties
        public SmartCandlestick(string rowOfData) : base(rowOfData)
        {
            ComputeProperties();                    // Function call to the function that computes the newly added SmartCandlestcik properties
            ComputePatternProperties();             // Function call to the function that builds the SmartCandlesticks's pattern dictionary
        }

        /// <summary>
        /// This function is responsible for calculating all the SmartCandlestick properties
        /// so that they are only calculated once when the constructor runs.
        /// </summary>
        void ComputeProperties()
        {
            Range = high - low;                     // Compute the Range of the entire Candlestick
            TopPrice = Math.Max(open, close);       // Compute Top Price of the Candlestcik
            BottomPrice = Math.Min(close, open);    // Compute the Bottom Price of the Candlestick
            BodyRange = TopPrice - BottomPrice;     // Compute the Range of the Candlestick's body
            TopTail = high - TopPrice;              // Compute the Range of the Top Tail
            BottomTail = BottomPrice - low;         // Compute the Range of the Bottom Tail 
        }
        /// <summary>
        /// This function is reponsible for creating the SmartCandlestick pattern dictionary
        /// and adding all the patterns and their bool expressions.
        /// </summary>
        void ComputePatternProperties()
        {
            if (CandleProperties == null)                                                         // If the CandleProperties is null then create the dictionary
            {
                CandleProperties = new Dictionary<string, bool>();                                // Create new Dictionary for properties
            }
            CandleProperties.Add("Bullish", close > open);                                      // Bullish when close greater than open
            CandleProperties.Add("Bearish", close < open);                                      // Bearish when close less than open
            CandleProperties.Add("Neutral", close == open);                                     // Neutral when close equal to open
            CandleProperties.Add("Hammer", BodyRange <= (decimal)0.25 * Range                   // BodyRange maximum of 25% of Range
                                            && BodyRange > (decimal)0.15 * Range                // BodyRange mininum of 15% of Range
                                            && (TopTail >= (decimal)0.75 * Range                // TopTail small and BottomTail long
                                            || BottomTail >= (decimal)0.75 * Range));           // Or BottomTail small and TopTail long

            CandleProperties.Add("Marubozu", BodyRange >= (decimal)0.94 * Range);               // BodyRange is minimum of 94% of Range

            CandleProperties.Add("Doji", (BodyRange <= (decimal)0.15 * Range));                 // BodyRange is less than or equal to 15% of Range

            CandleProperties.Add("DragonflyDoji", (BodyRange <= (decimal)0.15 * Range)          // BodyRange is less than or equal ro 15% of Range
                                                      && (BottomTail > (decimal)0.75 * Range)); // BottomTail is the longer than TopTail

            CandleProperties.Add("GravestoneDoji", (BodyRange <= (decimal)0.15 * Range)         // BodyRange is less than or equal ro 15% of Range
                                                       && (TopTail > (decimal)0.75 * Range));   // TopTail is the longer than BottomeTail

        }
    }
}
