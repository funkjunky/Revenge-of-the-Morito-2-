using Microsoft.Xna.Framework.Audio;

namespace Morito
{
    public class SoundEngine
    {
        public SoundEngine() { }

        public AudioEngine audioEngine;
        public WaveBank waveBank;
        public SoundBank soundBank;
        public Cue trackCue;
    }
}
