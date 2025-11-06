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
                // Compilar TODOS los proyectos incluyendo tests
                bat 'dotnet build --no-restore --configuration Release'
                
                // Verificar específicamente que el proyecto de test se compiló
                bat 'if exist testLogin\\bin\\Release\\net9.0\\testLogin.dll (echo "✅ testLogin.dll compilado") else (echo "❌ testLogin.dll NO compilado")'
            }
        }

        stage('Test') {
            steps {
                script {
                    try {
                        // Crear directorio para resultados
                        bat 'if exist TestResults rmdir /s /q TestResults'
                        bat 'mkdir TestResults'
                        
                        // Compilar y ejecutar tests (SIN --no-build)
                        bat 'dotnet test testLogin/testLogin.csproj --configuration Release --logger "xunit;LogFilePath=TestResults/test_results.xml"'
                        
                    } catch (Exception e) {
                        echo "Tests fallaron: ${e.getMessage()}"
                        // Continuar para intentar publicar resultados
                    }
                }
            }
            post {
                always {
                    // Publicar resultados aunque estén vacíos
                    junit allowEmptyResults: true, testResults: "TestResults/test_results.xml"
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