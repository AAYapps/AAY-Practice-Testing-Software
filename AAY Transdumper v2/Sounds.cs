using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAY_Transdumper_v2
{
    public abstract class Sounds
    {
        public static System.Media.SoundPlayer soundSwitchCheck = new System.Media.SoundPlayer(Properties.Resources.multi_plier_open_1);
        public static System.Media.SoundPlayer soundSwitchUncheck = new System.Media.SoundPlayer(Properties.Resources.multi_plier_close_1);
        public static System.Media.SoundPlayer startup = new System.Media.SoundPlayer(Properties.Resources.AAY_audio_2);
        public static System.Media.SoundPlayer finishedTest = new System.Media.SoundPlayer(Properties.Resources.Mission_Clear1);
        public static System.Media.SoundPlayer Passed = new System.Media.SoundPlayer(Properties.Resources.Result);
        public static System.Media.SoundPlayer Failed = new System.Media.SoundPlayer(Properties.Resources.Result_E);
        public static System.Media.SoundPlayer closeResult = new System.Media.SoundPlayer(Properties.Resources.switch_18);


        public static void PlayAudio(System.Media.SoundPlayer sound)
        {
            if (Properties.Settings.Default.Sounds)
                sound.Play();
        }
    }
}
