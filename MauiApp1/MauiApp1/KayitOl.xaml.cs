using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Maui.Controls;

namespace MauiApp1;

public partial class KayitOl : ContentPage
{
    private const string FirebaseWebApiKey = "AIzaSyB2d0n1xgYHs4FWiJUrtsnWuKuud2NVM9E\r\n";

    public KayitOl()
    {
        InitializeComponent();
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;

        try
        {
            var response = await SignUpUser(email, password);
           

            await Navigation.PushAsync(new Anasayfa());
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", $"Kayýt baþarýsýz: {ex.Message}", "Tamam");
        }
    }

    private async void OnNavigateToLogin(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Anasayfa());
    }

    private static async Task<string> SignUpUser(string email, string password)
    {
        var url = $"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={FirebaseWebApiKey}";
        var data = new
        {
            email = email,
            password = password,
            returnSecureToken = true
        };

        return await HttpPost(url, data);
    }

    private static async Task<string> HttpPost(string url, object data)
    {
        using (var client = new HttpClient())
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, jsonContent);
            response.EnsureSuccessStatusCode(); 
            return await response.Content.ReadAsStringAsync();
        }
    }
}
