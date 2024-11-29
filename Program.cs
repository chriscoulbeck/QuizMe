using Newtonsoft.Json;
using Spectre.Console;

public static class Program
{
    static readonly HttpClient client = new HttpClient();
    public static async Task Main(string[] args)
    {
        AnsiConsole.MarkupLine("[underline red]Hello[/] World!");
        Question[] questionList = new Question[10];
        var response = await client.GetStringAsync("https://opentdb.com/api.php?amount=10");
        var rootObject = JsonConvert.DeserializeObject<RootObject>(response);
        if (rootObject == null)
        {
            Environment.Exit(0);
        }
        questionList = rootObject.Questions;
    }
}

public class RootObject
{
    [JsonProperty("results")]
    public required Question[] Questions { get; set; }
}

public class Question
{
    public required string type { get; set; }
    public required string difficulty { get; set; }
    public required string category { get; set; }
    public required string question { get; set; }
    public required string correct_answer { get; set; }
    public required string[] incorrect_answers { get; set; }
}
