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
    }
}