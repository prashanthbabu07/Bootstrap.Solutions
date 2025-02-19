namespace Bootstrap.Data.Services;

public interface IWeatherDataService
{
	void AddSummary(string summary);
	bool RemoveSummary(string summary);
	IReadOnlyList<string> Summaries { get; }
}


/// <summary>
/// A singleton class to maintain weather data in memory
/// </summary>
public class WeatherDataService : IWeatherDataService
{
	private List<string> _summaries = new List<string>()
	{
		"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
	};

	private readonly object _lock = new object();

	public void AddSummary(string summary)
	{
		lock (_lock)
		{
			_summaries.Add(summary);
		}
	}

	public bool RemoveSummary(string summary)
	{
		lock(_lock)
		{
			return _summaries.Remove(summary);
		}
	}

	public IReadOnlyList<string> Summaries => _summaries.AsReadOnly();
}
