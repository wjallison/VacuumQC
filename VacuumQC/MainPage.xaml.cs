using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml.Media.Imaging;
//using 

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace VacuumQC
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public List<vacProfile> profiles = new List<vacProfile>();
        //open current, open press, open flow
        //half current, ...
        //closed ...
        public vacProfile selectedProfile;
        public Test test;
        public Report report;

        public List<double> tempList = new List<double>();

        public List<double> presentTestCurrent = new List<double>();
        public List<double> presentTestFlow = new List<double>();
        public List<double> presentTestPressure = new List<double>();

        public MainPage()
        {
            this.InitializeComponent();
            backButton.IsEnabled = false;
            //Pull images

            //Load values from USB drive
            InitProfiles();

            //Add items to list of vacuum types

        }

        public async void InitProfiles()
        {
            var removeableDevices = KnownFolders.RemovableDevices;
            var externalDrives = await removeableDevices.GetFoldersAsync();
            var usbDrive = externalDrives.Single(e => e.DisplayName.Contains("USB"));
            StorageFile appConfig = await usbDrive.CreateFileAsync(
                "Vacuum Profiles.jpg", CreationCollisionOption.OpenIfExists);

            using (StreamReader reader = new StreamReader(await appConfig.OpenStreamForReadAsync()))
            {
                //var data = await reader.ReadToEndAsync();
                
                while (!reader.EndOfStream)
                {
                    List<List<double>> ls = new List<List<double>>();
                    string n = reader.ReadLineAsync().Result;
                    for (int i = 0; i < 3; i++)
                    {
                        ls.Add(new List<double>());
                        string s = reader.ReadLineAsync().Result;
                        string[] ss = s.Split(',');
                        for(int j = 0; j < 3; j++)
                        {
                            ls[i].Add(Convert.ToDouble(ss[j]));
                        }
                    }
                    profiles.Add(new vacProfile(n, ls));
                }                
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            //mainFlipView.SelectedIndex++;
            switch (mainFlipView.SelectedIndex)
            {
                case 0:
                    selectedProfile = profiles[selectedModel.SelectedIndex];
                    test = new Test(snTextBox.Text, selectedProfile);
                    mainFlipView.SelectedIndex++;
                    break;
                case 1:
                    mainFlipView.SelectedIndex++;
                    nextButton.IsEnabled = false;
                    RunTest();
                    mainFlipView.SelectedIndex++;
                    nextButton.IsEnabled = true;
                    break;
                case 3:
                    mainFlipView.SelectedIndex++;
                    nextButton.IsEnabled = false;
                    RunTest();
                    mainFlipView.SelectedIndex++;
                    nextButton.IsEnabled = true;
                    break;
                case 5:
                    mainFlipView.SelectedIndex++;
                    nextButton.IsEnabled = false;
                    RunTest();
                    mainFlipView.SelectedIndex++;
                    //nextButton.IsEnabled = true;
                    report = new Report(test.serial, test, selectedProfile);
                    reportBlock.Text = report.report.ToString();
                    break;
            }
        }

        private void FlipViewItem_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (e.Delta.Translation.X != 0)
            {
                e.Handled = true;
            }
        }

        public void RunTest()
        {

        }
    }
    public struct vacProfile
    {
        public string name;
        //public double minOpenCurrent;
        //public double maxOpenCurrent;
        //public double minHalfCurrent;
        //public double maxHalfCurrent;
        //public double minClosedCurrent;
        //public double maxClosedCurrent;
        //public double minOpenFlow;
        //public double maxOpenFlow;
        //public double 
        public List<List<double>> minMax;

        public vacProfile(string model, List<double> mm)
        {
            name = model;
            minMax = new List<List<double>>();
            for(int i = 0; i < 3; i++)
            {
                minMax.Add(new List<double>());
                for (int j = 0; j < 3; j++)
                {
                    minMax[i].Add(mm[3 * i + j]);
                }
            }
        }
        public vacProfile(string model, List<List<double>> mmm)
        {
            name = model;
            minMax = new List<List<double>>();
            for (int i = 0; i < 3; i++)
            {
                minMax.Add(new List<double>());
                for (int j = 0; j < 6; j++)
                {
                    minMax[i].Add(mmm[i][j]);
                }
            }
        }
    }
}
