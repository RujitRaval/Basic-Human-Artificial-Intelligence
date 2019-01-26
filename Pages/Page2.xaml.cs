

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
    using System.Timers;
    using System.Data.Entity;
    //using Xceed.Wpf.Toolkit;
   
    public partial class Page2 : UserControl
    {
        private kinectdataEntities context = new kinectdataEntities();
        int AlarmFlag = 0;
        
        public Page2()
        {
            InitializeComponent();
            GetListData();
            StopAlarm.Visibility = System.Windows.Visibility.Hidden; 
            System.Timers.Timer myTimer = new System.Timers.Timer();
            // Tell the timer what to do when it elapses
            myTimer.Elapsed += new ElapsedEventHandler(myEvent);
            // Set it to go off every five seconds
            myTimer.Interval = 5000;
            // And start it        
            myTimer.Enabled = true;

            // Implement a call with the right signature for events going off
            
        }

       


        private void myEvent(object source, ElapsedEventArgs e) 
        {

            string ctime = DateTime.Now.ToString("HH:mm"); 
            kinectdataEntities con = new kinectdataEntities();
          // Alarm alarmdataRow = Imported1.SelectedItem as Alarm;
           var alarmD = (from p in con.Alarms                         
                         select p.AlarmTime).ToList();
           //MessageBox.Show(ctime.ToString());
           if (alarmD != null && alarmD.Count > 0)
           {
               foreach (var item in alarmD)
               {
                   string altime = Convert.ToString(item);
                   string alTime = altime.Substring(11, 5);
                   string curtime = DateTime.Now.ToString("HH:mm");
                   //MessageBox.Show(alTime+curtime);
                   //  MessageBox.Show(curtime);
                   int x1 = string.Compare(curtime, alTime);
                   string x = Convert.ToString(x1);
                   // MessageBox.Show(x);
                   //x = "0";
                   //x1 = 0;
                   //if (x1 == 0)
                   //{
                   //    //StopAlarm.Visibility = System.Windows.Visibility.Visible; 
                   //    if (AlarmFlag == 1)
                   //    {
                   //        for (int i = 300; i < 3000; i += 100)
                   //        {
                   //            Console.Beep(i, 1000);
                   //        }
                   //        AlarmFlag = 0;
                   //    }
                   //}
               }
           }
            //MessageBox.Show(alarmD);
         
                //if (AlarmFlag == 0 && x == "0")
            
                //{
                    
                   
                //    for (int i = 300; i < 3000; i += 100)
                //    {
                //        Console.Beep(i, 1000);
                       
                //    }
                //    //Console.Beep(5000, 1000); 
                    
                //}
            
             
            }


        
               
         

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            kinectdataEntities con = new kinectdataEntities();
            Alarm alarmdataRow = Imported1.SelectedItem as Alarm;
            var alarmD = (from p in con.Alarms
                          where p.Id == alarmdataRow.Id
                          select p).Single();
            con.Alarms.Remove(alarmD);
            con.SaveChanges();
            System.Windows.MessageBox.Show("Record deleted successfully.");
            GetListData();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            Alarm alr = new Alarm();
            if (chckbox1.IsChecked == true)
            {
                alr.Repeat = true;
            }

            else
            {
                alr.Repeat = false;
            }
            alr.AlarmTime = Convert.ToDateTime(timipicker1.Text);

            context.Alarms.Add(alr);
            context.SaveChanges();
            System.Windows.MessageBox.Show("Record saved successfully.");
            GetListData();

        }

        private void GetListData()
        {
            kinectdataEntities con = new kinectdataEntities();
            List<Alarm> list = con.Alarms.ToList();
            Imported1.ItemsSource = list;
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AlarmFlag = 1;
        }



    }
}
