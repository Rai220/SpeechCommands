using Microsoft.Speech.AudioFormat;
using Microsoft.Speech.Recognition;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Text;
//using Microsoft.Kinect;
using System.ComponentModel;
using System.Speech;
using System.Speech.Synthesis;
using WindowsInput;
using System.Diagnostics;
using System;
using System.Security.Permissions;
using System.Text.RegularExpressions;

namespace KinectMicrophone
{
    public partial class mainForm : Form
    {
        private string grammarsr = "";

        private SpeechRecognitionEngine sre;
        
        private BackgroundWorker bw = new BackgroundWorker();

        private Stream s;

        private SpeechSynthesizer synth = new SpeechSynthesizer();
        
        /** Коды регионов */
        private System.Collections.Generic.IEnumerable<String> lines;

        public mainForm()
        {
            InitializeComponent();
            lines = File.ReadAllLines("codes.txt");            
        }

        private void speech(String text)
        {
            synth.Speak(text);
        }
        
        private void activateButton_Click(object sender, EventArgs e)
        {
            try
            {
                activateButton.Enabled = false;

                Thread thread = new Thread(new ThreadStart(startRecognition));
                thread.Start();
            }
            catch (Exception ex)
            {
                log("" + ex);
            }
        }

        public void UnhandledThreadExceptionHandler(object sender, ThreadExceptionEventArgs e)
        {
            try
            {
                Exception ex = (Exception)e.Exception;

                MessageBox.Show("Whoops! Please contact the developers with the following"
                      + " information:\n\n" + ex.Message + ex.StackTrace,
                      "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                Application.Exit();
            }
        }

        void startRecognition()
        {
            try
            {
                try
                {
                    RecognizerInfo ri = GetRecognizer();
                    if (ri != null)
                    {
                        var g = new Grammar("car.xml");
                        //string srgs = KinectMicrophone.Properties.Resources.car;
                        //byte[] byteArray = Encoding.UTF8.GetBytes( srgs );
                        //MemoryStream stream = new MemoryStream( byteArray );
                        //var g = new Grammar(stream);
                        

                        sre = new SpeechRecognitionEngine(ri.Id);
                        //SpeechRecognitionEngine sre = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("RU"));
                        try
                        {
                            sre.LoadGrammar(g);
                            //sre.LoadGrammarAsync(g);
                        }
                        catch (Exception ex)
                        {
                            log("" + ex);
                        }
                        sre.SpeechRecognized += SreSpeechRecognized;
                        sre.SpeechHypothesized += SreSpeechHypothesized;
                        sre.SpeechRecognitionRejected += SreSpeechRecognitionRejected;
                        sre.SetInputToDefaultAudioDevice();

                        //sre.SetInputToDefaultAudioDevice();

                        sre.RecognizeAsync(RecognizeMode.Multiple);
                        log("Распознавание запущено");
                    }
                    else
                    {
                        log("Не найден модуль распознавания!");
                    }
                }
                catch (Exception ex)
                {
                    log("Ошибка запуска микрофона " + ex);
                }
            }
            catch (Exception ex2)
            {
                log("" + ex2);
            }
        }


        private void formClosing(object sender, FormClosingEventArgs e)
        {
            if (sre != null)
            {
                sre.RecognizeAsyncStop();
                sre = null;
            }

            if (s != null)
            {
                s.Close();
            }
        }

        void SreSpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            recTextBox.BeginInvoke(
                (MethodInvoker)delegate { recTextBox.Text = "- " + e.Result.Text + " (" + e.Result.Confidence + ")";  });            
        }

        void SreSpeechHypothesized(object sender, SpeechHypothesizedEventArgs e)
        {
            recTextBox.BeginInvoke(
                (MethodInvoker)delegate { recTextBox.Text = "? " + e.Result.Text + " (" + e.Result.Confidence + ")";  });
        }

        void SreSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            recTextBox.BeginInvoke(
                (MethodInvoker)delegate { recTextBox.Text = "! " + e.Result.Text + " (" + e.Result.Confidence + ")";  });
            
            if (e.Result.Confidence >= 0.85)
            {                              
                log(e.Result.Text);
                processICarDS(e.Result.Text);
            }            
        }

        void processICarDS(String text)
        {
            if (text.StartsWith("что за регион"))
            {
                text = text.Replace("что за регион ", "");
                string[] parts = text.Split(' ');
                string result = "";
                foreach (string p in parts) {                    
                    switch (p)
                    {
                        case "ноль":
                            result += 0;
                            break;
                        case "один":
                            result += 1;
                            break;
                        case "два":
                            result += 2;
                            break;
                        case "три":
                            result += 3;
                            break;
                        case "четыре":
                            result += 4;
                            break;
                        case "пять":
                            result += 5;
                            break;
                        case "шесть":
                            result += 6;
                            break;
                        case "семь":
                            result += 7;
                            break;
                        case "восемь":
                            result += 8;
                            break;
                        case "девять":
                            result += 9;
                            break;
                    }
                }

                string region =  "Регион не найден";
                Regex rgx = new Regex("[0-9,]");
                foreach (string line in lines) {
                    if (line.Contains(result)) {
                        string newline = rgx.Replace(line, "");
                        region = "" + result + " это " + newline;                        
                    }
                }
                log("Распознан регион: " + region);
                speech(region);
            }

            /*
            if (text.Contains("главное меню"))
            {
                InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_M);
            }
            if (text.Contains("музыку"))
            {
                InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_P);
            }
            if (text.Contains("следующий трек"))
            {
                InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_L);
            }
            if (text.Contains("предыдущий трек"))
            {
                InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_K);
            }
            if (text.Contains("ночной режим") || text.Contains("дневной режим"))
            {
                InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_Q);
            }
            if (text.Contains("визуализацию"))
            {
                InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_V);
            }
            if (text.Contains("навигатор"))
            {
                InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_N);
            }*/
        }

        private void log(String text)
        {
            logContainer.BeginInvoke(
                (MethodInvoker)delegate { logContainer.Text = text + "\r\n" + logContainer.Text; });
        }

        private static RecognizerInfo GetRecognizer()
        {            
            foreach (RecognizerInfo recognizer in SpeechRecognitionEngine.InstalledRecognizers())
            {
                if (recognizer.Id.Equals("SR_MS_ru-RU_TELE_11.0"))
                {
                    return recognizer;
                }
            }

            return null;
        }
    }
}
