//------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.Samples.Kinect.ControlsBasics
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using Microsoft.Kinect;
    using Microsoft.Kinect.Wpf.Controls;
    using Microsoft.Samples.Kinect.ControlsBasics.DataModel;
    using System.IO.Ports;
    using System.Speech.Recognition;
    using System.Speech.Synthesis;
    using System.Diagnostics;
    using System.Globalization;

   
    /// <summary>
    /// Interaction logic for MainWindow
    /// </summary>
    public partial class MainWindow
    {
        public SerialPort myport;
        /// <summary>
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class. 
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();

            KinectRegion.SetKinectRegion(this, kinectRegion);

            App app = ((App)Application.Current);
            app.KinectRegion = kinectRegion;

            // Use the default sensor
            this.kinectRegion.KinectSensor = KinectSensor.GetDefault();

            //// Add in display content
            var sampleDataSource = SampleDataSource.GetGroup("Group-1");
            this.itemsControl.ItemsSource = sampleDataSource;
            //RecognizeSpeechAndWriteToConsoleMain1();

            System.Windows.Threading.DispatcherTimer myDispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            myDispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 100); // 100 Milliseconds  
            myDispatcherTimer.Tick += myDispatcherTimer_Tick;
            myDispatcherTimer.Start();  
        }

      

        void myDispatcherTimer_Tick(object sender, EventArgs e)
        {
            tbk_clock.Text = DateTime.Now.ToString("hh:mm:ss");
        }  

        private SpeechRecognitionEngine _recognizer1 = null;
        //private ManualResetEvent manualResetEvent = null;
        private void RecognizeSpeechAndWriteToConsoleMain1()
        {
            _recognizer1 = new SpeechRecognitionEngine();
            _recognizer1.LoadGrammar(new Grammar(new GrammarBuilder("MUSIC PLAYER")));
            _recognizer1.LoadGrammar(new Grammar(new GrammarBuilder("HOME AUTOMATION")));
            _recognizer1.LoadGrammar(new Grammar(new GrammarBuilder("BHAAI")));
            _recognizer1.LoadGrammar(new Grammar(new GrammarBuilder("TASK")));
            _recognizer1.LoadGrammar(new Grammar(new GrammarBuilder("HELLO")));
           // _recognizer1.LoadGrammar(new Grammar(new GrammarBuilder("OPEN YOUTUBE")));// load a "test" grammar
            //_recognizer.LoadGrammar(new DictationGrammar());

            _recognizer1.SpeechRecognized += _recognizeSpeechAndWriteToConsole_SpeechRecognized_Main1; // if speech is recognized, call the specified method
            //_recognizer.SpeechRecognitionRejected += _recognizeSpeechAndWriteToConsole_SpeechRecognitionRejected; // if recognized speech is rejected, call the specified method
            _recognizer1.SetInputToDefaultAudioDevice(); // set the input to the default audio device
            _recognizer1.RecognizeAsync(RecognizeMode.Multiple); // recognize speech asynchronous
        }

      
        /// <summary>
        /// Handle a button click from the wrap panel.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void ButtonClick(object sender, RoutedEventArgs e)
        {            
            var button = (Button)e.OriginalSource;
            SampleDataItem sampleDataItem = button.DataContext as SampleDataItem;
           
            if (sampleDataItem != null && sampleDataItem.NavigationPage != null)
            {
                backButton.Visibility = System.Windows.Visibility.Visible;
                navigationRegion.Content = Activator.CreateInstance(sampleDataItem.NavigationPage);
                
            }
            else
            {
                var selectionDisplay = new SelectionDisplay(button.Content as string);
                this.kinectRegionGrid.Children.Add(selectionDisplay);
                
                // Selection dialog covers the entire interact-able area, so the current press interaction
                // should be completed. Otherwise hover capture will allow the button to be clicked again within
                // the same interaction (even whilst no longer visible).
                selectionDisplay.Focus();

                // Since the selection dialog covers the entire interact-able area, we should also complete
                // the current interaction of all other pointers.  This prevents other users interacting with elements
                // that are no longer visible.
                this.kinectRegion.InputPointerManager.CompleteGestures();

                e.Handled = true;
            }
        }

        /// <summary>
        /// Handle the back button click.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void GoBack(object sender, RoutedEventArgs e)
        {
            backButton.Visibility = System.Windows.Visibility.Hidden;
            navigationRegion.Content = this.kinectRegionGrid;
            init();
        }


        private void init()
        {

            myport = new SerialPort();
            myport.BaudRate = 9600;
            myport.PortName = "COM3";
            myport.Close();



        }
        int main_flag = 1;
        //private SpeechRecognitionEngine _recognizer = null;
        private void ControlsBasicsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            PromptBuilder builder1 = new PromptBuilder();
            builder1.StartSentence();
            builder1.AppendText("Welcome to Basic Human Artificial Intelligence");
            builder1.EndSentence();
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            //synthesizer.Speak(builder1);
            synthesizer.Dispose();

            RecognizeSpeechAndWriteToConsoleMain1();
        }
        
        private void _recognizeSpeechAndWriteToConsole_SpeechRecognized_Main1(object sender, SpeechRecognizedEventArgs e)
        {
            string itemContent = string.Format(
                                    CultureInfo.CurrentCulture,
                                    "Item Content: {0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}",
                                    "Curabitur class aliquam vestibulum nam curae maecenas sed integer cras phasellus suspendisse quisque donec dis praesent accumsan bibendum pellentesque condimentum adipiscing etiam consequat vivamus dictumst aliquam duis convallis scelerisque est parturient ullamcorper aliquet fusce suspendisse nunc hac eleifend amet blandit facilisi condimentum commodo scelerisque faucibus aenean ullamcorper ante mauris dignissim consectetuer nullam lorem vestibulum habitant conubia elementum pellentesque morbi facilisis arcu sollicitudin diam cubilia aptent vestibulum auctor eget dapibus pellentesque inceptos leo egestas interdum nulla consectetuer suspendisse adipiscing pellentesque proin lobortis sollicitudin augue elit mus congue fermentum parturient fringilla euismod feugiat");

            //MessageBox.Show("DATA IS:" + e.Result.Text);
            PromptBuilder builder1 = new PromptBuilder();
            QNS.Text = e.Result.Text;
            if (e.Result.Text == "MUSIC PLAYER" && main_flag == 1)
            {                
                SampleDataItem sampleDataItem =  new SampleDataItem(
                       "Group-1-Item-5",
                       "MUSIC PLAYER",
                       string.Empty,
                       null,
                       "MUSIC PLAYER",
                       itemContent,
                       null,
                       typeof(Window1));
                main_flag = 0;
                if (sampleDataItem != null && sampleDataItem.NavigationPage != null)
                {
                    backButton.Visibility = System.Windows.Visibility.Visible;
                    navigationRegion.Content = Activator.CreateInstance(sampleDataItem.NavigationPage);
                }
               
                
            }
            else if (e.Result.Text == "TASK" && main_flag == 0)
            {
                SampleDataItem sampleDataItem = new SampleDataItem(
                        "Group-1-Item-2",
                        "REMINDER",
                        string.Empty,
                        null,
                        "CheckBox and RadioButton controls",
                        itemContent,
                        null,
                        typeof(Page1));
                main_flag = 0;
                if (sampleDataItem != null && sampleDataItem.NavigationPage != null)
                {
                    backButton.Visibility = System.Windows.Visibility.Visible;
                    navigationRegion.Content = Activator.CreateInstance(sampleDataItem.NavigationPage);
                }


            }
            else if (e.Result.Text == "HELLO")
            {

                
                builder1.StartSentence();
               // builder1.AppendText("Hello sir ...");
                builder1.EndSentence();
                SpeechSynthesizer synthesizer = new SpeechSynthesizer();
                synthesizer.Speak(builder1);
                synthesizer.Dispose();


            }
            else if (e.Result.Text == "HOME AUTOMATION" && main_flag == 0)
            {
                 SampleDataItem sampleDataItem = new SampleDataItem (
                        "Group-1-Item-1",
                        "HOME AUTOMATION",
                        string.Empty,
                        null,
                        "Several types of buttons with custom styles",
                        itemContent,
                        null,
                        typeof(ButtonSample));
                 main_flag = 0;
                 if (sampleDataItem != null && sampleDataItem.NavigationPage != null)
                {
                    backButton.Visibility = System.Windows.Visibility.Visible;
                    navigationRegion.Content = Activator.CreateInstance(sampleDataItem.NavigationPage);
                }
               
            }

            else if (e.Result.Text == "OPEN YOUTUBE" && main_flag == 0)
            {
                main_flag = 0;
                Process.Start("chrome.exe", "http:\\www.YouTube.com");
            }
        }
        
    }
}
