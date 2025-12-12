# Script para ejecutar la aplicación en el emulador Android
# Uso: .\run-android.ps1

Write-Host "?? Iniciando aplicación DeckBrew.Mobile en Android..." -ForegroundColor Green

# Configurar variable de entorno
$env:ANDROID_HOME = "C:\Program Files (x86)\Android\android-sdk"

# Verificar si el emulador está corriendo
$devices = & "$env:ANDROID_HOME\platform-tools\adb.exe" devices
if ($devices -match "emulator-\d+\s+device") {
    Write-Host "? Emulador detectado" -ForegroundColor Green
} else {
    Write-Host "?? No se detectó ningún emulador. Iniciando emulador..." -ForegroundColor Yellow
    Start-Process -FilePath "$env:ANDROID_HOME\emulator\emulator.exe" -ArgumentList "-avd", "pixel_6_pro_-_api_36" -NoNewWindow
    Write-Host "? Esperando que el emulador inicie (30 segundos)..." -ForegroundColor Yellow
    Start-Sleep -Seconds 30
}

# Compilar y ejecutar
Write-Host "?? Compilando aplicación..." -ForegroundColor Cyan
dotnet build "$PSScriptRoot\DeckBrew.Mobile.csproj" -f net10.0-android

Write-Host "?? Desplegando en emulador..." -ForegroundColor Cyan
dotnet build "$PSScriptRoot\DeckBrew.Mobile.csproj" -f net10.0-android -t:Run

Write-Host "? Aplicación desplegada correctamente" -ForegroundColor Green
