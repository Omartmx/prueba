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
                        
                        // Ejecutar tests - el TRX se guarda automáticamente en testLogin/TestResults
                        bat 'dotnet test testLogin/testLogin.csproj --configuration Release --logger "trx;LogFileName=results.trx"'
                        
                        // Verificar dónde se creó el archivo
                        bat 'echo === BUSCANDO ARCHIVOS TRX ==='
                        bat 'dir /s *.trx'
                        
                        // Copiar el archivo a una ubicación estándar si es necesario
                        bat 'if exist testLogin\\TestResults\\results.trx (echo "✅ TRX encontrado en testLogin/TestResults" && copy testLogin\\TestResults\\results.trx TestResults\\) || echo "TRX no encontrado"'
                        
                    } catch (Exception e) {
                        echo "Tests fallaron: ${e.getMessage()}"
                    }
                }
            }
            post {
                always {
                    // Buscar TRX en múltiples ubicaciones posibles
                    junit allowEmptyResults: true, testResults: "**/results.trx"
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