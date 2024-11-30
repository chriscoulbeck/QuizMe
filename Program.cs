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

    public Question(string type, string difficulty, string category, string question, string correct_answer, string[] incorrect_answers)
    {
        this.type = System.Net.WebUtility.HtmlDecode(type);
        this.difficulty = System.Net.WebUtility.HtmlDecode(difficulty);
        this.category = System.Net.WebUtility.HtmlDecode(category);
        this.question = System.Net.WebUtility.HtmlDecode(question);
        this.correct_answer = System.Net.WebUtility.HtmlDecode(correct_answer);
        this.incorrect_answers = new string[incorrect_answers.Length];
        for (int i = 0; i < incorrect_answers.Length; i++)
        {
            this.incorrect_answers[i] = System.Net.WebUtility.HtmlDecode(incorrect_answers[i]);
        }
    }
}
