using System;

public class Manager
{
	private static Manager instance;

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
}
