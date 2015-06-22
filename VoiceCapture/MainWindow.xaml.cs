using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VoiceCapture
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SpeechSynthesizer reader;
       
        SpeechRecognitionEngine recognitionEngine;

        public MainWindow()
        {
            InitializeComponent();

            reader = new SpeechSynthesizer();

            recognitionEngine = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
            
            

            
            
        }

        private void recognitionEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            MessageBox.Show("Good");
            richtextbox1.AppendText("good too");
            richtextbox1.AppendText("Recognized text:  " + e.Result.Text);
        }

        private void btnToggleListening_Click(object sender, RoutedEventArgs e)
        {
            if (btnToggleListening.IsChecked == true)
            {
                MessageBox.Show("btnToggleListening_Click true", "Message");
                recognitionEngine.LoadGrammar(new DictationGrammar());
               // recognitionEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recognitionEngine_SpeechRecognized);
                recognitionEngine.SetInputToDefaultAudioDevice();
                //recognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
                recognitionEngine.SetInputToWaveFile(@"C:\Users\us1\Documents\opencv.wav");

                recognitionEngine.SpeechRecognized += (s, args) =>
                {
                    foreach (RecognizedWordUnit word in args.Result.Words)
                    {
                        //MessageBox.Show(word.Text, "............Message");
                        richtextbox1.AppendText("   "+ word.Text);
                    }
                };
                recognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
            }

            else
            {
                MessageBox.Show("btnToggleListening_Click false", "Message");
                recognitionEngine.RecognizeAsyncStop();
            }
        }

        private void speechRecognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            lblDemo.Content = e.Result.Text;
            if (e.Result.Words.Count == 2)
            {
                string command = e.Result.Words[0].Text.ToLower();
                string value = e.Result.Words[1].Text.ToLower();
                switch (command)
                {
                    case "weight":
                        FontWeightConverter weightConverter = new FontWeightConverter();
                        lblDemo.FontWeight = (FontWeight)weightConverter.ConvertFromString(value);
                        break;
                    case "color":
                        lblDemo.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(value));
                        break;
                    case "size":
                        switch (value)
                        {
                            case "small":
                                lblDemo.FontSize = 12;
                                break;
                            case "medium":
                                lblDemo.FontSize = 24;
                                break;
                            case "large":
                                lblDemo.FontSize = 48;
                                break;
                        }
                        break;
                }
            }
        }

        


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var text = new TextRange(richtextbox1.Document.ContentStart, richtextbox1.Document.ContentEnd).Text;
            reader.Dispose();
            if (text != "")
            {
                MessageBox.Show(text);
                reader = new SpeechSynthesizer();
                reader.SpeakAsync(text);

                reader.SpeakCompleted += new EventHandler<SpeakCompletedEventArgs>(reader_SpeakCompleted);
            }
            else
            {
                MessageBox.Show("Please Enter some Text in the Texy Box", "Message");
            }
        }

        void reader_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            MessageBox.Show("Completed reading", "Message");
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            var text = new TextRange(richtextbox1.Document.ContentStart, richtextbox1.Document.ContentEnd).Text;
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Command invoked: Open");
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Command invoked: Save");
        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
















        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            recognitionEngine.Dispose();
        }

        private void btnToggleListening_Checked(object sender, RoutedEventArgs e)
        {

        }

        
    }
}
