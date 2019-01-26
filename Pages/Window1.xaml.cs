//------------------------------------------------------------------------------
// <copyright file="SliderSample.xaml.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Windows;
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


namespace Microsoft.Samples.Kinect.ControlsBasics
{
   
    public partial class Window1 : UserControl
    {
        private bool mediaPlayerIsPlaying = false;
        private bool userIsDraggingSlider = false;

        public Window1()
        {
            this.InitializeComponent();
            RecognizeSpeechAndWriteToConsole();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
            
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

        int music_flag = 0;

        private void _recognizeSpeechAndWriteToConsole_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Text == "OK")
            {
                _recognizer.SpeechRecognized += _recognizeSpeechAndWriteToConsole_SpeechRecognized1;


                //MessageBox.Show(e.Result.Text);
                PromptBuilder builder1 = new PromptBuilder();
                builder1.StartSentence();
                Console.Beep(3000, 500);
                //builder1.AppendText("YES SIR .... ");
                builder1.EndSentence();
                music_flag = 1;
                SpeechSynthesizer synthesizer = new SpeechSynthesizer();
                //synthesizer.Speak(builder1);
                synthesizer.Dispose();
            }
        }

        private void _recognizeSpeechAndWriteToConsole_SpeechRecognized1(object sender, SpeechRecognizedEventArgs e)
        {
            PromptBuilder builder = new PromptBuilder();
            //MessageBox.Show(e.Result.Text);
                if (e.Result.Text == "OPEN MUSIC" & music_flag == 1)
                {
                    //MessageBox.Show(e.Result.Text);
                    music_flag = 0;
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.InitialDirectory = @"d:\music\";
                    openFileDialog.Filter = "Media files (*.mp3;*.mpg;*.mpeg)|*.mp3;*.mpg;*.mpeg|All files (*.*)|*.*";
                    if (openFileDialog.ShowDialog() == true)
                        mePlayer.Source = new Uri(openFileDialog.FileName);
                }
                else if (e.Result.Text == "PLAY" && music_flag == 1)
                {
                    mePlayer.Play();
                    mediaPlayerIsPlaying = true;
                    music_flag = 0;
                }
                else if (e.Result.Text == "PAUSE" && music_flag == 1)
                {
                    mePlayer.Pause();
                    music_flag = 0;
                }
                else if (e.Result.Text == "STOP" && music_flag == 1)
                {
                    mePlayer.Stop();
                    mediaPlayerIsPlaying = false;
                    music_flag = 0;

                }
                else if (e.Result.Text == "UP" && music_flag == 1)
                {
                    mePlayer.Volume += 1;
                   
                }
                else if (e.Result.Text == "DOWN" && music_flag == 1)
                {
                    mePlayer.Volume -= 1;
                   

                }
                else if (e.Result.Text == "NEW" && music_flag == 1)
                {
                    Button_Click(sender, new RoutedEventArgs());
                    listBox1.SelectedIndex = 0;

                    string name = Convert.ToString(listBox1.SelectedValue);
                    string path = "D:\\music\\NEW\\";
                    string path_music = String.Concat(path, name);
                    //string path_music = Convert.ToString(listBox1.SelectedValue);
                    mePlayer.Source = new Uri(path_music);
                    mePlayer.Play();
                    mediaPlayerIsPlaying = true;
                    data.Content = path_music;
                    music_flag = 0;
                }
                else if (e.Result.Text == "NEXT" && music_flag == 1)
                {
                    listBox1.SelectedIndex += 1;

                    string name = Convert.ToString(listBox1.SelectedValue);
                    string path = "D:\\music\\NEW\\";
                    string path_music = String.Concat(path, name);
                    //string path_music = Convert.ToString(listBox1.SelectedValue);
                    mePlayer.Source = new Uri(path_music);
                    mePlayer.Play();
                    data.Content = name;
                    mediaPlayerIsPlaying = true;
                    music_flag = 0;
                }
                else if (e.Result.Text == "PREVIOUS" && music_flag == 1)
                {
                    listBox1.SelectedIndex -= 1;
                    string name = Convert.ToString(listBox1.SelectedValue);
                    string path = "D:\\music\\NEW\\";
                    string path_music = String.Concat(path, name);
                    //string path_music = Convert.ToString(listBox1.SelectedValue);
                    mePlayer.Source = new Uri(path_music);
                    mePlayer.Play();
                    data.Content = name;
                    mediaPlayerIsPlaying = true;
                    music_flag = 0;
                }
                else if (e.Result.Text == "CLOSE ME" && music_flag == 1)
                {

                }

            }
   
        private void timer_Tick(object sender, EventArgs e)
        {
            if ((mePlayer.Source != null) && (mePlayer.NaturalDuration.HasTimeSpan) && (!userIsDraggingSlider))
            {
                sliProgress.Minimum = 0;
                sliProgress.Maximum = mePlayer.NaturalDuration.TimeSpan.TotalSeconds;
                sliProgress.Value = mePlayer.Position.TotalSeconds;
            }
        }

        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Media files (*.mp3;*.mpg;*.mpeg)|*.mp3;*.mpg;*.mpeg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
                mePlayer.Source = new Uri(openFileDialog.FileName);
        }

        private void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (mePlayer != null) && (mePlayer.Source != null);
        }

        private void Play_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mePlayer.Play();
            mediaPlayerIsPlaying = true;
        }

        private void PlaySad_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (mePlayer != null) && (mePlayer.Source != null);
        }

        private void PlaySad_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mePlayer.Play();
            mediaPlayerIsPlaying = true;
        }
        private void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = mediaPlayerIsPlaying;
        }

        private void Pause_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mePlayer.Pause();
        }

        private void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = mediaPlayerIsPlaying;
        }

        private void Stop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mePlayer.Stop();
            mediaPlayerIsPlaying = false;
        }

        private void sliProgress_DragStarted(object sender, DragStartedEventArgs e)
        {
            userIsDraggingSlider = true;
        }

        private void sliProgress_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            userIsDraggingSlider = false;
            mePlayer.Position = TimeSpan.FromSeconds(sliProgress.Value);
        }

        private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblProgressStatus.Text = TimeSpan.FromSeconds(sliProgress.Value).ToString(@"hh\:mm\:ss");
        }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            mePlayer.Volume += (e.Delta > 0) ? 0.1 : -0.1;
        }

        
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            listBox1.Items.Clear();
            String[] dirs = System.IO.Directory.GetFiles("D:\\music\\NEW\\");
           
            int i;

           
            for (i = 0; i < dirs.Length; i++)
            {
                //Console.WriteLine(dirs.Remove(5, 10));
                listBox1.Items.Add(dirs[i].Remove(0,13));
                
            }

            listBox1.SelectedIndex = 0;
            string name = Convert.ToString(listBox1.SelectedValue);
            string path = "D:\\music\\NEW\\";
            string path_music = String.Concat(path,name);
            //MessageBox.Show(path_music);
            mePlayer.Source = new Uri(path_music);
            mePlayer.Play();
            mediaPlayerIsPlaying = true;
            data.Content = name;
            //mePlayer.Source = new Uri("D:/music/IO.mp3/");
            //mePlayer.Play();
            //mediaPlayerIsPlaying = true;

            
        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //string path_music = Convert.ToString(listBox1.SelectedValue);
            //mePlayer.Source = new Uri(path_music);
            //mePlayer.Play();
            //mediaPlayerIsPlaying = true;
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            listBox1.Items.Clear();
            String[] dirs = System.IO.Directory.GetFiles("D:\\music\\NEW\\");
            int i;
            for (i = 0; i < dirs.Length; i++)
            {
                listBox1.Items.Add(dirs[i].Remove(0, 13));

                
            }
            //foreach (string filePath in dirs)
            //{
            //    string[] brokenPath = filePath.Split('/');
            //    listBox1.Items.Add(brokenPath);
            //}
            
            listBox1.SelectedIndex = 0;
            string path_music = Convert.ToString(listBox1.SelectedValue);
            mePlayer.Source = new Uri(path_music);
            mePlayer.Play();
            mediaPlayerIsPlaying = true;



            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            mePlayer.Volume += 0.2;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            mePlayer.Volume -= 0.2;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            listBox1.SelectedIndex += 1;

            string name = Convert.ToString(listBox1.SelectedValue);
            string path = "D:\\music\\NEW\\";
            string path_music = String.Concat(path, name);
            //string path_music = Convert.ToString(listBox1.SelectedValue);
            mePlayer.Source = new Uri(path_music);
            mePlayer.Play();
            data.Content = name;
            mediaPlayerIsPlaying = true;
            music_flag = 0;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            listBox1.SelectedIndex -= 1;
            string name = Convert.ToString(listBox1.SelectedValue);
            string path = "D:\\music\\NEW\\";
            string path_music = String.Concat(path, name);
            //string path_music = Convert.ToString(listBox1.SelectedValue);
            mePlayer.Source = new Uri(path_music);
            mePlayer.Play();
            data.Content = name;
            mediaPlayerIsPlaying = true;
            music_flag = 0;
        }

        

    }
}

