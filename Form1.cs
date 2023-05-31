using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WEATHER1
{
    public partial class Form1 : Form
    {
        private readonly HttpClient httpClient;
        private readonly string apiKey = "660ac1ccdec0a4f173e3e96aab7853b6";

        public Form1()
        {
            InitializeComponent();
            httpClient = new HttpClient();
        }

        private async void btn_search_Click(object sender, EventArgs e)
        {
            await GetWeatherAsync();
        }

        private async Task GetWeatherAsync()
        {
            string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", TBCity.Text, apiKey);

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();

                WeatherInfo.root info = JsonConvert.DeserializeObject<WeatherInfo.root>(jsonResponse);
                pic_icon.ImageLocation = $"https://openweathermap.org/img/w/{info.weather[0].icon}.png";
                lab_detail.Text = info.weather[0].description;
                lab_speed.Text = info.wind.speed.ToString();
                lab_pressure.Text = info.main.pressure.ToString();
                double tempKelvin = info.main.temp;
                double tempCelcius = tempKelvin - 273.15;
                int roundedTempCelcius = (int)Math.Round(tempCelcius);
  
                lab_temp.Text = roundedTempCelcius.ToString();
                lab_name.Text = info.name.ToString();
                

            }
            catch (HttpRequestException ex)
            {
                // Handle the exception
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private DateTime ConvertDateTime(long millisec)
        {
            DateTime day = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            day = day.AddSeconds(millisec).ToLocalTime();
            return day;
        }
        private void bunifuPictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuPictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void btn_search_Click_1(object sender, EventArgs e)
        {
            await GetWeatherAsync();
        }


    }
}
