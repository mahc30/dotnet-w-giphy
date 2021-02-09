using System;
using System.Text;

public class Virus
{
	private string Name { get; set; }
    private byte[] Sequence { get; }

    public string getName()
    {
        return Name;
    }

    public byte[] getSequence()
    {
        return Sequence;
    }

    public Virus(string _name, byte[] _sequence)
    {
			Name = _name;
			Sequence = _sequence;
    }

    public string SequenceToString()
    {
        return Encoding.Default.GetString(Sequence);
    }
    public string ToReadableByteArray()
    {
        return string.Join(", ", Sequence);
    }

    public int GetSequenceSize()
    {
        return Sequence.Length;
    }

   
}
