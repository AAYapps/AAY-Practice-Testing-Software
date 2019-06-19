using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AAY_Transdumper_v2
{
    public class SoundPlayer
    {
        localsoundplayer localsound;
        public SoundPlayer(Uri Path, bool playloop = false)
        {
            playLoopBool = playloop;
            localsound = new localsoundplayer(playloop);
            if (Path.AbsolutePath != "")
            {
                int i;
                localsound.LoadFile(false, out i, Path);
                localsound.Play();
                localsound.Stop();
            }
        }

        #region local sound
        public void loadFile(Uri targetFile)
        {
            int i;
            localsound.LoadFile(false, out i, targetFile);
        }

        float volume = 0f;
        public void Volume(float vol)
        {
            if (volume != vol)
            {
                volume = vol;
                localsound.Volume(vol);
            }
        }

        public void Play()
        {
            localsound.Play();
        }

        bool playLoopBool = false;

        public void pause()
        {
            localsound.pause();
        }

        public void resume()
        {
            localsound.resume();
        }

        public void Stop()
        {
            playLoopBool = false;
            localsound.Stop();
        }

        public double gettime()
        {
            return double.Parse(localsound.currentTime(false));
        }

        public bool gettimeend()
        {
            return (gettime() == double.Parse(localsound.currentTime(false, true)));
        }

        public void settime(double durration)
        {
            localsound.playBackPosition(durration);
        }

        public bool getPlayState()
        {
            return localsound.getPlayState();
        }

        public bool getPlayStatePaused()
        {
            return localsound.getPlayStatePaused();
        }

        public bool getPlayStateStopped()
        {
            return localsound.getPlayStateStopped();
        }

        public void playSpeed(float rate)
        {
            if (getPlayState())
            {
                localsound.playBackRate(rate);
            }
        }

        public void remove()
        {
            localsound.Stop();
            localsound.remove();
        }
        #endregion
    }
}