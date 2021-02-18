using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

// Miguel Ángel Hincapié Calle - 000148441
// 18/02/2021

// Clase Analyzer se encarga de almacenar/obtener la base de datos de virus
// Además de la lógica de comparar secuencias de bytes para identificar virus
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

    // Objeto Virus para guardar la secuencia de bytes del archivo leido
    private Virus Possible_Virus { get; set; } 

    // Base de datos de virus conocidos
    private Virus[] Virus_DB { get; set; }

    public Virus GetVirus()
    {
        return Virus;
    }

    //Inicializa el Campo Virus con la secuencia de bytes obtenida del archivo cargado
    public void loadVirus(string file_path)
    {
        try
        {
            Possible_Virus = new Virus("Unknown",
            FileReader.readFile(file_path));
        }
        catch (Exception)
        {
            throw new Exception("File Not Found");
        }
    }

    // Compara las secuencias de bytes de los virus de la base de datos
    // con la secuencia de bytes obtenida del archivo anteriormente cargado
    // Si encuentra un virus lo agrega a una lista con todos los virus encontrados
    // Luego convierte esta lista en un Task
    // El Task es necesario para poder hacer render desde la página web (cosas del Framework)
    public Task<Virus[]> analyze()
    {
        byte[] posible_virus = Possible_Virus.getSequence();
        byte[] known_sequence;
        Stack<Virus> found_virus = new Stack<Virus>();

        // Recorremos la base de datos de virus
        for (int i = 0; i < Virus_DB.Length; i++)
        {
            known_sequence = Virus_DB[i].getSequence();

            // Para cada virus de la base de datos, comprobamos si esa secuencia está contenida en la del archivo
            if (ByteArrayRocks.Search(posible_virus, known_sequence) != -1)
            {
               found_virus.Push(Virus_DB[i]);
            }

        }

        return Task.FromResult(found_virus.ToArray());
    }
}

// Clase de Helpers para manipular arrays de bytes
static class ByteArrayRocks
{
    // Encuentra una secuencia de bytes contenida dentro de otra basado en Pattern Matching
    // Si encuentra la secuencia retorna la posición en que se encontró, de lo contrario retorna -1
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