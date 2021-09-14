using System;
using System.Collections.Generic;
using System.Linq;

namespace NeeqDMIs.Music
{
    public static class MidiNotesUtils
    {
        public static List<MidiNotes> GetAllMidiNotesList()
        {
            return Enum.GetValues(typeof(MidiNotes)).Cast<MidiNotes>().ToList();
        }

        public static AbsNotes StandardStringToAbsNote(string str)
        {
            if (str.Contains("#"))
            {
                str = str.Remove(str.Length - 1);
                str = "s" + str;
            }

            var ret = (AbsNotes)Enum.Parse(typeof(AbsNotes), str, true);
            return ret;
        }
    }
}