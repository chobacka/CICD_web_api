# AI Daily Meme Generator & Web API

An automated, cloud-integrated application that generates a unique AI meme every day and serves it through a lightweight web interface. This project demonstrates end-to-end automation, integrating GitHub Actions for serverless execution, AWS S3 for cloud storage, and a C# Minimal API for the backend.

## 🚀 Key Features
* **Automated Daily Content:** A GitHub Actions CRON job triggers a Python script every morning at 08:00 UTC.
* **AI Image Generation:** Dynamically generates prompt combinations and creates images using the OpenAI DALL-E 3 API.
* **Cloud Storage:** Automatically syncs and archives the generated images to an AWS S3 bucket.
* **Web Dashboard:** A C# Minimal API that fetches data directly from AWS S3, displaying the daily meme and an archive of historical memes.

## 🛠️ Tech Stack
* **Backend:** C#, .NET Minimal API
* **Cloud Infrastructure:** AWS S3 (Simple Storage Service)
* **CI/CD & Automation:** GitHub Actions, YAML
* **Scripting:** Python, Bash
* **APIs:** OpenAI API (DALL-E 3), AWS SDK for .NET

## ⚙️ DevSecOps & Architecture Highlights
This project was built with a strong focus on modern DevSecOps principles and cloud-native architecture:
1. **Pipeline Automation:** Uses GitHub Actions (`daily-meme.yaml`) not just for building, but as a serverless compute environment to run Python scripts on a schedule.
2. **Secure Credential Management:** No hardcoded keys. All sensitive data (AWS credentials, OpenAI API keys) are strictly managed via GitHub Secrets and injected into the pipeline as environment variables.
3. **Decoupled Architecture:** The C# application does not store any images locally. It securely fetches the object list directly from the S3 bucket using the AWS SDK, ensuring high availability and stateless operation.

## 💻 How to Run Locally

### Prerequisites
* .NET SDK installed.
* An active AWS S3 Bucket (set to public read for images, or configured with IAM roles).
* Your own OpenAI API key.

### Setup Instructions
1. Clone the repository:

   git clone [https://github.com/jonathan-stanford/CICD_web_api.git](https://github.com/JonathanStanford/CICD_web_api.git)

2.  Navigate to the C# project directory and restore dependencies:

    dotnet restore

3.  Run the Minimal API:

    dotnet run

4.  Open your browser and navigate to http://localhost:5000 (or the port specified in your console).

Note: To run the daily generation script locally, you will need to execute the Python script manually and provide your own OPENAI_API_KEY in your environment variables.

> ⚠️ **Project Status: Infrastructure Offline**
> *Please note: The AWS S3 bucket and active cloud environment for this project have been spun down to avoid ongoing hosting costs. The CI/CD pipeline will therefore show as "failed" in recent automated runs. The codebase remains publicly available to demonstrate the architecture, GitHub Actions automation, and C# cloud integration.*
