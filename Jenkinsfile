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
                bat 'dotnet test testLogin/testLogin.csproj --no-build --configuration Release --logger "xunit;LogFileName=test_results.xml"'
            }
                post {
                    always {
                // Publica los resultados de prueba en Jenkins
                junit "test_results.xml"
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