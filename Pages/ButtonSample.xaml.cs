//------------------------------------------------------------------------------
// <copyright file="ButtonSample.xaml.cs" company="Microsoft">
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
    using System.Threading.Tasks;
    using System.Windows.Media;
    using System.Speech.Recognition;
    using System.Speech.Synthesis;

    /// <summary>
    /// Interaction logic for ButtonSample
    /// </summary>
    /// 


    public partial class ButtonSample : UserControl
    {
        public SerialPort myport;
        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonSample" /> class.
        /// </summary>
        public ButtonSample()
        {
            this.InitializeComponent();
            init();
            RecognizeSpeechAndWriteToConsole();
           
        }

        private SpeechRecognitionEngine _recognizer = null;
        //private ManualResetEvent manualResetEvent = null;
        private void RecognizeSpeechAndWriteToConsole()
        {
            _recognizer = new SpeechRecognitionEngine();
            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder("FAN ON")));
            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder("FAN OFF")));
            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder("Bulb on")));
            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder("Bulb off")));
            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder("TV on")));
            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder("TV off")));
            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder("PC on")));
            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder("PC off")));
            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder("BADHU BANDH")));
            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder("BADHU CHALU")));// load a "test" grammar
            //_recognizer.LoadGrammar(new DictationGrammar());

            _recognizer.SpeechRecognized += _recognizeSpeechAndWriteToConsole_SpeechRecognized; // if speech is recognized, call the specified method
            //_recognizer.SpeechRecognitionRejected += _recognizeSpeechAndWriteToConsole_SpeechRecognitionRejected; // if recognized speech is rejected, call the specified method
            _recognizer.SetInputToDefaultAudioDevice(); // set the input to the default audio device
            _recognizer.RecognizeAsync(RecognizeMode.Multiple); // recognize speech asynchronous
        }

        private void _recognizeSpeechAndWriteToConsole_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            //MessageBox.Show("DATA IS:" + e.Result.Text);
            PromptBuilder builder = new PromptBuilder();
            if (e.Result.Text == "FAN ON")
            {

                myport.WriteLine("A");
                btn1.Content = "OFF";
                led1_flag = 1;
                btn1.Background = new SolidColorBrush(Colors.Red);
            }
            else if (e.Result.Text == "FAN OFF")
            {

                myport.WriteLine("B");
                btn1.Content = "ON";
                led1_flag = 0;
                btn1.Background = new SolidColorBrush(Colors.RoyalBlue);
            }
            else if (e.Result.Text == "Bulb on")
            {

                myport.WriteLine("C");
                btn3.Content = "OFF";
                led3_flag = 1;
                btn3.Background = new SolidColorBrush(Colors.Red);
            }
            else if (e.Result.Text == "Bulb off")
            {
                myport.WriteLine("D");
                btn3.Content = "ON";
                led3_flag = 0;
                btn3.Background = new SolidColorBrush(Colors.RoyalBlue);
            }
            else if (e.Result.Text == "TV on")
            {
                myport.WriteLine("E");
                btn2.Content = "OFF";
                led2_flag = 1;
                btn2.Background = new SolidColorBrush(Colors.Red);
            }
            else if (e.Result.Text == "TV off")
            {

                myport.WriteLine("F");
                btn2.Content = "ON";
                led2_flag = 0;
                btn2.Background = new SolidColorBrush(Colors.RoyalBlue);
            }
            else if (e.Result.Text == "PC on")
            {

                myport.WriteLine("G");
                btn4.Content = "OFF";
                led4_flag = 1;
                btn4.Background = new SolidColorBrush(Colors.Red);
            }
            else if (e.Result.Text == "PC off")
            {

                myport.WriteLine("H");
                btn4.Content = "ON";
                led4_flag = 0;
                btn4.Background = new SolidColorBrush(Colors.RoyalBlue);
            }
            else if (e.Result.Text == "BADHU BANDH")
            {
                myport.WriteLine("B");
                myport.WriteLine("D");
                myport.WriteLine("F");
                myport.WriteLine("H");
                btn4.Content = "ON";
                led4_flag = 0;
                btn3.Content = "ON";
                led3_flag = 0;
                btn2.Content = "ON";
                led2_flag = 0;
                btn1.Content = "ON";
                led1_flag = 0;
                btn1.Background = new SolidColorBrush(Colors.RoyalBlue);
                btn2.Background = new SolidColorBrush(Colors.RoyalBlue);
                btn3.Background = new SolidColorBrush(Colors.RoyalBlue);
                btn4.Background = new SolidColorBrush(Colors.RoyalBlue);
            }

            else if (e.Result.Text == "BADHU CHALU")
            {
myport.WriteLine("A");
                myport.WriteLine("C");
                myport.WriteLine("E");
                myport.WriteLine("G");
                btn4.Content = "OFF";
                led4_flag = 1;
                btn3.Content = "OFF";
                led3_flag = 1;
                btn2.Content = "OFF";
                led2_flag = 1;
                btn1.Content = "OFF";
                led1_flag = 1;
                btn1.Background = new SolidColorBrush(Colors.Red);
                btn2.Background = new SolidColorBrush(Colors.Red);
                btn3.Background = new SolidColorBrush(Colors.Red);
                btn4.Background = new SolidColorBrush(Colors.Red);
            }

        }
        private void init()
        {
            
                myport = new SerialPort();
                myport.BaudRate = 9600;
                myport.PortName = "COM3";
               myport.Open();

               
            
            
            
        }

        public int led1_flag = 0;
        public int led2_flag =0;
        public int led3_flag = 0;
        public int led4_flag = 0;

        private void btn1_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            
            if (led1_flag == 0)
            {
                myport.WriteLine("A");
                btn1.Content = "OFF";
                led1_flag = 1;
                btn1.Background = new SolidColorBrush(Colors.Red);

            }
            else
            {
                myport.WriteLine("B");
                btn1.Content = "ON";
                led1_flag = 0;
                btn1.Background = new SolidColorBrush(Colors.RoyalBlue);
            }
           
        }
        private void btn2_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (led2_flag == 0)
            {
                myport.WriteLine("E");
                btn2.Content = "OFF";
                led2_flag = 1;
                btn2.Background = new SolidColorBrush(Colors.Red);
            }
            else
            {
                myport.WriteLine("F");
                btn2.Content = "ON";
                led2_flag = 0;
                btn2.Background = new SolidColorBrush(Colors.RoyalBlue);
            }
        }
        private void btn3_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (led3_flag == 0)
            {
                myport.WriteLine("C");
                btn3.Content = "OFF";
                led3_flag = 1;
                btn3.Background = new SolidColorBrush(Colors.Red);
            }
            else
            {
                myport.WriteLine("D");
                btn3.Content = "ON";
                led3_flag = 0;
                btn3.Background = new SolidColorBrush(Colors.RoyalBlue);
            }
        }
        private void btn4_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (led4_flag == 0)
            {
                myport.WriteLine("G");
                btn4.Content = "OFF";
                led4_flag = 1;
                btn4.Background = new SolidColorBrush(Colors.Red);
            }
            else
            {
                myport.WriteLine("H");
                btn4.Content = "ON";
                led4_flag = 0;
                btn4.Background = new SolidColorBrush(Colors.RoyalBlue);
            }
        }

        private void UserControl_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            myport.Close();
        }

      

        
    }
}
