#!/bin/bash

# Hentikan proses dotnet yang sedang berjalan
echo "Mencari dan menghentikan proses dotnet yang berjalan..."
PID=$(ps aux | grep 'dotnet run' | grep -v 'grep' | awk '{print $2}')

if [ -z "$PID" ]; then
  echo "Tidak ada proses dotnet yang berjalan."
else
  echo "Menghentikan proses dotnet dengan PID $PID..."
  kill -9 $PID
  echo "Proses dotnet dihentikan."
fi

# Tunggu sebentar untuk memastikan proses dihentikan
# sleep 2

# Jalankan kembali aplikasi dotnet
# echo "Menjalankan aplikasi dotnet..."
# dotnet run --launch-profile https &

# Tunggu sebentar untuk memastikan aplikasi sudah dijalankan
sleep 5

# Buka aplikasi di Chrome
echo "Membuka Swagger di Google Chrome..."
open -a "Google Chrome" "https://localhost:7017/swagger"

echo "Restart aplikasi selesai."

