properties([pipelineTriggers([githubPush()])])

pipeline {
    agent any

    stages {
        stage('hello') {
            steps {
                // Get some code from a GitHub repository
                echo 'hello'
            }
        }
        stage('deleteWorkspace') {
            steps {
                deleteDir()
            }
        }
        stage('clone') {
            steps {
                // Get some code from a GitHub repository
                git branch: 'main',
                url: 'https://github.com/ProductivityTools-Transfers/ProductivityTools.Transfers.Api'
            }
        }
        stage('build') {
            steps {
                bat(script: "dotnet publish ProductivityTools.Transfers.Api.sln -c Release ", returnStdout: true)
            }
        }
        stage('deleteDbMigratorDir') {
            steps {
                bat('if exist "C:\\Bin\\TransfersApiDdbMigration" RMDIR /Q/S "C:\\Bin\\TransfersApiDdbMigration"')
            }
        }
        stage('copyDbMigratdorFiles') {
            steps {
                bat('xcopy "ProductivityTools.Transfers.Api.DbUp\\bin\\Release\\net6.0\\publish" "C:\\Bin\\TransfersApiDdbMigration\\" /O /X /E /H /K')
            }
        }

        stage('runDbMigratorFiles') {
            steps {
                bat('C:\\Bin\\TransfersApiDdbMigration\\ProductivityTools.Transfers.Api.DbUp.exe')
            }
        }

        stage('stopMeetingsOnIis') {
            steps {
                bat('%windir%\\system32\\inetsrv\\appcmd stop site /site.name:Transfers')
            }
        }

        stage('deleteIisDir') {
            steps {
                retry(5) {
                    bat('if exist "C:\\Bin\\IIS\\Transfers" RMDIR /Q/S "C:\\Bin\\IIS\\Transfers"')
                }

            }
        }
        stage('copyIisFiles') {
            steps {         
                bat('xcopy "src\\Server\\ProductivityTools.Journal.WebApi\\bin\\Release\\net6.0\\publish" "C:\\Bin\\Journal\\" /O /X /E /H /K')
            }
        }

        stage('startMeetingsOnIis') {
            steps {
                bat('%windir%\\system32\\inetsrv\\appcmd start site /site.name:Journal')
            }
        }
        stage('byebye') {
            steps {
                // Get some code from a GitHub repository
                echo 'byebye1'
            }
        }
    }
	post {
		always {
            emailext body: "${currentBuild.currentResult}: Job ${env.JOB_NAME} build ${env.BUILD_NUMBER}\n More info at: ${env.BUILD_URL}",
                recipientProviders: [[$class: 'DevelopersRecipientProvider'], [$class: 'RequesterRecipientProvider']],
                subject: "Jenkins Build ${currentBuild.currentResult}: Job ${env.JOB_NAME}"
		}
	}
}