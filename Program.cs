var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// bucket-namn och region
string s3BucketUrl = "https://chobacka-meme-bucket.s3.eu-north-1.amazonaws.com/daily-meme.png";

app.MapGet("/", () => Results.Extensions.Html($@"
    <html>
    <body style='font-family: sans-serif; text-align: center;'>
        <h1>Hello World!</h1>
        <h2>Dagens Nano Banana Meme:</h2>
        <img src='/meme' style='max-width: 500px; border: 5px solid yellow;' />
        <br/><br/>
        <a href='/add?num1=5&num2=5'>Testa Add(5,5)</a>
    </body>
    </html>
"));

// Denna endpoint skickar användaren direkt till bilden på S3
app.MapGet("/meme", () => Results.Redirect(s3BucketUrl));

app.MapGet("/add", (int num1, int num2) => AddNumbers(num1, num2));
app.MapGet("/subtract", (int num1, int num2) => SubtractNumbers(num1, num2));

app.Run();

// Hjälpfunktioner
static int AddNumbers(int num1, int num2)
{
    return num1 + num2;
}

static int SubtractNumbers(int num1, int num2)
{
    return num1 - num2;
}

// Ett litet hjälp-tillägg för att kunna returnera HTML enkelt i Minimal APIs
static class ResultsExtensions
{
    public static IResult Html(this IResultExtensions resultExtensions, string html)
    {
        return Results.Content(html, "text/html");
    }
}