![Windows Security Logs to LimaCharlie Logo](docs/windows-security-to-limacharlie-csharp-donet.png)

This project collects Windows security logs (or other event logs) from your system, processes them, and sends them to LimaCharlie using a webhook. The logs are gathered from the **Application** event log (or other specified logs), with filtering for security-related events such as those logged by Windows Defender.


 [![.NET Framework](https://img.shields.io/badge/.NET%20Framework-%3E%3D%209.0-red.svg)](#)

Created by S. Ditlefsen


- Fetches the last 10 **Application** event logs or **Windows Defender**-specific logs.
- Converts the logs to JSON format.
- Sends the logs to a LimaCharlie webhook for processing.

**Download**<br>
![Download](download_24dp_2854C5_FILL0_wght400_GRAD0_opsz24.png)
[windows-security-to-limacharlie-csharp-donet.exe](https://github.com/ditlef9/windows-security-to-limacharlie-csharp-donet/raw/refs/heads/main/publish/windows-security-to-limacharlie-csharp-donet.exe)


---

## Index

[🏠 1 How to run locally](#-1-how-to-run-locally)<br>
[🖥️ 2 Compile to .exe](#%EF%B8%8F-2-compile-to-exe)<br>
[🛠️ 3 How I created the app](#%EF%B8%8F-3-how-i-created-the-app)<br>
[📜 4 License](#-4-license)<br>

---

## 🏠 1 How to run locally

### Prerequisites
- .NET SDK installed ([Download here](https://dotnet.microsoft.com/download))
- An organization created at [LimaCharlie (https://app.limacharlie.io)](https://app.limacharlie.io) (free with two sensors)

### Steps

**1. Clone this repository:**
   ```bash
   git clone https://github.com/ditlef9/fortra-count-licenses-csharp-donet.git
   cd fortra-count-licenses-csharp-donet
   ```
**2. Install dependencies (if any):**
   ```bash
   dotnet restore
   
   ```

**3. Run the application:**
```bash
dotnet run
```

**4. Configuration:**

Before running the application, configure it to interact with LimaCharlie.

Run the application, and it will prompt you for the following configuration values:

    - **LimaCharlie Organization ID**: The unique ID for your organization in LimaCharlie.
    - **LimaCharlie Hook URL**: The URL of your LimaCharlie webhook.
    - **LimaCharlie Webhook Name**: A name for the webhook you're using.
    - **LimaCharlie Webhook Secret**: The secret key for authentication with the webhook.

These settings will be stored in a `config.json` file for later use.


---

## 🖥️ 2 Compile to .exe

To compile the application into a standalone `.exe` file, follow these steps:

1. **Build the Application**: Open a terminal or command prompt in the project directory and run the following command:

    ```bash
    dotnet build windows-security-to-limacharlie-csharp-donet-.sln --configuration Release
    ```

2. **Publish the Application**: To generate the `.exe` file, run the `dotnet publish` command with the appropriate runtime for Windows. This will create an executable file ready to run without needing the .NET SDK installed on the target machine:

    ```bash
    dotnet publish windows-security-to-limacharlie-csharp-donet-.sln --configuration Release --runtime win-x64 --output ./publish
    ```

3. **Locate the .exe File**: After the build process completes, you will find the compiled `.exe` file in the `./publish` directory within your project folder. The file will be named after your project (e.g., `WindowsSecurityToLimaCharlie.exe`).

4. **Run the Executable**: You can now run the `.exe` file directly:

    ```bash
    ./publish/WindowsSecurityToLimaCharlie.exe
    ```

Notes

- Ensure that all configurations (e.g., webhook URL and secret) are set up correctly before running the compiled `.exe` file. The application will look for the `config.json` file in the same directory where the `.exe` is located.
- If you encounter issues while running the `.exe`, ensure that the necessary PowerShell permissions and modules are available on the target system.



--- 

## 🛠️ 3 How I created the app

New console app:
```
dotnet new console -n windows-security-to-limacharlie-csharp-donet
dotnet add package Newtonsoft.Json
dotnet add package System.Diagnostics.EventLog

```



---

## 📜 4 License

This project is licensed under the
[Apache License 2.0](https://www.apache.org/licenses/LICENSE-2.0).

```
Copyright 2024 github.com/ditlef9

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0
```
