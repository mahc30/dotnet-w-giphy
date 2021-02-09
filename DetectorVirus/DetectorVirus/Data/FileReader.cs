using System;
using System.IO;

/// <summary>
/// Summary description for Class1
/// </summary>
public class FileReader
{
	public FileReader()
	{
		//
		// TODO: Add constructor logic here
		// No need )?
	}

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
