pipeline {
    agent any
    environment {
        dotnet = 'C:\\Program Files\\dotnet\\dotnet.exe'
    }
    stages {
        stage('Checkout') {
            steps {
                git credentialsId: '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
', url: 'https://github.com/juleba580/GestionEtudiantApp.git', branch: 'master'
            }
        }
        stage('Build') {
            steps {
                bat "dotnet build SeleniumApp.sln --configuration Release"
            }
        }
        stage('Test') {
            steps {
                bat "dotnet test SeleniumAppTest\\SeleniumAppTest.csproj --logger:trx"
            }
        }
        stage('Release') {
            steps {
                bat 'dotnet build SeleniumApp.sln /p:PublishProfile="SeleniumApp\\Properties\\PublishProfiles\\SeleniumAppProfile.pubxml" /p:Platform="Any CPU" /p:DeployOnBuild=true /m'
            }
        }
    }
}
