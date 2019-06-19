using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AAY_Transdumper_v2
{
    public abstract class Sounds
    {
        public static SoundPlayer soundSwitchCheck = new SoundPlayer(new Uri("pack://application:,,,/Resources/multi-plier-open-1.wav"));
        public static SoundPlayer soundSwitchUncheck = new SoundPlayer(new Uri("pack://application:,,,/Resources/multi-plier-close-1.wav"));
        public static SoundPlayer startup = new SoundPlayer(new Uri("pack://application:,,,/Resources/AAY audio 2.wav"));
        public static SoundPlayer finishedTest = new SoundPlayer(new Uri("pack://application:,,,/Resources/Mission Clear.wav"));
        public static SoundPlayer Passed = new SoundPlayer(new Uri("pack://application:,,,/Resources/Result.wav"));
        public static SoundPlayer Failed = new SoundPlayer(new Uri("pack://application:,,,/Resources/Result E.wav"));
        public static SoundPlayer closeResult = new SoundPlayer(new Uri("pack://application:,,,/Resources/switch-18.wav"));


        public static void Volume(float vol)
        {
            soundSwitchCheck.Volume(vol);
            soundSwitchUncheck.Volume(vol);
            startup.Volume(vol);
            finishedTest.Volume(vol);
            Passed.Volume(vol);
            Failed.Volume(vol);
            closeResult.Volume(vol);
            Properties.Settings.Default.Volume = vol;
        }

        public static void PlayAudio(SoundPlayer sound)
        {
            soundSwitchCheck.Stop();
            soundSwitchUncheck.Stop();
            startup.Stop();
            finishedTest.Stop();
            Passed.Stop();
            Failed.Stop();
            closeResult.Stop();
            if (Properties.Settings.Default.Sounds)
            {
                sound.Play();
            }
        }

    }
}
