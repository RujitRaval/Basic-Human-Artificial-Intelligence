

namespace Microsoft.Samples.Kinect.ControlsBasics
{
    using System;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Threading;
    using Microsoft.Win32;
    using System.Windows.Controls;
    using Microsoft.Kinect;
    using Microsoft.Kinect.Wpf.Controls;
    using Microsoft.Samples.Kinect.ControlsBasics.DataModel;
    using System.IO.Ports;
    using System.Threading.Tasks;
    using System.Windows.Media;
    using System.Speech.Recognition;
    using System.Speech.Synthesis;
    using System.Data.Linq;
    using System.Linq;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Xceed.Wpf.Toolkit;
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : UserControl
    {
        private kinectdataEntities context = new kinectdataEntities();

        public Page1()
        {
            this.InitializeComponent();
            GetListData(); 
        }


        private SpeechRecognitionEngine _recognizer = null;

        private void RecognizeSpeechAndWriteToConsole()
        {
            _recognizer = new SpeechRecognitionEngine();
            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder("OPEN MUSIC")));
            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder("PAUSE")));
            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder("PLAY")));
            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder("STOP")));
            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder("DOWN")));
            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder("UP")));
            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder("NEW")));
            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder("NEXT")));
            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder("PREVIOUS")));
            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder("OK")));
            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder("CLOSE ME")));

            // load a "test" grammar
            //_recognizer.LoadGrammar(new DictationGrammar());

            _recognizer.SpeechRecognized += _recognizeSpeechAndWriteToConsole_SpeechRecognized; // if speech is recognized, call the specified method
            //_recognizer.SpeechRecognitionRejected += _recognizeSpeechAndWriteToConsole_SpeechRecognitionRejected; // if recognized speech is rejected, call the specified method
            _recognizer.SetInputToDefaultAudioDevice(); // set the input to the default audio device
            _recognizer.RecognizeAsync(RecognizeMode.Multiple); // recognize speech asynchronous
        }

        private void _recognizeSpeechAndWriteToConsole_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            PromptBuilder builder1 = new PromptBuilder();
            builder1.StartSentence();
            builder1.AppendText("What do you want me to remind?");
            builder1.EndSentence();
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            synthesizer.Speak(builder1);
            synthesizer.Dispose();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Eventdata eve = new Eventdata();
            eve.@event = txtEvent.Text;
            eve.eventmsg = txtDes.Text;
            eve.eventdate = Convert.ToDateTime(datePicker1.Text);
            eve.eventtime = Convert.ToDateTime(timipicker1.Text);
            //eve.eventdate =Convert.ToDateTime(Convert.ToDateTime(dtpDate.Text).ToShortDateString());
            //eve.eventtime = Convert.ToDateTime(Convert.ToDateTime(dtpTime.Text).ToShortDateString());
            //this._context.Events.Add(eve);
            //this._context.Events.AddObject(eve);
            //this._context.SaveChanges();

            context.Eventdatas.Add(eve);
            context.SaveChanges();
            System.Windows.MessageBox.Show("Record saved successfully.");
            GetListData();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Imported.Visibility = System.Windows.Visibility.Visible;
            kinectdataEntities con = new kinectdataEntities();      
            var cDate = DateTime.Now.Date;
            List<Eventdata> todayList = con.Eventdatas.Where(t => t.eventdate == cDate).ToList();
           

            foreach (var item in todayList)
            {
                PromptBuilder builder1 = new PromptBuilder();
                builder1.StartSentence();
                builder1.AppendText(item.@event);
                builder1.EndSentence();
                SpeechSynthesizer synthesizer = new SpeechSynthesizer();
                synthesizer.Speak(builder1);
                synthesizer.Dispose();
            }
        }

        private void GetListData()
        {
            kinectdataEntities con = new kinectdataEntities();
            List<Eventdata> list = con.Eventdatas.ToList();
            Imported.ItemsSource = list;
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            kinectdataEntities con = new kinectdataEntities();
            Eventdata eventdataRow = Imported.SelectedItem as Eventdata;
            var events = (from p in con.Eventdatas
                            where p.Id == eventdataRow.Id
                            select p).Single();
            con.Eventdatas.Remove(events);
            con.SaveChanges();
            System.Windows.MessageBox.Show("Record deleted successfully.");
            GetListData();
        }     
    
    }

    public class EventDataBase
    {
        public string Id { get; set; }
        public string eventdate { get; set; }
        public string eventtime { get; set; }
        public string @event { get; set;}
        public string eventmsg { get; set; }
    }
}
