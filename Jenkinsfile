pipeline {
    agent any

    environment {
        DOTNET_CLI_TELEMETRY_OPTOUT = "1"
        DOTNET_NOLOGO = "1"
        DEBUG = '' // Set as needed or pass as parameter
        PLAYWRIGHT_TRACE_PATH = "${env.WORKSPACE}/playwright-traces"
    }

    options {
        // Keep 10 builds and timeout after 1 hour, adjust as needed
        buildDiscarder(logRotator(numToKeepStr: '10'))
        timeout(time: 1, unit: 'HOURS')
    }

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

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

        stage('Setup .NET SDK') {
            steps {
                // Using official .NET SDK image or install SDK on agent if needed
                sh 'dotnet --version' // Validate SDK presence
            }
        }

        stage('Restore and Build') {
            steps {
                sh 'dotnet restore'
                sh 'dotnet build'
            }
        }

        stage('Install Playwright Browsers') {
            steps {
                sh 'pwsh TechnicalAssessmentTests/bin/Debug/net9.0/playwright.ps1 install'
            }
        }

        stage('Run Tests in Parallel') {
            parallel {
                stage('ContactFormValidation') {
                    steps {
                        sh '''
                        mkdir -p ${PLAYWRIGHT_TRACE_PATH}/ContactFormValidation
                        make test-group --Group=ContactFormValidation
                        '''
                        archiveArtifacts artifacts: 'TechnicalAssessmentTests/TestResults/**', fingerprint: true
                    }
                }
                stage('ContactFormSubmission') {
                    steps {
                        sh '''
                        mkdir -p ${PLAYWRIGHT_TRACE_PATH}/ContactFormSubmission
                        make test-group --Group=ContactFormSubmission
                        '''
                        archiveArtifacts artifacts: 'TechnicalAssessmentTests/TestResults/**', fingerprint: true
                    }
                }
                stage('CartCheckout') {
                    steps {
                        sh '''
                        mkdir -p ${PLAYWRIGHT_TRACE_PATH}/CartCheckout
                        make test-group --Group=CartCheckout
                        '''
                        archiveArtifacts artifacts: 'TechnicalAssessmentTests/TestResults/**', fingerprint: true
                    }
                }
            }
        }

        stage('Generate Report') {
            steps {
                sh '''
                dotnet tool install -g dotnet-reportgenerator-globaltool || true
                export PATH="$PATH:$HOME/.dotnet/tools"
                make report
                '''
                archiveArtifacts artifacts: 'test-report/**', fingerprint: true
            }
        }
    }

    post {
        always {
            archiveArtifacts artifacts: "${PLAYWRIGHT_TRACE_PATH}/**/*.zip", fingerprint: true
        }
        success {
            echo 'Tests and reporting completed successfully.'
        }
        failure {
            echo 'Some tests failed. Please check the reports and traces.'
        }
    }
}
