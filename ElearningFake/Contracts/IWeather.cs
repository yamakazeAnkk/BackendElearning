namespace ElearningFake.Contracts
{
    public interface IWeather
    {
        Task<WeatherForecast[]> GetWeatherForecast();
    }
}
