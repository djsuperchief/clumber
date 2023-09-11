namespace Clumber.Entities;

public class Config
{
    public string[] Browsers { get; set; } = default!;

    public bool Headless { get; set; } = default!;

    public static Config Load(string fileLocation)
    {
        // Should probably be Chromium
        var response = new Config()
        {
            Browsers = new[] { "Chrome" },
            Headless = true
        };

        if (File.Exists(fileLocation))
        {
            using var reader = new StreamReader(fileLocation);
            do
            {
                var readLine = reader.ReadLine();
                if (string.IsNullOrEmpty(readLine) || readLine?.Substring(0, 1) == "#")
                {
                    continue;
                }

                var line = readLine?.Split("=");

                if (line?.Length > 0)
                {
                    switch (line[0].ToLower())
                    {
                        case "browsers":
                            response.Browsers = line[1].Split(",");
                            break;
                        case "headless":
                            response.Headless = bool.Parse(line[1]);
                            break;
                    }
                }
            } while (reader.EndOfStream == false);
        }

        return response;
    }
}
