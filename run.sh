#!/bin/bash
# Menjalankan aplikasi dotnet
dotnet run --launch-profile https &

# Tunggu sebentar agar aplikasi dapat dijalankan
sleep 5

# Membuka Chrome
open -a "Google Chrome" "https://localhost:7017/swagger"
