using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCapture
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            voice();
            Console.WriteLine("...DONE...");
        }


        public static void voice()
        {
            SpeechRecognitionEngine recognitionEngine = new SpeechRecognitionEngine(); 
            recognitionEngine.SetInputToDefaultAudioDevice(); 
            recognitionEngine.LoadGrammar(new DictationGrammar()); 

            recognitionEngine.SpeechRecognized += (s, args) => 
            { 
                 foreach (RecognizedWordUnit word in args.Result.Words) 
                 { 
                      Console.Write("{0} ", word.Text); 
                 } 
            }; 
            recognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
        }

        public static void camera()
        {
            using (CvCapture cap = CvCapture.FromCamera(0))
            using (CvWindow w = new CvWindow("OpenCV Example"))
            {
                while (CvWindow.WaitKey(10) < 0)
                {
                    using (IplImage src = cap.QueryFrame())
                    using (IplImage gray = new IplImage(src.Size, BitDepth.U8, 1))
                    using (IplImage dstCanny = new IplImage(src.Size, BitDepth.U8, 1))
                    {
                        src.CvtColor(gray, ColorConversion.BgrToGray);
                        Cv.Canny(gray, dstCanny, 50, 50, ApertureSize.Size3);
                        w.Image = dstCanny;
                    }
                }
            }
        }
    }
}
