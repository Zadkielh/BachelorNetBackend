namespace BachelorOppgaveBackend;

public class WeatherForecast
{

    public int WeatherId {get; set;}

    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
    
    public string? location {get; set;}
}

public class AzureAd
{
  public string? name { get; set;} 
}
