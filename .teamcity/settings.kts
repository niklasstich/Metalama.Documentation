// This file is automatically generated when you do `Build.ps1 prepare`.

import jetbrains.buildServer.configs.kotlin.v2019_2.*

// Both Swabra and swabra need to be imported
import jetbrains.buildServer.configs.kotlin.v2019_2.buildFeatures.sshAgent
import jetbrains.buildServer.configs.kotlin.v2019_2.buildFeatures.Swabra
import jetbrains.buildServer.configs.kotlin.v2019_2.buildFeatures.swabra
import jetbrains.buildServer.configs.kotlin.v2019_2.failureConditions.*
import jetbrains.buildServer.configs.kotlin.v2019_2.buildSteps.powerShell
import jetbrains.buildServer.configs.kotlin.v2019_2.triggers.*

version = "2021.2"

project {

   buildType(DebugBuild)
   buildType(PublicBuild)
   buildType(DownstreamMerge)
   buildTypesOrder = arrayListOf(DebugBuild,PublicBuild,DownstreamMerge)
}

object DebugBuild : BuildType({

    name = "Build [Debug]"

    artifactRules = "+:artifacts/publish/public/**/*=>artifacts/publish/public\n+:artifacts/publish/private/**/*=>artifacts/publish/private\n+:artifacts/testResults/**/*=>artifacts/testResults\n+:artifacts/logs/**/*=>logs\n+:%system.teamcity.build.tempDir%/Metalama/AssemblyLocator/**/*=>logs\n+:%system.teamcity.build.tempDir%/Metalama/CompileTime/**/.completed=>logs\n+:%system.teamcity.build.tempDir%/Metalama/CompileTimeTroubleshooting/**/*=>logs\n+:%system.teamcity.build.tempDir%/Metalama/CrashReports/**/*=>logs\n+:%system.teamcity.build.tempDir%/Metalama/Extract/**/.completed=>logs\n+:%system.teamcity.build.tempDir%/Metalama/ExtractExceptions/**/*=>logs\n+:%system.teamcity.build.tempDir%/Metalama/Logs/**/*=>logs"

    params {
        text("BuildArguments", "", label = "Build Arguments", description = "Arguments to append to the 'Build' build step.", allowEmpty = true)
        text("TimeOut", "300", label = "Time-Out Threshold", description = "Seconds after the duration of the last successful build.", regex = """\d+""", validationMessage = "The timeout has to be an integer number.")
    }
    vcs {
        root(DslContext.settingsRoot)
        root(AbsoluteId("Metalama_Metalama20233_MetalamaSamples"), "+:. => source-dependencies/Metalama.Samples")
        root(AbsoluteId("Metalama_Metalama20233_MetalamaCommunity"), "+:. => source-dependencies/Metalama.Community")
    }

    steps {
        powerShell {
            name = "Kill background processes before cleanup"
            scriptMode = file {
                path = "Build.ps1"
            }
            noProfile = false
            param("jetbrains_powershell_scriptArguments", "tools kill")
        }
        powerShell {
            name = "Build"
            scriptMode = file {
                path = "Build.ps1"
            }
            noProfile = false
            param("jetbrains_powershell_scriptArguments", "test --configuration Debug --buildNumber %build.number% --buildType %system.teamcity.buildType.id% %BuildArguments%")
        }
    }

    failureConditions {
        failOnMetricChange {
            metric = BuildFailureOnMetric.MetricType.BUILD_DURATION
            units = BuildFailureOnMetric.MetricUnit.DEFAULT_UNIT
            comparison = BuildFailureOnMetric.MetricComparison.MORE
            compareTo = build {
                buildRule = lastSuccessful()
            }
            stopBuildOnFailure = true
            param("metricThreshold", "%TimeOut%")
        }
    }

    requirements {
        equals("env.BuildAgentType", "caravela04cloud")
    }

    features {
        swabra {
            lockingProcesses = Swabra.LockingProcessPolicy.KILL
            verbose = true
        }
    }

    dependencies {
        dependency(AbsoluteId("Metalama_Metalama20233_Metalama_DebugBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }

            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama"
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20233_MetalamaBackstage_ReleaseBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }

            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.Backstage"
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20233_MetalamaCommunity_DebugBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20233_MetalamaCompiler_ReleaseBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }

            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/packages/Release/Shipping/**/*=>dependencies/Metalama.Compiler"
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20233_MetalamaExtensions_DebugBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }

            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.Extensions"
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20233_MetalamaLinqPad_DebugBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }

            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.LinqPad"
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20233_MetalamaMigration_DebugBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }

            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.Migration"
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20233_MetalamaPatterns_DebugBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }

            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.Patterns"
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20233_MetalamaSamples_DebugBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }

            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.Samples"
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20233_MetalamaSamples_DebugBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }
        }

     }

})

object PublicBuild : BuildType({

    name = "Build [Public]"

    artifactRules = "+:artifacts/publish/public/**/*=>artifacts/publish/public\n+:artifacts/publish/private/**/*=>artifacts/publish/private\n+:artifacts/testResults/**/*=>artifacts/testResults\n+:artifacts/logs/**/*=>logs\n+:%system.teamcity.build.tempDir%/Metalama/AssemblyLocator/**/*=>logs\n+:%system.teamcity.build.tempDir%/Metalama/CompileTime/**/.completed=>logs\n+:%system.teamcity.build.tempDir%/Metalama/CompileTimeTroubleshooting/**/*=>logs\n+:%system.teamcity.build.tempDir%/Metalama/CrashReports/**/*=>logs\n+:%system.teamcity.build.tempDir%/Metalama/Extract/**/.completed=>logs\n+:%system.teamcity.build.tempDir%/Metalama/ExtractExceptions/**/*=>logs\n+:%system.teamcity.build.tempDir%/Metalama/Logs/**/*=>logs"

    params {
        text("BuildArguments", "", label = "Build Arguments", description = "Arguments to append to the 'Build' build step.", allowEmpty = true)
        text("TimeOut", "300", label = "Time-Out Threshold", description = "Seconds after the duration of the last successful build.", regex = """\d+""", validationMessage = "The timeout has to be an integer number.")
    }
    vcs {
        root(DslContext.settingsRoot)
        root(AbsoluteId("Metalama_Metalama20233_MetalamaSamples"), "+:. => source-dependencies/Metalama.Samples")
        root(AbsoluteId("Metalama_Metalama20233_MetalamaCommunity"), "+:. => source-dependencies/Metalama.Community")
    }

    steps {
        powerShell {
            name = "Kill background processes before cleanup"
            scriptMode = file {
                path = "Build.ps1"
            }
            noProfile = false
            param("jetbrains_powershell_scriptArguments", "tools kill")
        }
        powerShell {
            name = "Build"
            scriptMode = file {
                path = "Build.ps1"
            }
            noProfile = false
            param("jetbrains_powershell_scriptArguments", "test --configuration Public --buildNumber %build.number% --buildType %system.teamcity.buildType.id% %BuildArguments%")
        }
    }

    failureConditions {
        failOnMetricChange {
            metric = BuildFailureOnMetric.MetricType.BUILD_DURATION
            units = BuildFailureOnMetric.MetricUnit.DEFAULT_UNIT
            comparison = BuildFailureOnMetric.MetricComparison.MORE
            compareTo = build {
                buildRule = lastSuccessful()
            }
            stopBuildOnFailure = true
            param("metricThreshold", "%TimeOut%")
        }
    }

    requirements {
        equals("env.BuildAgentType", "caravela04cloud")
    }

    features {
        swabra {
            lockingProcesses = Swabra.LockingProcessPolicy.KILL
            verbose = true
        }
    }

    dependencies {
        dependency(AbsoluteId("Metalama_Metalama20233_Metalama_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }

            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama"
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20233_MetalamaBackstage_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }

            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.Backstage"
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20233_MetalamaCommunity_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20233_MetalamaCompiler_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }

            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/packages/Release/Shipping/**/*=>dependencies/Metalama.Compiler"
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20233_MetalamaExtensions_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }

            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.Extensions"
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20233_MetalamaLinqPad_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }

            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.LinqPad"
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20233_MetalamaMigration_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }

            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.Migration"
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20233_MetalamaPatterns_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }

            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.Patterns"
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20233_MetalamaSamples_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }

            artifacts {
                cleanDestination = true
                artifactRules = "+:artifacts/publish/private/**/*=>dependencies/Metalama.Samples"
            }
        }
        dependency(AbsoluteId("Metalama_Metalama20233_MetalamaSamples_PublicBuild")) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }
        }

     }

})

object DownstreamMerge : BuildType({

    name = "Downstream Merge"

    params {
        text("DownstreamMergeArguments", "", label = "Merge downstream Arguments", description = "Arguments to append to the 'Merge downstream' build step.", allowEmpty = true)
        text("TimeOut", "300", label = "Time-Out Threshold", description = "Seconds after the duration of the last successful build.", regex = """\d+""", validationMessage = "The timeout has to be an integer number.")
    }
    vcs {
        root(DslContext.settingsRoot)
    }

    steps {
        powerShell {
            name = "Merge downstream"
            scriptMode = file {
                path = "Build.ps1"
            }
            noProfile = false
            param("jetbrains_powershell_scriptArguments", "tools git merge-downstream %DownstreamMergeArguments%")
        }
    }

    failureConditions {
        failOnMetricChange {
            metric = BuildFailureOnMetric.MetricType.BUILD_DURATION
            units = BuildFailureOnMetric.MetricUnit.DEFAULT_UNIT
            comparison = BuildFailureOnMetric.MetricComparison.MORE
            compareTo = build {
                buildRule = lastSuccessful()
            }
            stopBuildOnFailure = true
            param("metricThreshold", "%TimeOut%")
        }
    }

    requirements {
        equals("env.BuildAgentType", "caravela04cloud")
    }

    features {
        swabra {
            lockingProcesses = Swabra.LockingProcessPolicy.KILL
            verbose = true
        }
        sshAgent {
            // By convention, the SSH key name is always PostSharp.Engineering for all repositories using SSH to connect.
            teamcitySshKey = "PostSharp.Engineering"
        }
    }

    triggers {
        vcs {
            watchChangesInDependencies = true
            branchFilter = "+:<default>"
            // Build will not trigger automatically if the commit message contains comment value.
            triggerRules = "-:comment=<<VERSION_BUMP>>|<<DEPENDENCIES_UPDATED>>:**"
        }
    }

    dependencies {
        dependency(DebugBuild) {
            snapshot {
                     onDependencyFailure = FailureAction.FAIL_TO_START
            }
        }

     }

})

