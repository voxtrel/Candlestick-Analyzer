using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Yaniel Gonzalez Velez
namespace Project1
{
    /// <summary>
    /// This class is responsible for defining the object for the candlesticks.
    /// It defines members for open, high, low, close, volume, and date.
    /// </summary>
    public class Candlestick
    {
        // Declare the member varibles of the candlestick class
        public decimal open { get; set; }       // Declare the member for Open with its get and set methods
        public decimal high { get; set; }       // Declare the member for High with its get and set methods
        public decimal low { get; set; }        // Declare the member for Low with its get and set methods
        public decimal close { get; set; }      // Declare the member for Close with its get and set methods
        public ulong volume { get; set; }       // Declare the member for Volume with its get and set methods
        public DateTime date { get; set; }      // Declare the member for Date with its get and set methods

        //Default Constructor
        public Candlestick() { }

        //Copy Constructor
        public Candlestick(Candlestick copy) {
            this.open = copy.open;          // Copy the open from candlestick passed
            this.high = copy.high;          // Copy the high from candlestick passed
            this.low = copy.low;            // Copy the low from candlestick passed
            this.close = copy.close;        // Copy the close from candlestick passed
            this.volume = copy.volume;      // Copy the volume from candlestick passed
            this.date = copy.date;          // Copy the date from candlestick passed
        }

        /// <summary>
        /// This method takes the data read from the csv file to create the candlestick objects
        /// </summary>
        /// <param name="rowOfData"></param>        // This represents the line read from the file
        public Candlestick(string rowOfData)
        {
            // Set the separators and use them to split the line into the sub strings of the columns
            char[] separators = new char[] { ',', ' ', '"' };                                   // set the separators
            string[] subs = rowOfData.Split(separators, StringSplitOptions.RemoveEmptyEntries); // use separators to split into sub strings

            string dateString = subs[0];                        // Use the first sub string to get the date 

            date = DateTime.Parse(dateString);                  // turn the string into a DateTime type to set the date class member
            
            decimal temp;                                       // temp variable used to set the values for the class members
            bool success = decimal.TryParse(subs[1], out temp); // turn the second sub string into a decimal and 
            if (success) open = temp;                           // set it to open class member
            
            success = decimal.TryParse(subs[2], out temp);      // turn the third sub string into a decimal and 
            if (success) high = temp;                           // set it to high class member

            success = decimal.TryParse(subs[3], out temp);      // turn the fourth sub string into a decimal and 
            if (success) low = temp;                            // set it to low class member
            
            success = decimal.TryParse(subs[4], out temp);      // turn the fifth sub string into a decimal and
            if (success) close = temp;                          // set it to close class member

            // We will not work with adjusted close so we skip the sixth sub string

            // Volumes in the csv files are large values so both Volume and tempVolume are initialized as long integers
            ulong tempVolume;       //temp variable used to set the valuee for the volume member

            success = ulong.TryParse(subs[6], out tempVolume);  // turn the seventh sub string into a long integer and
            if (success) volume = tempVolume;                   // set it to volume class member

        }
    }
}