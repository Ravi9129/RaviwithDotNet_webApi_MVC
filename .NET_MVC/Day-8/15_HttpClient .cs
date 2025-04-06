using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace .NET_MVC.Day-8
{
    public class 15_HttpClient 
    {
        
    }
}
----------------------------------------------
HttpClient ke baare mein — ekdum real world use case, apni language mein, bina table ke.
Samajhne ke baad tereko API calls karna, external services se baat karna, sab easy lagega 💪

📘 HttpClient Kya Hota Hai?
Ye ek built-in class hai .NET me jo humko kisi dusri web API se data lene ya bhejne me madad karti hai.

Jaise browser GET/POST request karta hai, waise ye programmatically karta hai.
------------------------------
🔥 Kab aur Kyun Use Karte Hain?
🔹 Jab tu kisi 3rd party API se data lena chahta hai (e.g. Weather, Payment Gateway)
🔹 Jab tu apne hi microservices ke beech communication kar raha hai
🔹 Jab tu API consume kar raha hai kisi app ke andar
---------------------------------------------------
🔧 Simple Example - Real Life Style
🧠 Use Case: Tu ek weather app bana raha hai aur tu chahata hai ki external weather API se temperature laaye.
🔹 Step 1: HttpClient Inject Karna (Recommended via DI)

public class WeatherService
{
    private readonly HttpClient _http;

    public WeatherService(HttpClient http)
    {
        _http = http;
    }

    public async Task<string> GetWeatherAsync(string city)
    {
        var response = await _http.GetAsync($"https://api.weatherapi.com/city/{city}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return content; // JSON return karega
        }

        return "Error fetching weather";
    }
}
----------------------------------------------
🔹 Step 2: Register HttpClient DI Container me

builder.Services.AddHttpClient<WeatherService>();
------------------------------------------
🔹 Step 3: Controller me use karna

public class HomeController : Controller
{
    private readonly WeatherService _weather;

    public HomeController(WeatherService weather)
    {
        _weather = weather;
    }

    public async Task<IActionResult> Index()
    {
        var result = await _weather.GetWeatherAsync("Delhi");
        return Content(result);
    }
}
--------------------------------------------
🧠 Important Points:
✅ 1. HttpClient ko DI ke through use karna chahiye (kyunki agar new se banaya to memory leak ho sakta hai)

var http = new HttpClient(); // ❌ ye avoid karna chahiye
------------------------------------------------------
✅ 2. Use headers, tokens, json serialization:

_http.DefaultRequestHeaders.Authorization = 
    new AuthenticationHeaderValue("Bearer", "your-jwt-token");
    -------------------------------------------------------
✅ 3. POST request bhi kar sakta hai:
var payload = new { name = "Ravi", city = "Delhi" };
var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

var response = await _http.PostAsync("https://api.example.com/user", content);
----------------------------------------------------
🔥 Real-World Examples:
✅ Payment Gateway Integration — payment API call via HttpClient
✅ Weather App — get forecast from OpenWeather API
✅ Stock Price Fetching — call financial API
✅ Microservices — UserService talks to OrderService
------------------------------------
🤖 Bonus: Named Client (Agar multiple APIs hain)

builder.Services.AddHttpClient("WeatherClient", c =>
{
    c.BaseAddress = new Uri("https://api.weatherapi.com/");
});
--------------------------------------
Aur inject karte waqt:

public WeatherService(IHttpClientFactory factory)
{
    _http = factory.CreateClient("WeatherClient");
}
-------------------------------------------------------
🎯 Conclusion:
HttpClient is the backbone of external API communication

Use DI + IHttpClientFactory for best performance

Handle timeouts, retries, and headers properly

