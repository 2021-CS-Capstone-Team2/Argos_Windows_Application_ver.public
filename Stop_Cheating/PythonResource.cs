using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.IO.Directory;
using System.Threading;
using System.Diagnostics;

namespace Stop_Cheating
{
    class PythonResource
    {
        public void CreateDir()
        {
            string dir = @"C:\ArgosAjou";
            if (File.Exists(@"C:\ArgosAjou\eyetracking0530.exe"))
            {
                Console.WriteLine("이미 존재함");
            }
            else
            {
                if (!Exists(dir))
                {
                    CreateDirectory(dir);
                    Console.WriteLine("폴더 만들기");
                }    
                //video
                string temp = Path.Combine(dir, "eyetracking0530.exe");

                using (FileStream fsDst = new FileStream(temp, FileMode.CreateNew, FileAccess.Write))
                {
                    byte[] bytes = Properties.Resources.GetVideoExe();

                    fsDst.Write(bytes, 0, bytes.Length);
                }

                //audio

                if (!File.Exists(@"C:\ArgosAjou\speech_recog.exe"))
                {
                    temp = Path.Combine(dir, "speech_recog.exe");

                    using (FileStream fsDst = new FileStream(temp, FileMode.CreateNew, FileAccess.Write))
                    {
                        byte[] bytes = Properties.Resources.GetAudioExe();

                        fsDst.Write(bytes, 0, bytes.Length);
                    }
                }
                else
                    Console.WriteLine("audio 이미 존재");
               

            }

        }

        public void CreateDat()
        {
            if (!File.Exists(".\\shape_predictor_68_face_landmarks.dat"))
            {
                string temp = Path.Combine(Directory.GetCurrentDirectory(), "shape_predictor_68_face_landmarks.dat");


                using (FileStream fsDst = new FileStream(temp, FileMode.CreateNew, FileAccess.Write))
                {
                    byte[] bytes = Properties.Resources.GetVideoDat();

                    fsDst.Write(bytes, 0, bytes.Length);
                }
            }
            else
            {
                Console.WriteLine("dat 이미 존재");
            }
        }

        public void CreateDat2()
        {
            if (!File.Exists(".\\shape_predictor_5_face_landmarks.dat"))
            {
                string temp = Path.Combine(Directory.GetCurrentDirectory(), "shape_predictor_5_face_landmarks.dat");


                using (FileStream fsDst = new FileStream(temp, FileMode.CreateNew, FileAccess.Write))
                {
                    byte[] bytes = Properties.Resources.GetVideoDat2();

                    fsDst.Write(bytes, 0, bytes.Length);
                }
            }
            else
            {
                Console.WriteLine("dat2 이미 존재");
            }
        }

        public void CreateDat3()
        {
            if (!File.Exists(".\\mmod_human_face_detector.dat"))
            {
                string temp = Path.Combine(Directory.GetCurrentDirectory(), "mmod_human_face_detector.dat");


                using (FileStream fsDst = new FileStream(temp, FileMode.CreateNew, FileAccess.Write))
                {
                    byte[] bytes = Properties.Resources.GetVideoDat3();

                    fsDst.Write(bytes, 0, bytes.Length);
                }
            }
            else
            {
                Console.WriteLine("dat3 이미 존재");
            }
        }

        public void CreateDat4()
        {
            if (!File.Exists(".\\dlib_face_recognition_resnet_model_v1.dat"))
            {
                string temp = Path.Combine(Directory.GetCurrentDirectory(), "dlib_face_recognition_resnet_model_v1.dat");


                using (FileStream fsDst = new FileStream(temp, FileMode.CreateNew, FileAccess.Write))
                {
                    byte[] bytes = Properties.Resources.GetVideoDat4();

                    fsDst.Write(bytes, 0, bytes.Length);
                }
            }
            else
            {
                Console.WriteLine("dat4 이미 존재");
            }
        }

        /*
           internal static byte[] VideoExe
        {
            get
            {
                object obj = ResourceManager.GetObject("eyetracking0526", resourceCulture);
                return ((byte[])(obj));
            }
        }
        public static byte[] GetVideoExe()
        {
            return VideoExe;
        }

        internal static byte[] VideoDat
        {
            get
            {
                object obj = ResourceManager.GetObject("shape_predictor_68_face_landmarks", resourceCulture);
                return ((byte[])(obj));
            }
        }
        public static byte[] GetVideoDat()
        {
            return VideoDat;
        }

        internal static byte[] VideoDat2
        {
            get
            {
                object obj = ResourceManager.GetObject("shape_predictor_5_face_landmarks", resourceCulture);
                return ((byte[])(obj));
            }
        }
        public static byte[] GetVideoDat2()
        {
            return VideoDat2;
        }

        internal static byte[] VideoDat3
        {
            get
            {
                object obj = ResourceManager.GetObject("mmod_human_face_detector", resourceCulture);
                return ((byte[])(obj));
            }
        }
        public static byte[] GetVideoDat3()
        {
            return VideoDat3;
        }

        internal static byte[] VideoDat4
        {
            get
            {
                object obj = ResourceManager.GetObject("dlib_face_recognition_resnet_model_v1", resourceCulture);
                return ((byte[])(obj));
            }
        }
        public static byte[] GetVideoDat4()
        {
            return VideoDat4;
        }

        internal static byte[] AudioExe
        {
            get
            {
                object obj = ResourceManager.GetObject("speech_recog", resourceCulture);
                return ((byte[])(obj));
            }
        }
        public static byte[] GetAudioExe()
        {
            return AudioExe;
        }
         */

    }
}
