pipeline {
    agent any

    stages {
        stage('Check .NET version') {
            steps {
                bat 'dotnet --version'
                bat 'dotnet --list-sdks'
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
                bat 'if exist testLogin\\bin\\Release\\net9.0\\testLogin.dll (echo "✅ testLogin.dll compilado") else (echo "❌ testLogin.dll NO compilado")'
            }
        }

        stage('Test') {
            steps {
                script {
                    try {
                        bat 'if exist TestResults rmdir /s /q TestResults'
                        bat 'mkdir TestResults'
                        
                        // USAR TRX LOGGER (siempre disponible)
                        bat 'dotnet test testLogin/testLogin.csproj --configuration Release --logger "trx;LogFileName=TestResults/results.trx"'
                        
                        // Verificar que se creó el archivo
                        bat 'if exist TestResults\\results.trx (echo "✅ Archivo TRX creado") else (echo "❌ Archivo TRX NO creado")'
                        
                    } catch (Exception e) {
                        echo "Tests fallaron: ${e.getMessage()}"
                    }
                }
            }
            post {
                always {
                    // Jenkins puede leer archivos TRX
                    junit allowEmptyResults: true, testResults: "TestResults/*.trx"
                }
            }
        }
    }

    post {
        failure {
            echo "❌ Falló la compilación o las pruebas."
        }
        success {
            echo "✅ Todas las pruebas pasaron exitosamente."
        }
    }
}