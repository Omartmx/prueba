pipeline {
    agent any

    environment {
        PATH = "C:\\Program Files\\dotnet;${env.PATH}"
    }

    stages {
        stage('Check .NET version') {
            steps {
                bat 'dotnet --version'
            }
        }

        stage('Restore') {
            steps {
                bat 'dotnet restore'
            }
        }

        stage('Build') {
            steps {
                bat 'dotnet build --no-restore --configuration Release'
            }
        }

        stage('Test') {
            steps {
                // Ejecuta todas las pruebas y genera archivo de resultados
                bat 'dotnet test --no-build --configuration Release --logger "trx;LogFileName=test_results.trx"'
            }
            post {
                always {
                    // Publica los resultados de test en Jenkins
                    junit '**/TestResults/*.xml'
                }
            }
        }
    }

    post {
        success {
            echo "✅ Build y pruebas completadas correctamente."
        }
        failure {
            echo "❌ Falló la compilación o las pruebas."
        }
    }
}

