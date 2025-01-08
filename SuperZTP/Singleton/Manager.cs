using System;

namespace SuperZTP.Controller
{
	public class Manager
	{
		private static Manager instance;

		// notatki - lista notatek
		// zadania - lista zadań
		// kategorie
		// tagi


		private Manager() { }
		public static Manager Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new Manager();
					Console.WriteLine("bu");
				}
				return instance;
			}
		}

		// wyszukaj
		public void Search()
		{
			Console.WriteLine("Odpala sie");
		}

		// grupuj
		public void Group()
		{
		
		}

		// sortuj
		public void Sort()
		{

		}

		// Generate
		public void Generate()
		{

		}
	}

}
