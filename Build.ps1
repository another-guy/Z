<#
.SYNOPSIS
  This is a helper function that runs a scriptblock and checks the PS variable $lastexitcode
  to see if an error occcured. If an error is detected then an exception is thrown.
  This function allows you to run command-line programs without having to
  explicitly check the $lastexitcode variable.
.EXAMPLE
  exec { svn info $repository_trunk } "Error executing SVN. Please verify SVN command-line client is installed"
#>
function Exec
{
    [CmdletBinding()]
    param(
        [Parameter(Position=0,Mandatory=1)][scriptblock]$cmd,
        [Parameter(Position=1,Mandatory=0)][string]$errorMessage = ($msgs.error_bad_command -f $cmd)
    )
    & $cmd
    if ($lastexitcode -ne 0) {
        throw ("Exec: " + $errorMessage)
    }
}

echo "============================ REMOVE OLD ARTIFACTS ==========================="
if(Test-Path .\artifacts) { Remove-Item .\artifacts -Force -Recurse }

echo "============================ RESTORE DEPENDENCIES ==========================="
exec { & dotnet restore }

echo "========================== BUILD TO RUN UNIT TESTS =========================="
exec { & dotnet test .\Z.Tests -c Release }

echo "============================= BUILD NUGET PACKAGE ==========================="
$tagOfHead = iex 'git tag -l --contains HEAD'
$prefixExpected = $tagOfHead + "-"
$projectJsonVersion = Get-Content '.\Z\project.json' | Out-String | ConvertFrom-Json | select -ExpandProperty version

if ([string]::IsNullOrEmpty($tagOfHead)) {
  $revision = @{ $true = $env:APPVEYOR_BUILD_NUMBER; $false = 1 }[$env:APPVEYOR_BUILD_NUMBER -ne $NULL];
  $revision = "b{0:D5}" -f [convert]::ToInt32($revision, 10)
  exec { & dotnet pack .\Z -c Release -o .\artifacts --version-suffix=$revision }
} elseif ($projectJsonVersion.StartsWith($prefixExpected,"CurrentCultureIgnoreCase")) {
  exec { & dotnet pack .\Z -c Release -o .\artifacts }
} else {
  throw ("Target commit is marked with tag " + $tagOfHead + " which is not compatible with project version retrieved from metadata: " + $projectJsonVersion)
}

echo "=============================== BUILD COMPLETE! ============================="