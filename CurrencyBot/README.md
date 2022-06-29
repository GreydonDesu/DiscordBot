
# CurrencyBot 

This bot (in it's current iteration) can convert the wizarding currency from the Harry Potter franchise.

I based the calculations on [those exchange rates](https://harrypotter.fandom.com/wiki/Wizarding_currency#Exchange_rate)

## Preparation

Before you can start building the bot, you have to have the package **Discord.Net** installed.\
Refer to [this wiki](https://www.nuget.org/packages/Discord.Net) on how to get it.

I used **[ConfigurationManager](https://www.nuget.org/packages/System.Configuration.ConfigurationManager)** to store the bot token externally in a .config file. Feel free to use your favorite package to store it externally.\
It is **not recommended** to have your bot token in your source code, as this may result in right abusing, if used inappropriately and may result in termination of your bot.

Finally, obviously, you need **.NET | DotNet** on your PC or server, depending on where you want to run the bot.\
There are various ways to get it, which I will let it open to your environment.
## Installation

Build the project with DotNet. Please refer to the internet, how to do it.

Once you build it, change to the directory `/bin/Debug/net[version number]`.
```bash
cd /bin/Debug/net[version number]
```

To run the bot, type this:
```bash
dotnet CurrencyBot.dll
```

Remember, that you have to keep the terminal open to keep the bot online.\
I would recommend to use **Screen** to detach the terminal, thus keeping the bot running.
## Authors

**Grey | GreydonDesu**
- [Twitch](https://twitch.tv/GreydonDesu)
- [Twitter](https://twitter.com/GreydonDesu)
- [All other links](https://greydon.de)