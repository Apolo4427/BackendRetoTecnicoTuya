#!/usr/bin/env bash
set -euo pipefail

ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"

# Carga password desde scripts/docker/.env (si existe)
ENV_FILE="$ROOT/scripts/docker/.env"
SA_PASSWORD=""
if [ -f "$ENV_FILE" ]; then
  SA_PASSWORD="$(grep -E '^SA_PASSWORD=' "$ENV_FILE" | head -n1 | cut -d= -f2-)"
fi

# Permite sobrescribir desde el entorno
: "${SA_PASSWORD:=${SA_PASSWORD:-YourStrong!Passw0rd}}"

export ConnectionStrings__DefaultConnection="Server=localhost,1433;Database=BackendTuyaDb;User Id=sa;Password=${SA_PASSWORD};TrustServerCertificate=True;Encrypt=True"

echo "[INFO] ConnectionStrings__DefaultConnection configurada (SQL Server en localhost:1433)"

dotnet run --project "$ROOT/BackendTuya.csproj"
