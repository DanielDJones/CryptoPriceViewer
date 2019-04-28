using System;
using System.Net.Http;
using System.Text.RegularExpressions;

public class Crypto
{
    private string url
    { get; set; }
    
    private Decimal held
    { get; set; }

    private string name
    { get; set; }

    public Crypto(string url, Decimal held, string name)
	{
        this.url = url;
        this.held = held;
        this.name = name;
	}

    public decimal getValue ()
    {
        // Download Coins page on https://coinmarketcap.com/ so the Coin value can be harvested(there is no API)
        using (HttpClient client = new HttpClient())
        {
            using (HttpResponseMessage response = client.GetAsync(url).Result)
            {
                using (HttpContent content = response.Content)
                {
                    string htmlPage = content.ReadAsStringAsync().Result;

                    //Use Regex to get the value of the cypto
                    MatchCollection cryptoValue = Regex.Matches(htmlPage, "<span data-currency-price data-usd=\"(.*?)\">");

                    //Has a type cast error without converting to a string first
                    return Convert.ToDecimal(Convert.ToString(cryptoValue[0].Groups[1]));

                }
            }
        }
    }

    public decimal getHeld()
    {
        return this.held;
    }

    public string getName()
    {
        return this.name;
    }

    public void setHeld(decimal held)
    {
        this.held = held;
        return;
    }
}
