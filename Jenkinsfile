pipeline {
    agent any
    
    environment {
        DOTNET_CLI_TELEMETRY_OPTOUT = "1"
        DOTNET_NOLOGO = "1"
        DEBUG = "${params.DEBUG ?: '0'}"
        PLAYWRIGHT_TRACE_PATH = "${WORKSPACE}${params.PLAYWRIGHT_TRACE_PATH ?: '/playwright-traces'}"
    }
    
    parameters {
        booleanParam(name: 'DEBUG', defaultValue: false)
        string(name: 'PLAYWRIGHT_TRACE_PATH', defaultValue: '/playwright-traces')
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
        
        stage('Checkout repository') {
            steps {
                checkout scm
            }
        }
        
        stage('Setup .NET SDK') {
            steps {
                sh '''
                    wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
                    chmod +x ./dotnet-install.sh
                    ./dotnet-install.sh --version latest --channel 9.0
                    export PATH="$PATH:$HOME/.dotnet"
                '''
            }
        }
        
        stage('Restore dependencies') {
            steps {
                sh '''
                    export PATH="$PATH:$HOME/.dotnet"
                    dotnet restore
                '''
            }
        }
        
        stage('Build') {
            steps {
                sh '''
                    export PATH="$PATH:$HOME/.dotnet"
                    dotnet build
                '''
            }
        }
        
        stage('Install Playwright Browsers') {
            steps {
                sh '''
                    export PATH="$PATH:$HOME/.dotnet"
                    pwsh TechnicalAssessmentTests/bin/Debug/net9.0/playwright.ps1 install
                '''
            }
        }
        
        stage('Run Tests') {
            steps {
                sh '''
                    export PATH="$PATH:$HOME/.dotnet"
                    make test
                '''
            }
        }
        
        stage('Upload Test Results Artifact') {
            steps {
                archiveArtifacts artifacts: 'TechnicalAssessmentTests/TestResults/**', allowEmptyArchive: true
            }
        }
        
        stage('Any traces?') {
            steps {
                sh '''
                    ls -la $PLAYWRIGHT_TRACE_PATH
                '''
            }
        }
        
        stage('Upload Playwright Traces') {
            steps {
                archiveArtifacts artifacts: ".${params.PLAYWRIGHT_TRACE_PATH}/*.zip", allowEmptyArchive: true
            }
        }
        
        stage('Download Test Results Artifact') {
            steps {
                sh 'echo "Test results already available in workspace"'
            }
        }
        
        stage('Install ReportGenerator Tool') {
            steps {
                sh '''
                    export PATH="$PATH:$HOME/.dotnet"
                    dotnet tool install -g dotnet-reportgenerator-globaltool
                    export PATH="$PATH:$HOME/.dotnet/tools"
                    dotnet tool restore
                '''
            }
        }
        
        stage('Generate HTML Report') {
            steps {
                sh '''
                    export PATH="$PATH:$HOME/.dotnet:$HOME/.dotnet/tools"
                    make report
                '''
            }
        }
        
        stage('Upload HTML Report Artifact') {
            steps {
                archiveArtifacts artifacts: 'test-report/**', allowEmptyArchive: true
            }
        }
    }
}