using System;

namespace NeeqDMIs.Music
{
    /// <summary>
    /// Enumeration of all the absolute notes.
    /// </summary>
    public enum AbsNotes
    {
        C,
        sC,
        D,
        sD,
        E,
        F,
        sF,
        G,
        sG,
        A,
        sA,
        B,
        NaN
    }

    public static class AbsNotesMethods
    {
        public static bool IsSharp(this AbsNotes note)
        {
            if (note.ToString().Contains("s"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Converts a note into the note name in standard notation, without "s" (substituted eventually with a "#").
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        public static string ToStandardString(this AbsNotes note)
        {
            string temp = note.ToString();

            if (temp.Contains("s"))
            {
                temp = temp.Remove(0, 1);
                temp += "#";
            }

            return temp;
        }

        public static MidiNotes ToMidiNote(this AbsNotes note, int octave)
        {
            return (MidiNotes)Enum.Parse(typeof(MidiNotes), note.ToString() + octave.ToString(), true);
        }
    }
}
