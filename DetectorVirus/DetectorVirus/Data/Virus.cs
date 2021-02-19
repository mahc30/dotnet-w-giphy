using System;
using System.Text;

// Miguel Ángel Hincapié Calle - 000148441
// 18/02/2021

// Un objeto Virus tiene dos campos 
// Name, un string para identificarlo ("Ébola", "Covid19"...)
// Sequence, un arreglo de bytes que es propio de ese virus
namespace DetectorVirus.Data
{
    public class Virus
    {
        private string Name { get; set; }
        private byte[] Sequence { get; }

        // Getter del nombre del virus
        public string getName()
        {
            return Name;
        }

        // Getter de la secuencia del virus
        public byte[] getSequence()
        {
            return Sequence;
        }

        // Constructor
        public Virus(string _name, byte[] _sequence)
        {
            Name = _name;
            Sequence = _sequence;
        }

        // Convierte los bytes a un string legible para humanos
        // Por ejemplo: 0x45 = 'E'
        public string SequenceToString()
        {
            return Encoding.Default.GetString(Sequence);
        }
    }
}