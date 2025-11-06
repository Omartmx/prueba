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
                // Compilar específicamente el proyecto de test también
                bat 'dotnet build testLogin/testLogin.csproj --no-restore --configuration Release'
                
                // Verificar que el proyecto de test se compiló
                bat 'dir testLogin\\bin\\Release /s'
            }
        }

        stage('Test') {
            steps {
                // Crear directorio para resultados
                bat 'if exist TestResults rmdir /s /q TestResults'
                bat 'mkdir TestResults'
                
                // Ejecutar tests
                bat 'dotnet test testLogin/testLogin.csproj --configuration Release --logger "xunit;LogFilePath=TestResults/test_results.xml"'
                
                // Verificar si se creó el archivo de resultados
                bat 'if exist TestResults\\test_results.xml (echo "✅ Archivo de resultados creado") else (echo "❌ Archivo de resultados NO creado")'
            }
            post {
                always {
                    junit "TestResults/test_results.xml"
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