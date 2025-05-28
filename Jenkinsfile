pipeline {
  agent any

  environment {
    Playwright_BrowserName = "chromium"
    Playwright_LaunchOptions_Headless = "false"
    DOTNET_CLI_TELEMETRY_OPTOUT = "1"
    DOTNET_NOLOGO = "1"
    DEBUG = "pw:api"
  }

  stages {
    stage('Install PowerShell') {
      steps {
        sh '''
          sudo apt-get update
          sudo apt-get install -y wget apt-transport-https software-properties-common
          wget -q https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb
          sudo dpkg -i packages-microsoft-prod.deb
          sudo apt-get update
          sudo apt-get install -y powershell
        '''
      }
    }

    stage('Checkout') {
      steps {
        checkout scm
      }
    }

    stage('Setup .NET SDK') {
      steps {
        sh '''
          wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
          chmod +x dotnet-install.sh
          ./dotnet-install.sh -Channel 9.0
          export PATH="$HOME/.dotnet:$PATH"
        '''
      }
    }

    stage('Restore dependencies') {
      steps {
        sh '~/.dotnet/dotnet restore'
      }
    }

    stage('Build') {
      steps {
        sh '~/.dotnet/dotnet build'
      }
    }

    stage('Install Playwright') {
      steps {
        sh 'pwsh TechnicalAssessmentTests/bin/Debug/net9.0/playwright.ps1 install'
      }
    }

    stage('Run Tests') {
      steps {
        sh 'make test'
      }
    }
  }
}
