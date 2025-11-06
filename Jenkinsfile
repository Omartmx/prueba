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
                        bat 'if exist testlogin\\TestResults rmdir /s /q testlogin\\TestResults'
                        
                        // Ejecutar tests
                        bat 'dotnet test testLogin/testLogin.csproj --configuration Release --logger "trx;LogFileName=results.trx"'
                        
                        // Diagnóstico: ver dónde se guardó el archivo
                        bat 'echo === VERIFICANDO ARCHIVOS TRX ==='
                        bat 'dir /s *.trx'
                        
                    } catch (Exception e) {
                        echo "Tests fallaron: ${e.getMessage()}"
                    }
                }
            }
            post {
                always {
                    // Buscar en TODAS las ubicaciones posibles con patrón global
                    junit testResults: "**/results.trx"
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