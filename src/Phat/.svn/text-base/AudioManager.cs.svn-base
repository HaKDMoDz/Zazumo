using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if !MONO
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
#endif

namespace Phat
{
    public class AudioManager
    {       
        public Boolean IsMuted { get; set; }

        private static AudioManager _current;

        public static AudioManager Current
        {
            get { return _current; }
        }

        static AudioManager()
        {
            _current = new AudioManager();
        }

        public void PlaySong(Object song)
        {
            #if !MONO
            if (!MediaPlayer.GameHasControl)
                return;

            if (IsMuted)
                return;

            MediaPlayer.Play((Song)song);
            #endif
        }

        public void Stop()
        {
            #if !MONO
            if (!MediaPlayer.GameHasControl)
                return;
            
            MediaPlayer.Stop();
            #endif
        }

        public void Pause()
        {
            #if !MONO
            if (!MediaPlayer.GameHasControl)
                return;
                        
            MediaPlayer.Pause();
            #endif
        }

        public void Resume()
        {
            #if !MONO
            if (!MediaPlayer.GameHasControl)
                return;
            
            if (IsMuted)
                return;

            MediaPlayer.Resume();
            #endif
        }

        public void PlaySoundEffectInstance(Object soundEffect)
        {
            #if !MONO
            if (!MediaPlayer.GameHasControl)
                return;
            
            if (IsMuted)
                return;

            ((SoundEffectInstance)soundEffect).Play();
            #endif
        }

        public void PlaySoundEffect(Object soundEffect)
        {
            #if !MONO
            if (!MediaPlayer.GameHasControl)
                return;

            if (IsMuted)
                return;

            ((SoundEffect)soundEffect).Play();
            #endif
        }

        public void ToggleAudio()
        {
            IsMuted = !IsMuted;
        }
    }
}
