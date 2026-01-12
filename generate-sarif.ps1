# Coloque este script na raiz da solução e execute no PowerShell
$editorconfig = @'
root = true

[*.{cs,vb}]
dotnet_analyzer_diagnostic.severity = warning
'@

Set-Content -Path .\.editorconfig -Value $editorconfig -Encoding UTF8
Write-Host "Wrote .editorconfig to" (Resolve-Path .\.editorconfig)

$solution = ".\Restaurant.Order.slnx"   # ajuste se necessário
$sarifPath = Join-Path $PWD "analyzers.sarif"

Write-Host "Building solution and requesting SARIF at $sarifPath..."
dotnet build $solution -p:RunAnalyzers=true -p:ErrorLog="$sarifPath" -v:minimal

if (Test-Path $sarifPath) {
    Write-Host "SARIF gerado em: $sarifPath"
    $json = Get-Content $sarifPath -Raw | ConvertFrom-Json
    $count = $json.runs[0].results.Count
    Write-Host "Total de diagnósticos:" $count

    Write-Host "`nTop 20 rules:"
    $json.runs[0].results | Group-Object ruleId | Sort-Object Count -Descending | Select -First 20 | Format-Table Name,Count

    Write-Host "`nTop 20 arquivos com mais problemas:"
    $json.runs[0].results |
      ForEach-Object { $_.locations[0].physicalLocation.artifactLocation.uri } |
      Group-Object | Sort-Object Count -Descending | Select -First 20 | Format-Table Name,Count
} else {
    Write-Host "SARIF NÃO foi criado. Provavelmente não há diagnósticos emitidos pelos analyzers."
    Write-Host "Rode o build em modo detalhado para inspecionar logs:"
    Write-Host "dotnet build $solution -p:RunAnalyzers=true -p:ErrorLog='$sarifPath' -v:d 2>&1 | Tee-Object build.log"
}