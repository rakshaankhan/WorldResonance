using static PlayerInstrument;

public static class NoteExtensions
{

    public static string ToArrowString(this Note note)
    {
        switch (note)
        {
            case Note._:
            return " ";
            case Note.A:
            return "↑"; // Arrow for A
            case Note.B:
            return "↓"; // Arrow for B
            case Note.C:
            return "←"; // Arrow for C
            case Note.D:
            return "→"; // Arrow for D
            default:
            return note.ToString(); // Fallback to default enum name
        }
    }
}
