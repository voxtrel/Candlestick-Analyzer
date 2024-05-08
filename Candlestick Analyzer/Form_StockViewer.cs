using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.AxHost;

// Yaniel Gonzalez Velez
namespace Project1
{
    public partial class Form_StockViewer : Form
    {
        // Declare the list to hold all the candlesticks created from the csv file
        List<SmartCandlestick> candlesticks = new List<SmartCandlestick>(1024);

        // Declare the binding list to hold all of the filtered candlesticks
        BindingList<SmartCandlestick> boundedCandlesticks = null;

        // Declare the dictionary that will hold the collection of recognizers
        Dictionary<string, Recognizer> Dictionary_Recognizer;

        /// <summary>
        /// This function is the Constructor for the Form_StockViewer class.
        /// This constructor is called when an instance of the Form_StockViewer class is created.
        /// </summary>
        public Form_StockViewer()
        {
            InitializeComponent();      // Function Call to initialize the form's components.
            initializeRecognizers();    // Function call to fill the collection of recognizers
        }

        /// <summary>
        /// This is the constructor for new form windows used when multiple files are selected.
        /// This constructor is responisble for setting up the new form with the file path they will
        /// display information from, as well as the start and end dates passed from the parent.
        /// </summary>
        /// <param name="stockPath"></param>    This represents the file path that will be used to read the file
        /// <param name="start"></param>        This represents the start date for the form's dateTimePicker_start
        /// <param name="end"></param>          This represents the end date for the forrm's dateTimePicker_end
        public Form_StockViewer(string stockPath, DateTime start, DateTime end)
        {
            InitializeComponent();                  // Function Call to initialize the form's components.
            initializeRecognizers();                // Function call to fill the collection of recognizers

            dateTimePicker_start.Value = start;     // Set the dateTimePicker_start value to the argument passed (start)
            dateTimePicker_end.Value = end;         // Set the dateTimePicker_end value to the argument passed (end)

            candlesticks = GoReadFile(stockPath);   // Function call to GoReadFile function with the file path argument to get the form set up
            FilterCandlesticks();                   // Function call to the function that filters candlesticks based on DateTimePickers
            NormalizeChart();                       // Function call to the function that adjusts the chart y-axis to better represent the data
            DisplayCandlesticks();                  // Function call to the function that displays the data to the Chart

        }


        // Event Handlers--------------------------------------------------------------------


        /// <summary>
        /// This function handles the event for when button_pickStock is clicked.
        /// This function is reponsible for launching the File Dialog to allow the user to select a file
        /// </summary>
        /// <param name="sender"></param>    This represents the object that triggered the event   
        /// <param name="e"></param>         This represents the event-specific information
        private void button_pickStock_Click(object sender, EventArgs e)
        {
            openFileDialog_TickerChooser.ShowDialog();  // Launch the File Dialog to allow the user to select a file
            comboBox_Pattern.Text = "Select a Pattern"; // Display this text to comboBox after selecting stock so it doesnt show previous selected pattern
        }



        /// <summary>
        /// This function handles the event when the user selects one or more files from the openFileDialog.
        /// This function calls several other functions, to read the file(s) selected, create the list of 
        /// candlesticks, filter the candlesticks, normalize the chart, and display them on a chart. 
        /// This function does this for every file selected and displays each on a new form after the first form.
        /// </summary>
        /// <param name="sender"></param>       This represents the object that triggered the event
        /// <param name="e"></param>            This represents the event-specific information
        private void openFileDialog_TickerChooser_FileOk(object sender, CancelEventArgs e)
        {
            DateTime startDate = dateTimePicker_start.Value;                // Set a variable to hold the current date selected as start date
            DateTime endDate = dateTimePicker_end.Value;                    // Set a variable to hold the current date selected as end date

            int numOfFiles = openFileDialog_TickerChooser.FileNames.Count();// Set a variable to hold the number of files selected

            for (int i = 0; i < numOfFiles; ++i)                             // Iterate by the number of files selected
            {
                string pathName = openFileDialog_TickerChooser.FileNames[i]; // Get the file path of the ith file
                string ticker = Path.GetFileNameWithoutExtension(pathName);  // Get the file name of the ith file

                Form_StockViewer form_StockViewer;                           // Create a new form_StockViewer instance 

                if (i == 0)                                                  // If this is the first form do: 
                {
                    form_StockViewer = this;                                 // Set the new form equal to the current form
                    form_StockViewer.Text = "Parent: " + ticker;             // Set the window text displayed to "Parent" and file name 
                    this.GoReadFile();           // Function call to the function that reads file and fills the list of candlesticks
                    this.FilterCandlesticks();   // Function call to the function that filters candlesticks based on DateTimePickers
                    this.NormalizeChart();       // Function call to the function that adjusts the chart y-axis to better represent the data
                    this.DisplayCandlesticks();  // Function call to the function that displays the data to the Chart
                }
                else                                                                        // else do:
                {
                    form_StockViewer = new Form_StockViewer(pathName, startDate, endDate);  // Provide the new form constructor with the file path, the start and end date
                    form_StockViewer.Text = "Child: " + ticker;                             // Set the window text of the new form as "Child" and file name
                }

                form_StockViewer.Show();                                                    // Display the new form created
                form_StockViewer.BringToFront();                                            // Display the new form above the others
            }
        }



        /// <summary>
        /// This function handles the event when button_update is clicked. When triggered, this function 
        /// calls the functions to filter the data based on DateTimePickers, normalize the chart y-axis, 
        /// and diplay the filtered candlesticks to the DataGridView and the Chart.
        /// </summary>
        /// <param name="sender"></param>   This represents the object that triggered the event
        /// <param name="e"></param>        This represents the event-specific information
        private void button_update_Click(object sender, EventArgs e)
        {
            FilterCandlesticks();                   // Function call to the function that filters candlesticks based on DateTimePickers
            NormalizeChart();                       // Function call to the function that adjusts the chart y-axis to better represent the data
            DisplayCandlesticks();                  // Function call to the function that displays the candlesticks
            chart_candlesticks.Annotations.Clear(); // Clear the pattern annotations from the previous set of filtered candlesticks
            comboBox_Pattern.Text = "Select a Pattern"; // Display this text to comboBox after update so it doesnt show previous selected pattern
        }

        // Project 3 Additions---------------------------------------------------------------
        
        /// <summary>
        /// This function is responsible for filling the Dictionary_Recognizer with the entries
        /// of all 14 recognizers.
        /// </summary>
        private void initializeRecognizers()
        {
            // Create the Dictionary_Recognizer to fill it with the recognizers
            Dictionary_Recognizer = new Dictionary<string, Recognizer>
            {
                // Add Bullish recognizer to the dictionary
                { "Bullish", new Recognizer_Bullish() },
                // Add Bearish recognizer to the dictionary
                { "Bearish", new Recognizer_Bearish() },
                // Add Neutral recognizer to the dictionary
                { "Neutral", new Recognizer_Neutral() },
                // Add Marubozu recognizer to the dictionary
                { "Marubozu", new Recognizer_Marubozu() },
                // Add Hammer recognizer to the dictionary
                { "Hammer", new Recognizer_Hammer() },
                // Add Doji recognizer to the dictionary
                { "Doji", new Recognizer_Doji() },
                // Add GravestoneDoji recognizer to the dictionary
                { "GravestoneDoji", new Recognizer_GravestoneDoji() },
                // Add DragonflyDoji recognizer to the dictionary
                { "DragonflyDoji", new Recognizer_DragonflyDoji() },
                // Add BullishEngulfing recognizer to the dictionary
                { "BullishEngulfing", new Recognizer_BullishEngulfing() },
                // Add BearishEngulfing recognizer to the dictionary
                { "BearishEngulfing", new Recognizer_BearishEngulfing() },
                // Add BullishHarami recognizer to the dictionary
                { "BullishHarami", new Recognizer_BullishHarami() },
                // Add BearishHarami recognizer to the dictionary
                { "BearishHarami", new Recognizer_BearishHarami() },
                // Add Peak recognizer to the dictionary
                { "Peak", new Recognizer_Peak() },
                // Add Valley recognizer to the dictionary
                { "Valley", new Recognizer_Valley() }
            };
        }
        


        /// <summary>
        /// This function is responsible using the list of smartcandlesticks passed to call
        /// RecognizeAll on all the recognizers in the recognizer dictionary (MultiPattern).
        /// This will build the all candlesticks' dictionary of patterns with the patterns in 
        /// the recongizers with new multi candlestick patterns.
        /// </summary>
        /// <param name="lscs"></param>
        private void analyzeAll(List<SmartCandlestick> lscs)
        {
            foreach (var recognizer in Dictionary_Recognizer.Values)         // Go through all the recognziers in the recognizer dictionary
            {
                recognizer.RecognizeAll(lscs);                      // Call RecognizeAll for the current recognizer using the list of smart candleticks passed
            }

        }

        /// <summary>
        /// This function is reponsible for adding the candlestick patterns from the recognizers to the 
        /// combo box by iterating through the recognizers and adding the patterns names from the key to it.
        /// </summary>
        private void PopulateComboBox_Pattern()
        {
            foreach (var recognizer in Dictionary_Recognizer)            // Go through all the recognizers in the recognizer Dictionary 
            {
                if (!comboBox_Pattern.Items.Contains(recognizer.Key)) // Check if the combo box has the pattern already
                {
                    comboBox_Pattern.Items.Add(recognizer.Key); // Add the pattern name to the combo box using the key from the dictionary entry 
                }
            }
        }

        /// <summary>
        /// This function is the event handler for when an item is selected in the combo box.
        /// This function is responsible for going through all the filtered candlesticks and 
        /// add annotations to the points in the chart based on whether the corresponding recognizer
        /// recognized the pattern selected or not.
        /// </summary>
        /// <param name="sender"></param>       This represents the object that triggered the event
        /// <param name="e"></param>            This represents the event-specific information
        private void comboBox_Pattern_SelectedIndexChanged(object sender, EventArgs e)
        {
            chart_candlesticks.Annotations.Clear(); // Clear the previous set of annotations from the chart

            string itemSelected = comboBox_Pattern.SelectedItem.ToString(); // Get the pattern name selected from the combobox

            Recognizer selectedPattern = Dictionary_Recognizer[itemSelected]; // Create a temp recognizer for the pattern selected in the combobox
           
            Color[] colors = { Color.Gold, Color.Blue, Color.HotPink, Color.BurlyWood, Color.LightBlue }; // Define an array of colors
            int colorIndex = 0; // Initialize color index

            for (int i = 0; i < boundedCandlesticks.Count; i++)             // Go through all filtered candlesticks
            {
                // If the pattern is in the candlestick dictionary and it was recognized as True by the Recognizer do:
                if (boundedCandlesticks[i].CandleProperties.ContainsKey(itemSelected) && boundedCandlesticks[i].CandleProperties[itemSelected])
                {
                    if(selectedPattern.patternLength == 1)                  // If it is a single candlestick pattern, then do a single annotation
                    {
                        ArrowAnnotation pattern = new ArrowAnnotation();    // Create an arrow annotation 
                        pattern.AnchorDataPoint = chart_candlesticks.Series["Series_OCHL"].Points[i];       // Anchor the annotation to the point at index i
                        pattern.Height = -5;                            // Set the annotation height to -5
                        pattern.ArrowSize = 3;                          // Set the annotation arrow size to 3
                        pattern.Width = 0;                              // Set the annotation width to 0
                        pattern.BackColor = Color.Gold;                 // Set the annotation color to Gold
                        chart_candlesticks.Annotations.Add(pattern);    // Add the annotation to the chart

                    }
                    else // else do:
                    {

                        if (i < selectedPattern.patternLength - 1) { continue; }            // If the index is invalid (lead to out range) go to next iteration

                        for (int j = 0; j < selectedPattern.patternLength; j++)             // iterate for the numebr of candlesticks in the patterns
                        {
                            ArrowAnnotation pattern = new ArrowAnnotation();                // Create an arrow annotation
                            pattern.AnchorDataPoint = chart_candlesticks.Series["Series_OCHL"].Points[i - j];   // Anchor the annotation to the point at index i-J
                            pattern.Height = -5;                                            // Set the annotation height to -5
                            pattern.ArrowSize = 3;                                          // Set the annotation arrow size to 3
                            pattern.Width = 0;                                              // Set the annotation width to 0
                            pattern.BackColor = colors[colorIndex];                         // Set the annotation color to Gold                       
                            chart_candlesticks.Annotations.Add(pattern);                    // Add the annotation to the chart

                            // If it is Peak or Valley pattern and it is the middle index do:
                            if ((j == selectedPattern.patternLength / 2) && (selectedPattern.patternName == "Peak" || selectedPattern.patternName == "Valley"))
                            {
                                TextAnnotation text = new TextAnnotation();               // Create a text Annotation
                                text.Text = selectedPattern.patternName;                  // Give the annotation text the name of the pattern
                                text.ForeColor = Color.Black;                             // Set the text to black
                                text.AnchorDataPoint = chart_candlesticks.Series["Series_OCHL"].Points[i - j];  // anchor to the data point of middle index
                                text.Height = -5;                                         // Set the height of the text annotation to -5
                                text.Alignment = ContentAlignment.TopCenter;              // Allign the text annotation to center
                                chart_candlesticks.Annotations.Add(text);                 // Add the annotation to the chart
                            }

                        }
                        colorIndex = (colorIndex + 1) % colors.Length; // Increment the color index and wrap around if it exceeds the length of the colors array
                    }
                        
                }
            }
            if(chart_candlesticks.Annotations.Count == 0)                       // If there are no annotations, then there is no candlestick with the pattern
            {
                MessageBox.Show("No candlesticks found for this pattern");      // Show an error to the user
            }
        }


        // Void Methods----------------------------------------------------------------------



        /// <summary>
        /// This the void version of GoReadFile method, which takes the filename from the open file dialog,
        /// and calls the method with arguments to read the file obtained, and set the list of candlesticks.
        /// </summary>
        private void GoReadFile()
        {
            string filename = openFileDialog_TickerChooser.FileName;    // use the open file dialog to get the file name
            //this.Text = filename;                                     // set text of the form to the file name received

            // Call the overloading method to read the file selected and return the list of candlesticks
            candlesticks = GoReadFile(filename);                        // candlesticks is the global list that will hold all candlesticks read
        }



        /// <summary>
        /// This is the void version of FilterCandlesticks method, which calls the method with arguments
        /// using the candlestick list, and the DateTimePickers as arguments to obtain a filtered list of
        /// candlesticks to be set into a binding list that can be used to bind to the Chart.
        /// </summary>
        private void FilterCandlesticks()
        { 
            // Call overloading method using the DateTimePicker values and the list of candlesticks
            // Then set the resulting filtered list into a binding list so that it can be bound to the Chart
            boundedCandlesticks = new BindingList<SmartCandlestick>(FilterCandlesticks(candlesticks, dateTimePicker_start.Value, dateTimePicker_end.Value));
            PopulateComboBox_Pattern();     // Function call to function that populates the combo box with all 14 pattern names
        }



        /// <summary>
        /// This function is reponsible for displaying the candlestick data from our binding list onto the Chart.
        /// </summary>
        private void DisplayCandlesticks()
        {
            // Add data to Chart
            chart_candlesticks.DataSource = boundedCandlesticks;                // set the chart Data source to the filtered binding list
            chart_candlesticks.DataBind();                                      // update the chart to display the candlestick data
        }



        /// <summary>
        /// This function adjusts the y-axis of the chart to better represent the candlesticks.
        /// It also handles invalid range inputs. 
        /// </summary>
        private void NormalizeChart()
        {
            // Find if there is data to display and if there isn't throw an exception and return.
            // This try and catch prevents a segmentation falut in the following lines if the selected range has no candlesticks
            try                                                                     // Start of Try and Catch
            {
                if (boundedCandlesticks.Count == 0)                                 // Count == 0 means there are no candlesticks in the list
                {
                    throw new Exception();                                          // Throw Exception                 
                }
            }
            catch (Exception)                                                       // Catch exception thrown
            {
                MessageBox.Show("Invalid range selection. No data to display.\nSelect another range to see more data."); // Show an error message
                return;                                                             // Exit function
            }

            // Adjust Y Axis to start 2% bellow lowest candlestick low and end at 2% above highest candlestick high
            decimal minLow = boundedCandlesticks.Min(cs => cs.low);                                                 // Find the minimum Low from the candlestick list
            chart_candlesticks.ChartAreas["ChartArea_OCHL"].AxisY.Minimum = Math.Floor((double)minLow * 0.98);      // Make the y-axis start at 2% bellow the floor of minimum value found

            decimal maxHigh = boundedCandlesticks.Max(cs => cs.high);                                               // Find the maximum High from the candlestick list
            chart_candlesticks.ChartAreas["ChartArea_OCHL"].AxisY.Maximum = Math.Ceiling((double)maxHigh * 1.02);   // Make the y-axis end at 2% above the ceiling of the maximum value found
        }



        // Overloaded Methods with Arguments and Return Values-------------------------------



        /// <summary>
        /// This is the 2nd version of GoReadFile which takes arguments and returns.
        /// This function reads a csv file passed and returns a list of candlesticks from the data in the file.
        /// </summary>
        /// <param name="filename"></param>         This parameter represents the path for the file to be read
        /// <returns></returns>                     This function returns a lits of candlesticks created from the file passed
        private List<SmartCandlestick> GoReadFile(string filename)
        {   
            // Declare the list that will be returned with the candlesticks read from file 
            List<SmartCandlestick> readCandlesticks = new List<SmartCandlestick>();

            // Reference string used to compare that the first line in the file are the column headers
            string referenceString = "Date,Open,High,Low,Close,Adj Close,Volume";

            // Create an instance of the StreamReader to read the file selected in the file dialog
            using (StreamReader sr = new StreamReader(filename))
            {
                // Read the first line of the csv file
                string line = sr.ReadLine();

                // If the first line matches the column headers in the reference string, continue to read the rest of the file
                if (line == referenceString)
                {
                    // This loop iterates through the given file, reading all the lines until the end of the file
                    while ((line = sr.ReadLine()) != null)               // if line is null, we have reached the end of file
                    {
                        SmartCandlestick cs = new SmartCandlestick(line);     // Create a new candlstick object from the line read
                        readCandlesticks.Add(cs);                        // Add the candlestick to the list of candlesticks
                    }
                }
                else { Text = "Bad File" + filename; } // Indicate that it is a bad file if the first line is not equal to the expected column headers
            }
            analyzeAll(readCandlesticks);                           // function call to add all the new entries that fill the candlesticks dictionaries
            return readCandlesticks;                                // Return the list of candlesticks read
        }



        /// <summary>
        ///  This is the 2nd version of FilterCandlesticks which takes arguments and returns.
        ///  This function is reponsible for filtering the candlestick list passed based on the dates specified in the
        ///  argumets and returns a filtered list of candlesticks.
        /// </summary>
        /// <param name="candlesticks"></param>         This parameter represents the list of all candlesticks to be filtered
        /// <param name="startDate"></param>            This parameter represents the value from a DateTimePicker to be used as the start
        /// <param name="endDate"></param>              This parameter represents the value from a DateTimepicker to be used as the end
        /// <returns></returns>                         This function returns a filtered list of candlesticks 
        private List<SmartCandlestick> FilterCandlesticks(List<SmartCandlestick> candlesticks, DateTime startDate, DateTime endDate)
        {
            chart_candlesticks.Annotations.Clear();                               // Clear the pattern annotations from the previous set of filtered candlesticks
            comboBox_Pattern.Items.Clear();                                       // Clear the previous patterns added to comboBox_Pattern

            // Filter the candlesticks based on the selected date range
            List<SmartCandlestick> filteredCandlesticks = new List<SmartCandlestick>();     // Initializes the list to hold the filtered candlesticks that will be returned
            foreach(SmartCandlestick c in candlesticks.Cast<SmartCandlestick>())  // Go through all the candlestick objects in candlesticks list (As SmartCandlesticks
            {
                if(c.date > endDate) { break; }                                   // Break when we have added all the candlesticks before the endDate
                if(c.date >= startDate) { 
                    filteredCandlesticks.Add(c);                                  // Add the candlesticks that are within the range specified
                }        
            }
            return filteredCandlesticks;                                        // Return the list of filtered candlesticks
        }

       
    }
}
