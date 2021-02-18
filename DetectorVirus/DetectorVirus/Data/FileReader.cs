using System;
using System.IO;

// Miguel Ángel Hincapié Calle - 000148441
// 18/02/2021

// Clase para manejo de archivos
// No necesita constructor porque para este caso solo cuenta con un método estático
public class FileReader
{

	// Lee un archivo local como un arreglo de bytes
	// Retorna un array de bytes
	public static byte[] readFile(string path) {
		
		byte[] file_bytes;

		try
		{
			file_bytes = File.ReadAllBytes(path);
		}
        catch (Exception)
        {
			file_bytes = new byte[0];
			throw;
        }

		return file_bytes;
	}
}
