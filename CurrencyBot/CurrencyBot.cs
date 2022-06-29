using System.Configuration;
using Discord;
using Discord.WebSocket;

namespace DiscordBot;

/*
 * @name    Currency conversion bot
 * @version 1.0
 * @author  Grey (Grey#1686 on Discord)
 *
 * This bot (in it's current iteration) can convert the wizarding currency from the Harry Potter franchise 
 */

public static class CurrencyBot
{
    //Not recommended: Exchange the expression "ConfigurationManager.AppSettings["BOT_TOKEN"]" to the token Discord provided you
    private static readonly string? BotToken = ConfigurationManager.AppSettings["BOT_TOKEN"];
    private static readonly string[] Separators = { " " };
    
    //Conversion rates
    private const double GalleonKnutRates = 493.0;
    private const double SickleKnutRates = 29.0;
    private const double KnutKnutRates = 1.0;
    
    private static string[] _result = null!;
    private static EmbedBuilder _embed = null!;

    private const string HelloCommand = ";hello";
    private const string HelpCommand = ";help";
    private const string ConvertCommand = ";convert";
    

    public static async Task Main()
    {
        //Discord bot creation
        var client = new DiscordSocketClient();
        
        //Events
        client.MessageReceived += ClientOnMessageReceived;

        //Connection to Discord
        await client.LoginAsync(TokenType.Bot, BotToken);

        //Bot start
        await client.StartAsync();
        await client.SetGameAsync($"Type {HelpCommand}");
        Console.WriteLine("CurrencyBot is online");

        //Necessary, so the bot doesn't terminate
        Thread.Sleep(-1);
    }

    private static async Task ClientOnMessageReceived(SocketMessage arg)
    {
        //Convert the read message to string
        var input = arg.ToString();

        //Initializing EmbedBuilder; Splitting message
        _embed = new EmbedBuilder();
        _result = input.Split(Separators, StringSplitOptions.None);
        
        //Switch what command got triggered
        switch (_result[0])
        {
            case HelloCommand:
                await CommandHello(arg);
                break;
            case HelpCommand:
                await CommandHelp(arg);
                break;
            case ConvertCommand:
                await CommandConvert(arg);
                break;
        }
    }

    /*
     * Triggered by HelloCommand
     * This has no purpose
     */
    private static async Task CommandHello(SocketMessage arg)
    {
        _embed
            .WithColor(Color.LightGrey)
            .WithTitle("Bot v1.0");
        await arg.Channel.SendMessageAsync("", false, _embed.Build());
    }

    /*
     * Triggered by HelpCommand
     * Explains the command ";convert"
     */
    private static async Task CommandHelp(SocketMessage arg)
    {
        _embed
            .WithColor(Color.Teal)
            .WithTitle("Help")
            .WithDescription(
                $"Thank you for using my bot.\n\n" +
                $"To use the converter, do the following command:\n\n" +
                $"`{ConvertCommand} <value> <currency_1> <currency_2>`")
            .AddField("value", "The value you want to convert")
            .AddField("currency_1", "The currency the value is in")
            .AddField("currency_2", "The currency you want to convert to")
            .AddField("Acceptable currency inputs are:", "g, galleon, galleons, s, sickle, sickles, k, knut, knuts")
            .WithFooter("Message Grey#1686 for any inquiries");
        await arg.Channel.SendMessageAsync("", false, _embed.Build());
    }

    /*
     * Triggered by ConvertCommand
     * Usually intended to be in this format ";convert <int> <string> <string>"
     * Converts the value to the desired currency
     * Calculations are based on cross-multiplication
     */
    private static async Task CommandConvert(SocketMessage arg)
    {
        var inputValue = _result[1];
        var inputCurrency = RewriteCurrency(_result[2]);
        var outputCurrency = RewriteCurrency(_result[3]);

        var result = inputCurrency switch
        {
            "galleon(s)" => Convert.ToDouble(inputValue) * GalleonKnutRates,
            "sickle(s)" => Convert.ToDouble(inputValue) * SickleKnutRates,
            "knut(s)" => Convert.ToDouble(inputValue) * KnutKnutRates,
            _ => double.NaN
        };

        if (result.Equals(double.NaN) || outputCurrency == "")
        {
            _embed
                .WithColor(Color.Red)
                .WithTitle("Error")
                .WithDescription(
                    $"Oh no! Something went wrong.\n\n" +
                    $"You probably did a wrong input.\n" +
                    $"Refer to the {HelpCommand} command for more info.");
            await arg.Channel.SendMessageAsync("", false, _embed.Build());
        }

        else
        {
            result = outputCurrency switch
            {
                "galleon(s)" => result / GalleonKnutRates,
                "sickle(s)" => result / SickleKnutRates,
                "knut(s)" => result / KnutKnutRates,
                _ => result
            };

            result = Math.Round(result, 4);

            _embed
                .WithColor(Color.Green)
                .WithTitle("Convert")
                .AddField("Input:", $"{inputValue} {inputCurrency}")
                .AddField("Result:", $"{result} {outputCurrency}")
                .WithFooter("Message Grey#1686 for any inquiries");
            await arg.Channel.SendMessageAsync("", false, _embed.Build());
        }
    }

    /*
     * A helper method to allow more variations of input
     */
    private static string RewriteCurrency(string input)
    {
        var result = input switch
        {
            ("g" or "galleon" or "galleons") => "galleon(s)",
            ("s" or "sickle" or "sickles") => "sickle(s)",
            ("k" or "knut" or "knuts") => "knut(s)",
            _ => ""
        };

        return result;
    }
}