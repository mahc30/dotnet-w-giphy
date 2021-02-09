using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Analyzer
{
    //Constructor
    public Analyzer()
    {
        Virus_DB = new Virus[5];

        //Initialize DB
        Virus_DB[0] = new Virus("Usama", new byte[] { 0x15, 0x30, 0x15, 0x49 });
        Virus_DB[1] = new Virus("Amtrax", new byte[] { 0x72, 0x72, 0x15, 0x29 });
        Virus_DB[2] = new Virus("éBola", new byte[] { 0x29, 0x32, 0x53, 0x29 });
        Virus_DB[3] = new Virus("AH1N1", new byte[] { 0x72, 0x32, 0x32, 0x20 });
        Virus_DB[4] = new Virus("Covid19", new byte[] { 0x30, 0x25, 0x20, 0x19 });
    }
    private Virus Virus { get; set; }
    private Virus[] Virus_DB { get; set; }

    public Virus GetVirus()
    {
        return Virus;
    }

    

    //Obtiene la secuencia de bytes del archivo seleccionado
    public void loadVirus(string file_path)
    {
        Console.WriteLine("Analyzing: " + file_path);

        try
        {
            Virus = new Virus("Unknown",
            FileReader.readFile(file_path));
        }
        catch (Exception)
        {

            throw new Exception("File Not Found");
        }

        Console.WriteLine("Byte Array is: " + Virus.ToReadableByteArray());
    }
    public Task<Virus[]> analyze()
    {
        Stack<Virus> found_virus = searchVirus();
        return Task.FromResult(found_virus.ToArray());
    }

    private Stack<Virus> searchVirus()
    {
        byte[] posible_virus = Virus.getSequence();
        byte[] known_sequence;


        Stack<Virus> found_virus = new Stack<Virus>();

        for (int i = 0; i < Virus_DB.Length; i++)
        {
            known_sequence = Virus_DB[i].getSequence();

            if (ByteArrayRocks.Search(posible_virus, known_sequence) != -1)
            {
               found_virus.Push(Virus_DB[i]);
            }

        }
        return found_virus;
    }
}
static class ByteArrayRocks
{
    public static int Search(byte[] src, byte[] pattern)
    {
        int c = src.Length - pattern.Length + 1;
        int j;
        for (int i = 0; i < c; i++)
        {
            if (src[i] != pattern[0]) continue;
            for (j = pattern.Length - 1; j >= 1 && src[i + j] == pattern[j]; j--) ;
            if (j == 0) return i;
        }
        return -1;
    }

}