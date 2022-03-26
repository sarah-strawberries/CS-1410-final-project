namespace FinanceManager.Data;

public class WeatherForecastService
{
    private static readonly string[] Summaries = new[]
    {
        "Rent", "None of your business... ;)", "School supplies", "Health insurance", "Textbooks", "Ice cream", "Hiring someone to debug my code >:(", "Gas", "Groceries", "Birthday present for Dad"
    
    };

    public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
    {
        return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = startDate.AddDays(index),
            TemperatureC = Random.Shared.Next(0, 250),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        }).ToArray());
    }
}