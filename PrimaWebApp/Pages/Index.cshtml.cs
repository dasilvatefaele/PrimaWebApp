using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PrimaWebApp.Model;
using System.Globalization;

namespace PrimaWebApp.Pages;

public class IndexModel : PageModel
{

    public string fileLocal;
    public string ambiente;
    public string formatoValuta;
    public string fusoOrario;
    public string versione;
    public string filePath;

    public DateTime ultimaAggiornamento = DateTime.Now;

    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }


    public void OnGet()
    {
        fileLocal = Environment.GetEnvironmentVariable("PRODOTTI_JSON_PATH") ?? "/wwwroot/data/prodotti.json";
        ambiente = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development";
        formatoValuta = Environment.GetEnvironmentVariable("FORMATO_CURRENCY") ?? "EUR";
        fusoOrario = Environment.GetEnvironmentVariable("TZ") ?? "Europe/Rome";
        versione = Environment.GetEnvironmentVariable("VERSION") ?? "1.0.0";
        filePath = "/app/build_time.txt";

        Console.WriteLine($"Versione: {versione}");

        // var culture = new CultureInfo("it-IT")
        // {
        //     NumberFormat = {}
        // };




        Console.WriteLine($"Ambiente: {ambiente}");
        Console.WriteLine($"File Path: {fileLocal}");
        Console.WriteLine($"Data: {ultimaAggiornamento}");
        Console.WriteLine($"Formato valuta: {formatoValuta}");
        Console.WriteLine($"Fuso orario: {fusoOrario}");

        if (System.IO.File.Exists(fileLocal))
        {
            var json = System.IO.File.ReadAllText(fileLocal);
            var prodotti = JsonConvert.DeserializeObject<List<Prodotto>>(json) ?? new List<Prodotto>();
        }
    }
}

