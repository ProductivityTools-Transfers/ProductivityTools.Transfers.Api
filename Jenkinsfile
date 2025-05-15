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
        stage('Git Clone') {
            steps {
                // Get some code from a GitHub repository
                git branch: 'main',
                url: 'https://github.com/ProductivityTools-Transfers/ProductivityTools.Transfers.Api'
            }
        }
        stage('Build solution') {
            steps {
                bat(script: "dotnet publish ProductivityTools.Transfers.Api.sln -c Release", returnStdout: true)
            }
        }
        stage('Delete databse migration directory') {
            steps {
                bat('if exist "C:\\Bin\\DbMigration\\Transfers.Api" RMDIR /Q/S "C:\\Bin\\DbMigration\\Transfers.Api"')
            }
        }
        stage('Copy database migration files') {
            steps {
                bat('xcopy "ProductivityTools.Transfers.Api.DbUp\\bin\\Release\\net9.0\\publish\\" "C:\\Bin\\DbMigration\\Transfers.Api\\" /O /X /E /H /K')
            }
        }

        stage('Run databse migration files') {
            steps {
                bat('C:\\Bin\\DbMigration\\Transfers.Api\\ProductivityTools.Transfers.Api.DbUp.exe')
            }
        }


        stage('Create page on the IIS') {
            steps {
                powershell('''
                function CheckIfExist($Name){
                    cd $env:SystemRoot\\system32\\inetsrv
                    $exists = (.\\appcmd.exe list sites /name:$Name) -ne $null
                    Write-Host $exists
                    return  $exists
                }
                
                 function Create($Name,$HttpbBnding,$PhysicalPath){
                    $exists=CheckIfExist $Name
                    if ($exists){
                        write-host "Web page already existing"
                    }
                    else
                    {
                        write-host "Creating app pool"
                        .\\appcmd.exe add apppool /name:$Name /managedRuntimeVersion:"v4.0" /managedPipelineMode:"Integrated"
                        write-host "Creating webage"
                        .\\appcmd.exe add site /name:$Name /bindings:http://$HttpbBnding /physicalpath:$PhysicalPath
                        write-host "assign app pool to the website"
                        .\\appcmd.exe set app "$Name/" /applicationPool:"$Name"


                    }
                }
                Create "PTTransfers" "*:8003"  "C:\\Bin\\IIS\\PTTransfers"                
                ''')
            }
        }

        stage('Stop page on the IIS') {
            steps {
                bat('%windir%\\system32\\inetsrv\\appcmd stop site /site.name:PTTransfers')
            }
        }
        
		stage('Delete PTTransfers IIS directory') {
            steps {
              powershell('''
                if ( Test-Path "C:\\Bin\\IIS\\PTTransfers")
                {
                    while($true) {
                        if ( (Remove-Item "C:\\Bin\\IIS\\PTTransfers" -Recurse *>&1) -ne $null)
                        {  
                            write-output "removing failed we should wait"
                        }
                        else 
                        {
                            break 
                        } 
                    }
                  }
              ''')

            }
        }
	  
        stage('Copy web page to the IIS Bin directory') {
            steps {         
                bat('xcopy "ProductivityTools.Transfers.Api\\bin\\Release\\net9.0\\publish" "C:\\Bin\\IIS\\PTTransfers\\" /O /X /E /H /K')
            }
        }

        stage('startMeetingsOnIis') {
            steps {
                bat('%windir%\\system32\\inetsrv\\appcmd start site /site.name:PTTransfers')
            }
        }

      stage('Create Login PTTransfers on SQL2022') {
             steps {
                 bat('sqlcmd -S ".\\SQL2022" -q "CREATE LOGIN [IIS APPPOOL\\PTTransfers] FROM WINDOWS WITH DEFAULT_DATABASE=[PTTransfers];"')
             }
        }

              stage('Create User PTTransfers on SQL2022') {
             steps {
                 bat('sqlcmd -S ".\\SQL2022" -q " USE PTTransfers;  CREATE USER [IIS APPPOOL\\PTTransfers]  FOR LOGIN [IIS APPPOOL\\PTTransfers];"')
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
