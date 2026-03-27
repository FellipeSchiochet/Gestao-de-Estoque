#!/usr/bin/env bash
set -euo pipefail

DOTNET_INSTALL_DIR="$(pwd)/.dotnet"

mkdir -p "$DOTNET_INSTALL_DIR"
curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --channel 8.0 --install-dir "$DOTNET_INSTALL_DIR"

echo "DOTNET_INSTALL_DIR=$DOTNET_INSTALL_DIR"
