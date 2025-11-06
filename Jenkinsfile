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
                        // Limpiar resultados anteriores
                        bat 'if exist TestResults rmdir /s /q TestResults'
                        bat 'if exist testLogin\\TestResults rmdir /s /q testLogin\\TestResults'
                        
                        // Ejecutar tests
                        bat 'dotnet test testLogin/testLogin.csproj --configuration Release --logger "trx;LogFileName=results.trx"'
                        
                    } catch (Exception e) {
                        echo "Tests fallaron: ${e.getMessage()}"
                    }
                }
            }
            post {
                always {
                    // Usar la ruta EXACTA donde se guarda el TRX
                    junit testResults: "testLogin/TestResults/results.trx"
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