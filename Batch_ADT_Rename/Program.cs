using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Batch_ADT_Rename
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.Title = ("Batch ADT Renamer");
			Console.WriteLine ("This tool will batch rename all adt's from a_x_y.adt to b_x_y.adt in the current directory.\n\nIt will also rename wdt/wdl files it finds that matches the x name on the adt's\n");

			DirectoryInfo workingdir = new DirectoryInfo (Directory.GetCurrentDirectory ());

			// Get the directory the program is being executed in
			Console.WriteLine ("Working in: " + workingdir + "\n");

			// Find wdl, wdt, and adt files in the workingdir.
			FileInfo[] wdlFiles = workingdir.GetFiles ("*.wdl");
			FileInfo[] wdtFiles = workingdir.GetFiles ("*.wdt");
			FileInfo[] adtFiles = workingdir.GetFiles ("*.adt");

			Console.WriteLine ("Found:\n" + wdlFiles.Length + " wdl file(s)\n" + wdtFiles.Length + " wdt file(s)\n" + adtFiles.Length + " adt file(s)\nin current directory.\n");

			Console.WriteLine ("What do you want to rename the adt's to?\n");
			string newname = "";

			// Make sure that the new name is not empty.
			while (newname == "") {
				newname = Console.ReadLine ();

				if (newname == "") {
					Console.WriteLine ("The new name may not be blank.\n");
				}
			}
			Console.WriteLine ("\n");

			// wdt files
			Console.WriteLine ("Renaming wdt file(s)");
			foreach (FileInfo f in wdtFiles) {
				Console.WriteLine ("Renaming " + f.Name.ToString () + " to " + newname + ".wdt");
				File.Move (f.FullName.ToString (), newname + ".wdt");
			}
			Console.WriteLine ("Done!\n");

			// wdl files
			Console.WriteLine ("Renaming wdl file(s)");
			foreach (FileInfo f in wdlFiles) {
				Console.WriteLine ("Renaming " + f.Name.ToString () + " to " + newname + ".wdl");
				File.Move (f.FullName.ToString (), newname + ".wdl");
			}
			Console.WriteLine ("Done!\n");

			// adt files
			Console.WriteLine ("Renaming adt file(s)");
			foreach (FileInfo f in adtFiles) {
				string name = f.FullName.ToString ();
				string part = "";
				int index = 0;

				int i = 0;

				// Get the _x_y.adt part of the file
				while (i++ < 2) {
					index = name.LastIndexOf ("_");
					if (i == 0) {
						part = name.Substring (index);
					} else {
						part = name.Substring (index) + part;
					}
					name = name.Substring (0, index);
				}

				// Rename the adt file
				Console.WriteLine ("Renaming " + f.Name.ToString () + " to " + newname + part);
				File.Move (f.FullName.ToString (), newname + part);
			}
			Console.WriteLine ("Done!\n");
			Console.WriteLine ("\nFinished!\nPress any key to exit...");
			Console.ReadKey ();
		}
	}
}
