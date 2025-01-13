using System.Net.Http;
using System.Threading.Tasks;
using static MauiApp1.Models.Haberlers;


namespace MauiApp1.Models;

public static class Servisler
{

    public static async Task<string> GetAltinDovizGuncelKurlar()
    {
        HttpClient client = new HttpClient();
        string url = "https://finans.truncgil.com/today.json";
        using HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        string jsondata = await response.Content.ReadAsStringAsync();
        return jsondata;
    }
    public static async Task<string> HaberleriGetir(Kategori ctg)
    {
        try
        {
            HttpClient client = new HttpClient();
            string url = $"https://api.rss2json.com/v1/api.json?rss_url={Uri.EscapeDataString(ctg.Link)}";
            using HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"HaberleriGetir Hatasý: {ex.Message}");
            return null;
        }
    }


}