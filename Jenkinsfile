pipeline {
    agent any

    environment {
        DOTNET_CLI_TELEMETRY_OPTOUT = "1"
        DOTNET_NOLOGO = "1"
        DEBUG = credentials('DEBUG') // Заменить на actual Jenkins secret ID или переменную
        PLAYWRIGHT_TRACE_PATH = "${WORKSPACE}${env.PLAYWRIGHT_TRACE_PATH ?: '/traces'}"
    }

    parameters {
        choice(name: 'GROUP', choices: ['ContactFormValidation', 'ContactFormSubmission', 'CartCheckout'], description: 'Select test group')
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

        stage('Restore Dependencies') {
            steps {
                sh 'dotnet restore'
            }
        }

        stage('Build') {
            steps {
                sh 'dotnet build'
            }
        }

        stage('Install Playwright Browsers') {
            steps {
                sh 'pwsh TechnicalAssessmentTests/bin/Debug/net9.0/playwright.ps1 install'
            }
        }

        stage('Run Tests') {
            steps {
                sh 'make test-group Group=${GROUP}'
            }
        }

        stage('Archive Test Results') {
            steps {
                archiveArtifacts artifacts: 'TechnicalAssessmentTests/TestResults/**/*', allowEmptyArchive: true
            }
        }

        stage('Archive Playwright Traces') {
            steps {
                archiveArtifacts artifacts: "${PLAYWRIGHT_TRACE_PATH}/*.zip", allowEmptyArchive: true
            }
        }
    }

    post {
        always {
            junit 'TechnicalAssessmentTests/TestResults/**/*.xml'
        }
    }
}
