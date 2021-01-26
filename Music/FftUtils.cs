using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeeqDMIs.Music
{
    public static class FftUtils
    {
        public static double GetFftBucket(MidiNotes midiNote, int sampleRate, int fftResolution)
        {
            int maxFreq = sampleRate / 2;
            return fftResolution * midiNote.GetFrequency() / maxFreq;
        }
    }
}
