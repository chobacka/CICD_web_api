using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Extensions.NETCore.Setup; // <-- Lägg till denna rad
var builder = WebApplication.CreateBuilder(args);

// Lägg till AWS S3-tjänsten så vi kan lista filer
builder.Services.AddAWSService<IAmazonS3>();

var app = builder.Build();

// Konfiguration
string bucketName = "chobacka-meme-bucket"; // <-- BYT TILL DITT BUCKET-NAMN
string bucketRegion = "eu-north-1"; 
string s3BaseUrl = $"https://{bucketName}.s3.{bucketRegion}.amazonaws.com";

app.MapGet("/", () => Results.Extensions.Html($@"
    <html>
    <head>
        <title>Jeff, Ragge & Marco</title>
        <style>
            body {{ font-family: sans-serif; text-align: center; background-color: #f0f0f0; }}
            img {{ max-width: 90%; border: 8px solid #333; border-radius: 10px; margin-top: 20px; }}
            .btn {{ display: inline-block; padding: 10px 20px; background: #333; color: white; text-decoration: none; border-radius: 5px; margin: 10px; }}
        </style>
    </head>
    <body>
        <h1>Dagens Jeff, Ragge & Marco</h1>
        <p>En ny AI-genererad bild varje morgon kl 08:00.</p>
        
        <img src='{s3BaseUrl}/daily-meme.png' />
        
        <br/><br/>
        
        <a href='/history' class='btn'>Se gamla memes</a>
        <a href='/add?num1=10&num2=5' class='btn'>Testa Kalkylatorn</a>
    </body>
    </html>
"));

// Ny endpoint för att visa historik
app.MapGet("/history", async (IAmazonS3 s3Client) => 
{
    try 
    {
        // Hämta lista på filer från S3
        var request = new ListObjectsV2Request
        {
            BucketName = bucketName,
            Prefix = "meme-" // Vi vill bara ha filer som heter meme-datum...
        };
        
        var response = await s3Client.ListObjectsV2Async(request);
        
        // Sortera så nyaste kommer först
        var files = response.S3Objects.OrderByDescending(o => o.LastModified).ToList();

        var htmlList = "";
        foreach (var file in files)
        {
            var url = $"{s3BaseUrl}/{file.Key}";
            // Ta fram datumet från filnamnet "meme-2023-10-25.png"
            var dateDisplay = file.Key.Replace("meme-", "").Replace(".png", "");
            
            htmlList += $@"
                <div style='margin-bottom: 40px; background: white; padding: 20px; border-radius: 10px; display: inline-block;'>
                    <h3>{dateDisplay}</h3>
                    <img src='{url}' style='width: 300px; border: 4px solid #ddd;' loading='lazy' />
                </div><br/>";
        }

        return Results.Extensions.Html($@"
            <html>
            <body style='font-family: sans-serif; text-align: center; background-color: #e0e0e0;'>
                <h1>Meme Arkivet</h1>
                <a href='/' style='font-size: 20px;'>&larr; Tillbaka till startsidan</a>
                <hr/>
                {htmlList}
            </body>
            </html>
        ");
    }
    catch (Exception ex)
    {
        return Results.Extensions.Html($"<h1>Kunde inte hämta bilder :(</h1><p>{ex.Message}</p>");
    }
});

app.MapGet("/add", (int num1, int num2) => (num1 + num2).ToString());
app.MapGet("/subtract", (int num1, int num2) => (num1 - num2).ToString());

app.Run();

static class ResultsExtensions
{
    public static IResult Html(this IResultExtensions resultExtensions, string html)
    {
        return Results.Content(html, "text/html");
    }
}