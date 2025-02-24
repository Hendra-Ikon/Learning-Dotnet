#!/bin/bash
# Menjalankan aplikasi dotnet
dotnet run --launch-profile http &

# Tunggu sebentar agar aplikasi dapat dijalankan
sleep 5

# Membuka Chrome
open -a "Google Chrome" "http://localhost:5052"
