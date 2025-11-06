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
            }
        }

        stage('Test') {
            steps {
                script {
                    try {
                        // Limpiar resultados anteriores
                        bat 'if exist TestResults rmdir /s /q TestResults'
                        bat 'if exist testlogin\\TestResults rmdir /s /q testlogin\\TestResults'
                        
                        // Ejecutar tests
                        bat 'dotnet test testLogin/testLogin.csproj --configuration Release --logger "trx;LogFileName=results.trx"'
                        
                        // Instalar tool para convertir TRX a JUnit
                        bat 'dotnet tool install -g trx2junit || echo "Tool ya instalada"'
                        
                        // Convertir TRX a JUnit XML
                        bat 'trx2junit testlogin\\TestResults\\results.trx'
                        
                        // Verificar archivos generados
                        bat 'dir /s *.xml'
                        
                    } catch (Exception e) {
                        echo "Tests fallaron: ${e.getMessage()}"
                    }
                }
            }
            post {
                always {
                    // Usar el archivo JUnit convertido
                    junit testResults: "**/results.xml"
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